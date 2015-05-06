using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firewood.Models
{
    public class RegisterForm
    {
        [Required(ErrorMessage = "学号必填")]
        [Display(Name = "学号")]
        [RegularExpression("[0-9]+")]
        [MinLength(1)]
        [MaxLength(20)]
        public string usernum { set; get; }


        [Required(ErrorMessage = "上网密码必填")]
        [Display(Name = "上网密码")]
        [DataType(DataType.Password)]
        public string webpwd { set; get; }

        [Required(ErrorMessage = "昵称必填")]
        [Display(Name = "昵称")]
        [MinLength(1)]
        [MaxLength(20)]
        public string username { set; get; }

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


        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(50)]
        public string password1 { set; get; }


        [Required(ErrorMessage = "请确认密码")]
        [Display(Name = "密码确认")]
        [DataType(DataType.Password)]
        [MinLength(1)]
        [MaxLength(50)]
        public string password2 { set; get; }
    }
}