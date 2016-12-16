using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStepBlog.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string  Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name="Content")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}