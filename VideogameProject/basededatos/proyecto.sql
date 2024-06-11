DROP DATABASE IF EXISTS FA_DataBase;
CREATE DATABASE IF NOT EXISTS  FA_DataBase /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE  FA_DataBase;
CREATE TABLE IF NOT EXISTS Player (
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL,
  password VARCHAR(45) NOT NULL,
  level INT DEFAULT 1,
  PRIMARY KEY (id)

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Ability (
  id INT NOT NULL AUTO_INCREMENT,
  amount INT NOT NULL,
  cost INT NOT NULL,
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
  player_id INT NOT NULL,
  num_round INT NOT NULL DEFAULT (0),
  PRIMARY KEY (id),
  CONSTRAINT fk_results_player FOREIGN KEY (player_id) REFERENCES Player(id)
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
  powerup2 INT DEFAULT NULL,
  powerup3 INT DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_deck_player FOREIGN KEY (id) REFERENCES Player(id),
  CONSTRAINT fk_deck_card1 FOREIGN KEY (card1) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card2 FOREIGN KEY (card2) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card3 FOREIGN KEY (card3) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card4 FOREIGN KEY (card4) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_card5 FOREIGN KEY (card5) REFERENCES Character_card(id),
  CONSTRAINT fk_deck_powerup1 FOREIGN KEY (powerup1) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup2 FOREIGN KEY (powerup2) REFERENCES Powerup_card(id),
  CONSTRAINT fk_deck_powerup3 FOREIGN KEY (powerup3) REFERENCES Powerup_card(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS Characters_Cards_played (
  id INT NOT NULL AUTO_INCREMENT,
  character_card_id INT DEFAULT NULL,
  amount INT DEFAULT 0,
  PRIMARY KEY (id),
  
  CONSTRAINT fk_played_character_card FOREIGN KEY (character_card_id) REFERENCES Character_card(id)
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE IF NOT EXISTS PowerUP_Cards_played (
  id INT NOT NULL AUTO_INCREMENT,
  PU_card_id INT DEFAULT NULL,
  amount INT DEFAULT 0,
  PRIMARY KEY (id),
  
  CONSTRAINT fk_played_PU_card FOREIGN KEY (PU_card_id) REFERENCES powerup_card(id)
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE VIEW Character_Ability AS
SELECT cc.id AS id,
 cc.name AS character_name, 
 a.amount AS attack, 
 a.cost AS abilityCost, 
 a.effect AS effect,
 cc.resistance,cc.health
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

CREATE VIEW Game_Resultado AS
SELECT
	p.name,
	g.player_id,
    g.num_round
    
FROM
    Game g 
JOIN Player p;


CREATE VIEW Cards_Played_Character AS
SELECT
	cp.character_card_id,
    cc.name,
    cp.amount
    FROM characters_Cards_played cp join character_card cc
    where cp.character_card_id=cc.id ORDER BY amount DESC;
  
CREATE VIEW PowerUp_Played_Character AS
SELECT
	Pc.PU_card_id,
    PUC.name,
    PC.amount
    FROM PowerUP_Cards_played PC join powerup_card PUC
    where PC.PU_card_id=PUC.id ORDER BY amount DESC
	LIMIT 5;


CREATE VIEW Player_Deck_Character AS
SELECT p.name AS player_name,
 dc.character_name, 
 dc.character_description
FROM Player p
JOIN Deck d ON p.id = d.player_id
JOIN Deck_Character dc ON d.id = dc.deck_id;


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
CREATE PROCEDURE Deck_pro (IN ID int)
BEGIN
SELECT cc.*
FROM Character_Ability cc
JOIN Deck ON cc.id IN (Deck.card1, Deck.card2, Deck.card3, Deck.card4, Deck.card5) where Deck.id=ID;
END $$
DELIMITER ; 

DELIMITER $$

CREATE PROCEDURE register_player(
    IN registered_name VARCHAR(45),
    IN registered_password VARCHAR(45)
)
BEGIN
    DECLARE username_exists INT;
    DECLARE p_id INT;

    SELECT COUNT(*) INTO username_exists
    FROM player
    WHERE name = registered_name;

    IF username_exists > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Ese nombre de usuario no se encuentra disponible';
    ELSE
        INSERT INTO player (name, password) VALUES (registered_name, registered_password);
        SELECT id INTO p_id FROM player WHERE name = registered_name;
        INSERT INTO deck (player_id, card1, card2, card3, card4, card5) VALUES (p_id, 1, 2, 3, 4, 5);
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
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Register_result(
	IN Player_ID int,
    IN Resultado int,
    OUT status_message VARCHAR(45)
)
BEGIN
	Insert into Game (player_id,num_round) values (Player_ID,Resultado);
    SET status_message = 'Game Registered succesfully';
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Save_Cards_used(
    IN ID_carta int,
    IN Amount int,
    OUT status_message varchar(64)
)
BEGIN
	Update Characters_Cards_played cc SET amount = amount + Amount WHERE ID_carta=cc.character_card_id;
    SET status_message = 'Cards updated succesfully';
END $$

DELIMITER ;

DELIMITER $$
CREATE PROCEDURE Save_PU_used(
	IN Player_ID int,
    IN ID_PU int,
    IN Amount int,
    OUT status_message varchar(255)
)
BEGIN
	insert into PowerUP_Cards_played (player_id,PU_card_id,amount) values (Player_ID,ID_PU, Amount);
    SET status_message = 'Cards registered succesfully';
END $$