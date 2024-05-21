using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aulas.Data;
using Aulas.Models;
using Microsoft.AspNetCore.Authorization;

namespace Aulas.Controllers {


   [Authorize]  // qq tarefa desta classe só pode ser efetuada
   // por pessoas autorizadas (ie. autenticadas)
   // exceto se se criar uma exceção
   public class CursosController : Controller {

      /// <summary>
      /// referência à BD do projeto
      /// </summary>
      private readonly ApplicationDbContext _context;

      /// <summary>
      /// objeto que contém os dados referentes ao ambiente 
      /// do Servidor
      /// </summary>
      private readonly IWebHostEnvironment _webHostEnvironment;

      public CursosController(
         ApplicationDbContext context,
         IWebHostEnvironment webHostEnvironment) {
         _context = context;
         _webHostEnvironment = webHostEnvironment;
      }

      // GET: Cursos
      /// <summary>
      /// mostra todos os cursos existentes na BD
      /// </summary>
      /// <returns></returns>
      [AllowAnonymous] // esta anotação isenta da obrigação
      // do utilizador estar aautenticado
      public async Task<IActionResult> Index() {
         return View(await _context.Cursos.ToListAsync());
      }

      // GET: Cursos/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         var cursos = await _context.Cursos
             .FirstOrDefaultAsync(m => m.Id == id);
         if (cursos == null) {
            return NotFound();
         }

         return View(cursos);
      }

      // GET: Cursos/Create
      [HttpGet] // facultativo, pois esta função,
                // por predefinição, já reage ao HTTP GET
      public IActionResult Create() {
         // a única ação desta função é mostrar a View
         // qd quero iniciar a adição de um Curso
         return View();
      }

      // POST: Cursos/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Nome")] Cursos curso, IFormFile ImagemLogo) {
         // [Bind] - anotação para indicar que dados, vindos da View,
         //          devem ser 'aproveitados'

         /* Algoritmo
          * 1- há ficheiro?
          *    1.1 - não há ficheiro!
          *          devolver à view dizendo que o ficheiro
          *          é obrigatório
          *    1.2 - há ficheiro!
          *          Mas, é uma imagem (PNG, JPG)?
          *          1.2.1 - não é PNG nem JPG
          *                  devolver o controlo à View
          *                  e pedir PNG ou JPG
          *          1.2.2 - é uma imagem
          *                  - determinar o nome a atribuir 
          *                    ao ficheiro
          *                  - escrever esse nome na BD
          *                  - se a escrita na BD se concretizar
          *                    é que o ficheiro é guardado no 
          *                    disco rígido
          */

         // vars. auxiliares
         string nomeImagem = "";
         bool haImagem = false;

         // há ficheiro?
         if (ImagemLogo == null) {
            ModelState.AddModelError("",
               "O fornecimento de um Logótipo é obrigatório!");
            return View(curso);
         }
         else {
            // há ficheiro, mas é imagem?
            if (!(ImagemLogo.ContentType == "image/png" ||
                   ImagemLogo.ContentType == "image/jpeg")
               ) {
               ModelState.AddModelError("",
              "Tem de fornecer para o Logótipo um ficheiro PNG ou JPG!");
               return View(curso);
            }
            else {
               // há ficheiro, e é uma imagem válida
               haImagem = true;
               // obter o nome a atribuir à imagem
               Guid g = Guid.NewGuid();
               nomeImagem = g.ToString();
               // obter a extensão do nome do ficheiro
               string extensao = Path.GetExtension(ImagemLogo.FileName);
               // adicionar a extensão ao nome da imagem
               nomeImagem += extensao;
               // adicionar o nome do ficheiro ao objeto que
               // vem do browser
               curso.Logotipo = nomeImagem;
            }
         }


         // avalia se os dados que chegam da View
         // estão de acordo com o Model
         if (ModelState.IsValid) {

            // adiciona os dados vindos da View à BD
            _context.Add(curso);
            // efetua COMMIT na BD
            await _context.SaveChangesAsync();

            // se há ficheiro de imagem,
            // vamos guardar no disco rígido do servidor
            if (haImagem) {
               // determinar onde se vai guardar a imagem
               string nomePastaOndeGuardarImagem =
                  _webHostEnvironment.WebRootPath;
               // já sei o caminho até à pasta wwwroot
               // especifico onde vou guardar a imagem
               nomePastaOndeGuardarImagem =
                  Path.Combine(nomePastaOndeGuardarImagem, "Imagens");
               // e, existe a pasta 'Imagens'?
               if (!Directory.Exists(nomePastaOndeGuardarImagem)) {
                  Directory.CreateDirectory(nomePastaOndeGuardarImagem);
               }
               // juntar o nome do ficheiro à sua localização
               string nomeFinalDaImagem =
                  Path.Combine(nomePastaOndeGuardarImagem, nomeImagem);

               // guardar a imagem no disco rigído
               using var stream = new FileStream(
                  nomeFinalDaImagem, FileMode.Create
                  );
               await ImagemLogo.CopyToAsync(stream);
            }


            // redireciona o utilizador para a página Index
            return RedirectToAction(nameof(Index));
         }

         // se cheguei aqui é pq alguma coisa correu mal :-(
         // volta à View com os dados fornecidos pela View
         return View(curso);
      }

      // GET: Cursos/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var cursos = await _context.Cursos.FindAsync(id);
         if (cursos == null) {
            return NotFound();
         }
         return View(cursos);
      }

      // POST: Cursos/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Logotipo")] Cursos cursos) {
         if (id != cursos.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(cursos);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!CursosExists(cursos.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(cursos);
      }

      // GET: Cursos/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var cursos = await _context.Cursos
             .FirstOrDefaultAsync(m => m.Id == id);
         if (cursos == null) {
            return NotFound();
         }

         return View(cursos);
      }

      // POST: Cursos/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var cursos = await _context.Cursos.FindAsync(id);
         if (cursos != null) {
            _context.Cursos.Remove(cursos);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool CursosExists(int id) {
         return _context.Cursos.Any(e => e.Id == id);
      }
   }
}
