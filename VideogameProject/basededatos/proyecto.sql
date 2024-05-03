CREATE DATABASE IF NOT EXISTS cards_db ;
USE cards_db;

CREATE TABLE Player (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  level INT NOT NULL,
  deck INT NOT NULL,
  PRIMARY KEY (id)
);

-- Tabla Deck
CREATE TABLE Deck (
  id INT NOT NULL AUTO_INCREMENT,
  card1 INT NOT NULL,
  card3 INT NOT NULL,
  card4 INT NOT NULL,
  card5 INT NOT NULL,
  powerup1 INT NOT NULL,
  powerup2 INT NOT NULL,
  powerup3 INT NOT NULL,
  PRIMARY KEY (id)
);


CREATE TABLE Powerup_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  image VARCHAR(9),
  description TEXT,
  ability INT NOT NULL,
  PRIMARY KEY (id)
);


CREATE TABLE Character_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  description TEXT,
  ability VARCHAR(45),
  resistance INT NOT NULL,
  health INT NOT NULL,
  speed INT NOT NULL,
  PRIMARY KEY (id)
);


CREATE TABLE Ability (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  amount INT NOT NULL,
  cost INT NOT NULL,
  cards_affected VARCHAR(45),
  effect VARCHAR(45),
  PRIMARY KEY (id)
);


CREATE TABLE Game (
  id INT NOT NULL AUTO_INCREMENT,
  cards INT NOT NULL,
  duration INT NOT NULL,
  enemies_played INT NOT NULL,
  PRIMARY KEY (id)
);


INSERT INTO `Character_card` (`id`, `name`, `description`, `ability`, `resistance`, `health`, `speed`) VALUES
(1, 'Martha', 'Chef', 1, 12, 70, 4),
(2, 'Wendy', 'Campista Insectos', 2, 9, 40, 10),
(3, 'Brick', 'Campista Armas', 3, 13, 40, 8),
(4, 'Eduardo', 'Leñador', 4, 15, 80, 3),
(5, 'Coach', 'Entrenador', 5, 20, 100, 6),
(6, 'Jack', 'Salvavidas', 6, 17, 70, 7),
(7, 'Mike', 'Campista Rayos', 7, 10, 40, 5);


INSERT INTO `Powerup_card` (`id`, `name`, `description`, `ability`) VALUES
(1, 'Bombón', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 8),
(2, 'Galleta', 'Junta esta carta con la de Chocolate y Bombón para restaurar la mitad de la vida de todos tus personajes', 9),
(3, 'Chocolate', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 10),
(4, 'Tienda de Acampar', 'Usa este power up para resguardar a uno de tus personajes activos durante 1 turno', 11),
(5, 'Botiquín de primeros auxilios', 'Equípalo a uno de tus personajes para que cure a su aliado por (x) cantidad de vida. Si esta carta es equipada a un curador, la cantidad de vida restaurada se duplica', 12),
(6, 'Repelente de mosquitos', 'Equípalo a alguno de tus personajes para protegerlos por (x) de daño durante 2 turnos. Esta carta no se puede equipar a Wendy', 13),
(7, 'Fogata', 'Colócala en el centro de tus cartas para otorgarle un escudo temporal a tus personajes de (x) cantidad durante 3 turnos', 14),
(8, 'Enciclopedia', 'Mejora la habilidad especial de Mike por 5 de daño', 15),
(9, 'Walkie-Talkie', 'Equípala a uno de tus personajes activos para que utilice una de las actividades de los personajes de tu banca durante este turno', 16),
(10, 'Barra de Granola', 'Equípala a uno de tus personajes para restaurarles (x) cantidad de vida', 17),
(11, 'Linterna', 'Cólocala en el centro de tus cartas para cegar a un oponente aleatorio durante 1 turno (esto le impedirá atacar en el siguiente turno)', 18),
(12, 'Curita', 'Cura al personaje al que se lo equipes por (x) de vida', 19),
(13, 'Bebida energética', 'Restaura (x) cantidad de energía a tu barra', 20),
(14, 'Guantes', 'Equipa esta carta a Wendy para evitar que sus habilidades le hagan daño a sí misma', 21),
(15, 'Camisa de Franela', 'Equipa esta carta al leñador para mejorar su resistencia por 2 puntos', 22),
(16, 'Protein Shake', 'Equipa esta carta al coach para poder utilizar alguna de sus habilidades 2 veces en el mismo turno', 23),
(17, 'Espada de madera', 'Equipa esta carta a Brick para mejorar su ataque básico 5 de daño', 24),
(18, 'Arco', 'Equipa esta carta a Brick para cambiar su resortera por un arco pero ten cuidado porque el arco sólo puede utilizarse si también obtienes la carta Flecha', 25),
(19, 'Insignia', 'Equipa esta carta a alguno de los campistas (Wendy/Brick/Mike) para inspirarlos y reducir el costo de energía de sus dos habilidades por 5 de manera permanente', 26),
(20, 'Afilador', 'Equipa esta carta al leñador para que su ataque básico genere el efecto sangrar (hace daño continuo al enemigo por 2 turnos)', 27),
(21, 'Mancuerna', 'Aumenta el poder del entrenador en 2 puntos', 28),
(22, 'Piedra extraña', 'Encuentras una piedra extraña que aumenta el poder de todos tus personajes en juego durante 2 turnos', 29),
(23, 'Sartén', 'Equipa esta carta a la Chef para permitirle cocinar una hamburguesa extra durante este turno (aumenta la cantidad de la curación)', 30),
(24, 'Bomba de gas', 'Hace daño a los dos enemigos pero también hace un pequeño daño a tus personajes', 31),
(25, 'Dulce', 'Aumenta la velocidad de tus personajes durante 1 turno', 32),
(26, 'Café de olla', 'Permite a uno de los personajes realizar un ataque sin gastar energía', 33),
(27, 'Flechas', 'Estas te permiten usar el arco', 34),
(28, 'Cuatrimoto', 'Permite cambiar una de tus cartas base sin que se acabe tu turno', 35),
(29, 'Red de pescar', 'Permite atrapar a uno de los personajes de tu oponente para que no pueda atacar en su siguiente turno', 36),
(30, 'Espátula', 'Permite al chef hacer comida más rápido para que puedan recuperar HP (si son jugadores que están en la baraja dos podrán recuperar hp, si es el jugador en ataque solo él podrá)', 37),
(31, 'Jetski', 'Equipar al salvavidas para esquivar un ataque', 38),
(32, 'Flotador', 'Protege a uno de tus personajes de los daños continuos', 39),
(33, 'Telescopio', 'Mejora la habilidad básica de Mike en (x) cantidad', 40);


