/*
Created		17/11/2011
Modified		12/12/2011
Project		
Model		
Company		
Author		
Version		
Database		mySQL 5 
*/


drop table IF EXISTS Produto;
drop table IF EXISTS parcela_receber;
drop table IF EXISTS conta_receber;
drop table IF EXISTS parcela_pagar;
drop table IF EXISTS conta_pagar;
drop table IF EXISTS pessoa_fisica;
drop table IF EXISTS Pessoa_juridica;
drop table IF EXISTS endereco;
drop table IF EXISTS fornecedor;
drop table IF EXISTS cliente;
drop table IF EXISTS funcionario;


Create table funcionario (
	id_funcionario Int NOT NULL AUTO_INCREMENT,
	id_pessoa_fisica Int NOT NULL,
	ativo Varchar(100),
	data_admissao Varchar(100),
	carteira_trabalho Varchar(100),
	status Int DEFAULT 1,
	UNIQUE (id_funcionario),
 Primary Key (id_funcionario)) ENGINE = MyISAM;

Create table cliente (
	id_cliente Int NOT NULL AUTO_INCREMENT,
	id_pessoa_fisica Int,
	id_pessoa_juridica Int,
	status Int DEFAULT 1,
	UNIQUE (id_cliente),
 Primary Key (id_cliente)) ENGINE = MyISAM;

Create table fornecedor (
	id_fornecedor Int NOT NULL AUTO_INCREMENT,
	id_pessoa_juridica Int NOT NULL,
	status Int DEFAULT 1,
	UNIQUE (id_fornecedor),
 Primary Key (id_fornecedor)) ENGINE = MyISAM;

Create table endereco (
	id_endereco Int NOT NULL AUTO_INCREMENT,
	rua Varchar(100),
	numero Varchar(100),
	bairro Varchar(100),
	complemento Varchar(100),
	cidade Varchar(100),
	cep Varchar(100),
	uf Varchar(2),
	status Int,
	UNIQUE (id_endereco),
 Primary Key (id_endereco)) ENGINE = MyISAM;

Create table Pessoa_juridica (
	id_pessoa_juridica Int NOT NULL AUTO_INCREMENT,
	id_endereco Int NOT NULL,
	razao_social Varchar(100),
	nome_fantasia Varchar(100),
	cnpj Varchar(100),
	inscricao_estadual Varchar(100),
	telefone Varchar(100),
	celular Varchar(100),
	email Varchar(100),
	status Int DEFAULT 1,
	UNIQUE (id_pessoa_juridica),
 Primary Key (id_pessoa_juridica)) ENGINE = MyISAM;

Create table pessoa_fisica (
	id_pessoa_fisica Int NOT NULL AUTO_INCREMENT,
	id_endereco Int NOT NULL,
	nome Varchar(80),
	cpf Varchar(100),
	rg Varchar(100),
	genero Varchar(100),
	data_nascimento Date,
	telefone Varchar(100),
	celular Varchar(100),
	email Varchar(100),
	status Int DEFAULT 1,
	UNIQUE (id_pessoa_fisica),
 Primary Key (id_pessoa_fisica)) ENGINE = MyISAM;

Create table conta_pagar (
	id_conta_pagar Int NOT NULL AUTO_INCREMENT,
	id_fornecedor Int,
	id_funcionario Int,
	data_emissao Date,
	credor Varchar(100),
	num_dias Int,
	valor_total Decimal(10,2),
	num_parcelas Int,
	entrada Decimal(10,2),
	data_vencimento Date,
	situacao Varchar(30),
	documento Varchar(100),
	valor_pago Decimal(10,2),
	status Int DEFAULT 1,
	UNIQUE (id_conta_pagar),
 Primary Key (id_conta_pagar)) ENGINE = MyISAM;

Create table parcela_pagar (
	id_parcela_pagar Int NOT NULL AUTO_INCREMENT,
	id_conta_pagar Int NOT NULL,
	valor Decimal(10,2),
	data_vencimento Datetime,
	status Int DEFAULT 1,
	situacao Varchar(20),
	UNIQUE (id_parcela_pagar),
 Primary Key (id_parcela_pagar)) ENGINE = MyISAM;

Create table conta_receber (
	id_conta_receber Int NOT NULL AUTO_INCREMENT,
	id_cliente Int,
	data_emissao Date,
	num_dias Int,
	valor_total Decimal(10,2),
	num_parcelas Int,
	entrada Decimal(10,2),
	data_vencimento Date,
	situacao Varchar(30),
	documento Varchar(100),
	valor_pago Decimal(10,2),
	status Int DEFAULT 1,
	UNIQUE (id_conta_receber),
 Primary Key (id_conta_receber)) ENGINE = MyISAM;

Create table parcela_receber (
	id_parcela_receber Int NOT NULL AUTO_INCREMENT,
	valor Decimal(10,2),
	data_vencimento Datetime,
	desconto Decimal(10,2),
	acrescimo Decimal(10,2),
	status Int DEFAULT 1,
	situacao Varchar(20),
	id_conta_receber Int NOT NULL,
	UNIQUE (id_parcela_receber),
 Primary Key (id_parcela_receber)) ENGINE = MyISAM;

Create table Produto (
	id_produto Int NOT NULL AUTO_INCREMENT,
	id_fornecedor Int NOT NULL,
	descricao Varchar(200),
	codigo Varchar(200),
	estoque Bigint,
	quantidade_minima Int,
	preco_compra Decimal(10,2),
	preco_venda Decimal(10,2),
	unidade Varchar(200),
	status Int DEFAULT 1,
 Primary Key (id_produto)) ENGINE = MyISAM;


Alter table conta_pagar add Foreign Key (id_funcionario) references funcionario (id_funcionario) on delete  restrict on update  restrict;
Alter table conta_receber add Foreign Key (id_cliente) references cliente (id_cliente) on delete  restrict on update  restrict;
Alter table conta_pagar add Foreign Key (id_fornecedor) references fornecedor (id_fornecedor) on delete  restrict on update  restrict;
Alter table Produto add Foreign Key (id_fornecedor) references fornecedor (id_fornecedor) on delete  restrict on update  restrict;
Alter table pessoa_fisica add Foreign Key (id_endereco) references endereco (id_endereco) on delete  restrict on update  restrict;
Alter table Pessoa_juridica add Foreign Key (id_endereco) references endereco (id_endereco) on delete  restrict on update  restrict;
Alter table cliente add Foreign Key (id_pessoa_juridica) references Pessoa_juridica (id_pessoa_juridica) on delete  restrict on update  restrict;
Alter table fornecedor add Foreign Key (id_pessoa_juridica) references Pessoa_juridica (id_pessoa_juridica) on delete  restrict on update  restrict;
Alter table funcionario add Foreign Key (id_pessoa_fisica) references pessoa_fisica (id_pessoa_fisica) on delete  restrict on update  restrict;
Alter table cliente add Foreign Key (id_pessoa_fisica) references pessoa_fisica (id_pessoa_fisica) on delete  restrict on update  restrict;
Alter table parcela_pagar add Foreign Key (id_conta_pagar) references conta_pagar (id_conta_pagar) on delete  restrict on update  restrict;
Alter table parcela_receber add Foreign Key (id_conta_receber) references conta_receber (id_conta_receber) on delete  restrict on update  restrict;


