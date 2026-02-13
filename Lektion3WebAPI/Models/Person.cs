using System.ComponentModel.DataAnnotations;

namespace Lektion3WebAPI.Models
{
    public class Person
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(0,120)]
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name} is {Age} years old";
        }
    }
}
