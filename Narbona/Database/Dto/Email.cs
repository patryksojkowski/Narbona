using System.ComponentModel.DataAnnotations;

namespace Narbona.Database.Dto
{
    public class Email
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
