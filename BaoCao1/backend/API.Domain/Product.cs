namespace TranAnhDung.API.Domain
{
    public class Product
    {
        public long ProductId { get; set; } // Id của Product
        public string Title { get; set; }  // Tên của Product
        public string Slug { get; set; }  // Đường dẫn slug
        public string Summary { get; set; }  // Tóm tắt sản phẩm
        public string? Description { get; set; } // Mô tả sản phẩm, không bắt buộc
        public string Photo { get; set; }  // Đường dẫn ảnh sản phẩm
        public int Stock { get; set; } // Số lượng tồn kho
        public string? Size { get; set; } // Kích thước sản phẩm, không bắt buộc
        public string Condition { get; set; }  // Tình trạng sản phẩm
        public string Status { get; set; }  // Trạng thái sản phẩm
        public decimal Price { get; set; } // Giá sản phẩm
        public decimal Discount { get; set; } // Giá giảm của sản phẩm
        public short IsFeatured { get; set; } // Xác định xem đây có phải là sản phẩm nổi bật không
        public long? CatId { get; set; } // Id của Category, nếu có
        public long? ChildCatId { get; set; } // Id của sub-Category, nếu có
        public long? BrandId { get; set; } // Id của Brand, nếu có
        public DateTime? CreatedAt { get; set; } // Thời gian tạo sản phẩm, không bắt buộc
        public DateTime? UpdatedAt { get; set; } // Thời gian cập nhật sản phẩm, không bắt buộc

        // Navigation properties
        public virtual Category? Cat { get; set; } // Tham chiếu đến Category của sản phẩm
    }
}
