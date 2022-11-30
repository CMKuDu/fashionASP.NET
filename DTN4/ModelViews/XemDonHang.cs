using System.ComponentModel;

namespace DTN4.ModelViews
{
    public partial class XemDonHang
    { 
        
        [DisplayName("Mã hóa đơn")]
        public int MaHoaDon { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int MaSanPham { get; set; }
        [DisplayName("Tổng tiền")]
        public int? TongTien { get; set; }
        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }
    }
}
