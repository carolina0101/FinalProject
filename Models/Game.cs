using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Game
    {
        public int ID { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]

        [Display(Name = "Game Name")]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage="Ati introdus un nume ce nu corespunde regulilor"), Required,
          StringLength(50, MinimumLength = 3)]

        public string GameGenre { get; set; }
        [Range(1, 300)]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int PlayersID { get; set; }
        public Players Players { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
