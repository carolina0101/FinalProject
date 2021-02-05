using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Players
    {
        public int ID { get; set; }
        public string PlayersName { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
