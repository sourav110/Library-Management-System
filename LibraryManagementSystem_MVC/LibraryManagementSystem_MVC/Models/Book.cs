using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem_MVC.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage ="Please Enter Book Title")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Remote("IsTitleExists", "Books", ErrorMessage ="Sorry, Book Already Exists!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter Book Author Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please Enter Book Publisher Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Publisher { get; set; }
    }
}