using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly FinalProject.Data.FinalProjectContext _context;

        public IndexModel(FinalProject.Data.FinalProjectContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; }

        public async Task OnGetAsync()
        {
            Game = await _context.Game
                .Include(b=>b.Players)
                .ToListAsync();
        }
    }
}
