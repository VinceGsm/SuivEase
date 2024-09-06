using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string? Name { get; set; }        
        public string? Street { get; set; }
        public string? Street2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        public string? Country { get; set; } = "France";
        public string? Information { get; set; } //door code, etc...
    }
}