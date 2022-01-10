create database QLBHMT
go 

use QLBHMT
go 

create table Quyen
(
	IDQuyen int IDENTITY(1,1) primary key,
	TenQuyen nvarchar(20) NULL
)
go

create table NguoiDung
(
	MaNguoiDung int IDENTITY(1,1) primary key,
	HoTen nvarchar(50) NULL, 
	Email varchar(100) NOT NULL, 
	MatKhau varchar(50) NOT NULL,
	SDT char(11) NULL, 
	DiaChi nvarchar(100) NULL,
	IDQuyen int NOT NULL,
	foreign key (IDQuyen) references dbo.Quyen(IDQuyen)
)
go

create table HangSanXuat
(
	MaHSX int IDENTITY(1,1) primary key,
	TenHSX varchar(10) NULL,
)
go

create table HeDieuHanh
(
	MaHDH int IDENTITY(1,1) primary key,
	TenHDH nvarchar(20) NULL,
)
go

create table SanPham
(
	MaSP int IDENTITY(1,1) primary key,
	TenSP nvarchar(50) not null,
	SoLuong int not null, 
	DonGiaNhap float not null, 
	DonGiaBan float not null, 
	HinhAnh nvarchar(50) null, 
	ManHinh nvarchar(50) null,
	CPU varchar(50) null,
	Ram varchar(50) null,
	DoHoa varchar(50) null,
	OCung varchar(50) null,
	Pin varchar(50) null,
	XuatXu nvarchar(30) null,
	NamRaMat int null,
	MaHSX int not null,
	MaHDH int not null,
	foreign key (MaHDH) references dbo.HeDieuHanh(MaHDH),
	foreign key (MaHSX) references dbo.HangSanXuat(MaHSX)
)
go

create table DonDatHang
(
	MaDDH int IDENTITY(1,1) primary key,
	NgayDat datetime null, 
	NgayGiao datetime null,
	TinhTrang int null,
	DaThanhToan bit null,
	MaNguoiDung int not null,
	foreign key (MaNguoiDung) references dbo.NguoiDung(MaNguoiDung)
)
go

create table ChiTietDonHang
(
	MaDDH int not null,
	MaSP int not null,
	SoLuong int not null,
	DonGia float not null,
	ThanhTien float null,
	primary key (MaDDH, MaSP),
	foreign key (MaDDH) references dbo.DonDatHang(MaDDH),
	foreign key (MaSP) references dbo.SanPham(MaSP)
)
go