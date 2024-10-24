create database Store_api
use Store_api


CREATE TABLE [Products] (
  [product_id] VARCHAR(255) PRIMARY KEY,
  [category_id] INT,
  [product_name] nVARCHAR(255),
  [brand_id] int,
  [price] DECIMAL(10,2),
  [productimage_url] varchar(255),
  [description] nTEXT,
  [detail] nTEXT,
  [hide] int
)																							
GO

UPDATE [Products]
SET [hide] = 1
WHERE [hide] IS NULL; -- Hoặc bạn có thể bỏ điều kiện WHERE nếu bạn muốn cập nhật tất cả



ALTER TABLE [Products]
ADD [hide] INT;
GO

CREATE TABLE [Product_Size_Quantity] (
  [product_size_quantity_id] INT PRIMARY KEY IDENTITY(1,1),
	[product_id] VARCHAR(255),
  [size] VARCHAR(10),
  [quantity] INT
);


CREATE TABLE [Brands] (
  [brand_id] INT PRIMARY KEY IDENTITY(1,1),
  [brand_name] nVARCHAR(100)
)
GO

CREATE TABLE [Category] (
  [category_id] INT PRIMARY KEY IDENTITY(1,1),
  [category_name] nVARCHAR(100)
)
GO

CREATE TABLE [User] (
  [user_id] varchar(255) PRIMARY KEY,
  [role_id] varchar(20),
  [name] nvarchar(255),
  [password] varchar(10),
  [email] varchar(255),
  [address] nvarchar(255),
  [phonenumber] VARCHAR(15),
  [account_date] DATETIME
)
GO



CREATE TABLE [Product_Review] (
  [product_review_id] varchar(255) PRIMARY KEY,
  [product_id] VARCHAR(255),
  [user_id] VARCHAR(255),
  [rating] INT,
  [comment] nTEXT,
  [review_date] DATETIME
);
GO


CREATE TABLE [Shopping_Cart] (
  [cart_id] varchar(255) PRIMARY KEY,
  [user_id] varchar(255),
  [product_id] VARCHAR(255),
  [size] varchar(255),
  [quantity] INT,
  [price] DECIMAL(10,2)
)
GO


CREATE TABLE [Orders] (
  [orders_id] varchar(255) PRIMARY KEY,
  [user_id] varchar(255),
  [total_amount] DECIMAL(10,2),
  [orders_date] datetime, 
  [delivery_date] datetime,
  [shipping_address] ntext,
  [user_phone] VARCHAR(20),
  [oderstatus_id] int
)
GO

CREATE TABLE [Order_Details] (
  [order_details_id] VARCHAR(255) PRIMARY KEY,
  [order_id] varchar(255),
  [product_id] VARCHAR(255),
  [size] varchar(255),
  [price_oder] DECIMAL(10,2),
  [quantity] INT
)
GO


CREATE TABLE [Role] (
  [role_id] varchar(20) PRIMARY KEY,
  [role_name] nvarchar(20)
)
GO

CREATE TABLE [oderStatusCheck] (
  [oderstatus_id] INT PRIMARY KEY IDENTITY(1,1),
  [status] nvarchar(255)
)
GO

ALTER TABLE [Products] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([category_id])
GO
ALTER TABLE [Products] ADD FOREIGN KEY ([brand_id]) REFERENCES [Brands] ([brand_id])
GO
ALTER TABLE [Product_Review] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO
ALTER TABLE [Product_Review] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO

ALTER TABLE [Product_Size_Quantity] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

ALTER TABLE [Shopping_Cart] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

ALTER TABLE [Shopping_Cart] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO


