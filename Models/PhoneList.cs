using System.ComponentModel.DataAnnotations;

namespace PhoneList2.Models
{
    public class PhoneList
    {
        public PhoneList()
        {
            Numbers = new List<PhoneNumber>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        
        public List<PhoneNumber> Numbers { get; set; }

    }
    public class PhoneNumber
    {
        [Key]  // Primary key to satisfy EF requirements
        public int PhoneNumberID { get; set; }

        [Required]
        [Phone]
        public string Number { get; set; }

        [StringLength(20)]
        public string Label { get; set; }  // Optional: e.g. "Home", "Work", etc.
    }
}
