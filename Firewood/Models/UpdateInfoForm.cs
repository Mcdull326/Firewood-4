using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Firewood.Models
{
    public class UpdateInfoForm
    {
        [Required(ErrorMessage = "学号必填")]
        [Display(Name = "学号")]
        [RegularExpression("[0-9]+")]
        [MinLength(1)]
        [MaxLength(20)]
        public string usernum { set; get; }

        [Required(ErrorMessage = "邮箱必填")]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress, ErrorMessage = "请填写正确格式邮箱")]
        [MinLength(5)]
        [MaxLength(50)]
        public string usermail { set; get; }

        [Required(ErrorMessage = "手机号必填")]
        [Display(Name = "手机号")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "请填写正确格式手机号")]
        [RegularExpression("[0-9]+")]
        [MinLength(6)]
        [MaxLength(50)]
        public string usertel { set; get; }

        [Required(ErrorMessage = "年级必填")]
        [Display(Name = "年级")]
        [MinLength(4)]
        [MaxLength(4)]
        public string usergrade { set; get; }

        [Required(ErrorMessage = "性别必填")]
        [Display(Name = "性别")]
        public string usersex { set; get; }

        [Display(Name = "真实姓名")]
        [MaxLength(20)]
        public string truename { set; get; }
    }
}