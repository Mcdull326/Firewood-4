using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firewood.Models
{
    public class OrgRegForm
    {
        [Required(ErrorMessage = "社团/组织名称必填")]
        [Display(Name = "社团/组织名称")]
        [MinLength(1)]
        [MaxLength(20)]
        public string orgname { set; get; }

        [Required(ErrorMessage = "所属部门必填")]
        [Display(Name = "所属部门")]
        [MinLength(1)]
        [MaxLength(20)]
        public string orgdepartment { set; get; }

        [Required(ErrorMessage = "负责人姓名必填")]
        [Display(Name = "负责人姓名")]
        [MinLength(1)]
        [MaxLength(20)]
        public string orgprincipal { set; get; }

        [Required(ErrorMessage = "负责人手机号必填")]
        [Display(Name = "负责人手机号")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "请填写正确格式手机号")]
        [RegularExpression("[0-9]+")]
        [MinLength(6)]
        [MaxLength(50)]
        public string orgtel { set; get; }

        [Required(ErrorMessage = "社团/组织联系方式必填")]
        [Display(Name = "社团/组织联系方式")]
        [MinLength(1)]
        [MaxLength(20)]
        public string orgcontact { set; get; }

        [Required(ErrorMessage = "简介必填")]
        [Display(Name = "简介")]
        [MinLength(1)]
        [MaxLength(500)]
        public string orgintro { set; get; }

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