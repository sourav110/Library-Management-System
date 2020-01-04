using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem_MVC.Models
{
    public class BorrowBook
    {
        [Key]
        public int BorrowId { get; set; }

        [Required(ErrorMessage ="Please Enter Member Number")]
        [Column(TypeName="varchar")]
        [StringLength(50)]
        [DisplayName("Enter Member No.")]
        [Remote("IsMemberNotExists", "BorrowBooks", ErrorMessage = "Sorry, Member number doesn't exist")]
        public string MemberNo { get; set; }

        [Required(ErrorMessage ="Please Select a Book")]
        [DisplayName("Select a Book")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [NotMapped]
        public string Author { get; set; }
        [NotMapped]
        public string Publisher { get; set; }

        public bool IsBorrowed { get; set; }

    }
}