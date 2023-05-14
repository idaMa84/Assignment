using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Like
    {
        [DisplayName("Like Id")]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("About Page")]
        public string About { get; set; }
        [Required]
        [MaxLength(1000)]
        [DisplayName("Page Description")]
        public string Description { get; set; }

        public Like()
        { }
        public Like(string id, string about, string description)
        {
            Id = id;
            About = about;
            Description = description;
        }

    }
}
