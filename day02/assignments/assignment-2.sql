CREATE TABLE categories (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50) NOT NULL,
	status VARCHAR(15) NOT NULL
);

CREATE TABLE country (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50) NOT NULL
);

CREATE TABLE state (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50)  NOT NULL,
	country_id INT FOREIGN KEY REFERENCES country(id)
);
 
CREATE TABLE City (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(50) NOT NULL,
	state_id INT FOREIGN KEY REFERENCES state(id)
);
 
CREATE TABLE area (
	zipcode INT PRIMARY KEY,
	name VARCHAR(50),
	city_id INT FOREIGN KEY REFERENCES city(id)
);

CREATE TABLE address (
	id INT PRIMARY KEY IDENTITY,
	door_number INT NOT NULL,
	addressline1 VARCHAR(100) NOT NULL,
	zipcode INT FOREIGN KEY REFERENCES area(zipcode)
);

CREATE TABLE supplier (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(100) NOT NULL,
	contact_person VARCHAR(100) NOT NULL,
	phone VARCHAR(100) NOT NULL,
	email VARCHAR(100),
	address_id INT FOREIGN KEY REFERENCES address(id),
	status VARCHAR(15) NOT NULL
);

CREATE TABLE product (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(100) NOT NULL,
	unit_price INT NOT NULL,
	quantity INT NOT NULL,
	description VARCHAR(500),
	image VARCHAR(100) 
);

CREATE TABLE product_supplier (
	transaction_id INT PRIMARY KEY IDENTITY,
	product_id INT FOREIGN KEY REFERENCES product(id),
	supplier_id INT FOREIGN KEY REFERENCES supplier(id),
	date_of_supply DATE NOT NULL,
	quantity INT NOT NULL
)

CREATE TABLE Customer (
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(100) NOT NULL,
	phone VARCHAR(12),
	age INT,
	address_id INT FOREIGN KEY REFERENCES address(id)
);

CREATE TABLE "order" (
	order_number INT  PRIMARY KEY IDENTITY,
	customer_id INT  FOREIGN KEY REFERENCES Customer(id),
	Date_of_order DATE  NOT NULL,
	amount INT  NOT NULL,
	order_status VARCHAR(15)  NOT NULL
);

CREATE TABLE order_details (
	id INT PRIMARY KEY IDENTITY,
	order_number INT FOREIGN KEY REFERENCES "order"(order_number),
	product_id INT FOREIGN KEY REFERENCES product(id),
	quantity INT NOT NULL,
	unit_price INT NOT NULL
)