INSERT INTO `Ability` (`id`, `name`, `amount`, `cost`, `cards_affected`, `effect`) VALUES
(1, 'Curacion con Queso', 0, 20, 'aliado', 'curacion'),
(2, 'Rana Venenosa', 0, 12, 'enemigo seleccionado', 'dano'),
(3, 'Destruccion DIY', 0, 10, 'enemigo seleccionado', 'dano'),
(4, 'Golpe de Lenador', 0, 25, 'ambos enemigos', 'dano'),
(5, 'Pep-talk', 0, 20, 'aliado', 'mejora_dano'),
(6, 'RCP', 0, 30, 'carta de tu mazo', 'curacion'),
(7, 'Juicio Celestial', 0, 15, 'enemigo seleccionado', 'dano'),
(8, 'Bombón', 0, 0, 'mismo', 'curacion'),
(9, 'Galleta', 0, 0, 'mismo', 'curacion'),
(10, 'Chocolate', 0, 0, 'mismo', 'curacion'),
(11, 'Tienda de Acampar', 0, 0, 'mismo', 'escudo'),
(12, 'Botiquín de primeros auxilios', 0, 0, 'mismo', 'escudo'),
(13, 'Repelente de mosquitos', 0, 0, 'mismo', 'escudo'),
(14, 'Fogata', 0, 0, 'mismo', 'escudo'),
(15, 'Enciclopedia', 0, 0, 'Mike', 'mejora_dano'),
(17, 'Barra de Granola', 0, 0, 'mismo', 'curacion'),
(18, 'linterna', 0, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(19, 'Curita', 0, 0, 'mismo', 'curacion'),
(20, 'Bebida energética', 0, 0, 'mismo', 'restaura_energia'),
(22, 'Camisa de Franela', 0, 0, 'mismo', 'mejora_resistencia'),
(24, 'Espada de madera', 0, 0, 'Brick', 'mejora_dano'),
(25, 'Arco', 0, 0, 'Brick', 'mejora_dano'),
(26, 'Insignia', 0, 0, 'mismo', 'restaura_energia'),
(27, 'Afilador', 0, 0, 'Eduardo', 'mejora_dano'),
(28, 'Mancuerna', 0, 0, 'Entrenador', 'mejora_dano'),
(29, 'Piedra extraña', 0, 0, 'activos', 'mejora_dano'),
(30, 'Sarten', 0, 0, 'Martha', 'mejora_curacion'),
(32, 'Dulce', 0, 0, 'mismo', 'mejora_velocidad'),
(33, 'Café de olla', 0, 0, 'mismo', 'restaura_energia'),
(34, 'Flechas', 0, 0, 'Brick', 'mejora_dano'),
(36, 'Red de Pescar', 0, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(37, 'Espatula', 0, 0, 'Martha', 'mejora_curacion'),
(38, 'Jetski', 0, 0, 'Jack', 'bloquea_dano'),
(39, 'Flotador', 0, 0, 'mismo', 'bloquea_dano'),
(40, 'Telescopio', 0, 0, 'Mike', 'mejora_dano');


