create database bd_avkode
default character set utf8
default collate utf8_general_ci;
drop database bd_avkode;

use bd_avkode;

drop table tbl_Cliente;

select * from tbl_Cliente;
select * from tbl_Usuario;

create table tbl_Cliente
(
id_Cliente int primary key auto_increment,
nome_Cli varchar(80) not null,
email_Cli varchar(80) not null,
cpf_Cli char(11) not null,
nasc_Cli date not null,
no_log_Cli char(5),
complemento_Cli varchar(60),
cep_Cli char(8),
fone_Cli varchar(13),
id_Usu int,
constraint foreign key(id_Usu) references tbl_Usuario(id_Usu)
)
default charset utf8;
alter table tbl_Cliente
drop column nm_log_Cli;

alter table tbl_Cliente
modify column nasc_Cli date;

create table tbl_Usuario
(
id_Usu int primary key auto_increment,
nome_Usu varchar(80) not null,
senha_Usu char(8) not null,
tipo_Usu char(1) not null
)
default charset utf8;
select * from tbl_Usuario;

insert into tbl_usuario values (default, 'admin', '12345678', 'A');

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

-- $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   STORED PROCEDURE   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

-- ##########################################   TABELA USUÁRIO   ##################################################################################
-- ##########################################   PROCEDURE INSERT USUÁRIO  #########################################################################
DROP PROCEDURE if exists pcd_insertUsuario;
DELIMITER $$   
CREATE PROCEDURE pcd_insertUsuario(              
_nomeUsu varchar(80),
_senhaUsu varchar(8),
_tipoUsu char(1)
)
	BEGIN
		START TRANSACTION;  
			INSERT INTO tbl_usuario(nome_Usu, senha_Usu, tipo_Usu)
			VALUES(_nomeUsu, _senhaUsu, _tipoUsu);    
	COMMIT;
		ROLLBACK;
END $$
    CALL pcd_insertUsuario("Carlão",12345678, "A");
    CALL pcd_insertUsuario("Victor",87654321, "B");
    
      /*##################################   PROCEDURE CONSULTAR USUÁRIO POR CODIGO   ##################################################################*/   
DROP PROCEDURE IF EXISTS pcd_Select_UsuarioPorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_UsuarioPorCod( _idUsu int )
BEGIN
	SELECT * FROM tbl_usuario WHERE  id_Usu = _idUsu;
END $$
call pcd_Select_UsuarioPorCod(2);


/*#####################################    PROCEDURE CONSULTAR USUÁRIO   ###########################################################################*/
DROP PROCEDURE IF EXISTS pcd_Select_Usuario;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_Usuario( )
BEGIN
	SELECT * FROM tbl_usuario;
END $$
call pcd_Select_Usuario();
  
-- ##########################################   PROCEDURE ATUALIZAR USUÁRIO   #####################################################################
drop PROCEDURE if exists sp_UpdateUsuario;
DELIMITER $$
CREATE PROCEDURE sp_UpdateUsuario(
in p_idUsu int,
in p_nome_Usu varchar(80),
in p_senha_Usu char(8),
in p_tipo_Usu char(1)
)
BEGIN 

 UPDATE tbl_usuario
    SET  id_Usu = p_idUsu, nome_Usu = p_nome_Usu, senha_Usu = p_senha_Usu, tipo_Usu = p_tipo_Usu
    WHERE id_Usu = p_idUsu;
    
END $$
DELIMITER ;

CALL sp_UpdateUsuario(1, "Carlão", "12345678", "A");
call pcd_Select_Usuario();

-- ####################################################   DELETAR USUÁRIO   #########################################################################--
DROP PROCEDURE IF EXISTS pcd_Deletar_Usuario;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Usuario( _idUsu int)
BEGIN
   DELETE FROM tbl_usuario WHERE id_Usu = _idUsu;
END $$

CALL pcd_Deletar_Usuario();
    
