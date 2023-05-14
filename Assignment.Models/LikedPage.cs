using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class LikedPage
    {
        [DisplayName("Like Id")]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Page Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("About Page")]
        public string About { get; set; }
        [Required]
        [MaxLength(1000)]
        [DisplayName("Page Description")]
        public string Description { get; set; }
    }
}
