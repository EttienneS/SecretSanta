using System.ComponentModel.DataAnnotations;

namespace Secret.Model
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phrase { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}