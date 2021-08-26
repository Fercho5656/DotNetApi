using System.ComponentModel.DataAnnotations;

namespace DotNetApi.Models {
    public class Student {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}