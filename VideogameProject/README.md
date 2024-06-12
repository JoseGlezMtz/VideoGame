# **Forest Adventure**

## _Game Design Document_

---

##### José Antonio González Martínez, Valentina González, Enrique Martínez

##
## _Index_

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## _Game Design_

---

### **Summary**

Forest Adventure" is a DBG that tells the story of the camp "_____", which one day is invaded by creatures from another dimension.... themselves. Our campers and staff have the responsibility to defend the camp at all costs.

### **Gameplay**

The game consists of a P v CPU where players will fight against the different creatures trying to attack their campsite. Before the match starts, you will be able to select 5 cards from the different characters that are available.

When the match begins, the user will be assigned to either one of the sides. Each player will have 2 characters on the board and 3 in their hand. Each character has different abilities and statistics that will define their fighting style. The user can use the abilities of the characters while the Energy Bar has enough energy, and they will also be able to switch a character at the cost of their turn. In addition, your campers will recieve assitance from different power ups to help them boost their atacks or damage their enemies.

The game will end when either your campers defeat all the monsters or lose their lives at battle. 
### **Mindset**

We want players to feel ambitious about progressing in the game. We aim for them to sense the ability to create their own strategies within the arena and to improve their proficiency in the game's mechanics.

## _Technical_

---

### **Screens**

1. Title Screen
    - Play Game
    - Build-Deck
    - Exit
2. Build-Deck
3. Play_game

**Temporary Title Screen**

![Title Screen](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/TitlePage.png)

**Playing Screen**

![In-Game Scene](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/game%20scene.png)

**Example of deck scene**


![Deck scene](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/deck%20scene.png)

This is where the player will create his deck. The 'Add' button is to add the selected character to his deck, the 'Remove' button to remove this character, and the 'Save' button is for saving your deck. The images of the characters that are at the bottom of the screen are the ones that are in your deck.


### **Controls**

The player will interact with all the UI with the mouse and the click

### **Mechanics**
![Mechanics Image](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/mechanics.png)

#### Before the game:
Before starting the game, the player have the option to configurate their deck of 5 characters, giving the option of creat a team based in the best strategy that the player considers, they have the options to choose 2 starting cards and 3 cards that they'll keep in their hand.


#### In-game:
At the strat of the match they will start with 2 cards in the field and the rest of the cards in the hand.

The fisrt instance, the play will optain a "Power-up" card, and will have two options:

1. Use it in a card that is on the field
2. Save it to use it in a later round
   1. Some cards have a limit toime to save it, maening that after specific number of rounds, they will disapper
   2. If the powr-ups deck run out of cards, it will shuffle and reestart again

After this, the player will have several option:

1. Attack the enemy: this attack will cost certain amount of energy.
2. Change a card: The player can swap a character with one card from their hand. The player can only perform this action if they haven't attacked an enemy card.
3. End turn: The player can end their turn if they finish all their movement options or if they want to conserve energy or execute any other strategic maneuver.

   
At the end of the turn, the energy bar will restore 20 points of energy. 

After the player's turn, the enemy will follow a similar flow, and the loop will reapeat.

## _Rounds Design_

Every time the player wins a round, there will reapered other cards of the enemy with ability boosts, this making more dificult the game every round you win

### **Game Flow**

1. The player register
2. Players' log-in
3. Create the deck
4. Enters battle

## _Development_


### **Abstract Classes / Components**
1.Register Manager
2.Login Manager
3.Buld_deck manager
4.BoardManager 
    1. Cards Manager
    2. Enemy Controller
    3. Power Ups Manager
4.Scene Manager
5.Conection with the API
6.Audio controller


## _Graphics_

**Card Template:**

![Wendy Card Design](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/wendy.png)

**Initial Character Models**
![Early character models](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/initialCharacterModels.png)


### **Style Attributes**
**Color Palette:**
![Color Palette](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/colorpalette_forestAdventure.png)

**Graphic Style:** The style that we decided to use is a 2d cartoony inspired by the animation of the Disney series Gravity Falls which is a simple style with minimal, flat shadows and a black outline for the key elements in the scene. 

### **Graphics Needed**

1. Cards
    1. Characters
    2. Power Ups
2. Backgrounds
    1. Main Page
    2. In-Game
    3. Deck Building
3. UI
    1. Buttons
    2. Energy Bar
    3. Win/Loss sign
    4. Character statistics (health, speed, resistance)
4. Animations

## _Sounds/Music_

---

### **Style Attributes**

We're looking for a soft music for the main menu, something that transmite a forest vibe, that make the player to feel inside the camp, but also in the match we want them to feel the tension of the fight, that the weight of the enemy traspass the sccreen.

### **Sounds Needed**

1. Effects
    1. Hit effect
    2. Change-card sound
    3. Select card effect
2. Feedback (pendiente)
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)



### **Music Needed**
(pendiente)




## _Schedule_

---

1. Week 1
    1. Unity
        1. Start cards in positions
        2. Attack and change mechanic
        3. Main menu
    2. Data base
        1.Creation of data base
2. Week 2
    1.Unity
        1.create Power ups
        2. 