-- ##########################################   TABELA CLIENTE   ##################################################################################
-- ##########################################   PROCEDURE INSERT CLIENTE   ########################################################################
DROP PROCEDURE if exists pcd_insertcliente;
DELIMITER $$   
CREATE PROCEDURE pcd_insertcliente(              
_nome_Cli varchar(80),
_email_Cli varchar(80),
_cpf_Cli char(11),
_nasc_Cli varchar(10),
_nm_log_Cli varchar(80),
_no_log_Cli char(5),
_complemento_Cli varchar(60),
_cep_Cli char(8),
_fone_Cli varchar(13),
_id_Usu int
)
	BEGIN
		START TRANSACTION;  
			INSERT INTO tbl_cliente(nome_Cli, email_Cli, cpf_Cli, nasc_Cli, nm_log_Cli, no_log_Cli, complemento_Cli, cep_Cli, fone_Cli, id_Usu)
			VALUES(_nome_Cli, _email_Cli, _cpf_Cli, _nasc_Cli, _nm_log_Cli, _no_log_Cli, _complemento_Cli, _cep_Cli, _fone_Cli, _id_Usu);    
	COMMIT;
		ROLLBACK;
END $$
    CALL pcd_insertcliente("Nilson","nilson@gmail.com", 12345678912, "10/10/2010", "Rua Nova York", 123, "Apto 56", 09876543, "(11)2532-1196", 2);
    CALL pcd_insertcliente("Carlos Eduardo","carloseduardo@gmail.com", 12345678911, "10/10/1990", "Rua Jaguaré", 383, "Apto 73", 09876542, "(11)2532-1186");
    CALL pcd_insertcliente("Eduardo Pereira","eduardopereira@gmail.com", 12345656912, "10/09/2005", "Rua Princesa Isabel", 110, "Casa", 09876783, "(11)3432-1196");
    CALL pcd_insertcliente("Matheus Nascimento","matheus@gmail.com", 12345128912, "10/10/2004", "Rua Gil Eanes", 983, "Apto 45", 09986543, "(11)5432-1196");
    CALL pcd_insertcliente("Victor Massao","massao@gmail.com", 10945128912, "10/05/2003", "Rua Cristovão Colombo", 783, "Casa", 09986673, "(11)2345-1196");
    CALL pcd_insertcliente("Kayky da Silva","Kayky@gmail.com", 12349028912, "10/10/2006", "Avenida João Bosco", 783, "Apto 65", 09984563, "(11)9876-1196");
  
  insert 
  
 -- ##########################################   PROCEDURE INSERT CLIENTE/USUÁRIO   ###################################################################
 
drop procedure if exists sp_InserirCliUsu;
delimiter //
create procedure sp_InserirCliUsu(
	in _nomeUsu varchar(80),
    in _senhaUsu char(8),
    in _nomeCli varchar(80),
    in _emailCli varchar(80),
    in _cpfCli char(11),
    in _nascCli datetime,
    in _nologCLi char(5),
    in _complementoCli varchar(20),
    in _cepCli char(8),
    in _foneCli varchar(13)
)
begin
	declare vid int; -- criando variavel para receber id do cliente
    
     insert into tbl_Usuario
    (nome_Usu, senha_Usu, tipo_Usu)values(_nomeUsu,_senhaUsu, "C");
    
        set vid = last_insert_id();
    
    insert into tbl_Cliente
    (nome_Cli,email_CLi, cpf_Cli, nasc_Cli, no_log_Cli, complemento_Cli, cep_Cli, fone_Cli, id_Usu)
    values(_nomeCli, _emailCli, _cpfCli, _nascCli, _nologCli, _complementoCli, _cepCli, _foneCli, vid);
    
end //
delimiter ;

  /*##################################   PROCEDURE CONSULTAR CLIENTE POR CODIGO   ##################################################################*/   
DROP PROCEDURE IF EXISTS pcd_Select_ClientePorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_ClientePorCod( _idCli int )
BEGIN
	SELECT * FROM tbl_cliente WHERE  id_Cliente = _idCli;
END $$
call pcd_Select_ClientePorCod(4);

