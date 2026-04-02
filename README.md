# Web bán hàng - ASP.NET Core MVC

Dự án website cá nhân theo mô hình **MVC Core** với đề tài **Quản lý bán hàng**.

## Chức năng đã có

### Phía Admin
- Quản lý sản phẩm (thêm/sửa/xóa/xem)
- Quản lý loại sản phẩm (thêm/sửa/xóa/xem)

### Phía người dùng
- Xem danh sách sản phẩm
- Giỏ hàng (thêm/xóa sản phẩm trong giỏ)

### Chức năng bắt buộc
- Đăng ký
- Đăng nhập
- Phân quyền (`Admin`, `User`)

## Công nghệ
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core + SQLite
- ASP.NET Core Identity

## Cách chạy
1. Cài .NET SDK 8.
2. Khôi phục package:
   ```bash
   dotnet restore
   ```
3. Chạy ứng dụng:
   ```bash
   dotnet run
   ```
4. Mở trình duyệt theo URL hiển thị trong terminal.

Dữ liệu mẫu và tài khoản admin mặc định sẽ được seed khi chạy lần đầu:
- Email: `admin@webbanhang.local`
- Password: `admin123`

## Lưu ý vấn đáp
Để chuẩn bị cho phần vấn đáp, nên nắm rõ:
- Luồng đăng ký/đăng nhập và cách Identity lưu user/role.
- Cách `[Authorize]` và `[Authorize(Roles = "Admin")]` bảo vệ controller/action.
- Quan hệ dữ liệu `Category` - `Product` và xử lý giỏ hàng theo từng user.
