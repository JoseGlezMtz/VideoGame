# **Forest Adventure**

## _Game Design Document_

---

##### **Copyright notice / author information / boring legal stuff nobody likes**

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

Forest Adventure" is a DBG that tells the story of the camp "_____", which one day is invaded by creatures from another dimension. Our campers and staff have the responsibility to defend the camp at all costs.

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
    1. Play Game
    2. Build-Deck
    3. Exit
2. Build-Deck
3. Play_game

**Temporary Title Screen**

![Title Screen](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/TitlePage.png)


**Example of Playing Screen**
![In-Game Scene](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/Assets/ejemploPantalla.png)

### **Controls**

The player will interact with all the UI with the mouse and the click

### **Mechanics**
![Mechanics Image](https://github.com/JoseGlezMtz/VideoGame/blob/main/VideogameProject/mechanics.png)

#### Before the game:
Before starting the game, the player have the option to configurate their deck of 5 characters, giving the option of creat a team based in the best strategy that the player considers, they have the options to choose 2 starting cards and 3 cards that they'll keep in their hand.

#### In-game:
At the strat of the match they will start with 2 coards in the field and the rest of the cards in the hand.

The fisrt instance, the play will optain a "Power-up" card, and will have two options:

1. Use it in a card that is on the field
2. Save it to use it in a later round
   1. Some cards have a limit toime to save it, maening that after specific number of rounds, they will disapper
   2. If the powr-ups deck run out of cards, it will shuffle and reestart again

After this, the player will have several option:

1. Attack the enemy: this attack will cost certain amount of energy, choosing between the normal or special attack that have each character
2. Change a card: the player can change a character with one card of their hand, this will cost the end of the turn of the player
3. End turn: The player can just end his turn in order to save energy or strategic descition that the player wants to do

At the end of the turn, the energy bar will restore a certain amount of energy

After the player's turn, the enemy will follow a similar flow, and the loop will reapeat.

## _Level Design_

---

_(Note : These sections can safely be skipped if they&#39;re not relevant, or you&#39;d rather go about it another way. For most games, at least one of them should be useful. But I&#39;ll understand if you don&#39;t want to use them. It&#39;ll only hurt my feelings a little bit.)_

### **Themes**

1. Forest
    1. Mood
        1. Dark, calm, foreboding
    2. Objects
        1. _Ambient_
            1. Fireflies
            2. Beams of moonlight
            3. Tall grass
        2. _Interactive_
            1. Wolves
            2. Goblins
            3. Rocks
2. Castle
    1. Mood
        1. Dangerous, tense, active
    2. Objects
        1. _Ambient_
            1. Rodents
            2. Torches
            3. Suits of armor
        2. _Interactive_
            1. Guards
            2. Giant rats
            3. Chests

_(example)_

### **Game Flow**

1. Player starts in forest
2. Pond to the left, must move right
3. To the right is a hill, player jumps to traverse it (&quot;jump&quot; taught)
4. Player encounters castle - door&#39;s shut and locked
5. There&#39;s a window within jump height, and a rock on the ground
6. Player picks up rock and throws at glass (&quot;throw&quot; taught)
7. … etc.

_(example)_

## _Development_

---

### **Abstract Classes / Components**

1.Menu manager
2.Buld_deck manager
3.BoardManager 
    1. Cards Manager
4.Scene Manager
5.getCards

_(example)_


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

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (dirt floor)
    2. Sharper Footsteps (stone floor)
    3. Soft Landing (low vertical velocity)
    4. Hard Landing (high vertical velocity)
    5. Glass Breaking
    6. Chest Opening
    7. Door Opening
2. Feedback
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_


## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_
