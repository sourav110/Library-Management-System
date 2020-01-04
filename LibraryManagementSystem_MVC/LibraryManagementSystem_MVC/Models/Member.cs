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
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required(ErrorMessage ="Please Enter Member No.")]
        [DisplayName("Number")]
        [Remote("IsMemberNoExixts", "Members", ErrorMessage ="Sorry, Member Already Exists!")]
        public string MemberNo { get; set; }

        [Required(ErrorMessage ="Please Enter Member Name")]
        [DisplayName("Name")]
        [Column(TypeName ="varchar")]
        [StringLength(50)]
        public string MemberName { get; set; }

    }
}