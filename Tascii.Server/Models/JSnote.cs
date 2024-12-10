using System.ComponentModel.DataAnnotations;

namespace Tascii.Server.Models
{
    public class JSNote
    {
        [Key]
        public int DBID { get; set; }
        public int JSID { get; set; }
        public int BoardId { get; set; }
        public string? content { get; set; }
        public int? xCoord { get; set; }
        public int? yCoord { get; set; }
    }
}
