CREATE TABLE PRODUCTS (
	ProductId integer primary key,
	ProductName varchar(50) not null
);



CREATE TABLE USER (
	UserId integer primary key,
	UserName nvarchar(50) not null,
	Password nvarchar(50) not null,
	Name nvarchar(50) 
);

CREATE TABLE TOKENS (
	TokenId integer primary key,
	AuthToken nvarchar(250) not null,
	IssuedOn datetime not null,
	ExpriresOn datetime not null,
	UserId integer references USER(UserId)
);