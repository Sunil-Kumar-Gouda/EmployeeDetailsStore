using System.ComponentModel.DataAnnotations;

namespace EmployeeDetailStore.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
