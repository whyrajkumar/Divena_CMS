using System;
using System.ComponentModel.DataAnnotations;

namespace Divena_CMS.Models
{
    public partial class Blog : SEO
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int? PrimaryImageId { get; set; }

        public string PrimaryImageUrl { get; set; }

        [Required]
        public string Name { get; set; }



        [Required]
        public string Description { get; set; }
        public DateTime AddedOn { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
