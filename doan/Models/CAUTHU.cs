namespace doan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class CAUTHU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAUTHU()
        {
            this.BANTHANG = new HashSet<BANTHANG>();
        }

        // Mã cầu thủ
        public string MaCT { get; set; }

        // Tên cầu thủ
        public string TenCT { get; set; }

        // Ngày sinh cầu thủ, nullable để chấp nhận giá trị null
        public Nullable<System.DateTime> NgaySinh { get; set; }

        // Vị trí thi đấu
        public string ViTri { get; set; }

        // Số áo, nullable nếu có thể không có giá trị
        public Nullable<int> SoAo { get; set; }

        // Cân nặng cầu thủ, nullable nếu có thể không có giá trị
        public Nullable<double> CanNang { get; set; }

        // Hình ảnh cầu thủ
        public string Images { get; set; }

        // Mã đội bóng của cầu thủ
        public string MaDoi { get; set; }

        // Quan hệ với bảng BANTHANG, một cầu thủ có thể có nhiều bàn thắng
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANTHANG> BANTHANG { get; set; }

        // Quan hệ với bảng DOIBONG, mỗi cầu thủ thuộc về một đội bóng
        public virtual DOIBONG DOIBONG { get; set; }
    }
}
