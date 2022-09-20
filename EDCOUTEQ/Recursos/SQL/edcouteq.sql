drop database edcouteq;

create database edcouteq;

use edcouteq;

create table rol(
id int primary key,
rolUser varchar(20))

insert into rol (id, rolUser) values (1, 'comun'),
									(2, 'admin');

select * from rol;

create table usuarios(
id int not null identity(1,1) primary key,
rol int not null default 1 foreign key references rol(id),
nombre varchar(50),
apellido varchar(50),
email varchar(255),
pass varchar(20)
);

select * from usuarios;

create table modalidad(
id int primary key,
modalidad varchar(20))

insert into modalidad(id, modalidad) values(1, 'presencial'),
											(2, 'en linea');

create table cursos(
id int not null identity(1,1) primary key,
nombre varchar(50),
modalidad int not null foreign key references modalidad(id),
lugar varchar(255),
horas int,
costo decimal(8,2),
costoPref decimal(8,2), 
urlTemario varchar(255),
requisitos varchar(100),
criterioEval varchar(100),
imgUrl varchar(255));

select * from cursos;

create proc SP_registraUsuario(
@Rol int,
@Nombre varchar(50),
@Apellido varchar(50),
@Email varchar(255),
@Pass varchar(20),
@Registrado bit output,
@Mensaje varchar(100) output)
as
begin
	if(not exists(select * from usuarios where email = @Email))
	begin
		insert into usuarios(rol, nombre, apellido, email, pass) values(@Rol, @Nombre, @Apellido, @Email, @Pass)
		set @Registrado = 1
		set @Mensaje = 'Usuario registrado'
	end
	else
	begin
		set @Registrado = 0
		set @Mensaje = 'Correo ya registrado'
	end
end


create proc SP_validaUsuario(
@Email varchar(255),
@Pass varchar(20))
as
begin
	if(exists(select * from usuarios where email = @Email and pass = @Pass))
		select id from usuarios where email = @Email and pass = @Pass
	else
		select '0'
end	

create proc SP_obtenUsuario(
@Email varchar(255),
@Pass varchar(20))
as
begin
	if(exists(select * from usuarios where email = @Email and pass = @Pass))
		select * from usuarios where email = @Email and pass = @Pass
	else
		select '0'
end	


create proc SP_registraCurso(
@Modalidad int,
@Nombre varchar(50),
@Lugar varchar(255),
@Horas int,
@Costo decimal(8,2),
@CostoPref decimal(8,2),
@UrlTemario varchar(255),
@Requisitos varchar(100),
@CriterioEval varchar(100),
@ImgUrl varchar(255),
@Registrado bit output,
@Mensaje varchar(100) output)
as
begin
	if(not exists(select * from cursos where nombre = @Nombre))
	begin
		insert into cursos(nombre, modalidad, lugar, horas, costo, costoPref, urlTemario, requisitos, criterioEval, imgUrl) values(@Nombre, @Modalidad, @Lugar, @Horas, @Costo, @CostoPref, @UrlTemario, @Requisitos, @CriterioEval, @ImgUrl)
		set @Registrado = 1
		set @Mensaje = 'Curso registrado'
	end
	else
	begin
		set @Registrado = 0
		set @Mensaje = 'Curso ya existe'
	end
end

create proc SP_editaCurso(
@Id int,
@Modalidad int,
@Nombre varchar(50),
@Lugar varchar(255),
@Horas int,
@Costo decimal(8,2),
@CostoPref decimal(8,2),
@UrlTemario varchar(255),
@Requisitos varchar(100),
@CriterioEval varchar(100),
@ImgUrl varchar(255),
@Editado bit output)
as
begin
	if(exists(select * from cursos where id = @Id))
	begin
		update cursos set modalidad = @Modalidad, nombre = @Nombre, lugar = @Lugar, horas = @Horas, costo = @Costo, costoPref = @CostoPref, urlTemario = @UrlTemario, requisitos = @Requisitos, criterioEval = @CriterioEval, imgUrl = @ImgUrl where id = @Id;
		set @Editado = 1
	end
	else
	begin
		select '0'
		set @Editado = 0
	end
end	

drop table usuarios;

drop table cursos;

drop proc SP_registraUsuario;

drop proc SP_validaUsuario;

drop proc SP_registraCurso;

drop proc SP_editaCurso;

declare @registrado bit, @mensaje varchar(100)

exec SP_registraUsuario 2, 'Jose', 'Martinez', 'admin@mail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', @registrado output, @mensaje output;

exec SP_validaUsuario 'admin@mail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3';

exec SP_obtenUsuario 'admin@mail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3';

declare @registrado bit, @mensaje varchar(100)

exec SP_registraCurso 2, 'Excel Avanzado', 'Google Meet', 35, 350.00, 300.00, 'https://www.excel-avanzado.com/', 'Excel Basico', 'Examen', '', @registrado output, @mensaje output; 

declare @Editado bit

exec SP_editaCurso 1, 1, 'Excel Avanzado', 'Google Meet', 35, 350.00, 300.00, 'https://www.excel-avanzado.com/', 'Excel Basico', 'Examen', '', @Editado output; 



select @registrado

select @mensaje

truncate table usuarios;


select usuarios.id, usuarios.nombre, usuarios.apellido, rol.rolUser, usuarios.email, usuarios.pass
from usuarios
inner join rol on rol.id = usuarios.rol;

