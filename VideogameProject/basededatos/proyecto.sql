CREATE DATABASE IF NOT EXISTS cards_db /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE cards_db;

CREATE TABLE IF NOT EXISTS Player (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  level INT NOT NULL,
  deck INT NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE IF NOT EXISTS Powerup_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  image VARCHAR(9),
  description TEXT,
  ability INT NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE IF NOT EXISTS Character_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  description TEXT,
  ability VARCHAR(45),
  resistance INT NOT NULL,
  health INT NOT NULL,
  speed INT NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Ability (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  amount INT NOT NULL,
  cost INT NOT NULL,
  cards_affected VARCHAR(45),
  effect VARCHAR(45),
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Game (
  id INT NOT NULL AUTO_INCREMENT,
  cards INT NOT NULL,
  duration INT NOT NULL,
  enemies_played INT NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Deck (
  id INT NOT NULL AUTO_INCREMENT,
  card1 INT NOT NULL,
  card3 INT NOT NULL,
  card4 INT NOT NULL,
  card5 INT NOT NULL,
  powerup1 INT NOT NULL,
  powerup2 INT NOT NULL,
  powerup3 INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_deck_card1 FOREIGN KEY (card1) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card3 FOREIGN KEY (card3) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card4 FOREIGN KEY (card4) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card5 FOREIGN KEY (card5) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_powerup1 FOREIGN KEY (powerup1) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup2 FOREIGN KEY (powerup2) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup3 FOREIGN KEY (powerup3) REFERENCES Powerup_card(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


INSERT INTO `Character_card` (`id`, `name`, `description`, `ability`, `resistance`, `health`, `speed`) VALUES
(1, 'Martha', 'Chef', 1, 12, 70, 4),
(2, 'Wendy', 'Campista Insectos', 2, 9, 40, 10),
(3, 'Brick', 'Campista Armas', 3, 13, 40, 8),
(4, 'Eduardo', 'Leñador', 4, 15, 80, 3),
(5, 'Coach', 'Entrenador', 5, 20, 100, 6),
(6, 'Jack', 'Salvavidas', 6, 17, 70, 7),
(7, 'Mike', 'Campista Rayos', 7, 10, 40, 5);


INSERT INTO `Powerup_card` (`id`, `name`, `description`, `ability`) VALUES
(8, 'Bombón', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 8),
(9, 'Galleta', 'Junta esta carta con la de Chocolate y Bombón para restaurar la mitad de la vida de todos tus personajes', 9),
(10, 'Chocolate', 'Junta esta carta con la de Galleta y Bombón para restaurar la mitad de la vida de todos tus personajes', 10),
(11, 'Tienda de Acampar', 'Usa este power up para resguardar a uno de tus personajes activos durante 1 turno', 11),
(12, 'Botiquín de primeros auxilios', 'Equípalo a uno de tus personajes para que cure a su aliado por (x) cantidad de vida. Si esta carta es equipada a un curador, la cantidad de vida restaurada se duplica', 12),
(13, 'Repelente de mosquitos', 'Equípalo a alguno de tus personajes para protegerlos por (x) de daño durante 2 turnos. Esta carta no se puede equipar a Wendy', 13),
(14, 'Fogata', 'Colócala en el centro de tus cartas para otorgarle un escudo temporal a tus personajes de (x) cantidad durante 3 turnos', 14),
(15, 'Enciclopedia', 'Mejora la habilidad especial de Mike por 5 de daño', 15),
(17, 'Barra de Granola', 'Equípala a uno de tus personajes para restaurarles (x) cantidad de vida', 17),
(18, 'Linterna', 'Cólocala en el centro de tus cartas para cegar a un oponente aleatorio durante 1 turno (esto le impedirá atacar en el siguiente turno)', 18),
(19, 'Curita', 'Cura al personaje al que se lo equipes por (x) de vida', 19),
(20, 'Bebida energética', 'Restaura (x) cantidad de energía a tu barra', 20),
(22, 'Camisa de Franela', 'Equipa esta carta al leñador para mejorar su resistencia por 2 puntos', 22),
(24, 'Espada de madera', 'Equipa esta carta a Brick para mejorar su ataque básico 5 de daño', 24),
(25, 'Arco', 'Equipa esta carta a Brick para cambiar su resortera por un arco pero ten cuidado porque el arco sólo puede utilizarse si también obtienes la carta Flecha', 25),
(26, 'Insignia', 'Equipa esta carta a alguno de los campistas (Wendy/Brick/Mike) para inspirarlos y reducir el costo de energía de sus dos habilidades por 5 de manera permanente', 26),
(27, 'Afilador', 'Equipa esta carta al leñador para que su ataque básico genere el efecto sangrar (hace daño continuo al enemigo por 2 turnos)', 27),
(28, 'Mancuerna', 'Aumenta el poder del entrenador en 2 puntos', 28),
(29, 'Piedra extraña', 'Encuentras una piedra extraña que aumenta el poder de todos tus personajes en juego durante 2 turnos', 29),
(30, 'Sartén', 'Equipa esta carta a la Chef para permitirle cocinar una hamburguesa extra durante este turno (aumenta la cantidad de la curación)', 30),
(32, 'Dulce', 'Aumenta la velocidad de tus personajes durante 1 turno', 32),
(33, 'Café de olla', 'Permite a uno de los personajes realizar un ataque sin gastar energía', 33),
(34, 'Flechas', 'Estas te permiten usar el arco', 34),
(36, 'Red de pescar', 'Permite atrapar a uno de los personajes de tu oponente para que no pueda atacar en su siguiente turno', 36),
(37, 'Espátula', 'Permite al chef hacer comida más rápido para que puedan recuperar HP (si son jugadores que están en la baraja dos podrán recuperar hp, si es el jugador en ataque solo él podrá)', 37),
(38, 'Jetski', 'Equipar al salvavidas para esquivar un ataque', 38),
(39, 'Flotador', 'Protege a uno de tus personajes de los daños continuos', 39),
(40, 'Telescopio', 'Mejora la habilidad básica de Mike en (x) cantidad', 40);


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



