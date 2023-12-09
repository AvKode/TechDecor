CREATE DATABASE bd_avkode
DEFAULT CHARACTER SET utf8
DEFAULT COLLATE utf8_general_ci;

USE bd_avkode;

-- ####################################################################################################################################################
-- #############################################     TABELA USUÁRIO     ###############################################################################
-- ####################################################################################################################################################

create table tbl_Usuario
(
id_Usu int primary key auto_increment,
nome_Usu varchar(80) not null,
senha_Usu char(8) not null,
tipo_Usu char(1) not null
)
default charset utf8;

-- ####################################################################################################################################################
-- #############################################     TABELA CLIENTE     ###############################################################################
-- ####################################################################################################################################################

CREATE TABLE tbl_Cliente
(
id_Cliente int primary key auto_increment,
nome_Cli varchar(80) not null,
email_Cli varchar(80) not null,
cpf_Cli char(11) not null,
nasc_Cli varchar(10) not null,
no_log_Cli char(5),
complemento_Cli varchar(60),
cep_Cli char(8),
fone_Cli varchar(13),
id_Usu int,
CONSTRAINT FOREIGN KEY(id_Usu) REFERENCES tbl_Usuario(id_Usu)
)
DEFAULT CHARSET utf8;

-- ####################################################################################################################################################
-- #############################################     TABELA CATEGORIA     #############################################################################
-- ####################################################################################################################################################

create table tbl_Categoria
(
id_Categoria int primary key auto_increment,
nm_Categoria varchar(30) not null,
ds_Categoria varchar(500) not null
)
default charset utf8;

-- ####################################################################################################################################################
-- #############################################     TABELA PRODUTO     ###############################################################################
-- ####################################################################################################################################################

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

-- ####################################################################################################################################################
-- #############################################     TABELA PAGAMENTO     #############################################################################
-- ####################################################################################################################################################

create table tbl_Pagamento(
id_Pagamento int primary key auto_increment,
ds_Pagamento varchar(50) not null
)
default charset utf8;

-- ####################################################################################################################################################
-- #############################################     TABELA PLANO     #################################################################################
-- ####################################################################################################################################################

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

-- ####################################################################################################################################################
-- #############################################     TABELA CARRINHO     ##############################################################################
-- ####################################################################################################################################################

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

-- ####################################################################################################################################################
-- #############################################     PROCEDURES TABELA USUÁRIO     ##############################################################################
-- ####################################################################################################################################################

-- ##########################################     PROCEDURE INSERT USUÁRIO    #####################################################################

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
 
 -- ##################################     PROCEDURE CONSULTAR USUÁRIO POR CODIGO     ################################################################
   
DROP PROCEDURE IF EXISTS pcd_Select_UsuarioPorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_UsuarioPorCod( _idUsu int )
BEGIN
	SELECT * FROM tbl_usuario WHERE  id_Usu = _idUsu;
END $$
DELIMITER $$

-- ##################################      PROCEDURE CONSULTAR USUÁRIO POR NOME      ##################################################################
  
DROP PROCEDURE IF EXISTS pcd_Select_UsuarioPorNome;
DELIMITER $$
CREATE PROCEDURE pcd_Select_UsuarioPorNome(
    IN _nomeUsu VARCHAR(80)
)
BEGIN
    SELECT *
    FROM tbl_usuario
    WHERE nome_Usu LIKE _nomeUsu;

END $$
DELIMITER ;

-- #####################################     PROCEDURE CONSULTAR USUÁRIO POR TIPO   ###################################################################
   
DROP PROCEDURE IF EXISTS pcd_Select_UsuarioPorTipo;
DELIMITER $$
    
CREATE PROCEDURE pcd_Select_UsuarioPorTipo(_tipo char(1))
    
    BEGIN
    
    SELECT * FROM tbl_usuario WHERE tipo_Usu = _tipo;
    
    END $$
    
  DELIMITER ;

-- #########################################    PROCEDURE LISTAR USUÁRIO   ############################################################################

DROP PROCEDURE IF EXISTS pcd_Select_Usuario;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_Usuario( )
BEGIN
	SELECT * FROM tbl_usuario;
END $$
  
-- ##########################################   PROCEDURE EDITAR USUÁRIO   ############################################################################

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

-- ###############################################   DELETAR USUÁRIO   ################################################################################

DROP PROCEDURE IF EXISTS pcd_Deletar_Usuario;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Usuario( _idUsu int)
BEGIN
   DELETE FROM tbl_usuario WHERE id_Usu = _idUsu;
END $$

-- ####################################################################################################################################################
-- #############################################     PROCEDURES TABELA CLIENTE     ##############################################################################
-- ####################################################################################################################################################

-- ##########################################   PROCEDURE INSERT CLIENTE   ########################################################################

