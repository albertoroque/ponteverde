
-- PAIS
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('1', 'Brasil');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('2', 'United States of America');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('3', 'Argentina');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('4', 'Portugal');

-- CIDADE
INSERT INTO `ponteverde`.`cidade` (`id`, `nome`, `idPais`) VALUES (1, 'São Paulo', 1);
INSERT INTO `ponteverde`.`cidade` (`id`, `nome`, `idPais`) VALUES (2, 'Guarulhos', 1);

-- BAIRRO
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (1, 'Centro', 1);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (2, 'Centro', 2);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (3, 'Portal dos Gramados', 2);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (4, 'Vila dos Andrades', 1);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (5, 'Bom Retiro', 1);

-- USUARIO
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (1, 'alberto', '123456', 'alberto@mail', '1');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (2, 'wisley', '123456', 'wisley@mail', '1');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (3, 'lojaorganicos', '123456', 'org@mail.com', '2');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (4, 'portalorganicos', '123456', 'org@mail.com', '2');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (5, 'superorganicos', '123456', 'org@mail.com', '2');

-- CLIENTE
INSERT INTO `ponteverde`.`cliente` (`id`, `idUsername`, `nome`, `fotowall`, `fotoperfil`, `cep`, `logradouro`, `numero`, `idBairro`, `statusPublico`) 
VALUES (1, 1, 'Alberto Roque', '../Content/images/wall2.jpg', '../Content/images/foto2.jpg', '07124000', 'Av. Benjamin', '1750', 3, '1');
INSERT INTO `ponteverde`.`cliente` (`id`, `idUsername`, `nome`, `fotowall`, `fotoperfil`, `cep`, `logradouro`, `numero`, `idBairro`, `statusPublico`) 
VALUES (2, 2, 'Wisley Pereira', '../Content/images/default/wall.jpg', '../Content/images/default/face.jpg', '0', 'Rua Três', '900', 4, '1');                  

-- LOJAS
INSERT INTO `ponteverde`.`loja` (`id`, `idUsername`, `nome`, `fotowall`, `fotoperfil`, `cep`, `logradouro`, `numero`, `latitude`, `longitude`, `cnpj`, `idBairro`, `telefone`, `votacoes`, `media_nota`) 
VALUES 
('1', '3', 'Loja Orgânicos', '../Content/images/wall-loja.jpg', '../Content/images/foto-loja.jpg', '07010-003', 'Avenida Dom Pedro II', '555', '-23.4696737', '-46.52592130', '0000000000', '2', '222-222', '0', '0'),
('2', '4', 'Portal Orgânicos', '../Content/images/wall-loja.jpg', '../Content/images/loja4.jpg', '01023-001', 'R. Barão de Duprat', '389', '-23.540713', '-46.6312944', '0000000000', '1', '222-222', '0', '0'),
('3', '5', 'Super Orgânicos', '../Content/images/loja3.jpg', '../Content/images/logo-loja1.jpg', '01123-001', 'R. Três Rios', '1', '-23.5299436', '-46.6353338', '0000000000', '5', '222-222', '0', '0');

-- CATEGORIA
INSERT INTO `ponteverde`.`categoria` (`id`, `descricao`, `idLoja`) VALUES (1, 'Novidades', 1);
INSERT INTO `ponteverde`.`categoria` (`id`, `descricao`, `idLoja`) VALUES (2, 'Orgânicos', 1);
INSERT INTO `ponteverde`.`categoria` (`id`, `descricao`, `idLoja`) VALUES (3, 'Todos Produtos', 2);
INSERT INTO `ponteverde`.`categoria` (`id`, `descricao`, `idLoja`) VALUES (4, 'Todos produtos', 3);


-- PRODUTO
INSERT INTO `ponteverde`.`produto` (`id`, `nome`, `descricao`, `preco`, `idLoja`, `idCategoria`, `prioridade`, `dataCriacao`, `linkfoto`) 
VALUES 
(1, 'Tomate', 'Tomate', 3, 1, 1, 0, 10/10/2015, '../Content/images/prod/tomate.jpg'),
(2, 'Beterraba', 'Beterraba', 4.00, 1, 1, 0.90, 10/10/2015, '../Content/images/prod/beterraba.jpg'),
(3, 'Cenoura', 'Cenoura', 5, 1, 1, 0.99, 10/10/2015, '../Content/images/prod/cenoura.jpg'),
(4, 'Couve', 'Couve', 5, 1, 1, 0.99, 10/10/2015, '../Content/images/prod/couve.jpg'),
(5, 'Arroz Integral', 'Arroz Integral', 5, 1, 1, 0.5, 10/10/2015, '../Content/images/prod/integral.jpg'),
(6, 'Pepino', 'Pepino', 5, 1, 1, 0.6, 10/10/2015, '../Content/images/prod/Pepino.jpg'),
(7, 'Tomate Suiço', 'Tomate Suiço', 5, 1, 1, 0.3, 10/10/2015, '../Content/images/prod/tomate2.jpg');


-- LOJA FAVORITA
INSERT INTO `ponteverde`.`lojafavorita` (`id`, `idLoja`, `idCliente`) VALUES (1, 1, 1);
INSERT INTO `ponteverde`.`lojafavorita` (`id`, `idLoja`, `idCliente`) VALUES (2, 2, 2);

-- PRODUTO FAVORITO
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (1, 1, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (2, 2, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (3, 3, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (4, 4, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (5, 5, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (6, 6, 1, 1);
INSERT INTO `ponteverde`.`produtofavorito` (`id`, `idProduto`, `idLojaFavorita`, `idCliente`) VALUES (7, 7, 1, 1);