/*#####################################    PROCEDURE CONSULTAR CLIENTES   ###########################################################################*/
DROP PROCEDURE IF EXISTS pcd_Select_Cliente;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_Cliente( )
BEGIN
	SELECT * FROM tbl_cliente;
END $$
call pcd_Select_Cliente();

/*#####################################    PROCEDURE CONSULTAR CLIENTES   ###########################################################################*/
DROP PROCEDURE IF EXISTS pcd_Logar;
DELIMITER $$   
CREATE PROCEDURE pcd_Logar( 
	in p_usu varchar(80),
    in p_sen char(8))
	SELECT * FROM tbl_usuario where nome_Usu = p_usu and senha_Usu = p_sen;
call pcd_Logar('Kallex', '123456');
  
-- ##########################################   PROCEDURE ATUALIZAR CLIENTE   #####################################################################
drop PROCEDURE if exists sp_UpdateCliente;
DELIMITER $$
CREATE PROCEDURE sp_UpdateCliente(
in p_id_Usu int,
in p_nome_Cli varchar(50),
in p_email_Cli varchar(80),
in p_cpf_Cli char(11),
in p_nasc_Cli varchar(10),
in p_nm_log_Cli varchar(80),
in p_no_log_Cli char(5),
in p_complemento_Cli varchar(20),
in p_cep_Cli char(8),
in p_fone_Cli varchar(13),
in p_id_Cliente int
)
BEGIN

    UPDATE tbl_Cliente
    SET id_Usu = p_id_Usu, nome_Cli = p_nome_Cli, email_Cli = p_email_Cli, cpf_Cli = p_cpf_Cli, nasc_Cli = p_nasc_Cli, nm_log_Cli = p_nm_log_Cli, no_log_Cli = p_no_log_Cli,
    complemento_Cli = p_complemento_Cli, cep_Cli =  p_cep_Cli, fone_Cli = p_fone_Cli
    WHERE id_Cliente = p_id_Cliente;
    
END $$
DELIMITER ;

CALL sp_UpdateCliente(1, "Johanna","johanna@gmail.com", 12345678912, "10/10/2010", "Rua Nova York", 123, "Apto 56", 09876543, "(11)2532-1196", 10);

-- ####################################################   DELETAR CLIENTE   #########################################################################--
DROP PROCEDURE IF EXISTS pcd_Deletar_Cliente;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Cliente( _idCli int)
BEGIN
   DELETE FROM tbl_cliente WHERE id_Cliente = _idCli;
END $$

CALL PCD_DELETAR_CLIENTE(10);
call pcd_Select_Cliente();

-- ##########################################   TABELA PAGAMENTO   ##################################################################################
-- ##########################################   PROCEDURE PARA RECEBER FORMA DE PAGAMENTO   #########################################################
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

-- ##########################################   PROCEDURE PARA MOSTRAR FORMAS DE PAGAMENTO   #########################################################

create procedure spMostrarPgto()
select * from tbl_Pagamento;

call spMostrarPgto();
/*###########################################   PROCEDURE PARA ALTERAR PAGAMENTO   ##################################################################*/
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

/*##############################################   PROCEDURE INSERT PRODUTO   #################################################################################*/
DROP PROCEDURE if exists pcd_insertProduto;
DELIMITER $$   
CREATE PROCEDURE pcd_insertProduto(              
_idCategoria int,
_nmProduto varchar(40),
_dsProduto varchar(150),
_vlProduto decimal(10,2),
_stProduto bit(1),
_urlProduto varchar(254)
)
	BEGIN
		START TRANSACTION;  
			INSERT INTO tbl_Produto(id_Categoria, nm_Produto, ds_Produto, vl_Produto, st_Produto, url_Produto)
			VALUES(_idCategoria, _nmProduto, _dsProduto, _vlProduto, _stProduto, _urlProduto);    
	COMMIT;
		ROLLBACK;
END $$

CALL(1, "", "", , , "");