DROP PROCEDURE IF EXISTS pcd_insertcliente;
DELIMITER $$   
CREATE PROCEDURE pcd_insertcliente(              
_nome_Cli VARCHAR(80),
_email_Cli VARCHAR(80),
_cpf_Cli CHAR(11),
_nasc_Cli DATE,
_no_log_Cli CHAR(5),
_complemento_Cli VARCHAR(60),
_cep_Cli CHAR(8),
_fone_Cli VARCHAR(13),
_id_Usu INT
)
	BEGIN
		START TRANSACTION;  
			INSERT INTO tbl_cliente(nome_Cli, email_Cli, cpf_Cli, nasc_Cli, no_log_Cli, complemento_Cli, cep_Cli, fone_Cli, id_Usu)
			VALUES(_nome_Cli, _email_Cli, _cpf_Cli, _nasc_Cli, _no_log_Cli, _complemento_Cli, _cep_Cli, _fone_Cli, _id_Usu);    
	COMMIT;
		ROLLBACK;
END $$
 
-- ##########################################   PROCEDURE INSERT CLIENTE/USUÁRIO   ###################################################################
 
DROP PROCEDURE IF EXISTS sp_InserirCliUsu;
delimiter //
CREATE PROCEDURE sp_InserirCliUsu(
	IN _nomeUsu VARCHAR(80),
    IN _senhaUsu CHAR(8),
    IN _nomeCli VARCHAR(80),
    IN _emailCli VARCHAR(80),
    IN _cpfCli CHAR(11),
    IN _nascCli DATE,
    IN _nologCLi CHAR(5),
    IN _complementoCli VARCHAR(20),
    IN _cepCli CHAR(8),
    IN _foneCli VARCHAR(13)
)
BEGIN
	DECLARE vid INT; -- criando variavel para receber id do cliente
    
     INSERT INTO tbl_Usuario
    (nome_Usu, senha_Usu, tipo_Usu)values(_nomeUsu,_senhaUsu, "C");
    
        SET vid = last_insert_id();
    
    INSERT INTO tbl_Cliente
    (nome_Cli,email_CLi, cpf_Cli, nasc_Cli, no_log_Cli, complemento_Cli, cep_Cli, fone_Cli, id_Usu)
    VALUES(_nomeCli, _emailCli, _cpfCli, _nascCli, _nologCli, _complementoCli, _cepCli, _foneCli, vid);
    
END //
delimiter ;

-- ########################################     PROCEDURE CONSULTAR CLIENTE POR CODIGO   ####################################################################   

DROP PROCEDURE IF EXISTS pcd_Select_ClientePorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_ClientePorCod( _idCli int )
BEGIN
	SELECT * FROM tbl_cliente WHERE  id_Cliente = _idCli;
END $$

-- #########################################    PROCEDURE CONSULTAR CLIENTE POR NOME   ######################################################################  

DROP PROCEDURE IF EXISTS pcd_Select_ClientePorNome;
DELIMITER $$
CREATE PROCEDURE pcd_Select_ClientePorNome(
    IN _nomeCli VARCHAR(80)
)
BEGIN
    SELECT *
    FROM tbl_Cliente
    WHERE nome_Cli LIKE _nomeCli;

END $$
DELIMITER ;

-- ############################################    PROCEDURE CONSULTAR LISTA CLIENTES   ###############################################################

DROP PROCEDURE IF EXISTS pcd_Select_Cliente;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_Cliente( )
BEGIN
	SELECT * FROM tbl_cliente;
END $$

-- ##########################################      PROCEDURE EDITAR CLIENTE   ############################################################################

DROP PROCEDURE IF EXISTS sp_UpdateCliente;
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

-- ################################################   DELETAR CLIENTE   ###########################################################################

DROP PROCEDURE IF EXISTS pcd_Deletar_Cliente;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Cliente( _idCli int, _idUsu int)
BEGIN
   DELETE FROM tbl_cliente WHERE id_Cliente and id_Usu = _idCli;
END $$

DROP PROCEDURE IF EXISTS pcd_Logar;
DELIMITER $$   
CREATE PROCEDURE pcd_Logar( 
	in p_usu varchar(80),
    in p_sen char(8))
	SELECT * FROM tbl_usuario where nome_Usu = p_usu and senha_Usu = p_sen;

-- ####################################################################################################################################################
-- #########################################     PROCEDURES TABELA CATEGORIA     ##################################################################
-- ####################################################################################################################################################

-- ###########################################   PROCEDURE INSERT CATEGORIA   #########################################################################

