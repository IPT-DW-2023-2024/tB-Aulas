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

   //  [Authorize]
   public class UCsController : Controller {

      private readonly ApplicationDbContext _context;

      public UCsController(ApplicationDbContext context) {
         _context = context;
      }

      // GET: UCs
      public async Task<IActionResult> Index() {
         var applicationDbContext = _context.UCs.Include(u => u.Curso);
         return View(await applicationDbContext.ToListAsync());
      }

      // GET: UCs/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         var unidadesCurriculares = await _context.UCs
             .Include(u => u.Curso)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (unidadesCurriculares == null) {
            return NotFound();
         }

         return View(unidadesCurriculares);
      }

      // GET: UCs/Create
      public IActionResult Create() {

         // procurar os dados a apresentar na 'dropdown' dos Cursos
         ViewData["CursoFK"] = new SelectList(_context.Cursos.OrderBy(c => c.Nome), "Id", "Nome");

         // Obter a lista de professores,
         // para enviar para a View
         // em SQL: SELECT * FROM Professores p ORDER BY p.Nome
         // em LINQ:
         var listaProfs = _context.Professores.OrderBy(p => p.Nome).ToList();
         ViewData["listaProfessores"] = listaProfs;

         return View();
      }

      // POST: UCs/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Nome,AnoCurricular,Semestre,CursoFK")] UnidadesCurriculares unidadeCurricular, int[] escolhaProfessores) {
         // var. auxiliar
         bool haErros = false;

         // Validações
         if (escolhaProfessores.Length == 0) {
            // não escolhi nenhum professor
            ModelState.AddModelError("", "Escolha um professor, pf.");
            haErros = true;
         }

         if (unidadeCurricular.CursoFK == -1) {
            // não foi selecionad nenhum curso
            ModelState.AddModelError("", "Escolha um curso, pf.");
            haErros = true;
         }


         if (ModelState.IsValid && !haErros) {
            _context.Add(unidadeCurricular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }


         // se chego aqui, é pq houve problemas
         // vou devolver o controlo à View
         // Tenho de preparar os dados a enviar
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Nome", unidadeCurricular.CursoFK);
         var listaProfs = _context.Professores.OrderBy(p => p.Nome).ToList();
         ViewData["listaProfessores"] = listaProfs;



         return View(unidadeCurricular);
      }

      // GET: UCs/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var unidadesCurriculares = await _context.UCs.FindAsync(id);
         if (unidadesCurriculares == null) {
            return NotFound();
         }
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", unidadesCurriculares.CursoFK);
         return View(unidadesCurriculares);
      }

      // POST: UCs/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,AnoCurricular,Semestre,CursoFK")] UnidadesCurriculares unidadesCurriculares) {
         if (id != unidadesCurriculares.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(unidadesCurriculares);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!UnidadesCurricularesExists(unidadesCurriculares.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", unidadesCurriculares.CursoFK);
         return View(unidadesCurriculares);
      }

      // GET: UCs/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var unidadesCurriculares = await _context.UCs
             .Include(u => u.Curso)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (unidadesCurriculares == null) {
            return NotFound();
         }

         return View(unidadesCurriculares);
      }

      // POST: UCs/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var unidadesCurriculares = await _context.UCs.FindAsync(id);
         if (unidadesCurriculares != null) {
            _context.UCs.Remove(unidadesCurriculares);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool UnidadesCurricularesExists(int id) {
         return _context.UCs.Any(e => e.Id == id);
      }
   }
}
