using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW7Project.Models
{
    public class VMLogin
    {
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(20, ErrorMessage = "帳號不得超過20字")]
        [RegularExpression("[A-Za-z][A-Za-z0-9]{4,19}", ErrorMessage = "帳號格式錯誤")]
        //[A-Za-z][A-Za-z0-9]{4,19}：第一碼為[A-Za-z]，後面可輸入[A-Za-z0-9]至少4次(aka帳號最少1+4碼)，最多19次
        [DisplayName("帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼不得低於8碼")]
        [MaxLength(30, ErrorMessage = "密碼不得超過30碼")]
        [DisplayName("密碼")]
        public string Password { get; set; }
    }
}