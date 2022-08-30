using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW7Project.Models
{
    public class PayTypes
    {
        [Key]
        public int PayTypeID { get; set; }

        [Required(ErrorMessage = "付款方式名稱為必填")]
        [StringLength(10, ErrorMessage = "付款方式不得超過10字")]
        [DisplayName("付款方式名稱")]
        public string PayTypeName { get; set; }
    }
}