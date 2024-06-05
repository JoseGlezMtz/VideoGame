USE fa_database;
INSERT INTO `Ability` (`id`,`amount`, `cost`, `cards_affected`, `effect`) VALUES
(1,  15, 20, 'aliado', 'curacion'),
(2,  12, 12, 'enemigo seleccionado', 'dano'),
(3,  10, 10, 'enemigo seleccionado', 'dano'),
(4,  15, 30, 'ambos enemigos', 'dano'),
(5,  10, 15, 'aliado', 'mejora_dano'),
(6,  50, 30, 'carta de tu mazo', 'curacion'),
(7,  20, 20, 'enemigo seleccionado', 'dano'),
(8,  20, 0, 'mismo', 'curacion'),
(9,  50, 0, 'mismo', 'curacion'),
(10, 20, 0, 'mismo', 'curacion'),
(11,  3, 0, 'mismo', 'escudo'),
(12,  60, 0, 'mismo', 'curacion'),
(13,  1, 0, 'mismo', 'escudo'),
(14,  2, 0, 'mismo', 'escudo'),
(15, 20, 0, 'Mike', 'mejora_dano'), 
(16,  10, 0, 'mismo', 'curacion'),
(17, 5, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(18, 10, 0, 'mismo', 'curacion'),
(19, 10, 0, 'mismo', 'restaura_energia'),
(20, 5, 0, 'Eduardo', 'mejora_resistencia'),
(21, 5, 0, 'Brick', 'mejora_dano'),
(22, 5, 0, 'Brick', 'mejora_dano'),
(23, 5, 0, 'mismo', 'restaene'),
(24, 50, 0, 'Eduardo', 'mejora_dano'),
(25, 5, 0, 'Entrenador', 'mejora_dano'),
(26, 5, 0, 'activos', 'mejora_dano'),
(27, 5, 0, 'Martha', 'mejora_curacion'),
(28, 5, 0, 'mismo', 'mejora_velocidad'),
(29, 6, 0, 'mismo', 'restaura_energia'),
(30, 20, 0, 'Brick', 'mejora_dano'),
(31, 100, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(32, 10, 0, 'Martha', 'mejora_curacion'),
(33, 100, 0, 'Jack', 'bloquea_dano'),
(34, 0, 0, 'mismo', 'bloquea_dano'),
(35, 20, 0, 'Mike', 'mejora_dano'),
(36, 50, 0, 'aliados', 'curacion');

INSERT INTO `Character_card` (`id`, `name`,`nameAbility`, `description`, `ability`, `resistance`, `health`, `speed`) VALUES
(1, 'Chef', 'Chef','Curacion con Queso', 1, 12, 100, 4),
(2, 'Wendy', 'Campista Insectos','Rana Venenosa', 2, 9, 100, 10),
(3, 'Brick', 'Campista Armas','Destruccion DIY', 3, 13, 100, 8),
(4, 'Lumberjack', 'Leñador','Golpe de Lenador', 4, 15, 100, 3),
(5, 'Coach', 'Entrenador','Pep-talk', 5, 20, 100, 6),
(6, 'Lifeguard', 'Salvavidas','RCP', 6, 17, 100, 7),
(7, 'Mike', 'Campista Rayos','Juicio Celestial', 7, 10, 100, 5);


INSERT INTO `Powerup_card` (`id`, `name`, `description`, `ability`) VALUES
(8, 'Bombón', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 8),
(9, 'Galleta', 'Junta esta carta con la de Chocolate y Bombón para restaurar la mitad de la vida de todos tus personajes', 9),
(10, 'Chocolate', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 10),
(11, 'Tienda de Acampar', 'Usa este power up para resguardar a uno de tus personajes activos durante 1 turno', 11),
(12, 'Botiquín de primeros auxilios', 'Equípalo a uno de tus personajes para que cure a su aliado por (x) cantidad de vida. Si esta carta es equipada a un curador, la cantidad de vida restaurada se duplica', 12),
(13, 'Repelente de mosquitos', 'Equípalo a alguno de tus personajes para protegerlos por (x) de daño durante 2 turnos. Esta carta no se puede equipar a Wendy', 13),
(14, 'Fogata', 'Colócala en el centro de tus cartas para otorgarle un escudo temporal a tus personajes de (x) cantidad durante 3 turnos', 14),
(15, 'Enciclopedia', 'Mejora la habilidad especial de Mike por 5 de daño', 15),
(16, 'Barra de Granola', 'Equípala a uno de tus personajes para restaurarles (x) cantidad de vida', 16),
(17, 'Linterna', 'Cólocala en el centro de tus cartas para cegar a un oponente aleatorio durante 1 turno (esto le impedirá atacar en el siguiente turno)', 17),
(18, 'Curita', 'Cura al personaje al que se lo equipes por (x) de vida', 18),
(19, 'Bebida energética', 'Restaura (x) cantidad de energía a tu barra', 19),
(20, 'Camisa de Franela', 'Equipa esta carta al leñador para mejorar su resistencia por 2 puntos', 20),
(21, 'Espada de madera', 'Equipa esta carta a Brick para mejorar su ataque básico 5 de daño', 21),
(22, 'Arco', 'Equipa esta carta a Brick para cambiar su resortera por un arco pero ten cuidado porque el arco sólo puede utilizarse si también obtienes la carta Flecha', 22),
(23, 'Insignia', 'Equipa esta carta a alguno de los campistas (Wendy/Brick/Mike) para inspirarlos y reducir el costo de energía de sus dos habilidades por 5 de manera permanente',23),
(24, 'Afilador', 'Equipa esta carta al leñador para que su ataque básico genere el efecto sangrar (hace daño continuo al enemigo por 2 turnos)', 24),
(25, 'Mancuerna', 'Aumenta el poder del entrenador en 2 puntos', 25),
(26, 'Piedra extraña', 'Encuentras una piedra extraña que aumenta el poder de todos tus personajes en juego durante 2 turnos', 26),
(27, 'Sartén', 'Equipa esta carta a la Chef para permitirle cocinar una hamburguesa extra durante este turno (aumenta la cantidad de la curación)', 27),
(28, 'Dulce', 'Aumenta la velocidad de tus personajes durante 1 turno',28),
(29, 'Café  de olla', 'Permite a uno de los personajes realizar un ataque sin gastar energía', 29),
(30, 'Flechas', 'Estas te permiten usar el arco', 30),
(31, 'Red de pescar', 'Permite atrapar a uno de los personajes de tu oponente para que no pueda atacar en su siguiente turno', 31),
(32, 'Espátula', 'Permite al chef hacer comida más rápido para que puedan recuperar HP (si son jugadores que están en la baraja dos podrán recuperar hp, si es el jugador en ataque solo él podrá)', 32),
(33, 'Jetski', 'Equipar al salvavidas para esquivar un ataque', 33),
(34, 'Flotador', 'Protege a uno de tus personajes de los daños continuos', 34),
(35, 'Telescopio', 'Mejora la habilidad básica de Mike en (x) cantidad', 35);


