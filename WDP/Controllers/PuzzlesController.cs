using BL;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WDP.Controllers
{
    public class PuzzlesController : Controller
    {
        private readonly PuzzleContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PuzzlesController(PuzzleContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator, MemberPlus, Member")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Puzzles.ToListAsync());
        }




        // Check the user submitted answer
        [HttpPost]
        public async Task<JsonResult> Check([FromBody] Answer answer)
        {
            var message = "";
            bool check = true;
            Dictionary<string, int> answerKey = new();

            if (answer.one.Length == 2 && answer.two.Length == 2 && answer.three.Length == 2 && answer.four.Length == 2 && answer.five.Length == 2 && answer.six.Length == 2 && answer.seven.Length == 2 && answer.eight.Length == 2 && answer.nine.Length == 2 && answer.ten.Length == 2)
            {
                answerKey.Add(answer.one.Substring(0, 1), Int32.Parse(answer.one.Substring(1, 1)));
                answerKey.Add(answer.two.Substring(0, 1), Int32.Parse(answer.two.Substring(1, 1)));
                answerKey.Add(answer.three.Substring(0, 1), Int32.Parse(answer.three.Substring(1, 1)));
                answerKey.Add(answer.four.Substring(0, 1), Int32.Parse(answer.four.Substring(1, 1)));
                answerKey.Add(answer.five.Substring(0, 1), Int32.Parse(answer.five.Substring(1, 1)));
                answerKey.Add(answer.six.Substring(0, 1), Int32.Parse(answer.six.Substring(1, 1)));
                answerKey.Add(answer.seven.Substring(0, 1), Int32.Parse(answer.seven.Substring(1, 1)));
                answerKey.Add(answer.eight.Substring(0, 1), Int32.Parse(answer.eight.Substring(1, 1)));
                answerKey.Add(answer.nine.Substring(0, 1), Int32.Parse(answer.nine.Substring(1, 1)));
                answerKey.Add(answer.ten.Substring(0, 1), Int32.Parse(answer.ten.Substring(1, 1)));

                var puzzle = await _context.Puzzles
                    .FirstOrDefaultAsync(m => m.Id == answer.id);

                if (!string.IsNullOrEmpty(puzzle.Letters))
                {
                    for (int i = 0; i < puzzle.Letters.Length; i++)
                    {

                        //         
                        if (i != answerKey[puzzle.Letters.Substring(i, 1)])
                        {
                            check = false;
                        }
                    }

                    if (check)
                    {
                        ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
                        

                        if(applicationUser != null)
                        {
                            puzzle.Solved = DateTime.Now;
                            puzzle.SolvedBy = applicationUser.Id;

                            _context.Update(puzzle);
                            await _context.SaveChangesAsync();
                            message = "Congratulations!! You Solved the Puzzle!!";
                        }
                        else
                        {
                            message = "Congratulations!! You Solved the Puzzle!! Unfortunately you are not logged-in. To get credit for solving puzzles in the future, please log in or create an account.";
                        }

                    }

                    else { message = "Sorry, Incorrect. Please try again."; }
                }
                else
                {
                    message = "Something went wrong. Try refreshing the page.";
                }

            }
            else
            {
                message = "Make sure you have filled in answers for each letter.";
            }
          
            return Json(message);
        }

        public string ConvertToLetters(string letters, string number)
        {
            string converted = "";
            for (int i = 0; i<number.Length; i++)
            {
                var x =   letters.Substring(int.Parse(number.Substring(i, 1)),1 ) ;
                converted += x;
            }
            return converted;
        }

        // GET: Puzzles/LandingPage
        public async Task<IActionResult> LandingPage()
        {
            var puzzles = await _context.Puzzles.ToListAsync();
       
            List<SolvedAnswer> solvedAnswers = new();
            

            Random rand = new();

            var solved = puzzles.Where(s => s.Solved != null)
                .OrderBy(t => t.Solved)
                .Take(4)
                .ToList();

            foreach (var solv in solved)
            {
                SolvedAnswer solvedAnswer = new();
                var user = await _userManager.FindByIdAsync(solv.SolvedBy.ToString());
                solvedAnswer.id = solv.Id;
                solvedAnswer.Uid = user.Id;
                solvedAnswer.UserName = user.UserName;
                solvedAnswer.Divisor = ConvertToLetters(solv.Letters, solv.Divisor);
                solvedAnswer.Quotient = ConvertToLetters(solv.Letters, solv.Quotient);
                solvedAnswers.Add(solvedAnswer);
            }



            ViewData["solved"] = solvedAnswers;
            ViewData["unsolved"] = puzzles.Where(s => s.Solved == null)
                .OrderBy(t => t.Created)
                .Take(5)
                .ToList();


            if (puzzles.Count != 0)
            {
                return View(puzzles[rand.Next(puzzles.Count)]);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }



        [Authorize(Roles = "Administrator, MemberPlus, Member")]
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


        [Authorize(Roles = "Administrator, MemberPlus, Member")]
        public async Task<IActionResult> Create()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            Random rand = new Random();
            Puzzle aPuzzle = new();
            aPuzzle.Letters = MakeLetters(); // Get the string of letters.
            aPuzzle.Divisor = rand.Next(99, 999).ToString();
            aPuzzle.Quotient = rand.Next(10000, 99999).ToString();

            // Seems like most of the remaining  problems are solved just by eliminating the possibility of having,
            //      zeros in the quotient. The below removes the 0 possibility.
            while (aPuzzle.Quotient.ToString().Contains("0"))
            {
                aPuzzle.Quotient = rand.Next(10000, 99999).ToString();
            }


            aPuzzle.Created = DateTime.Now;
            aPuzzle.UId = applicationUser.Id;
            aPuzzle.Seed = Guid.NewGuid();

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

        [Authorize(Roles = "Administrator, MemberPlus, Member")]
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

        [Authorize(Roles = "Administrator, MemberPlus, Member")]
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
