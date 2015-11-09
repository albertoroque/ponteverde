INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('1', 'Brasil');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('2', 'United States of America');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('3', 'Argentina');
INSERT INTO `ponteverde`.`pais` (`id`, `nome`) VALUES ('4', 'Portugal');

INSERT INTO `ponteverde`.`cidade` (`id`, `nome`, `idPais`) VALUES (1, 'São Paulo', 1);
INSERT INTO `ponteverde`.`cidade` (`id`, `nome`, `idPais`) VALUES (2, 'Guarulhos', 1);

INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (1, 'Centro', 1);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (2, 'Centro', 2);
INSERT INTO `ponteverde`.`bairro` (`id`, `nome`, `idCidade`) VALUES (3, 'Vila Rio', 2);

INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (1, 'alberto', '123456', 'alberto@mail', '1');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (2, 'wisley', '123456', 'wisley@mail', '1');
INSERT INTO `ponteverde`.`usuario` (`id`, `username`, `password`, `email`, `tipo`) VALUES (3, 'lojaorganicos', '123456', 'org@mail.com', '2');

INSERT INTO `ponteverde`.`cliente` (`id`, `idUsername`, `nome`, `fotowall`, `fotoperfil`, `cep`, `logradouro`, `numero`, `idBairro`, `statusPublico`) VALUES (1, 1, 'Alberto Roque', '../Content/images/wall2.jpg', '../Content/images/foto2.jpg', '07124000', 'Av. Benjamin', '1750', 3, '1');
