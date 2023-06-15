using System.ComponentModel.DataAnnotations;

namespace Divena_CMS.Models
{
    public partial class SEO
    {
        [Required]
        public string Url { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        //public string MetalIndex { get; set; }
        //public string MetalFollow { get; set; }
        //[NotMapped]
        //public string MetaCaronical { get;set; }
    }
}
