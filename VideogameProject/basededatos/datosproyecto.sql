USE fa_database;
INSERT INTO `Ability` (`id`, `amount`, `cost`, `effect`) VALUES
(1,  15, 30, 'curacion'),
(2,  10, 20, 'dano'),
(3,  10, 15, 'dano'),
(4,  25, 30, 'dano'),
(5,  15, 20, 'mejora_dano'),
(6,  20, 30, 'curacion'),
(7,  20, 20, 'dano'),
(8,  0, 0, 'revive'),
(9,  0, 0, 'revive'),
(10, 0, 0, 'revive'),
(11,  2, 0, 'escudo'),
(12,  15, 0, 'curacion'),
(13,  1, 0, 'escudo'),
(14,  1, 0, 'escudo'),
(15, 10, 0, 'mejora_dano'), 
(16,  5, 0, 'curacion'),
(17, 2, 0, 'bloquea_dano'),
(18, 12, 0, 'curacion'),
(19, 10, 0, 'restaura_energia'),
(20, 10, 0, 'mejora_resistencia'),
(21, 5, 0, 'mejora_dano'),
(22, 5, 0, 'mejora_dano'),
(23, 10, 0, 'restaura_energia'),
(24, 20, 0, 'mejora_dano'),
(25, 5,0, 'mejora_dano'),
(26, 5, 0, 'mejora_dano'),
(27, 5, 0, 'mejora_resistencia'),
(28, 5, 0, 'mejora_resistencia'),
(29, 6, 0, 'restaura_energia'),
(30, 10,0, 'mejora_dano'),
(31, 3, 0, 'bloquea_dano'),
(32, 10, 0, 'mejora_resistencia'),
(33, 1, 0, 'escudo'),
(34, 10, 0, 'curacion'),
(35, 20, 0, 'mejora_dano'),
(36, 70, 0, 'revive');

select * from Ability;
INSERT INTO `Character_card` (`id`, `name`,`nameAbility`, `description`, `ability`, `resistance`, `health`) VALUES
(1, 'Chef', 'Chef','Curacion con Queso', 1, 7, 100),
(2, 'Wendy', 'Campista Insectos','Rana Venenosa', 2, 3, 100),
(3, 'Brick', 'Campista Armas','Destruccion DIY', 3, 8, 100),
(4, 'Lumberjack', 'Leñador','Golpe de Lenador', 4, 9, 100),
(5, 'Coach', 'Entrenador','Pep-talk', 5, 14, 100),
(6, 'Lifeguard', 'Salvavidas','RCP', 6, 11, 100),
(7, 'Mike', 'Campista Rayos','Juicio Celestial', 7, 4, 100);



INSERT INTO `Powerup_card` (`id`, `name`, `description`, `ability`) VALUES
(8, 'Bombón', 'Board with cookies and chocolate to creat a smore.', 8),
(9, 'Cookie', 'Board with marshmellow and chocolate to creat a smore', 9),
(10, 'Chocolate', 'Board with cookies and marshmellow to creat a smore', 10),
(11, 'Tent', 'Protects a character by (2) turns', 11),
(12, 'First Aid Kit', 'Heals an ally by (15) hp', 12),
(13, 'Mosquito Repellent', 'Protects a character by (1) turn ', 13),
(14, 'Campfire', 'Protects a character by (1) turn ', 14),
(15, 'Encyclopedia', 'Increases  attack by (10) points', 15),
(16, 'Granola Bar', 'Restores (5) hp', 16),
(17, 'Flashlight', 'Blinds an opponent by (2) turns', 17),
(18, 'Bandage', 'Restores health by (12) hp', 18),
(19, 'Energy Drink', 'Reduces the energy cost of a card by (10) points',19),
(20, 'Flannel Shirt', 'Increases  resistance of a card by (10) points', 20),
(21, 'Wooden Sword', 'Increases  attack of a card by (5) points', 21),
(22, 'Bow', 'Increases  attack of a card by (5) points', 22),
(23, 'Badge', 'Reduces energy cost of a card by (10) points', 23),
(24, 'Sharpener', 'Increases  attack of a card by (20) points', 24),
(25, 'Dumbbell', 'Increases  attack of a card by (5) points', 25),
(26, 'Strange Stone', 'Increases atack of a card by (5) points', 26),
(27, 'Frying Pan', 'Increases resistence of a card by (5) points', 27),
(28, 'Candy', 'Increases resistence of a card by (5) points', 28),
(29, 'Coffee', 'Reduces energy cost of a card by (6) points', 29),
(30, 'Arrows', 'Improves damage of a card by (10) points', 30),
(31, 'Fishing Net', 'Traps an opponent for (3) turns so he cannot attack', 31),
(32, 'Spatula', 'Improves resistence of a card by (10) points ', 32),
(33, 'Jetski', 'Allows a character to dodge an enemy atack for (1) turn', 33),
(34, 'Float', 'Heals an ally by (10) points', 34),
(35, 'Telescope', 'Increases atack of a card by (20) points', 35),
(36, 'Smore', 'Revives a character, giving him 70 points of HP.', 36);

INSERT INTO Characters_Cards_played (character_card_id) VALUES (1), (2), (3), (4), (5), (6), (7);

INSERT INTO powerup_cards_played (PU_card_id) VALUES 
(8), (9), (10), 
(11), (12), (13), (14), (15), (16), (17), (18), (19), (20), 
(21), (22), (23), (24), (25), (26), (27), (28), (29), (30), 
(31), (32), (33), (34), (35), (36);

SELECT * FROM Characters_Cards_played;

