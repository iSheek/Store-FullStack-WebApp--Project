create table if not exists categories(
	id int not null primary key,
	name varchar(30)
);

create table if not exists products_images(
	id int not null primary key,
	image_path varchar(50)
);

create table if not exists products(
	id int not null primary key,
	name varchar(50),
	price decimal(18,2),
	category_id int,
	image_id int,
	foreign key (image_id) references products_images(id),
	foreign key (category_id) references categories(id)
);



