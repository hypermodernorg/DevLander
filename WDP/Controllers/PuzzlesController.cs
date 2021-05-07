using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NLog;
using BL;
using DAL;

namespace WDP.Controllers
{
    public class PuzzlesController : Controller
    {
        private readonly PuzzleContext _context;

        public PuzzlesController(PuzzleContext context)
        {
            _context = context;
        }

        // GET: Puzzles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Puzzles.ToListAsync());
        }

        // GET: Puzzles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puzzle = await _context.Puzzles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puzzle == null)
            {
                return NotFound();
            }

            return View(puzzle);
        }

        // GET: Puzzles/Details/5
        public async Task<IActionResult> Details2(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puzzle = await _context.Puzzles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puzzle == null)
            {
                return NotFound();
            }

            return View(puzzle);
        }

        public string MakeLetters()
        {
            Random rand = new Random();
            var alphebet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

            var letters = "";

            for (int i = 0; i < 10; i++) // Get 9 letters
            {
                var n = rand.Next(0, 25 - i); // Get a random letter, decreasing number of available letters each time by 1.
                letters += alphebet[n]; // Add the letter to the string of letters
                alphebet.Remove(alphebet[n]); //Remove letter so it cannot be selected again.
            }

            return letters;
        }

        // GET: Puzzles/Create
     
        public async Task<IActionResult> Create()
        {
            Random rand = new Random();
            Puzzle aPuzzle = new();
            aPuzzle.Letters = MakeLetters(); // Get the string of letters.
            aPuzzle.Divisor = rand.Next(99, 999).ToString();
            aPuzzle.Quotient = rand.Next(10000, 99999).ToString();
            aPuzzle.Created = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                aPuzzle.Id = Guid.NewGuid();
                _context.Add(aPuzzle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Puzzles/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var puzzle = await _context.Puzzles.FindAsync(id);
        //    if (puzzle == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(puzzle);
        //}

        // POST: Puzzles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,UId,Seed,SolvedBy,Dividend,Divisor,Quotient,Letters,Created,Solved")] Puzzle puzzle)
        //{
        //    if (id != puzzle.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(puzzle);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PuzzleExists(puzzle.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(puzzle);
        //}

        // GET: Puzzles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puzzle = await _context.Puzzles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puzzle == null)
            {
                return NotFound();
            }

            return View(puzzle);
        }

        // POST: Puzzles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var puzzle = await _context.Puzzles.FindAsync(id);
            _context.Puzzles.Remove(puzzle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuzzleExists(Guid id)
        {
            return _context.Puzzles.Any(e => e.Id == id);
        }
    }
}
