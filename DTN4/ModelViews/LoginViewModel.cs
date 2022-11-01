using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DTN4.ModelViews
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Địa chỉ Email")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5,ErrorMessage = "Bạn cần đặt mặt khẩu tối hiểu 5")]
        public string Password { get; set; }

    }
}