ALTER TABLE [User] ADD FOREIGN KEY ([role_id]) REFERENCES [Role] ([role_id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([oderstatus_id]) REFERENCES [oderStatusCheck] ([oderstatus_id])
GO

ALTER TABLE [Order_Details] ADD FOREIGN KEY ([order_id]) REFERENCES [Orders] ([orders_id])
GO
ALTER TABLE [Order_Details] ADD FOREIGN KEY ([product_id]) REFERENCES [Products]  ([product_id])
GO



-- Thêm d? li?u vào b?ng Role (n?u có)
INSERT INTO Role (role_id, role_name)
VALUES ('R1', 'Customer'),
       ('R2', 'Admin');

INSERT INTO [User] ([user_id], [role_id], [name], [password], [email], [address], [phonenumber], [account_date])
VALUES ('admin123', 'R2', 'Admin User', 'Adminpass', 'admin@example.com', '123 Admin Street', '123456789', GETDATE());

INSERT INTO oderStatusCheck ( [status])
VALUES ( N'Đang chờ duyệt'),
       ( N'Đang xử lý'),
       ( N'Đang giao'),
       ( N'Đã giao'),
	   ( N'Đã huỷ');

-- Thêm d? li?u vào b?ng Brands
INSERT INTO Brands ( brand_name)
VALUES ( 'Nike'),
       ( 'Adidas'),
       ( 'Puma'),
	   ( 'Mizuno'),
	   ( 'Joma'),
	   ( 'Asics'),
	   ( 'Kamito'),
	   ( 'Zocker');

-- Thêm d? li?u vào b?ng Category
INSERT INTO Category ( category_name)
VALUES ( N'Giày cỏ nhân tạo'),
       (N'Giày Futsal'),
	   (N'Giày cỏ tự nhiên') ;   

-- Thêm d? li?u vào b?ng Products
INSERT INTO Products (product_id, category_id, product_name, brand_id, price, productimage_url, [description],detail)   
/*giày nhân tạo*/
    /*nike*/
VALUES ('S0001',  1, 'GIÀY ĐÁ BÓNG NIKE PHANTOM LUNA II ACADEMY TF SHADOW - BLACK FJ2566-001', 1, 2350000 , 'img/conhantao/Nike/1.jpg' , N'Đúng như mọi năm, tại thời điểm này, Nike tiếp tục cho ra mắt bộ sưu tập giày bóng đá "Shadow Pack", thường bao gồm 2 phiên bản Phantom GX và Air Zoom Mercurial, với một thiết kế mang phong cách mạnh mẽ và cuốn hút cùng màu sắc đen tối. Nhưng năm nay, điểm đặc biệt của bộ sưu tập này chính là sự “gia nhập” thêm của mẫu giày bóng đá Tiempo Legend 10 - một phiên bản được kỳ vọng mang đến làn gió mới mẻ và đặc biệt cho người dùng.Đinh TF đặc trưng của Nike giống với 2 dòng sản phẩm Mercurial. Đây có lẽ là thiết kế đinh TF tối ưu nhất ở thời điểm hiện tại. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0002',  1, 'GIÀY ĐÁ BÓNG NIKE AIR ZOOM MERCURIAL VAPOR 15 ACADEMY TF PEAK READY - HYPER TURQUOISE/FUCHSIA DREAM/BLACK/WHITE KIDS DJ5621-300', 1,1850000 , 'img/conhantao/Nike/2.jpg' , N'Hòa mình vào không khí sôi động của mùa giải Champions League, Nike chính thức ra mắt bộ sưu tập Peak Ready vào hôm nay (16/10) với tất cả 4 silo: Mercurial, Tiempo, Phantom GX và Phantom Luna lấy cảm hứng từ chủ đề Champions League. Hyper Turq/Black/Fuchia Dream/White là phối màu dành cho những trận đấu ở UEFA Champions League, đây là pack sản phẩm dành cho những cầu thủ đại sứ thi đấu tại giải đấu hấp dẫn nhất hành tinh này.Giày đá bóng Nike Air Zoom Mercurial Vapor 15 Academy TF Peak Ready - Hyper Turquoise/Fuchsia Dream/Black/White Kids DJ5621-300 là mẫu giày phổ thông dành cho trẻ em chơi ở mặt sân cỏ nhân taọ 5-7 người. Với gam màu chính thức là Hyper Turquoise/Fuchsia Dream/Black/White,  80% giày được tô điểm bằng màu xanh, kết hợp với logo đen nổi bật và thêm một vài họa tiết màu hồng phía gót giày tạo nên sự tinh tế và cá tính.  ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0003',  1, 'GIÀY ĐÁ BÓNG NIKE PHANTOM', 1,2800000 , 'img/conhantao/Nike/3.jpg' , N' Sau thành công của hai bộ sưu tập , mới đây Nike đã chính thức giới thiệu bộ sưu tập  mang một giao diện hoàn toàn mới với thiết kế ấn tượng cùng sự góp mặt của cho phiên bản giày đá banh hàng đầu: Phantom GX II, Luna II, Air Zoom Mercurial và Tiempo Legend. Điểm gây chú ý đầu tiên phải kể đến là sự thay đổi màu sắc đặc trưng. Bộ sưu tập bất ngờ chuyển hướng từ gam màu ngọc lục của BST sang tông màu đen, trắng và vàng, tạo nên sự sang trọng và quý phái.Mặc dù không được tích hợp bộ đệm React giống như người tiền nhiệm, thế nhưng, bộ đệm mới của Legend 10 Pro theo mình đánh giá mang lại nhiều cảm giác êm ái và đàn hồi hơn trước.', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	  
	/*giày nhân tạo*/
/*adidas*/
	   ('S0006',  1, 'GIÀY ĐÁ BÓNG ADIDAS PREDATOR ACCURACY .1 TF OWN YOUR FOOTBALL - CORE BLACK/FOOTWEAR WHITE/SHOCK PINK GW4633', 2,2550000 , 'img/conhantao/Adidas/1.jpg', 'adidas Predator Accuracy .1 TF Own Your Football - Core Black/Footwear White/Shock Pink là mẫu giày cỏ nhân tạo phân khúc cao cấp nhất dành cho mặt sân cỏ nhân tạo 5 người. Phối màu Đen - Hồng - Trắng mang lại 1 diện mạo hài hoà, pha trộn được giữa nét cổ điển và hiện đại. Form giày vẫn giữ đặc trưng tương đối thoải mái, phù hợp với các thể loại chân thon - bè. Rút kinh nghiệm từ những lần ra mắt trước, Adidas đã chuẩn bị rất kỹ cho Predator Accuracy khi dành hẳn 6 tháng cho các ngôi sao mang trải nghiệm và test trên chân trước khi tung ra thị trường. Đây chắc hẳn là mẫu giày tâm huyết nhất của Adidas trong thời gian sắp tới. Bộ đệm Bounce là chất liệu mang đến sự hỗ trợ cao hơn chất liệu đệm EVA truyền thống về cả độ thoải mái, độ êm và độ đàn hồi.  ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),   
	   ('S0008',  1, 'GIÀY ĐÁ BÓNG ADIDAS PREDATOR', 2,1750000 , 'img/conhantao/Adidas/3.jpg', N'adidas Predator Accuracy .3 Low TF Heatspawn - Solar Orange/Core Black là mẫu giày phổ thông dành cho mặt sân cỏ nhân tạo 5-7 người. Predator đã duy trì sự thay đổi trong suốt những năm qua, và 2023 là một ngoại hình hoàn toàn mới, thiết kế có dây, và đặc biệt Predator Accuracy mang đậm chất hoài cổ khi có logo 3 sọc uốn cong (giống như phiên bản predator 1998).Adidas Predator Accuracy có phần upper bằng da tổng hợp kết hợp với các lớp vân High Definition Texture giúp tăng khả năng kiểm soát và rê bóng. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0009',  1, 'GIÀY ĐÁ BÓNG ADIDAS X CRAZYFAST MESSI .3 TF INFINITO - SILVER METALLIC/BLISS BLUE/CORE BLACK IE4074', 2,1690000 ,  'img/conhantao/Adidas/4.jpg', N'adidas X Crazyfast Messi .3 TF Infinito - Silver Metallic/Bliss Blue/Core Black là phiên bản giày dành cho sân cỏ nhân tạo 5-7 người.  Điều này tăng độ êm trong từng bước di chuyển của người chơi trên bề mặt sân nhân tạo, giảm tối đa phản lực tác động lên các khớp chân, gối của người chơi. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	  
       /*giày nhân tạo*//*Puma*/
	   ('S0013',  1, 'GIÀY ĐÁ BÓNG PUMA FUTURE 7 MATCH TT PHENOMENAL - PUMA WHITE/PUMA BLACK/POISON PINK 107720-01', 3,2030000 ,  'img/conhantao/Puma/1.jpg', N'Puma Future 7 Phenomenal là một tác phẩm nghệ thuật với bản phối màu "Puma White/Puma Black/Poison Pink". Phần upper của giày được phủ một lớp trắng tinh khôi tạo nền cho những vệt màu pastel nhẹ nhàng, từ hồng phấn đến xanh da trời  mô phỏng cho bầu trời lấp lánh huyền ảo. Logo "Puma" màu đen tương phản, được đặt ở phía hông giày, làm nổi bật thương hiệu mà không làm mất đi vẻ đẹp tổng thể. Đặc biệt, phần đế giày màu hồng rực rỡ, không chỉ tạo ra một điểm nhấn về màu sắc mà còn thể hiện được sự táo bạo nhưng vẫn có một chút nhẹ nhàng trong đó. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0014',  1, 'GIÀY ĐÁ BÓNG PUMA ULTRA ULTIMATE CAGE TT PHENOMENAL - POISON PINK/PUMA WHITE/PUMA BLACK 107745-01', 3,2400000 ,  'img/conhantao/Puma/2.jpg', N'PUMA Ultra Ultimate Cage TT Phenomenal - Poison Pink/PUMA White/PUMA Black là mẫu giày cao cấp dành cho sân cỏ nhân tạo 5-7 người. Khoác lên vẻ bề ngoài mẫu giày đá bóng PUMA Ultra Ultimate Cage TT Phenomenal - Poison Pink/PUMA White/PUMA Black là một gam màu Poison Pink/ bắt mắt cùng với  họa tiết mang tính biểu tượng được sáng tạo hiện đại với những vệt màu trắng, đen hài hòa làm điểm nhấn rõ nét. Tất cả họa tiết và màu sắc đồng nhất, hỗ trợ lẫn nhau tạo nên vẻ đẹp táo bạo rất hiếm có trong những thiết kế trước đây của nhà Báo Đức. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0015',  1, 'GIÀY ĐÁ BÓNG PUMA FUTURE ULTIMATE CAGE TT VOLTAGE - YELLOW BLAZE/WHITE/BLACK 107364-04', 3,2789000 ,  'img/conhantao/Puma/3.jpg', N'Một sự cải tiến không kém phần ấn tượng khi Puma đã lược bỏ hoàn toàn chất liệu da thật để mang đến sự thay thế xứng đáng mang tên K-Better, một chất liệu giả da cung cấp cảm giác bóng không hề kém cạnh với da thật nhưng vượt trội về độ bền và trọng lượng, và tất nhiên, K-Better là một bước tiến được Puma tạo ra trong xu thế sử dụng vật liệu thân thiện với môi trường. Cảm giác êm ái, thanh thoát và sự đằm chân trong từng bước chuyển động sẽ là những gì chúng ta có thể cảm nhận rất rõ ở Puma King Ultimate ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	  
	   /*giày nhân tạo*//*Mizuno*/
	   ('S0019',  1, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA SALA CLASSIC TF - WHITE/RED Q1GB240291', 4,1790000 ,  'img/conhantao/Mizuno/1.jpg', N'Thương hiệu Mizuno nổi tiếng với những thiết kế bằng cổ điển được yêu thích trên toàn thế giới, luôn nâng cấp và chỉnh chu trong từng chi tiết, đầy tỉ mỉ và tinh tế. Giày đá bóng Mizuno Morelia Sala Classic TF - White/Red Q1GB240291 là mẫu giày chuyên cho sân cỏ nhân tạo 5-7 người. Với thiết kế truyền thống kết hợp những chi tiết hiện đại, Morelia Sala Classic TF thực sự là một lựa chọn ấn tượng trong phân khúc tầm trung. Với những cầu thủ yêu thích dòng giày Morelia và mong muốn có một mẫu giày hiệu suất cao, êm ái cùng mức giá hợp lý thì không thể bỏ qua Morelia Sala Classic TF. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0020',  1, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA SALA', 4,1950000 ,  'img/conhantao/Mizuno/2.jpg', N'Thương hiệu Mizuno nổi tiếng với những thiết kế bằng cổ điển được yêu thích trên toàn thế giới, luôn nâng cấp và chỉnh chu trong từng chi tiết, đầy tỉ mỉ và tinh tế. Giày đá bóng Mizuno Morelia Sala Classic TF - White/Red Q1GB240291 là mẫu giày chuyên cho sân cỏ nhân tạo 5-7 người. Với thiết kế truyền thống kết hợp những chi tiết hiện đại, Morelia Sala Classic TF thực sự là một lựa chọn ấn tượng trong phân khúc tầm trung. Với những cầu thủ yêu thích dòng giày Morelia và mong muốn có một mẫu giày hiệu suất cao, êm ái cùng mức giá hợp lý thì không thể bỏ qua Morelia Sala Classic TF. ', N'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	  /*giày nhân tạo*//*Joma*/

	   /*giày nhân tạo*//*Asics*/
	 
	   /*giày nhân tạo*//*Kamito*/
	   ('S0025',  1, 'GIÀY ĐÁ BÓNG KAMITO VELOCIDAD LEGEND TF - ORANGE/BLACK KMA230160', 7,499000 ,  'img/conhantao/Kamito/1.jpg', N'GIÀY ĐÁ BÓNG KAMITO VELOCIDAD LEGEND TF - ORANGE/BLACK KMA230160 Sau thời gian vắng bóng, chú ngựa chiến "Tốc Độ" huyền thoại Velocidad chính thức trở lại với phong cách Cool ngầu - nhẹ nhàng và thanh thoát nhưng vẫn giữ nguyên form giày thuần Việt. Phiên bản mới nhất này được mang tên Velocidad Legend.  ', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	   ('S0026',  1, 'GIÀY ĐÁ BÓNG KAMITO TA11 PRO TF TOUCH OF MAGIC - NEON/GOLD KMA230125', 7,499000 ,  'img/conhantao/Kamito/2.jpg', N'Kamito TA11 là mẫu giày được đặt tên theo tên và số áo của tiền vệ Nguyễn Tuấn Anh. Mẫu giày này đã ra mắt được một thời gian và nhận được sự ủng hộ rất lớn của người chơi bóng đá trên cả nước bởi chất lượng tốt, độ ổn định cao, thiết kế nổi bật cùng với đó là mức giá vô cùng hợp lý. Với những yếu tố đó, Kamito TA11 được nhiều người hâm mộ nhận định là mẫu giày “quốc dân”, phù hợp với nhiều người chơi Việt Nam. ', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
	
	   /*giày nhân tạo*//*Zocker*/
	   ('S0029',  1, 'GIÀY ĐÁ BÓNG ZOCKER PIONEER - WHITE/NEON SNS-006-WHITE', 8,390000 ,  'img/conhantao/Zocker/1.jpg', N'Từ trước tới nay, không ít người có định kiến rằng: “Giày trong nước sao so được với các hãng lớn như Nike, Adidas, Mizuno,...”. Chính vì vậy, Zocker luôn mong mỏi có thể tạo ra một “mũi giáo tiên phong” đánh tan định kiến đó và chinh phục phân khúc giày bóng đá Top-End trong nước, nơi trước giờ vẫn chỉ ghi “dấu chân" của các thương hiệu ngoại nhập. Giày đá bóng Zocker Pioneer ra đời với mục đích đem lại trải nghiệm, chất lượng không kém gì những đôi giày phân khúc cao cấp của thương hiệu quốc tế mà giá chỉ bằng một nửa, phù hợp với mặt bằng thu nhập của người Việt. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	   ('S0030',  1, 'GIÀY ĐÁ BÓNG ZOCKER INSPIRE PRO TF - BLACK SNS-005-BLACK', 8,729000 ,  'img/conhantao/Zocker/2.jpg', N'Thương hiệu Zocker chính thức ra mắt từ năm 2018. Nhưng từ trước đó những nhà sáng lập – vốn là những người rất đam mê thể thao, thường xuyên chơi bóng đá đã rất trăn trở về việc thị trường ở thời điểm đó rất thiếu những đôi giày bóng đá sân cỏ nhân tạo phù hợp với lối chơi bóng linh hoạt của người Việt, cũng như cấu tạo bàn chân, bề mặt sân. Sản phẩm được thiết kế hiện đại với các hoạt tiết đơn giản nhưng bắt mắt. Logo Zocker hình chú sóc truyền cảm hứng về sự thông minh nhanh nhẹn. Các kỹ sư của hãng cũng gửi gắm trong Inspire những hàm ý về nỗ lực vươn lên để trưởng thành. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	
	  
/*giày futsal*/
        /*Nike*/        
		('S0033',  2, 'GIÀY ĐÁ BÓNG NIKE TIEMPO LEGEND 9 ACADEMY IC LUMINOUS - BARELY VOLT/VOLT/SUMMIT WHITE DA1190-705', 1,875000 ,  'img/futsal/Nike/1.jpg', N'Nike Tiempo Legend 9 Academy IC Luminous - Barely Volt/Volt/Summit White là phiên bản phổ thông dành cho bóng đá đường phố và sân Futsal. Phiên bản giày đá banh Tiempo Legend IX không pha lẫn màu sắc nổi bật nào khác, giao diện của silo hoàn toàn là một màu xanh volt tươi mát, Nike chỉ sử dụng màu đen và một màu xanh lục đậm hơn trên Swoosh để tạo điểm nhấn.Về công nghệ của phiên bản Nike Tiempo Legend 9 Academy IC Luminous - Barely Volt/Volt/Summit White: ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0034',  2, 'GIÀY ĐÁ BÓNG NIKE LUNARGATO II IC SMALL SIDED - UNIVERSITY BLUE/WHITE 580456-400', 1,2250000 ,  'img/futsal/Nike/2.jpg', N'Về tiểu sử của Nike Lunar Gato từ lâu đã nổi tiếng về khả năng bám sân, giữ thăng bằng tốt và mang tới cảm giác bóng cực kì chất lượng. Hiểu được những điều mà các cầu thủ của mình muốn, đầu năm 2019 NIKE đã Reborn ( Tái sản xuất ) NIKE LUNAR GATO 2, và đội ngũ thiết kế của Nike đã không ngừng sáng tạo để mang đến những bản phối màu đặc sắc và ấn tượng cho các Fan của đôi giày huyền thoại này.Chất liệu da được cấu tạo bằng da thật mềm cao cấp mang lại cảm giác cực êm và bám trong những pha chạm bóng', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0035',  2, 'GIÀY ĐÁ BÓNG DESPORTE CAMPINAS JP6 IC DS2030 - WHITE/BLACK/GOLD', 1,2400000 ,  'img/futsal/Nike/3.jpg', N'Desporte là một thương hiệu thể thao được tạo ra bằng cách kết hợp chữ “D” để rê bóng và “Esporte”, có nghĩa là thể thao trong tiếng Bồ Đào Nha. Được thành lập vào năm 2003 tại Shizuoka, Nhật Bản. Desporte nhanh chóng nổi lên như một trong những thương hiệu futsal hàng đầu tại Nhật Bản.Giày đá banh Desporte Campinas JP6 IC DS2030 - White/Black/Gold là phiên bản cao cấp cho mặt sân futsal. ', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		
		/*giày futsal*//*Adidas*/
        ('S0041',  2, 'GIÀY ĐÁ BÓNG ADIDAS TOP SALA COMPETITION IC - ROYAL BLUE/SOLAR YELLOW/CLOUD WHITE FZ6123', 2,1690000 , 'img/futsal/Adidas/3.jpg', N'Top Sala Competition là phiên bản dành cho mặt sân FUTSAL trong nhà (nhưng cũng có thể chơi tốt trên sân cỏ nhân tạo 5 người với điều kiện thời tiết tốt và bề mặt cỏ không quá dầy). Có lẽ thứ duy nhất còn giữ lại là cái tên Top Sala, còn lại tất cả mọi thứ đều khác. Form mới bè và thoải mái hơn, ngoại hình cũng hiện đại hơn. Và đặc biệt được trang bị thêm bộ đệm LightStrike (bộ đệm êm nhất ở thời điểm hiện tại của Adidas). Phần mũi giày cũng được thiết kế bè hơn để hỗ trợ chính mũi (kỹ thuật quá đỗi quen thuộc trong Futsal), Phần đề trước cũng được làm dầy và cao hơn so với phiên bản cũ. Thiết kế này rất giống với dòng sản phẩm Adidas Copa Pure cũng vừa ra mắt của Adidas. Về công nghệ của phiên bản Adidas Top Sala Competition IC: Chất liệu da tổng hợp ở phần sau giót giúp định hình form giày cứng cáp, chất liệu giả da (Simili) mềm mại ở phần mũi mang lại cảm giác êm ái và sự mềm mại trong những pha chạm bóng.. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0042',  2, 'GIÀY ĐÁ BÓNG ADIDAS X SPEEDFLOW .1 IN SAPPHIRE EDGE - SKY RUSH/SHOCK PINK/FOOTWEAR WHITE GW7464', 2,1690000 , 'img/futsal/Adidas/4.jpg', N'GIÀY ĐÁ BÓNG ADIDAS X SPEEDFLOW .1 IN SAPPHIRE EDGE - SKY RUSH/SHOCK PINK/FOOTWEAR WHITE GW7464 là phiên bản dành cho mặt sân FUTSAL trong nhà (nhưng cũng có thể chơi tốt trên sân cỏ nhân tạo 5 người với điều kiện thời tiết tốt và bề mặt cỏ không quá dầy). Có lẽ thứ duy nhất còn giữ lại là cái tên Top Sala, còn lại tất cả mọi thứ đều khác. Form mới bè và thoải mái hơn, ngoại hình cũng hiện đại hơn. Và đặc biệt được trang bị thêm bộ đệm LightStrike (bộ đệm êm nhất ở thời điểm hiện tại của Adidas).  ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),

		/*giày futsal*//*Puma*/
		
		/*giày futsal*//*Mizuno*/
		('S0044',  2, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA SALA ELITE IN AZURE BLUE - BLUE/WHITE/COPPER Q1GA230125', 4,2750000 , 'img/futsal/Mizuno/1.jpg', N'Morelia là dòng giày đã được ra mắt từ năm 1985 với thiết kế truyền thống mang tính chuẩn mực cho một đôi giày bóng đá. Trải qua gần 40 năm, những mẫu Morelia mới nhất vẫn giữ những nét truyền thống đó pha lẫn với những chi tiết hiện đại tạo nên một đôi giày tinh tế phù hợp với nhiều cầu thủ. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0045',  2, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA NEO III PRO IN LẠC VIỆT - RED/YELLOW LIMITED EDITION Q1GB228444', 4,2490000 , 'img/futsal/Mizuno/2.jpg', N'Để kỉ niệm 7 năm Mizuno có mặt tại thị trường Việt Nam và để tri ân người chơi bóng đá yêu mến Mizuno, chúng tôi xin giới thiệu phiên bản giày bóng đá giới hạn đặc biệt: Morelia Neo III Lac Viet Limited Edition. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),

		/*giày futsal*//*Joma*/
		('S0047',  2, 'GIÀY ĐÁ BÓNG JOMA TOP FLEX 2317 IN - BLUE/GOLD TOPS2317IN', 5,2650000 ,  'img/futsal/Joma/1.jpg', N'Được làm bằng da Nubuck cao cấp, với nylon chất lượng cao bên trong cung cấp sức mạnh. JOMA TOP FLEX 2317  kết hợp công nghệ BẢO VỆ ở mũi giày để tăng cường sức đề kháng và sự ổn định của nó đối với những trường hợp hao mòn. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0048',  2, 'GIÀY ĐÁ BÓNG JOMA TOP FLEX 2122 IN - WHITE/ROYAL TOPS2122IN', 5,1490000 ,  'img/futsal/Joma/2.jpg', N'Đế trong làm bằng EVA tự nhiên để cải thiện đệm bằng cách hấp thụ tác động của bước đi. Đế EVA đúc sẵn phù hợp với bề mặt của bàn chânCông nghệ FLEXO được thiết kế tiện lợi để mang lại sự chuyển tiếp tốt nhất từ ​​gót chân đến ngón chân trong mỗi bước đi. Ở phần gót, nó cho phép sự uốn cong chỉ xảy ra ở phần đế, mà không ảnh hưởng đến cấu trúc, giúp hỗ trợ tốt hơn và cho phép dấu chân tự nhiên và thoải mái hơn. Với hệ thống ROTATION, một cấu trúc hình học nằm trên đế tạo điều kiện thuận lợi cho các cú xoay người, giày ngăn ngừa chấn thương chủ yếu ở đầu gối và mắt cá chân. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0049',  2, 'GIÀY ĐÁ BÓNG JOMA TOP FLEX 2307 IN - ORANGE/NAVY TOPS2307IN', 5,1490000 ,  'img/futsal/Joma/3.jpg', N'Đế trong làm bằng EVA tự nhiên để cải thiện đệm bằng cách hấp thụ tác động của bước đi. Đế EVA đúc sẵn phù hợp với bề mặt của bàn chânCông nghệ FLEXO được thiết kế tiện lợi để mang lại sự chuyển tiếp tốt nhất từ ​​gót chân đến ngón chân trong mỗi bước đi. Ở phần gót, nó cho phép sự uốn cong chỉ xảy ra ở phần đế, mà không ảnh hưởng đến cấu trúc, giúp hỗ trợ tốt hơn và cho phép dấu chân tự nhiên và thoải mái hơn. Với hệ thống ROTATION, một cấu trúc hình học nằm trên đế tạo điều kiện thuận lợi cho các cú xoay người, giày ngăn ngừa chấn thương chủ yếu ở đầu gối và mắt cá chân. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen')
,
		/*giày futsal*//*Asics*/
		('S0052',  2, 'GIÀY ĐÁ BÓNG ASICS DESTAQUE FF 2 IC - CLASSIC RED/BEET JUICE 1111A217-600', 6,2650000 ,  'img/futsal/Asics/1.jpg', N'Asics có thể coi là nguồn cảm hứng, là khuôn mẫu tạo ra bước đi đầu tiên của đế chế Nike - thương hiệu nổi tiếng đến từ Mỹ. Phil Knight là người đồng sáng lập, chủ tịch danh dự của đế chế NIKE. Ông muốn tìm hiểu xem có dòng giày nào có thể so sánh được với các đôi giày đến từ Đức đang rất được yêu thích trên thế giới thời điểm hiện tại. Trong một lần đi khảo sát và học hỏi, ông đã bị bất ngờ trước chất lượng các đôi giày Asics đem lại. Tiếp đến, ông xin được phân phối và sau đó là săn đón các kỹ sư thiết kế để tạo ra thương hiệu Nike cho riêng mình. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0053',  2, 'GIÀY ĐÁ BÓNG ASICS CALCETTO WD 9 IC - WHITE/BLACK 1113A037-100', 6,1650000 ,  'img/futsal/Asics/2.jpg', N'Asics có thể coi là nguồn cảm hứng, là khuôn mẫu tạo ra bước đi đầu tiên của đế chế Nike - thương hiệu nổi tiếng đến từ Mỹ. Phil Knight là người đồng sáng lập, chủ tịch danh dự của đế chế NIKE. Ông muốn tìm hiểu xem có dòng giày nào có thể so sánh được với các đôi giày đến từ Đức đang rất được yêu thích trên thế giới thời điểm hiện tại. Trong một lần đi khảo sát và học hỏi, ông đã bị bất ngờ trước chất lượng các đôi giày Asics đem lại. Tiếp đến, ông xin được phân phối và sau đó là săn đón các kỹ sư thiết kế để tạo ra thương hiệu Nike cho riêng mình.  ',N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	/*giày futsal*//*Kamito*/
		('S0056',  2, 'GIÀY ĐÁ BÓNG KAMITO TA11 IC TOUCH OF MAGIC - RED/GOLD F21007', 7,621000 ,  'img/futsal/Kamito/1.jpg', N'KAMITO TA11 IC TOUCH OF MAGIC - RED/GOLD là một trong những mẫu giày đá bóng vô cùng nổi tiếng trong giới bóng đá phong trào, được thiết kế với sự góp ý của cầu thủ Tuấn Anh - Hoàng Anh Gia Lai Việt Nam. Kamito TA11 ra đời để vinh danh cũng như ghi nhận những đóng góp đáng giá của cầu thủ Nguyễn Tuấn Anh. Giống như những mẫu giày "Signature" khác, Kamito TA11 sở hữu những đặc trưng khác biệt và thú vị liên quan đến đội trưởng của CLB Hoàng Anh Gia Lai.Những sản phẩm từ thương hiệu Kamito được kỳ công sáng tạo bởi những người thợ lành nghề, dồn tâm huyết vào từng đường kim mũi chỉ, mong muốn đem đến những sản phẩm tốt nhất, giúp các cầu thủ thăng hoa khi thi đấu cùng hiệu suất cao nhất.  ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	
		/*giày futsal*//*Zocker*/
	
	
/*giày tự nhiên*/
         /*Nike*/    
	    ('S0057',  3, 'GIÀY ĐÁ BÓNG NIKE AIR ZOOM MERCURIAL SUPERFLY 9 ELITE FG READY - BRIGHT CRIMSON/WHITE/BLACK DJ4977-600', 1,5190000 ,  'img/cotunhien/Nike/1.jpg', N'Một mùa hè với nhiều giải đấu lớn sôi động đang đến gần, để tiếp thêm sức mạnh cho các cầu thủ khi ra sân, Nike hé lộ những hình ảnh đầu tiên cho BST mới của mình mang tên Ready Pack. BST này được coi là một bản phối màu rực lửa nhất dành cho cả 3 phiên bản Air Zoom Mercurial, Tiempo và Phantom GX từ đầu năm tới nay.Bộ sưu tập giày đá banh Nike Ready Pack được thiết kế với một phong cách phối màu mang đậm chất lửa bên trong nhờ sử dụng gam màu chủ đạo là Bright Crimson (đỏ đậm và tươi) kết hợp cùng các điểm nhấn màu đen trắng. Nghệ thuật sử dụng màu sắc này không chỉ thể hiện được sức nóng của một mùa hè sôi động mà còn tạo ra được sức hút đặc biệt cho các cầu thủ nổi tiếng như Cristiano Ronaldo, Kylian Mbappé, Erling Haaland,... khi họ mang thi đấu trên sân trong thời gian tới.  ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0058',  3, 'GIÀY ĐÁ BÓNG NIKE AIR ZOOM MERCURIAL VAPOR 15 ACADEMY MG LUMINOUS - PINK BLAST/VOLT/GRIDIRON DJ5631-605', 1,3890000 ,  'img/cotunhien/Nike/2.jpg', N'Nike Air Zoom Mercurial Vapor 15 Academy MG Luminous - Pink Blast/Volt/Gridiron là phiên bản giày phổ thông dành chuyên cho sân cỏ tự nhiên 11 người. VAPOR là tên gọi của các phiên bản cổ thấp thường được Bruno, Haaland.. ưa chuộng. Với Air Zoom Mercurial, cả hai phiên bản Vapor XV và Superfly IX đều sở hữu sắc màu nổi bật "Pink Blast/Volt/Gridiron". Gam màu hồng tươi trẻ làm nền, kết hợp cùng các điểm nhấn màu volt hiện đại đặc biệt cho biểu tượng Swoosh trên thân giày và mặt đế, tạo nên vẻ đẹp độc đáo và cuốn hút, khiến cho đôi giày trở thành tâm điểm chú ý trên sân cỏ. Về thông số kỹ thuật Nike Air Zoom Mercurial Vapor 15 Academy MG Luminous - Pink Blast/Volt/Gridiron:  . ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0059',  3, 'GIÀY ĐÁ BÓNG NIKE PHANTOM GX ACADEMY MG BLACK PACK - BLACK/SUMMIT WHITE/DARK SMOKE GREY DD9473-010', 1,2090000 ,  'img/cotunhien/Nike/3.jpg', N'Về mặt công nghệ, cũng giống như Phantom GT, mẫu giày đá bóng Nike Phantom GX được tạo ra là để dành cho những cầu thủ mong muốn có một độ chính xác và khả năng kiểm soát bóng tốt như những vị trí tiền vệ hoặc tiền đạo. Các cầu thủ sử dụng Phantom GT II trước đó đều đồng loạt chuyển sang Phantom GX. Bên cạnh đó Nike cũng làm form giày tương đối thoải mái và tích hợp khá nhiều công nghệ trên bản Elite này. Lưỡi gà lệch chéo để  giúp tăng diện tích mặt tiếp xúc bóng ở mu bàn chân, đây có thể coi là một ưu điểm xuất sắc giúp các cầu thủ cảm giác bóng và hỗ trợ sút bóng chuẩn xác hơn. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	/*giày tự nhiên*//*Adidas*/
		('S0063',  3, 'GIÀY ĐÁ BÓNG ADIDAS X CRAZYFAST LEAGUE FG SOLAR ENERGY - SOLAR YELLOW/CORE BLACK/FOOTWEAR WHITE IG0605', 2,1950000 ,  'img/cotunhien/Adidas/1.jpg', N'Về công nghệ của phiên bản  Giày đá bóng adidas X Crazyfast League FG Solar Energy - Solar Yellow/Core Black/Footwear White IG0605:Chất liệu từ da tổng hợp mới dạng lưới mang lại sự co giãn và tối ưu trọng lượng.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0064',  3, 'GIÀY ĐÁ BÓNG ADIDAS X CRAZYFAST .3 FG MARINERUSH - BRIGHT ROYAL/FOOTWEAR WHITE/SOLAR RED GY7428', 2,1850000 ,  'img/cotunhien/Adidas/2.jpg', N'adidas X Crazyfast .3 FG Marinerush - Bright Royal/Footwear White/Solar Red là phiên bản giày phổ thông có dây dành cho sân cỏ tự nhiên 11 người. adidas X Crazyfast .3 FG Marinerush sở hữu gam màu chủ đạo là "Bright Royal/White/Solar Red" với màu xanh được làm nổi bật với viền màu xanh ngọc, kết hợp với biểu tượng ba sọc màu trắng và màu đỏ của phần đế.Sản phẩm adidas X Crazyfast .3 FG Marinerush được các cầu thủ nổi tiếng đại diện gồm có Lionel Messi, Karim Benzema, Marcelo, Mohamed Salah, Son Hueng Min và Timo Werner...', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0065',  3, 'GIÀY ĐÁ BÓNG ADIDAS COPA PURE .1 FG CRAZYRUSH - FOOTWEAR WHITE/CORE BLACK/LUCID LEMON HQ8971', 2,3500000 ,  'img/cotunhien/Adidas/3.jpg', N'Với bản chất nhẹ nhàng đặc trưng, phần trên của giày (upper) được Adidas sử dụng da Fusionskin, tạo nên cảm giác vừa vặn và thoải mái tuyệt đối. Các đệm xốp ở phần trước bàn chân mang đến cảm giác độc đáo không thể nhầm lẫn với các dòng giày khác. Công nghệ Torsion Frame được sử dụng cho mặt đế, cung cấp độ ổn định và độ bám tốt giúp người chơi dễ dàng giữ thăng bằng và tạo ra được những đường chuyền xoay chuyển đẹp mắt.', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	/*giày tự nhiên*//*Puma*/
		('S0069',  3, 'GIÀY ĐÁ BÓNG PUMA FUTURE ULTIMATE FG/AG VOLTAGE - SEDATE GRAY/ASPHALT/YELLOW BLAZE 107355-04', 3,3050000 ,  'img/cotunhien/Puma/1.jpg', N'Càng gần về cuối năm các giải đấu thể thao diễn ra càng trở nên sôi động và gay gắt hơn, để hỗ trợ tốt nhất cho các cầu thủ, Puma đã chính thức giới thiệu "Voltage Pack" - một bộ sưu tập giày mới, kế nhiệm cho bộ "Gear Up" đã ra mắt trước đó trong tháng 9, hứa hẹn sẽ mang đến những bất ngờ thú vị. Lần này, Puma không chỉ tập trung vào thiết kế mà còn chú trọng đến việc tạo nên những đôi giày mang lại hiệu quả vượt trội hơn nhằm mục đích phục vụ cho các ngôi sao sân cỏ như Neymar Jr, Kingsley Coman, và nhiều tên tuổi nổi tiếng khác.Đúng như tên gọi "Voltage Pack”, Puma Future Ultimate tái hiện những hình ảnh đặc trưng thể hiện sự tốc độ qua các họa tiết tia chớp và ánh sáng, tạo nên một diện mạo vô cùng độc đáo và cá tính.', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	    ('S0072',  3, 'GIÀY ĐÁ BÓNG PUMA ULTRA ULTIMATE FG/AG BREAKTHROUGH - PUMA WHITE/PUMA BLACK/FIRE ORCHID 107311-01', 3,3900000 ,  'img/cotunhien/Puma/4.jpg', N'PUMA Ultra Ultimate FG/AG Breakthrough - PUMA White/PUMA Black/Fire Orchid là phiên bản giày đá banh cao cấp dành cho sân cỏ tự nhiên 11 người. Trong BST BREAKTHROUGH PACK lần này, Ultra Ultimate được coi là ngôi sao sáng với gam màu chủ đạo "Puma White/Fire Orchid", không chỉ bởi màu sắc mà còn vì sự thay đổi về công nghệ. Phiên bản Ultra Ultimate lần này là lần đầu tiên Puma kết hợp của công nghệ PWRTAPE - công nghệ chính của Future Ultimate. Nhưng công nghệ PWRTAPE - dải băng zigzag không được sử dụng bên ngoài như Future mà được đặt nằm ngay bên trong giày, đảm bảo mang đến sự cải tiến về độ cứng cáp và duy trì ổn định, hỗ trợ tối đa sự thoải mái cho việc di chuyển của các cầu thủ . Không dừng lại ở đó, đế giày được nâng cấp hoàn toàn mới mang lại một bước tiến mới về tốc độ và khả năng bám sân khi thi đấu.Về công nghệ của phiên bản PUMA Ultra Ultimate FG/AG:Sản phẩm được các cầu thủ nổi tiếng đại diện gồm có Antoine Griezmann, Sergio Aguero, Antony..... ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0073',  3, 'GIÀY ĐÁ BÓNG PUMA FUTURE ULTIMATE FG/AG BREAKTHROUGH - PUMA WHITE/PUMA BLACK/FIRE ORCHID 107355-01', 3,3900000 ,  'img/cotunhien/Puma/5.jpg', N'PUMA Future Ultimate FG/AG Breakthrough - PUMA White/PUMA Black/Fire Orchid là phiên bản giày đá banh cao cấp dành cho sân cỏ tự nhiên 11 người. Future Ultimate, với màu sắc "Puma White/Fire Orchid", trở nên nổi bật trong BST với một bản thiết kế tối giản nhưng ấn tượng. Chủ đạo là màu trắng tinh khiết, kết hợp với các dải màu đỏ tương phản tạo nên sự độc đáo từ các dải zigzag - công nghệ PWRTAPE nằm dọc hai bên giày. Đinh giày cũng mang màu đỏ rực, tạo nên sự liên kết màu sắc đồng bộ từ thân giày tới đế. kiểm soát bóng khi rê, chuyền và dứt bóng. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
	
		/*giày tự nhiên*//*Mizuno*/
		('S0074',  3, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA NEO IV BETA ELITE FG CHARGE - WHITE/RADIANT RED P1GA244260', 4,4320000 ,  'img/cotunhien/Mizuno/1.jpg', N'Morelia là dòng sản phẩm lâu đời nhất của Mizuno (ra mắt năm 1985), nhưng trải qua gần 30 năm hình thành và phát triển, Moreila đang không ngừng cải tiến từng ngày, từ thiết kế đến công nghệ nhằm bắt kịp xu thế hiện đại. Morelia III Beta cũng là dòng sản phẩm được Sergio Ramos làm đại diện trước khi chuyển qua Mizuno Alpha. Giữa thị trường giày bóng đá ngày càng đa dạng với nhiều thương hiệu đình đám, chúng ta vẫn phải công nhận những cải tiến và đột phá của mình đã giúp Mizuno có chỗ đứng vững chắc trong lòng người hâm mộ.Phiên bản Charge Pack - Với tone màu chủ đạo trắng đỏ cho bạn cảm giác đầy quyền lực, sẵn sàng bức phá và tự tin tỏa sáng trên sân. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0075',  3, 'GIÀY ĐÁ BÓNG MIZUNO MORELIA NEO IV PRO FG DYNA - SAFETY YELLOW/RED P1GA243445', 4,3050000 ,  'img/cotunhien/Mizuno/2.jpg', N'Mizuno Morelia Neo IV Pro FG DYNA - Safety Yellow/Red là mẫu giày chuyên cho sân cỏ tự nhiên 11 người. Mẫu giày này được các cầu thủ đánh giá rất cao vì sự toàn diện, bền bỉ và hỗ trợ các cầu thủ rất tốt.  Những cầu thủ có lối chơi thiên về tốc độ và mong muốn một đôi giày nhẹ, êm ái, cảm giác bóng tốt không thể bỏ qua Mizuno Morelia Neo IV Pro FG DYNA - Safety Yellow/Red.Về công nghệ của phiên bản Mizuno Morelia Neo IV Pro FG DYNA - Safety Yellow/Red:chân chịu áp lực lớn và tác động mạnh từ việc chạy, nhảy, điều này có thể gây tổn thương cơ và xương chân của bạn. Công nghệ giảm chấn cung cấp một lớp đệm mềm và êm ái dưới lòng bàn chân nhằm hấp thụ và phân tán lực đồng đều lên lớp đệm giúp giảm phản lực từ mặt sân tác động lên bàn chân, giảm thiểu áp lực lên gót chân và nguy cơ chấn thương chân, tạo cảm giác thoải mái cho bạn để có thể tập trung kiến tạo những pha bóng then chốt định đoạt trận đấu. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),

		/*giày tự nhiên*//*Joma*/
	
		/*giày tự nhiên*//*Asics*/
		('S0079',  3, 'GIÀY ĐÁ BÓNG ASICS DS LIGHT X-FLY 5 LIMITED - WHITE AQUARIUM 1101A050-110', 6,3483000 ,  'img/cotunhien/Asics/1.jpg', N'Asics có thể coi là nguồn cảm hứng , là khuôn mẫu tạo ra bước đi đầu tiên của đế chế Nike thương hiệu nổi tiếng đến từ Mỹ . Phil Knight là người đồng sáng lập , chủ tịch danh dự của đế chế NIKE . Ông muốn tìm hiểu xem có dòng giày nào có thể so sánh được với các đôi giày đến từ Đức đang rất được yêu thích trên thế giới thời điểm hiện tại . Trong một lần đi khảo sát và học hỏi đã bị bất ngờ trước chất lượng các đôi giày Asics đem lại . Ông xin được phân phối và sau đó là săn đón các kỹ sư thiết kế để tạo ta thương hiệu Nike của riêng mình . ASICS DS LIGHT X-FLY 5 LIMITED - White Aquarium là mẫu giày cao cấp cho bề mặt sân cỏ tự nhiên được các cầu thủ J-League sử dụng . Cầm trên tay ASICS DS LIGHT X-FLY 5 LIMITED - White Aquarium  bạn sẽ phải bất ngờ với trọng lượng nhẹ và thiết kế chất lượng chắc chắn. ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
		('S0080',  3, 'GIÀY ĐÁ BÓNG ASICS DS LIGHT WD FG - CRIMSON/BLACK 1103A074-700', 6,1690000 ,  'img/cotunhien/Asics/2.jpg', N'ASICS DS LIGHT WD FG - Crimson/Black là mẫu giày chuyên cho bề mặt sân cỏ tự nhiên . Cầm trên tay ASICS DS LIGHT WD sẽ phải bất ngờ với trọng lượng nhẹ và thiết kế chất lượng chắc chắn. Bề mặt chất liệu ASICS DS LIGHT là chất liệu da tổng hợp độc quyền cực kỳ êm ái và ôm chân. Nếu bạn đã từng sử dụng qua dòng Tiempo 7 phân khúc ELITE huyền thoại của Nike bạn sẽ cảm nhận được chỉ với mức giá tiền dưới 2 triệu đã được trải nghiệm cảm giác tương tự khi sử dụng ASICS DS LIGHT WD ', N'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen');
	
		/*giày tự nhiên*//*Kamito*/


CREATE TABLE #TempSizes (
    Size VARCHAR(10)
);
-- Thêm các kích thước vào bảng tạm thời
INSERT INTO #TempSizes (Size)
VALUES ('39'), ('40'), ('41'), ('42');
-- Tạo biến @Quantity để đặt số lượng sản phẩm cho mỗi kích thước
DECLARE @Quantity INT = 20;

-- Tạo câu lệnh INSERT INTO để thêm dữ liệu vào bảng Product_Size_Quantity cho mỗi sản phẩm và kích thước
INSERT INTO Product_Size_Quantity (product_id, size, quantity)
SELECT p.product_id, ts.Size, @Quantity
FROM Products p
CROSS JOIN #TempSizes ts;

-- Xóa bảng tạm thời
DROP TABLE #TempSizes;