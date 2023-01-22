using System.ComponentModel.DataAnnotations;

namespace Narbona.Database.Dto
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        public string Description { get; set; }

        public IList<Email> Emails { get; set; } = new List<Email>();

    }
}
