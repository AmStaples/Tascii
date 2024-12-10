using System.ComponentModel.DataAnnotations;

namespace Tascii.Server.Models
{
    public class Boards
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? OwnerId { get; set; }
        
    }
}
