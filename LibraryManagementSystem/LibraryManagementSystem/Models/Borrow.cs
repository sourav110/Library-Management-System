using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }
        [Required(ErrorMessage ="Please Enter Member No.")]
        [Display(Name ="Enter Member No")]

        [Remote("IsMemberNotExists", "Borrows", ErrorMessage = "Sorry, Member number doesn't exist")]
        public string MemberNo { get; set; }

        [Display(Name = "Select a Book")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public string Author { get; set; }
        public string Publisher { get; set; }

        public bool IsReturn { get; set; }
    }
}