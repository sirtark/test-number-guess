using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend
{
#nullable disable
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string FK_Session { get; set; }
        public byte Number { get; set; }
        public byte CurrentTries { get; set; }

        [ForeignKey(nameof(FK_Session))]
        public Session Session { get; set; }
    }
}
