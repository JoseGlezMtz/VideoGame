DROP DATABASE IF EXISTS FA_DataBase;

CREATE DATABASE IF NOT EXISTS  FA_DataBase /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE  FA_DataBase;

CREATE TABLE IF NOT EXISTS Player (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  password VARCHAR(45) NOT NULL,
  level INT DEFAULT 1,
  #deck_id INT NOT NULL,
  PRIMARY KEY (id)

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE IF NOT EXISTS Ability (
  id INT NOT NULL AUTO_INCREMENT,
  amount INT NOT NULL,
  cost INT NOT NULL,
  cards_affected VARCHAR(45),
  effect VARCHAR(45),
  PRIMARY KEY (id)
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE IF NOT EXISTS Character_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  nameAbility VARCHAR(45) NOT NULL,
  description TEXT,
  ability INT NOT NULL UNIQUE,
  resistance INT NOT NULL,
  health INT NOT NULL,
  speed INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_character_ability FOREIGN KEY (ability) REFERENCES Ability(id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Powerup_card (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  image VARCHAR(9),
  description TEXT,
  ability INT NOT NULL UNIQUE,
  PRIMARY KEY (id),
  CONSTRAINT fk_powerup_ability FOREIGN KEY (ability) REFERENCES Ability(id) ON DELETE CASCADE ON UPDATE CASCADE
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
  player_id INT NOT NULL,
  card1 INT NOT NULL,
  card2 INT NOT NULL,
  card3 INT NOT NULL,
  card4 INT NOT NULL,
  card5 INT NOT NULL,
  powerup1 INT DEFAULT NULL,
  powerup2 INT  DEFAULT NULL,
  powerup3 INT   DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_deck_player FOREIGN KEY (player_id) REFERENCES Player(id),
  CONSTRAINT fk_deck_card1 FOREIGN KEY (card1) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card2 FOREIGN KEY (card2) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card3 FOREIGN KEY (card3) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card4 FOREIGN KEY (card4) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card5 FOREIGN KEY (card5) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_powerup1 FOREIGN KEY (powerup1) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup2 FOREIGN KEY (powerup2) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup3 FOREIGN KEY (powerup3) REFERENCES Powerup_card(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

 CREATE TABLE IF NOT EXISTS Cards_played (
  id INT NOT NULL AUTO_INCREMENT,
  game_id INT NOT NULL,
  player_id INT NOT NULL,
  card_id INT NOT NULL,
  is_powerup BOOLEAN NOT NULL DEFAULT FALSE,
  PRIMARY KEY (id),
  CONSTRAINT fk_played_game FOREIGN KEY (game_id) REFERENCES Game(id),
  CONSTRAINT fk_played_player FOREIGN KEY (player_id) REFERENCES Player(id),
  CONSTRAINT fk_played_card FOREIGN KEY (card_id) REFERENCES Character_card(id),
  CONSTRAINT chk_is_powerup CHECK (is_powerup IN (0, 1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE VIEW Character_Ability AS
SELECT cc.id AS id,
 cc.name AS character_name, 
 a.amount AS attack, 
 a.cost AS abilityCost, 
 a.cards_affected, 
 a.effect AS effect,
 cc.resistance,cc.health,cc.speed
FROM Character_card cc
JOIN Ability a ON cc.ability = a.id;

CREATE VIEW  Powerup_Ability AS
SELECT pc.id, 
pc.name, 
pc.description,
 a.effect AS ability_effect, 
 a.amount AS ability_amount
FROM Powerup_card pc
JOIN Ability a ON pc.ability = a.id;

CREATE VIEW Deck_Character AS
SELECT d.id AS deck_id, 
p.name AS player_name, 
cc.name AS character_name, 
cc.description AS character_description
FROM Deck d
JOIN Character_card cc ON d.card1 = cc.id OR d.card2 = cc.id OR d.card3 = cc.id OR d.card4 = cc.id OR d.card5 = cc.id
JOIN Player p ON d.player_id = p.id;



/*
CREATE VIEW Player_Deck_Character AS
SELECT p.name AS player_name,
 dc.character_name, 
 dc.character_description
FROM Player p
JOIN Deck d ON p.id = d.player_id
JOIN Deck_Character dc ON d.id = dc.deck_id;

CREATE VIEW Player_Cards_Played AS
SELECT g.id AS game_id,
 p.name AS player_name, 
 cp.card_id, cp.is_powerup
FROM Game g
JOIN Cards_played cp ON g.id = cp.game_id
JOIN Player p ON cp.player_id = p.id;


CREATE VIEW Character_Ability_Cost AS
SELECT cc.name AS character_name, 
a.effect AS ability_effect,
 a.cost AS ability_cost
FROM Character_card cc
JOIN Ability a ON cc.ability = a.id;

CREATE VIEW Powerup_Effects AS
SELECT pc.name AS powerup_name,
 pc.description AS powerup_description, 
 a.effect AS ability_effect
FROM Powerup_card pc
JOIN Ability a ON pc.ability = a.id;

CREATE VIEW Player_Deck_Powerups AS
SELECT p.name AS player_name, 
d.powerup1, 
d.powerup2,
 d.powerup3
FROM Player p
JOIN Deck d ON p.id = d.player_id;

CREATE VIEW Game_Statistics AS
SELECT g.id AS game_id, 
g.cards AS num_cards, 
g.duration AS game_duration,
 g.enemies_played AS num_enemies_played
FROM Game g;
CREATE VIEW Character_Health_Speed AS
SELECT cc.name AS character_name, 
cc.health AS character_health,
 cc.speed AS character_speed
FROM Character_card cc;*/

select * from powerup_card;






INSERT INTO `Ability` (`id`,`amount`, `cost`, `cards_affected`, `effect`) VALUES
(1,  15, 20, 'aliado', 'curacion'),
(2,  12, 12, 'enemigo seleccionado', 'dano'),
(3,  10, 10, 'enemigo seleccionado', 'dano'),
(4,  15, 30, 'ambos enemigos', 'dano'),
(5,  10, 15, 'aliado', 'mejora_dano'),
(6,  50, 30, 'carta de tu mazo', 'curacion'),
(7,  20, 20, 'enemigo seleccionado', 'dano'),
(8,  0, 0, 'mismo', 'curacion'),
(9,  0, 0, 'mismo', 'curacion'),
(10, 0, 0, 'mismo', 'curacion'),
(11,  100, 0, 'mismo', 'escudo'),
(12,  10, 0, 'mismo', 'escudo'),
(13,  10, 0, 'mismo', 'escudo'),
(14,  15, 0, 'mismo', 'escudo'),
(15, 5, 0, 'Mike', 'mejora_dano'), 
(16,  10, 0, 'mismo', 'curacion'),
(17, 0, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(18, 5, 0, 'mismo', 'curacion'),
(19, 10, 0, 'mismo', 'restaura_energia'),
(20, 0, 0, 'Eduardo', 'mejora_resistencia'),
(21, 5, 0, 'Brick', 'mejora_dano'),
(22, 5, 0, 'Brick', 'mejora_dano'),
(23, 5, 0, 'mismo', 'restaura_energia'),
(24, 0, 0, 'Eduardo', 'mejora_dano'),
(25, 2, 0, 'Entrenador', 'mejora_dano'),
(26, 5, 0, 'activos', 'mejora_dano'),
(27, 5, 0, 'Martha', 'mejora_curacion'),
(28, 5, 0, 'mismo', 'mejora_velocidad'),
(29, 0, 0, 'mismo', 'restaura_energia'),
(30, 0, 0, 'Brick', 'mejora_dano'),
(31, 100, 0, 'enemigo_seleccionado', 'bloquea_dano'),
(32, 10, 0, 'Martha', 'mejora_curacion'),
(33, 100, 0, 'Jack', 'bloquea_dano'),
(34, 0, 0, 'mismo', 'bloquea_dano'),
(35, 0, 0, 'Mike', 'mejora_dano'),
(36, 50, 0, 'aliados', 'curacion');

INSERT INTO `Character_card` (`id`, `name`,`nameAbility`, `description`, `ability`, `resistance`, `health`, `speed`) VALUES
(1, 'Martha', 'Chef','Curacion con Queso', 1, 12, 100, 4),
(2, 'Wendy', 'Campista Insectos','Rana Venenosa', 2, 9, 100, 10),
(3, 'Brick', 'Campista Armas','Destruccion DIY', 3, 13, 100, 8),
(4, 'Eduardo', 'Leñador','Golpe de Lenador', 4, 15, 100, 3),
(5, 'Coach', 'Entrenador','Pep-talk', 5, 20, 100, 6),
(6, 'Jack', 'Salvavidas','RCP', 6, 17, 100, 7),
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
(29, 'Café de olla', 'Permite a uno de los personajes realizar un ataque sin gastar energía', 29),
(30, 'Flechas', 'Estas te permiten usar el arco', 30),
(31, 'Red de pescar', 'Permite atrapar a uno de los personajes de tu oponente para que no pueda atacar en su siguiente turno', 31),
(32, 'Espátula', 'Permite al chef hacer comida más rápido para que puedan recuperar HP (si son jugadores que están en la baraja dos podrán recuperar hp, si es el jugador en ataque solo él podrá)', 32),
(33, 'Jetski', 'Equipar al salvavidas para esquivar un ataque', 33),
(34, 'Flotador', 'Protege a uno de tus personajes de los daños continuos', 34),
(35, 'Telescopio', 'Mejora la habilidad básica de Mike en (x) cantidad', 35);

DELIMITER $$
CREATE PROCEDURE  Character_Ability_pro () 
BEGIN
Select * from character_ability;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Powerup_Ability_pro ()
BEGIN
Select * from powerup_ability;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Deck_pro ()
BEGIN
Select * from deck_character;
END $$
DELIMITER ; 

DELIMITER $$

CREATE PROCEDURE register_player(
    IN registered_name VARCHAR(45),
    IN registered_password VARCHAR(45)
)
BEGIN
    DECLARE username_exists INT;

    SELECT COUNT(*) INTO username_exists
    FROM player
    WHERE name = registered_name;

    IF username_exists > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Ese nombre de usuario no se encuentra disponible';
    ELSE
        
        INSERT INTO player (name, password) VALUES (registered_name, registered_password);
    END IF;
END $$

DELIMITER ;

DELIMITER $$

CREATE TRIGGER before_player_insert
BEFORE INSERT ON player
FOR EACH ROW
BEGIN
    DECLARE username_exists INT;

    SELECT COUNT(*) INTO username_exists
    FROM player
    WHERE name = NEW.name;

    IF username_exists > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Ese nombre de usuario no esta disponible';
    END IF;
END $$

DELIMITER ;



DELIMITER $$
CREATE PROCEDURE validate_login(
    IN registered_name VARCHAR(45),
    IN registered_password VARCHAR(45),
    OUT registered_id INT,
    OUT login_successful BOOL,
    OUT status_message VARCHAR(45)
)
BEGIN 
    DECLARE db_password VARCHAR(45);
    DECLARE db_id INT;

    SET registered_id = NULL;
    SET login_successful = FALSE;
    SET status_message = 'Login failed';

    SELECT id, password 
    INTO db_id, db_password
    FROM player
    WHERE name = registered_name;

    IF db_id IS NOT NULL AND db_password = registered_password THEN
        SET registered_id = db_id;
        SET login_successful = TRUE;
        SET status_message = 'Login successful';
    END IF;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_deck(
	IN 	deck_id INT,
	IN card1_id INT,
    IN card2_id INT,
    IN card3_id INT,
    IN card4_id INT,
    IN card5_id INT,
    OUT status_message VARCHAR(45)
)
BEGIN
	UPDATE deck
    SET card1 = card1_id, card2 = card2_id, card3 = card3_id, card4 = card4_id, card5 = card5_id
    WHERE id = deck_id;
    SET status_message = 'Deck Updated succesfully';
END $$

    
        






