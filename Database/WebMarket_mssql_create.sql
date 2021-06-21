USE [master]
GO

IF DB_ID('WebMarket') IS NOT NULL DROP DATABASE [WebMarket] 
GO
CREATE DATABASE [WebMarket]
GO
USE WebMarket
GO


CREATE TABLE ['category'] (
	ID integer NOT NULL IDENTITY,
	name nvarchar(50) NOT NULL UNIQUE,
	image nvarchar(100)
  CONSTRAINT [PK_CATEGORY] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

CREATE TABLE [type] (
	ID integer NOT NULL IDENTITY,
	name nvarchar(50) NOT NULL UNIQUE,
	ID_category integer NOT NULL,
  CONSTRAINT [PK_TYPE] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [provider] (
	ID integer NOT NULL IDENTITY,
	name nvarchar(50) NOT NULL UNIQUE,
	address nvarchar(255) NULL,
	phone varchar(11) NULL,
  CONSTRAINT [PK_PROVIDER] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [product] (
	ID integer NOT NULL IDENTITY,
	name nvarchar(255) NOT NULL UNIQUE,
	price float,
	image nvarchar(255) NOT NULL,
	description nvarchar(max) ,
	ID_provider integer NULL,
	ID_type integer NOT NULL,
	discount float NOT NULL,
	quantity_stock integer DEFAULT 0,
	quantity_sold integer DEFAULT 0,
	status nvarchar(10) NOT NULL CHECK (status IN('Disable', 'Visible')) DEFAULT 'Visible',
  CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

ALTER TABLE [product]
ADD CONSTRAINT df_desc
DEFAULT 'Empty' FOR DESCRIPTION
GO

CREATE TABLE [productdetail] (
	ID integer NOT NULL IDENTITY,
	ID_warehouse integer,
	ID_product integer NOT NULL,
	quantity integer NOT NULL,
	entry_date DATETIME NOT NULL,
	MFG DATE NOT NULL,
	EXP DATE NOT NULL,
  CONSTRAINT [PK_PRODUCTDETAIL] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [admininfo] (
	ID integer NOT NULL IDENTITY,
	username varchar(50) NOT NULL UNIQUE,
	password varchar(50) NOT NULL,
	name nvarchar(50) NOT NULL,
	address nvarchar(255) NOT NULL,
	phone varchar(11) NOT NULL,
	type integer NOT NULL,
  CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [orderdetail] (
	ID integer NOT NULL IDENTITY,
	ID_order integer NOT NULL,
	ID_product integer NOT NULL,
	quantity float NOT NULL,
	discount float NOT NULL,
	newprice float,
  CONSTRAINT [PK_ORDERDETAIL] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [order] (
	ID integer NOT NULL IDENTITY,
	ID_customer integer NOT NULL,
	ID_admin integer NULL,
	order_date datetime NOT NULL,
	delivery_date datetime NULL,
	address nvarchar(255) NOT NULL,
	name nvarchar(50),
	phone varchar(11),
	payment_type varchar(50) NULL,
	shipping_type varchar(50),
	ship_cost float(50),
	status integer default 0,
	note nvarchar(50),
	total_price float,
	
  CONSTRAINT [PK_ORDER] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

CREATE TABLE [orderupdate](
	ID integer NOT NULL IDENTITY PRIMARY KEY CLUSTERED,
	ID_order integer NOT NULL,
	ID_admin integer NULL,
	old_status integer NULL,
	new_status integer NULL,
	date_update DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
	date_end DATETIME2 GENERATED ALWAYS AS ROW END  NOT NULL,
	PERIOD FOR SYSTEM_TIME (date_update, date_end)

)WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.orderhistory, DATA_CONSISTENCY_CHECK = ON));
GO

CREATE TABLE [customer] (
	ID integer NOT NULL ,
	name nvarchar(50) NOT NULL,
	address nvarchar(255),
	phone varchar(11) ,
	date_of_birth datetime,
	image nvarchar(255),
	email varchar(50) NOT NULL,
	status integer NOT NULL,
  CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [account] (
	ID integer NOT NULL IDENTITY,
	username varchar(50) NOT NULL UNIQUE,
	password varchar(50),
	type integer NOT NULL
  CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [priceupdate] (
	ID integer NOT NULL IDENTITY PRIMARY KEY CLUSTERED,
	ID_product integer NULL,
	ID_admin integer NOT NULL,
	price float NOT NULL,
	priceupdated float NOT NULL,
	date_update DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
	date_end DATETIME2 GENERATED ALWAYS AS ROW END  NOT NULL,
	PERIOD FOR SYSTEM_TIME (date_update, date_end)

)WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.pricehistory, DATA_CONSISTENCY_CHECK = ON));
GO

GO
CREATE TABLE [background](
		ID integer NOT NULL IDENTITY,
		name varchar(50),
		image nvarchar(100),
		description varchar(255) 
 )

ALTER TABLE [type] WITH CHECK ADD CONSTRAINT [type_fk0] FOREIGN KEY ([ID_category]) REFERENCES [category]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [type] CHECK CONSTRAINT [type_fk0]
GO


ALTER TABLE [product] WITH CHECK ADD CONSTRAINT [product_fk0] FOREIGN KEY ([ID_provider]) REFERENCES [provider]([ID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [product] CHECK CONSTRAINT [product_fk0]
GO
ALTER TABLE [product] WITH CHECK ADD CONSTRAINT [product_fk1] FOREIGN KEY ([ID_type]) REFERENCES [type]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [product] CHECK CONSTRAINT [product_fk1]
GO



ALTER TABLE [productdetail] WITH CHECK ADD CONSTRAINT [productdetail_fk0] FOREIGN KEY ([ID_product]) REFERENCES [product]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [productdetail] CHECK CONSTRAINT [productdetail_fk0]
GO



ALTER TABLE [orderdetail] WITH CHECK ADD CONSTRAINT [orderdetail_fk0] FOREIGN KEY ([ID_order]) REFERENCES [order]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [orderdetail] CHECK CONSTRAINT [orderdetail_fk0]
GO
ALTER TABLE [orderdetail] WITH CHECK ADD CONSTRAINT [orderdetail_fk1] FOREIGN KEY ([ID_product]) REFERENCES [product]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [orderdetail] CHECK CONSTRAINT [orderdetail_fk1]
GO





ALTER TABLE [order] WITH CHECK ADD CONSTRAINT [order_fk0] FOREIGN KEY ([ID_customer]) REFERENCES [customer]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [order] CHECK CONSTRAINT [order_fk0]
GO
ALTER TABLE [order] WITH CHECK ADD CONSTRAINT [order_fk1] FOREIGN KEY ([ID_admin]) REFERENCES [admininfo]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [order] CHECK CONSTRAINT [order_fk1]
GO


ALTER TABLE [customer] WITH CHECK ADD CONSTRAINT [customer_fk0] FOREIGN KEY ([ID]) REFERENCES [account]([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [customer] CHECK CONSTRAINT [customer_fk0]
GO



ALTER TABLE [priceupdate] WITH CHECK ADD CONSTRAINT [priceupdate_fk0] FOREIGN KEY ([ID_product]) REFERENCES [product]([ID])
--ON UPDATE CASCADE
GO
ALTER TABLE [priceupdate] CHECK CONSTRAINT [priceupdate_fk0]
GO

ALTER TABLE [priceupdate] WITH CHECK ADD CONSTRAINT [priceupdate_fk1] FOREIGN KEY ([ID_admin]) REFERENCES [admininfo]([ID])
--ON UPDATE CASCADE
GO
ALTER TABLE [priceupdate] CHECK CONSTRAINT [priceupdate_fk1]
GO

ALTER TABLE [orderupdate] WITH CHECK ADD CONSTRAINT [orderupdate_fk0] FOREIGN KEY ([ID_order]) REFERENCES [order]([ID])
--ON UPDATE CASCADE
GO
ALTER TABLE [orderupdate] CHECK CONSTRAINT [orderupdate_fk0]
GO

ALTER TABLE [orderupdate] WITH CHECK ADD CONSTRAINT [orderupdate_fk1] FOREIGN KEY ([ID_admin]) REFERENCES [admininfo]([ID])
--ON UPDATE CASCADE
GO
ALTER TABLE [orderupdate] CHECK CONSTRAINT [orderupdate_fk1]
GO