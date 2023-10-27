using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Model
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public Employe employe { get; set; }
    }
}