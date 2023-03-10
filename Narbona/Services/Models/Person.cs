namespace Narbona.Services.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public IList<Email> Emails { get; set; } = new List<Email>();
    }
}
