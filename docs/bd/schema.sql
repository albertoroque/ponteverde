SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

DROP SCHEMA IF EXISTS `ponteverde` ;
CREATE SCHEMA IF NOT EXISTS `ponteverde` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `ponteverde` ;

-- -----------------------------------------------------
-- Table `Usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Usuario` ;

CREATE  TABLE IF NOT EXISTS `Usuario` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `username` VARCHAR(45) NOT NULL ,
  `password` VARCHAR(45) NOT NULL ,
  `email` VARCHAR(45) NULL ,
  `tipo` ENUM('1', '2') NULL COMMENT '1 - Cliente\n2 - Loja' ,
  PRIMARY KEY (`id`) ,
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Pais`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Pais` ;

CREATE  TABLE IF NOT EXISTS `Pais` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(45) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Cidade`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Cidade` ;

CREATE  TABLE IF NOT EXISTS `Cidade` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(45) NOT NULL ,
  `idPais` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idPais_id_idx` (`idPais` ASC) ,
  CONSTRAINT `fk_idPais_id`
    FOREIGN KEY (`idPais` )
    REFERENCES `Pais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Bairro`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Bairro` ;

CREATE  TABLE IF NOT EXISTS `Bairro` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(50) NOT NULL ,
  `idCidade` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idCidade_id_idx` (`idCidade` ASC) ,
  CONSTRAINT `fk_idCidade_id`
    FOREIGN KEY (`idCidade` )
    REFERENCES `Cidade` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Cliente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Cliente` ;

CREATE  TABLE IF NOT EXISTS `Cliente` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `idUsername` BIGINT NOT NULL ,
  `nome` VARCHAR(100) NOT NULL ,
  `fotowall` TEXT NOT NULL ,
  `fotoperfil` TEXT NOT NULL ,
  `cep` VARCHAR(45) NULL ,
  `logradouro` VARCHAR(100) NULL ,
  `numero` VARCHAR(25) NULL ,
  `latitude` DECIMAL(11,8) NULL ,
  `longitude` DECIMAL(11,8) NULL ,
  `idBairro` BIGINT NOT NULL ,
  `statusPublico` ENUM('1', '2') NOT NULL COMMENT '1 - Publico\n2 - Privado' ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idUsername_idx` (`idUsername` ASC) ,
  INDEX `fk_idBairro_id_idx` (`idBairro` ASC) ,
  CONSTRAINT `fk_idUsername`
    FOREIGN KEY (`idUsername` )
    REFERENCES `Usuario` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_idBairro_id`
    FOREIGN KEY (`idBairro` )
    REFERENCES `Bairro` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Loja`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Loja` ;

CREATE  TABLE IF NOT EXISTS `Loja` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `idUsername` BIGINT NOT NULL ,
  `nome` VARCHAR(25) NOT NULL ,
  `fotowall` TEXT NOT NULL ,
  `fotoperfil` TEXT NOT NULL ,
  `cep` VARCHAR(45) NULL ,
  `logradouro` VARCHAR(100) NULL ,
  `numero` VARCHAR(25) NULL ,
  `latitude` DECIMAL(11,8) NULL ,
  `longitude` DECIMAL(11,8) NULL ,
  `cnpj` INT(20) NULL ,
  `idBairro` BIGINT NOT NULL ,
  `telefone` VARCHAR(20) NULL ,
  `linkface` TEXT NULL ,
  `votacoes` INT NULL ,
  `media_nota` FLOAT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idusernameloja_id_idx` (`idUsername` ASC) ,
  INDEX `fk_IdBairroLoja_idx` (`idBairro` ASC) ,
  CONSTRAINT `fk_idUsernameLoja_id`
    FOREIGN KEY (`idUsername` )
    REFERENCES `Usuario` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_IdBairroLoja`
    FOREIGN KEY (`idBairro` )
    REFERENCES `Bairro` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `LojaFavorita`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `LojaFavorita` ;

