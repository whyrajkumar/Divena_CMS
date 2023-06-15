using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divena_CMS.Models
{
    public partial class Page : SEO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AddedOn { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}