using System;
using System.Collections.Generic; // Thêm để sử dụng List<T>

namespace TranAnhDung.API.Domain
{
    public class Category
    {
        public long CategoryId { get; set; } // Id của Category
        public string Title { get; set; } // Tiêu đề của Category
        public string Slug { get; set; }  // Đường dẫn slug
        public string? Summary { get; set; } // Tóm tắt của Category
        public string? Photo { get; set; } // Đường dẫn ảnh của Category
        public short IsParent { get; set; } // Xác định xem đây có phải là danh mục cha không
        public long? ParentId { get; set; } // Id của danh mục cha, nếu có
        public long? AddedBy { get; set; } // Id của người thêm, nếu có
        public string? Status { get; set; }  // Trạng thái của Category
        public DateTime? CreatedAt { get; set; } // Thời gian tạo, không bắt buộc
        public DateTime? UpdatedAt { get; set; } // Thời gian cập nhật, không bắt buộc

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>(); // Danh sách sản phẩm thuộc danh mục này
    }
}