CREATE  TABLE IF NOT EXISTS `LojaFavorita` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `idLoja` BIGINT NOT NULL ,
  `idCliente` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idCliente_id_idx` (`idCliente` ASC) ,
  INDEX `fk_idLoja_id_idx` (`idLoja` ASC) ,
  CONSTRAINT `fk_idCliente_id`
    FOREIGN KEY (`idCliente` )
    REFERENCES `Cliente` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_idLoja_id`
    FOREIGN KEY (`idLoja` )
    REFERENCES `Loja` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Categoria`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Categoria` ;

CREATE  TABLE IF NOT EXISTS `Categoria` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `descricao` VARCHAR(50) NOT NULL ,
  `idLoja` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_Categoria_Loja1_idx` (`idLoja` ASC) ,
  CONSTRAINT `fk_Categoria_Loja1`
    FOREIGN KEY (`idLoja` )
    REFERENCES `Loja` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Produto`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Produto` ;

CREATE  TABLE IF NOT EXISTS `Produto` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(45) NOT NULL ,
  `descricao` VARCHAR(100) NULL ,
  `preco` FLOAT(10,2) NOT NULL ,
  `idLoja` BIGINT NOT NULL ,
  `idCategoria` BIGINT NOT NULL ,
  `prioridade` DECIMAL(2,2) NOT NULL ,
  `dataCriacao` INT NOT NULL ,
  `linkfoto` TEXT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idLojaProduto_id_idxx` (`idLoja` ASC) ,
  INDEX `fk_Produto_Categoria1_idx` (`idCategoria` ASC) ,
  CONSTRAINT `FK_idLoja_Produto_id`
    FOREIGN KEY (`idLoja` )
    REFERENCES `Loja` (`id` )
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Produto_Categoria1`
    FOREIGN KEY (`idCategoria` )
    REFERENCES `Categoria` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ProdutoFavorito`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ProdutoFavorito` ;

CREATE  TABLE IF NOT EXISTS `ProdutoFavorito` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `idProduto` BIGINT NOT NULL ,
  `idLojaFavorita` BIGINT NOT NULL ,
  `idCliente` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_ProdutoFavorito_LojaFavorita1_idx` (`idLojaFavorita` ASC) ,
  INDEX `fk_idProduto_id_idx` (`idProduto` ASC) ,
  INDEX `fk_ProdutoFavorito_Cliente1` (`idCliente` ASC) ,
  CONSTRAINT `fk_ProdutoFavorito_LojaFavorita1`
    FOREIGN KEY (`idLojaFavorita` )
    REFERENCES `LojaFavorita` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_idProduto_id`
    FOREIGN KEY (`idProduto` )
    REFERENCES `Produto` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ProdutoFavorito_Cliente1`
    FOREIGN KEY (`idCliente` )
    REFERENCES `Cliente` (`id` )
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `LojaFavorita`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `LojaFavorita` ;

CREATE  TABLE IF NOT EXISTS `LojaFavorita` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `idLoja` BIGINT NOT NULL ,
  `idCliente` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_idCliente_id_idx` (`idCliente` ASC) ,
  INDEX `fk_idLoja_id_idx` (`idLoja` ASC) ,
  CONSTRAINT `fk_idCliente_id`
    FOREIGN KEY (`idCliente` )
    REFERENCES `Cliente` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_idLoja_id`
    FOREIGN KEY (`idLoja` )
    REFERENCES `Loja` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Promocao`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Promocao` ;

CREATE  TABLE IF NOT EXISTS `Promocao` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `novopreco` FLOAT(10,2) NOT NULL ,
  `idProduto` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_Promocao_Produto1_idx` (`idProduto` ASC) ,
  CONSTRAINT `fk_Promocao_Produtoid`
    FOREIGN KEY (`idProduto` )
    REFERENCES `Produto` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Servico`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Servico` ;

CREATE  TABLE IF NOT EXISTS `Servico` (
  `id` BIGINT NOT NULL AUTO_INCREMENT ,
  `titulo` VARCHAR(50) NULL ,
  `descricao` VARCHAR(200) NULL ,
  `idLoja` BIGINT NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_Servico_Loja1_idx` (`idLoja` ASC) ,
  CONSTRAINT `fk_Servico_Loja1`
    FOREIGN KEY (`idLoja` )
    REFERENCES `Loja` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
