using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomeSell.Models
{
    public partial class Table_ProductV 
    {
        [DisplayName("รหัสสินค้า")]
        public int P_ProductID { get; set; }
        [DisplayName("ชื่อสินค้า")]
        [Required(ErrorMessage = "กรณากรอกข้อมูล ชื่อสินค้า")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]
        public string P_ProName { get; set; }
        [DisplayName("ราคาสินค้า")]
        [Required(ErrorMessage = "กรณากรอกข้อมูล ราคาสินค้า")]
        [Range(1, 100000, ErrorMessage = "กรุณากรอกข้อมูล {0} ถึง {0}")]



        public Nullable<int> P_Price { get; set; }
        [DisplayName("รูปสินค้า")]


        public byte[] P_IMG { get; set; }
        [DisplayName("จำนวนสินค้า")]
        [Required(ErrorMessage = "กรณากรอกข้อมูล จำนวนสินค้า")]
        [Range(1, 100000, ErrorMessage = "กรุณากรอกข้อมูล {0} ถึง {0}")]



        public Nullable<int> P_Amout { get; set; }
        [DisplayName("ประเภท")]
        [Required(ErrorMessage = "กรณากรอกข้อมูล ประเภท")]


        public Nullable<int> P_Type { get; set; }

    }
    [MetadataType(typeof(Table_ProductV))]
    public partial class Table_Product { }

    public partial class Table_TypeProductV
    {
        [DisplayName("รหัสประเภท")]
        public int T_TypeProID { get; set; }
        [DisplayName("ประเภท")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]
        public string T_Name { get; set; }

    }
    [MetadataType(typeof(Table_TypeProductV))]
    public partial class Table_TypeProduct { }

    public partial class Table_AddProductV
    {
        [DisplayName("รหัสเพิ่มจำนววนเข้าคลัง")]
        public int A_AddProID { get; set; }
        [DisplayName("สินค้า")]
        public Nullable<int> A_Product { get; set; }
        [DisplayName("จำนวน")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        [Range(1, 100000, ErrorMessage = "กรุณากรอกข้อมูล {0} ถึง {0}")]
        public Nullable<int> A_amount { get; set; }
        [DisplayName("วันที่นำเข้า")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.Date)]
        public string A_date { get; set; }
        [DisplayName("สถานะ")]
        public Nullable<int> A_StatusAdd { get; set; }

    }
    [MetadataType(typeof(Table_AddProductV))]
    public partial class Table_AddProduct { }

    public partial class Table_UserV
    {
        [DisplayName("ชื่อผู้ใช้")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]

        public string U_UserName { get; set; }
        [DisplayName("รหัสผ่าน")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]

        public string U_PassWord { get; set; }
        [DisplayName("คำนำหน้าชื่อ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]


        public Nullable<int> U_Tilie { get; set; }
        [DisplayName("ชื่อ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]

        public string U_Name { get; set; }
        [DisplayName("นามสกุล")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [MinLength(3, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 3 ตัวอักษร ")]
        [MaxLength(30, ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 30 ตัวอักษร ")]

        public string U_LastName { get; set; }
        [DisplayName("วันเกิด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_HBD { get; set; }

        [DisplayName("เบอร์โทรศัพท์")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Phone { get; set; }
        [DisplayName("บ้านเลขที่")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Address { get; set; }
        [DisplayName("ตำบล")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Tampo { get; set; }
        [DisplayName("อำเภอ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Amper { get; set; }
        [DisplayName("จังหวัด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Province { get; set; }
        [DisplayName("ไปรษณีย์")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string U_Zipcode { get; set; }
        [DisplayName("สิทธิ์การเข้าใช้งาน")]

        public Nullable<int> U_Role { get; set; }

    }
    [MetadataType(typeof(Table_UserV))]
    public partial class Table_User { }

    public partial class Table_OderV
    {


        public int O_OderID { get; set; }
        [DisplayName("วันที่ซื้อ")]
        public string O_Date { get; set; }
        [DisplayName("ผู้ซื้อ")]

        public string O_User { get; set; }
        [DisplayName("ราคารวม")]

        public Nullable<int> O_Total { get; set; }
        [DisplayName("สถานะ")]

        public Nullable<int> O_Status { get; set; }
        [DisplayName("หลักฐานการชำระเงิน")]
        public byte[] O_IMGPAY { get; set; }

    }
    [MetadataType(typeof(Table_OderV))]
    public partial class Table_Oder { }
    public partial class Table_OderDetailV
    {
        public int OD_OrderDetail { get; set; }
        public Nullable<int> OD_Oder { get; set; }
        [DisplayName("จำนวนสินค้า")]

        public Nullable<int> OD_Amount { get; set; }
        [DisplayName("สินค้า")]

        public Nullable<int> OD_Product { get; set; }


    }
    [MetadataType(typeof(Table_OderDetailV))]
    public partial class Table_OderDetail { }

    public partial class Table_TilieNameV
    {

        [DisplayName("รหัสคำนำหน้าชื่อ")]
        public int T_TItleID { get; set; }
        [DisplayName("คำนำหน้าชื่อ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]

        public string T_TilName { get; set; }


    }
    [MetadataType(typeof(Table_TilieNameV))]
    public partial class Table_TilieName { }
}