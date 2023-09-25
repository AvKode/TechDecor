create database bd_avkode
default character set utf8
default collate utf8_general_ci;

use bd_avkode;

create table tbl_Cliente
(
id_Cliente int primary key auto_increment,
nome_Cli varchar(80) not null,
email_Cli varchar(80) not null,
cpf_Cli char(11) not null,
nasc_Cli date not null,
nm_log_Cli varchar(80),
no_log_Cli char(5),
complemento_Cli varchar(60),
cep_Cli char(8),
fone_Cli varchar(13),
id_Usu int,
constraint foreign key(id_Usu) references tbl_Usuario(id_Usu)
)
default charset utf8;

create table tbl_Usuario
(
id_Usu int primary key auto_increment,
nome_Usu varchar(80) not null,
senha_Usu char(8) not null,
tipo_Usu char(1) not null
)
default charset utf8;

create table tbl_Categoria
(
id_Categoria int primary key auto_increment,
nm_Categoria varchar(30) not null,
ds_Categoria varchar(100) not null
)
default charset utf8;


create table tbl_Produto
(
id_Produto int primary key auto_increment,
id_Categoria int,
nm_Produto varchar(40) not null,
ds_Produto varchar(150) not null,
vl_Produto decimal(10,2) not null,
st_Produto bit(1) not null,
url_Produto varchar(254) not null,
constraint foreign key(id_Categoria) references tbl_Categoria(id_Categoria)
)
default charset utf8;


create table tbl_Plano
(
id_Plano int primary key auto_increment,
id_Produto int,
nm_Planos varchar(30) not null,
ds_Planos varchar(100) not null,
url_Produto varchar(254) not null,
constraint foreign key(id_Produto) references tbl_Produto(id_Produto)
)
default charset utf8;


create table tbl_Carrinho
(
id_Carrinho int primary key auto_increment,
id_Cliente int,
id_Pagamento int,
vl_Total decimal(10,2) not null,
data_Compra date not null,
prazo_Servico date not null,
constraint foreign key(id_Cliente) references tbl_Cliente(id_Cliente),
constraint foreign key(id_Pagamento) references tbl_Pagamento(id_Pagamento)
)
default charset utf8;


create table tbl_Pagamento(
id_Pagamento int primary key auto_increment,
ds_Pagamento varchar(50) not null
)
default charset utf8;


create procedure sp_insPagamento(
in p_ds_Pagamento varchar(50) -- parametro que a procedure vai receber
)
insert into tbl_Pagamento(ds_Pagamento) -- declarações
values(p_ds_Pagamento);

-- executando a stored procedures
call sp_insPagamento('Pix'); -- chamando a proc sp_insPgto
call sp_insPagamento('Cartão de Débito'); -- chamando a proc sp_insPgto
call sp_insPagamento('Boleto Bancário'); -- chamando a proc sp_insPgto
call sp_insPagamento('Cartão Crédito'); -- chamando a proc sp_insPgto

create procedure spMostrarPgto()
select * from tbl_Pagamento;

call spMostrarPgto();

-- procedure update
drop PROCEDURE if exists sp_UpdateCliente;
DELIMITER $$
CREATE PROCEDURE sp_UpdateCliUsu(
id_Usu int,
in p_nome_Usu varchar(80),
in p_senha_Usu char(8),
in p_tipo_Usu char(1),
in p_id_Cliente int,
in p_nome_Cli varchar(50),
in p_email_Cli varchar(80),
in p_cpf_Cli char(11),
in p_nasc_Cli date ,
in p_nm_log_Cli varchar(80),
in p_no_log_Cli char(5),
in p_complemento_Cli varchar(20),
in p_cep_Cli char(8),
in p_fone_Cli varchar(13)
)
BEGIN
 UPDATE tbl_Usuario
    SET nome_Usu = p_nome_Usu, senha_Usu = p_senha_Usu, tipo_Usu = p_tipo_Usu
    WHERE id_Usu = p_id_Usu;
    
    UPDATE tbl_Cliente
    SET nome_Cli = p_nome_Cli, email_Cli = p_email_Cli, cpf_Cli = p_cpf_Cli, nasc_Cli = p_nasc_Cli, nm_log_Cli = p_nm_log_Cli, no_log_Cli = p_no_log_Cli,
    complemento_Cli = p_complemento_Cli, cep_Cli =  p_cep_Cli, fone_Cli = p_fone_Cli
    WHERE id_Cliente = p_id_Cliente;
    
END $$
DELIMITER ;

CALL sp_UpdateCliUsu(1, "Carlos Eduardo", "12345678", "A", "1", "Carlos", "carlos@gmail.com", "12345678912", "10/10/2010", "Rua Americo Brasiliense", "123", "Bloco A apto 2025", 05678120, "+5511989998999");

-- excluir pelo id

drop PROCEDURE if exists sp_DeleteCliUsu;
DELIMITER $$
CREATE PROCEDURE sp_DeleteCliUsu(
    IN p_id_Cliente int,
    IN p_id_Usu int
)
BEGIN
    DELETE FROM tbl_Cliente
    WHERE id_Cliente = p_ClientId;

    DELETE FROM tbl_Usuario
    WHERE id_Usu = p_id_Usu;
END $$
DELIMITER ;

CALL sp_DeleteCliUsu(1); 

drop procedure if exists sp_AltPagamento;
delimiter $$
create procedure sp_AltPagamento(
in p_dsPagamento varchar(50),
in p_idPagamento int
)
begin
update tbl_Pagamento
set ds_Pagamento = p_dsPagamento where id_Pagamento = p_idPagamento;
end $$
delimiter ;

call sp_AltPagamento('Cartão de Crédito', 4); -- executando a alteração na tbl_Pagamento

-- Procedure pesquisa pelo produto
DELIMITER $$
CREATE PROCEDURE sp_BuscarProduto(
    IN p_BuscarProduto VARCHAR(50)
)
BEGIN
    SELECT *
    FROM tbl_Produto
    WHERE nm_Produto LIKE p_BuscarProduto;

END $$
DELIMITER ;



