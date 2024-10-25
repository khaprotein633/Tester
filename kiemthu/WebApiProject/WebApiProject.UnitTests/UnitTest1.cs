using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiProject.Models;

namespace WebApiProject.Tests
{
    [TestFixture]
    public class DangKyTests
    {
        // Hàm hỗ trợ để validate model
        private IList<ValidationResult> ValidateModel(DangKy model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

        // Test Case 1: Thông tin hợp lệ -> Đăng ký thành công
        [Test]
        public void TestCase1_DangKy_Valid_AllFieldsValid_Success()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "email@example.com",
                soDienThoai = "0901234567",
                password = "Password123",
                confirm_password = "Password123",
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Test đăng ký thành công (không có lỗi)
            Assert.AreEqual(0, validationResults.Count);
        }

        // Test Case 2: Họ tên trống -> Bắt lỗi
        [Test]
        public void TestCase2_DangKy_HotenEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "",  // Trường họ tên trống
                email = "email@example.com",
                soDienThoai = "0901234567",
                password = "Password123",
                confirm_password = "Password123",
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường họ tên
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập họ tên!")));
        }

        // Test Case 3: Email trống -> Bắt lỗi
        [Test]
        public void TestCase3_DangKy_EmailEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "",  // Trường email trống
                soDienThoai = "0901234567",
                password = "Password123",
                confirm_password = "Password123",
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường email
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập Email!")));
        }

        // Test Case 4: Số điện thoại trống -> Bắt lỗi
        [Test]
        public void TestCase4_DangKy_SoDienThoaiEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "email@example.com",
                soDienThoai = "",  // Trường số điện thoại trống
                password = "Password123",
                confirm_password = "Password123",
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường số điện thoại
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Số điện thoại là trường bắt buộc.")));
        }

        // Test Case 5: Mật khẩu trống -> Bắt lỗi
        [Test]
        public void TestCase5_DangKy_PasswordEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "email@example.com",
                soDienThoai = "0901234567",
                password = "",  // Trường mật khẩu trống
                confirm_password = "Password123",
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường mật khẩu
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập mật khẩu")));
        }

        // Test Case 6: Xác nhận mật khẩu trống -> Bắt lỗi
        [Test]
        public void TestCase6_DangKy_ConfirmPasswordEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "email@example.com",
                soDienThoai = "0901234567",
                password = "Password123",
                confirm_password = "",  // Trường xác nhận mật khẩu trống
                diachi = "123 Main Street"
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường xác nhận mật khẩu
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Mật khẩu không khớp")));
        }

        // Test Case 7: Địa chỉ trống -> Bắt lỗi
        [Test]
        public void TestCase7_DangKy_DiaChiEmpty_Error()
        {
            var model = new DangKy
            {
                hoten = "Nguyen Van A",
                email = "email@example.com",
                soDienThoai = "0901234567",
                password = "Password123",
                confirm_password = "Password123",
                diachi = ""  // Trường địa chỉ trống
            };

            var validationResults = ValidateModel(model);

            // Kiểm tra có lỗi thông báo cho trường địa chỉ
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập địa chỉ!")));
        }
    }
 


        [TestFixture]
        public class ShoesTests
        {
            // Hàm hỗ trợ để validate model
            private IList<ValidationResult> ValidateModel(Shoes model)
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(model, null, null);
                Validator.TryValidateObject(model, context, validationResults, true);
                return validationResults;
            }

            // Test Case 1: Tất cả thông tin hợp lệ -> Thêm sản phẩm thành công
            [Test]
            public void TestCase1_Shoes_Valid_AllFieldsValid_Success()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "Giày Thể Thao",
                    brand_id = 1,
                    price = 150000,
                    productimage_url = "http://example.com/image.jpg",
                    description = "Mô tả sản phẩm",
                    detail = "Chi tiết sản phẩm",
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra đăng ký thành công (không có lỗi)
                Assert.AreEqual(0, validationResults.Count);
            }

            // Test Case 2: Tên sản phẩm trống -> Bắt lỗi
            [Test]
            public void TestCase2_Shoes_ProductNameEmpty_Error()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "", // Trường tên sản phẩm trống
                    brand_id = 1,
                    price = 150000,
                    productimage_url = "http://example.com/image.jpg",
                    description = "Mô tả sản phẩm",
                    detail = "Chi tiết sản phẩm",
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra có lỗi thông báo cho trường tên sản phẩm
                Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập tên sản phẩm !")));
            }

            // Test Case 3: Giá sản phẩm trống -> Bắt lỗi
            [Test]
            public void TestCase3_Shoes_PriceEmpty_Error()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "Giày Thể Thao",
                    brand_id = 1,
                    price = null, // Trường giá sản phẩm trống
                    productimage_url = "http://example.com/image.jpg",
                    description = "Mô tả sản phẩm",
                    detail = "Chi tiết sản phẩm",
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra có lỗi thông báo cho trường giá sản phẩm
                Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập giá !")));
            }

            // Test Case 4: URL hình ảnh trống -> Bắt lỗi
            [Test]
            public void TestCase4_Shoes_ImageUrlEmpty_Error()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "Giày Thể Thao",
                    brand_id = 1,
                    price = 150000,
                    productimage_url = "", // Trường URL hình ảnh trống
                    description = "Mô tả sản phẩm",
                    detail = "Chi tiết sản phẩm",
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra có lỗi thông báo cho trường URL hình ảnh
                Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập Url hình ảnh !")));
            }

            // Test Case 5: Mô tả trống -> Bắt lỗi
            [Test]
            public void TestCase5_Shoes_DescriptionEmpty_Error()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "Giày Thể Thao",
                    brand_id = 1,
                    price = 150000,
                    productimage_url = "http://example.com/image.jpg",
                    description = "", // Trường mô tả trống
                    detail = "Chi tiết sản phẩm",
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra có lỗi thông báo cho trường mô tả
                Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập mô tả !")));
            }

            // Test Case 6: Chi tiết sản phẩm trống -> Bắt lỗi
            [Test]
            public void TestCase6_Shoes_DetailEmpty_Error()
            {
                var model = new Shoes
                {
                    product_id = "1",
                    category_id = 1,
                    product_name = "Giày Thể Thao",
                    brand_id = 1,
                    price = 150000,
                    productimage_url = "http://example.com/image.jpg",
                    description = "Mô tả sản phẩm",
                    detail = "", // Trường chi tiết sản phẩm trống
                    brandname = "Nike",
                    hide = 0,
                    Comments = new List<BinhLuanSP>()
                };

                var validationResults = ValidateModel(model);

                // Kiểm tra có lỗi thông báo cho trường chi tiết sản phẩm
                Assert.IsTrue(validationResults.Any(v => v.ErrorMessage.Contains("Bạn chưa nhập chi tiết sản phẩm!")));
            }
        }
    

}