DROP PROCEDURE if exists pcd_insertCategoria;
DELIMITER $$   
CREATE PROCEDURE pcd_insertCategoria(              
_nmCategoria varchar(30),
_dsCategoria varchar(500)
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
    
-- ###########################################   PROCEDURE PESQUISA CATEGORIA POR CÓDIGO  #############################################################

DROP PROCEDURE IF EXISTS pcd_Select_CategoriaPorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_CategoriaPorCod( _idCategoria int )
BEGIN
	SELECT * FROM tbl_Categoria WHERE  id_Categoria = _idCategoria;
END $$

-- ##########################################    PROCEDURE CONSULTAR CATEGORIA POR NOME   #############################################################
   
DROP PROCEDURE IF EXISTS pcd_Select_CategoriaPorNome;
DELIMITER $$
CREATE PROCEDURE pcd_Select_CategoriaPorNome(
    IN _nmCategoria VARCHAR(30)
)
BEGIN
    SELECT *
    FROM tbl_Categoria
    WHERE nm_Categoria LIKE _nmCategoria;

END $$
DELIMITER ;

-- ###########################################   PROCEDURE LISTAR CATEGORIA   ########################################################################

DROP PROCEDURE IF EXISTS sp_ListarCategoria;
DELIMITER $$   
CREATE PROCEDURE sp_ListarCategoria( )
BEGIN
	SELECT * FROM tbl_Categoria;
END $$

-- ###########################################   PROCEDURE EDITAR CATEGORIA   #########################################################################

drop procedure if exists sp_AltCategoria;
delimiter $$
create procedure sp_AltCategoria(
in _nmCategoria varchar(30),
in _dsCategoria varchar(500),
in _idCategoria int
)
begin 

update tbl_Categoria
set nm_Categoria = _nmCategoria, ds_Categoria = _dsCategoria where id_Categoria = _idCategoria;

end $$
delimiter ;

-- ###########################################   PROCEDURE DELETAR CATEGORIA   ########################################################################

DROP PROCEDURE IF EXISTS pcd_Deletar_Categoria;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Categoria( _idCategoria int)
BEGIN
   DELETE FROM tbl_Categoria WHERE id_Categoria = _idCategoria;
END $$

-- ####################################################################################################################################################
-- #############################################     PROCEDURES TABELA PRODUTO     ##############################################################################
-- ####################################################################################################################################################

-- ##############################################   PROCEDURE INSERT PRODUTO   ########################################################################

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


 -- ##########################################   PROCEDURE INSERT PRODUTO/CATEGORIA   #################################################################
 
drop procedure if exists sp_InserirProCat;
delimiter //
create procedure sp_InserirProCat(
    in _idCategoria int,
    in _nmProduto varchar(40),
    in _dsProduto varchar(150),
    in _vlProduto decimal(10,2),
    in _stProduto bit,
    in _urlProduto varchar(254)
)
begin
    
    insert into tbl_produto
    (id_Categoria, nm_Produto, ds_Produto, vl_Produto, st_Produto, url_Produto)
    values(_idCategoria, _nmProduto, _dsProduto, _vlProduto, _stProduto, _urlProduto);
    
end //
delimiter ;


-- ########################################   PROCEDURE PESQUISA PELO PRODUTO POR CÓDIGO  #############################################################

DROP PROCEDURE IF EXISTS pcd_Select_ProdutoPorCod;
DELIMITER $$   
CREATE PROCEDURE pcd_Select_ProdutoPorCod( _idProduto int )
BEGIN
	SELECT * FROM tbl_Produto WHERE  id_Produto = _idProduto;
END $$


-- ########################################   PROCEDURE CONSULTAR PRODUTO POR NOME   ##################################################################
  
DROP PROCEDURE IF EXISTS pcd_Select_ProdutoPorNome;
DELIMITER $$
CREATE PROCEDURE pcd_Select_ProdutoPorNome(
    IN _nmProduto VARCHAR(40)
)
BEGIN
    SELECT *
    FROM tbl_produto
    WHERE nm_Produto LIKE concat('%', '', '%') ;

END $$
DELIMITER ;


-- ###########################################   PROCEDURE LISTAR PRODUTO   ############################################################################

DROP PROCEDURE IF EXISTS sp_BuscarProduto;
DELIMITER $$   
CREATE PROCEDURE sp_BuscarProduto( )
BEGIN
	SELECT * FROM tbl_produto;
END $$

-- ###########################################   PROCEDURE EDITAR PRODUTO   ##########################################################################

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

-- ###########################################   PROCEDURE DELETAR PRODUTO   ##########################################################################

DROP PROCEDURE IF EXISTS pcd_Deletar_Produto;
DELIMITER $$
CREATE  PROCEDURE pcd_Deletar_Produto( _idProduto int)
BEGIN
   DELETE FROM tbl_Produto WHERE id_Produto = _idProduto;
END $$

-- ####################################################################################################################################################
-- #############################################     PROCEDURES TABELA PAGAMENTO     ##############################################################################
-- ####################################################################################################################################################

-- ##########################################   PROCEDURE PARA RECEBER FORMA DE PAGAMENTO   ###########################################################

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

-- ###########################################   PROCEDURE PARA ALTERAR PAGAMENTO   ##################################################################

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