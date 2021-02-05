using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Pages.Games
{
    public class EditModel : GameCategories
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public EditModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Game
                .Include(b => b.Players)
                .Include(b => b.GameCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Game == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Game);

            ViewData["PlayersID"] = new SelectList(_context.Set<Models.Players>(), "ID", "PlayersName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var GameToUpdate = await _context.Game
            .Include(i => i.Players)
            .Include(i => i.GameCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (GameToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Game>(
            GameToUpdate,


            "Game",
            i => i.Name, i => i.GameGenre,
            i => i.Price, i => i.PublishingDate, i => i.Players))
            {
                UpdateGameCategories(_context, selectedCategories, GameToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateAlbumCategories pentru a aplica informatiile din checkboxuri la entitatea Albums care
            //este editata
            UpdateGameCategories(_context, selectedCategories, GameToUpdate);
            PopulateAssignedCategoryData(_context, GameToUpdate);
            return Page();
        }
    }
}


