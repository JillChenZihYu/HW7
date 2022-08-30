using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HW7Project.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage ="請填寫員工姓名")]
        [StringLength(100,ErrorMessage ="員工姓名不得超過100字")]
        [DisplayName("員工姓名")]
        public string EmployeeName { get; set; }


        [DisplayName("建立日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode = true)] /*0為預設值，總長為0:yyyy/MM/dd hh:mm:ss，若要總長輸入0即可。月份一定要大寫，不然會寫入分鐘。//ApplyFormatInEditMode：編輯模式下一併使用該模式*/
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(20, ErrorMessage = "帳號不得超過20字")]
        [RegularExpression("[A-Za-z][A-Za-z0-9]{4,19}",ErrorMessage = "帳號格式錯誤")]
        //[A-Za-z][A-Za-z0-9]{4,19}：第一碼為[A-Za-z]，後面可輸入[A-Za-z0-9]至少4次(aka帳號最少1+4碼)，最多19次
        [DisplayName("帳號")]
        public string Account { get; set; }


        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        public string Password { get; set; }


    }
}