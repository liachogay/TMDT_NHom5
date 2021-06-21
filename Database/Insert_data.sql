USE [WebMarket]
GO

INSERT INTO [category] ([name],[image]) VALUES
    ('Groceries','/images/p2.jpg'),
    ('Household','/images/p3.jpg'),
    ('Personal Care','/images/p4.jpg'),
    ('Package Foods','/images/111.jpg')
GO
INSERT INTO [type] ([name],[ID_category]) VALUES
    (N'Dầu ăn - Gia vị - Đồ khô',1),
    (N'Đồ đông lạnh/mát',1),
    (N'Sữa - Sản phẩm từ sữa',1),
    (N'Hóa phẩm - Giấy',2),
    (N'Đồ dùng gia đình',2),
    (N'Chăm sóc cá nhân',3),
    (N'Chăm sóc cho bé',3),
    (N'Bánh kẹo - Đồ ăn vặt',4),
    (N'Đồ uống - Giải khát',4)
GO
INSERT INTO [background] ([name],[image],[description]) VALUES 
('BG1','images/img-banner/11.jpg','Buy Rice Products Are Now On Line With Us'),
('BG2','images/img-banner/22.jpg','Whole Spices Products Are Now On Line With Us'),
('BG3','images/img-banner/44.jpg','Whole Spices Products Are Now On Line With Us')
GO
INSERT INTO [admininfo]([username],[password],[name],[address],[phone],[type]) values ('admin','admin','ADMIN','HCM','0123456789',1)
GO

Select * From Productdetail
