using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DTN4.ModelViews
{
    public class RegisterViewModel
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "ValidateEmail", controller: "Accounts")]
        public string Email { get; set; }
        [MaxLength(11)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Accounts")]
        public string Phone { get; set; }
        [Display(Name = "Mặt khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bặt cần đặt mặt khẩu tối thiểu 5 ký ừ")]
        public string Password { get; set; }
        [MinLength(5,ErrorMessage = "Bặn cần đặt mặt khẩ tối thiểu 5 ký tự")]
        [Display(Name = "Nhập lại mặt khẩu")]
        [Compare("Password",ErrorMessage = "Vui lòng nhập mặt khẩu")]
        public string ConfirmPassword { get; set; }
    }

}
