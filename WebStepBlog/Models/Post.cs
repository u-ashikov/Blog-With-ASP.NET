using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStepBlog.Models
{
    public class Post
    {
        public Post()
        {
            this.Tags = new List<Tag>();
            this.Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        public Comment Comment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name ="Tags")]
        [StringLength(50)]
        public string Tag { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}