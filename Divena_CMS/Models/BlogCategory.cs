using System;
using System.ComponentModel.DataAnnotations;

namespace Divena_CMS.Models
{
    public partial class BlogCategory : SEO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [Required]
        public string Name { get; set; }



        [Required]
        public string Description { get; set; }
        public DateTime AddedOn { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
