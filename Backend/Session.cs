using System.ComponentModel.DataAnnotations;

namespace Backend
{
    public class Session
    {
        [Key]
        public required string Code { get; set; }
    }
}
