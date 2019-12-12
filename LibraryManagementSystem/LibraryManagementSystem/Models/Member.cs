using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Please enter Member No")]
        [Display(Name = "Number")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Remote("IsMemberNoExists", "Members", ErrorMessage = "Sorry, Member already exists")]
        public string MemberNo { get; set; }

        [Required(ErrorMessage = "Please enter Member Name")]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string MemberName { get; set; }
    }
}