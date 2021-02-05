using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Pages.Games
{
    public class CreateModel : GameCategories
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public CreateModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PlayersID"] = new SelectList(_context.Set<Models.Players>(), "ID", "PlayersName");
            var game = new Game();
            game.GameCategories = new List<GameCategory>();
            PopulateAssignedCategoryData(_context, game);
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newGame = new Game();
            if (selectedCategories != null)
            {
                newGame.GameCategories = new List<GameCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new GameCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newGame.GameCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Game>(
            newGame,
            "Game",
            i => i.Name, i => i.GameGenre,
            i => i.Price, i => i.PublishingDate, i => i.PlayersID))
            

            {

                _context.Game.Add(newGame);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newGame);
            return Page();
        }
    }

}