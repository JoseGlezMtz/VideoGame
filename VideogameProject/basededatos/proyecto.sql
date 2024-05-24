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
FROM Character_card cc;



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
