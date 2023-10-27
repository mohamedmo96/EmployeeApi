using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Employee.Model
{
    public class Employe
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "Name cannot contain spaces.")]
        public string Name { get; set; }
        [Required]
        [Range(21, int.MaxValue, ErrorMessage = "Age must be greater than 20.")]
        public int Age { get; set; }

        public List<Address>? Addresses { get; set; }=new List<Address>();
    }
}