/*###########################################   PROCEDURE PESQUISA PELO PRODUTO POR CÓDIGO  #########################################################*/
DROP PROCEDURE IF EXISTS pcd_Select_ProdutoPorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_ProdutoPorCod( _idProduto int )
BEGIN
	SELECT * FROM tbl_Produto WHERE  id_Produto = _idProduto;
END $$
call pcd_Select_ProdutoPorCod(4);
/*###########################################   PROCEDURE CONSULTA LISTA PRODUTO   ###################################################################*/
DROP PROCEDURE IF EXISTS sp_BuscarProduto;
DELIMITER $$   
CREATE PROCEDURE sp_BuscarProduto( )
BEGIN
	SELECT * FROM tbl_produto;
END $$

CALL sp_BuscarProduto();

/*###########################################   PROCEDURE ALTERAR PRODUTO   ##############################################################*/
drop procedure if exists sp_AltProduto;
delimiter $$
create procedure sp_AltProduto(
in _idCategoria int,
in _nmProduto varchar(40),
in _dsProduto varchar(150),
in _vlProduto decimal(10,2),
in _stProduto bit(1),
in _urlProduto varchar(254),
in _idProduto int
)
begin 

update tbl_Produto
set id_Categoria = _idCategoria, nm_Produto = _nmProduto, ds_Produto = _dsProduto, vl_Produto = _vlProduto, 
st_Produto = _stProduto, url_Produto = urlProduto where id_Produto = p_idProduto;

end $$
delimiter ;

/*###########################################   PROCEDURE DELETAR PRODUTO   ##############################################################*/
DROP PROCEDURE IF EXISTS pcd_Deletar_Produto;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Produto( _idProduto int)
BEGIN
   DELETE FROM tbl_Produto WHERE id_Produto = _idProduto;
END $$

CALL pcd_Deletar_Produto(3);

/*###########################################   TABELA CATEGORIA  ##############################################################*/
/*###########################################   PROCEDURE INSERT CATEGORIA   ##############################################################*/
DROP PROCEDURE if exists pcd_insertCategoria;
DELIMITER $$   
CREATE PROCEDURE pcd_insertCategoria(              
_nmCategoria varchar(30),
_dsCategoria varchar(100)
)
	BEGIN
		START TRANSACTION;  
			INSERT INTO tbl_Categoria(nm_Categoria, ds_Categoria)
			VALUES(_nmCategoria, _dsCategoria);    
	COMMIT;
		ROLLBACK;
END $$
    CALL pcd_insertCategoria("Retrô","O estilo retro traz um pouco de romantismo ao design, causado pelo próprio sentimento das pessoas que tendem a sentir que a vida era muito mais simples, menos estressante e relaxada na época da escola ou faculdade. Este é, provavelmente, o lugar onde o design retro nos leva: a nostalgia.");
    CALL pcd_insertCategoria("Gamer","O estilo gamer é bem colorido e cheio de referências. São inúmeras inspirações para a decoração: papéis de parede e roupas de cama temáticas, almofadas personalizadas, coleções de miniaturas dos personagens, iluminação diferenciada, e até mesmo métodos artesanais. É possível, inclusive, unir referências de jogos diferentes. ");
    CALL pcd_insertCategoria("Vintage","O estilo vintage, caracterizado pelo resgate de peças de mobiliário originais que não sofreram modificações e modernizações. Esse tipo de decoração é perfeito para quem gosta de delicadeza, tons sóbrios e aquele toque “antiguinho”. A especialista afirma que móveis, lustres e produtos das décadas de 50, 60, 70 podem ser contrastados com ambientes modernos, clean, arrojados, e compor espaços únicos, cheios de charme e história.");
    CALL pcd_insertCategoria("Rock","O estilo rock na decoração é utilizado da mesma forma que na moda, sempre aplicado para demonstrar atitude, personalidade forte e a grande preferência por este ritmo da música. Quem sonha em montar cômodos repletos de referências ao estilo rock rock’n’roll precisa apenas investir em acessórios certos, peças-chave e ter disposição para garimpar bons e excelentes itens que reflitam um pouco desse movimento.");