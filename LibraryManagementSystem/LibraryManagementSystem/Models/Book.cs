using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter Book title")]
        [Display(Name = "Title")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Remote("IsTitleExists", "Books", ErrorMessage = "Sorry, Book already exists")]
        public string BookTitle { get; set; }

        [Required(ErrorMessage = "Please enter Author Name")]
        [Display(Name = "Author")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter Publisher Name")]
        [Display(Name = "Publisher")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Publisher { get; set; }
    }
}
