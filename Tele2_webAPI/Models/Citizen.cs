using System.ComponentModel.DataAnnotations;

namespace Tele2API.Models
{
    public class Citizen
    {
        [Key]
        public int Id_num { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sex { get; set; }
        public int? Age { get; set; } = 0;
    }
}
