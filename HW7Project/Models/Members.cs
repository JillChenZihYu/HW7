using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HW7Project.Models
{
    public class Members
    {
        [Key]
        [DisplayName("會員編號")]
        public int MemberID { get; set; }

        [DisplayName("姓名")]
        [StringLength(100)]
        [Required]
        public string MemberName { get; set; }

        [DisplayName("照片")]
        [MaxLength]
        public string MemberPhotoFile { get; set; }

        [DisplayName("生日")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime MemberBirdthday { get; set; }

        [DisplayName("建立日期")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }


        [DisplayName("帳號")]
        [Required]
        [StringLength(20)]
        [CheckAccount(ErrorMessage ="此帳號已註冊過")]
        public string Account { get; set; }



        
       
        


        //密碼的雜湊

        //field
        string password;  //定義一個Password的field

        [DisplayName("密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password  //此Password為物件
        {
            get
            {
                return password;

            }
            set
            {
                byte[] hashValue;
                string result = "";

                System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

                byte[] pwBytes = ue.GetBytes(value); //GetBytes把value(密碼內容)改為byte

                SHA256 shHash = SHA256.Create(); //創造一個SHA256的物件

                hashValue = shHash.ComputeHash(pwBytes); //ComputeHash做雜湊的運算

                foreach (byte b in hashValue)
                {
                    result += b.ToString();
                }

                password = result;
            }

        }

        //自訂驗證規則寫法
        public class CheckAccount : ValidationAttribute
        {
            public CheckAccount()
            {
                ErrorMessage = "此帳號已註冊過";
            }
            public override bool IsValid(object value)
            {
                if (value == null)
                    value = "abc";
                
                HW7ProjectContext db = new HW7ProjectContext();
                var account = db.Members.Where(m => m.Account == value.ToString()).FirstOrDefault();

                return (account == null)?true:false;
            }
        }



    }
}