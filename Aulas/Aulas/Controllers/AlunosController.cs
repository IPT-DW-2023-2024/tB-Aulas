using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aulas.Data;
using Aulas.Models;

namespace Aulas.Controllers {
   public class AlunosController : Controller {
      private readonly ApplicationDbContext _context;

      public AlunosController(ApplicationDbContext context) {
         _context = context;
      }

      // GET: Alunos
      public async Task<IActionResult> Index() {
         var applicationDbContext = _context.Alunos.Include(a => a.Curso);
         return View(await applicationDbContext.ToListAsync());
      }

      // GET: Alunos/Details/5
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         var alunos = await _context.Alunos
             .Include(a => a.Curso)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (alunos == null) {
            return NotFound();
         }

         return View(alunos);
      }

      // GET: Alunos/Create
      public IActionResult Create() {
         // efetuar uma pesquisa na BD pelos Cursos
         // que podem estar associados à FK Cursos
         // SelectList -> cria uma lista de 'options' para a dropdown
         // Expressão LINQ para efetuar a pesquisa dos Cursos
         //    _context.Cursos.OrderBy(c=>c.Nome)
         ViewData["CursoFK"] = new SelectList(_context.Cursos.OrderBy(c => c.Nome), "Id", "Nome");

         // devolve controlo à View  
         return View();
      }

      // POST: Alunos/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("NumAluno,Propinas,PropinasAux,DataMatricula,CursoFK,Nome,DataNascimento,Telemovel")] Alunos aluno) {
        
         
         if (ModelState.IsValid) {

            // transferir o valor de PropinasAux para Propinas
            aluno.Propinas = Convert.ToDecimal( aluno.PropinasAux.Replace('.',',')   );

            _context.Add(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", aluno.CursoFK);
         return View(aluno);
      }

      // GET: Alunos/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var alunos = await _context.Alunos.FindAsync(id);
         if (alunos == null) {
            return NotFound();
         }
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", alunos.CursoFK);
         return View(alunos);
      }

      // POST: Alunos/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("NumAluno,Propinas,DataMatricula,CursoFK,Id,Nome,DataNascimento,Telemovel")] Alunos alunos) {
         if (id != alunos.Id) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(alunos);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!AlunosExists(alunos.Id)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["CursoFK"] = new SelectList(_context.Cursos, "Id", "Id", alunos.CursoFK);
         return View(alunos);
      }

      // GET: Alunos/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var alunos = await _context.Alunos
             .Include(a => a.Curso)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (alunos == null) {
            return NotFound();
         }

         return View(alunos);
      }

      // POST: Alunos/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var alunos = await _context.Alunos.FindAsync(id);
         if (alunos != null) {
            _context.Alunos.Remove(alunos);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool AlunosExists(int id) {
         return _context.Alunos.Any(e => e.Id == id);
      }
   }
}
