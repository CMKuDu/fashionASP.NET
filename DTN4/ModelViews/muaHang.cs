using System.ComponentModel.DataAnnotations;

namespace DTN4.ModelViews
{
    public class muaHang
    {
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string fullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ nhận hàng")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Tỉnh/Thành")]
        public int TinhThanh { get; set; }
        [Required(ErrorMessage = "Quận huyện")]
        public int QuanHuyen { get; set; }
        [Required(ErrorMessage = "Thị xã")]
        public int ThiXa { get; set; }
        public int PaymentID { get; set; }
        public int Note
        {get; set;
        }
    }
}
