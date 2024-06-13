USE fa_database;

INSERT INTO Player (name, password, level) VALUES
('Alice', 'password123', 5),
('Bob', 'securepass', 3),
('Charlie', 'mypassword', 4),
('Diana', 'pass1234', 2),
('Eve', 'evepass', 1);

INSERT INTO Game (player_id, num_round) VALUES
(1, 1),
(2, 6),
(3, 3),
(4, 5),
(5, 2);

INSERT INTO Deck (player_id, card1, card2, card3, card4, card5) VALUES
(1, 1, 2, 3, 4, 5),
(2, 2, 3, 4, 5, 6),
(3, 3, 4, 5, 6, 7),
(4, 4, 5, 6, 7, 1),
(5, 5, 6, 7, 1, 2 );

INSERT INTO Characters_Cards_played (character_card_id, amount) VALUES 
(1, 3), 
(2, 5), 
(3, 2), 
(4, 7), 
(5, 1), 
(6, 4), 
(7, 6);

INSERT INTO PowerUP_Cards_played (PU_card_id, amount) VALUES 
(8, 10), 
(9, 8), 
(10, 12), 
(11, 3), 
(12, 5), 
(13, 6), 
(14, 7), 
(15, 9), 
(16, 4), 
(17, 2), 
(18, 1), 
(19, 11), 
(20, 13), 
(21, 6), 
(22, 5), 
(23, 4), 
(24, 3), 
(25, 2), 
(26, 1), 
(27, 9), 
(28, 8), 
(29, 7), 
(30, 6), 
(31, 5), 
(32, 4), 
(33, 3), 
(34, 2), 
(35, 1), 
(36, 14);