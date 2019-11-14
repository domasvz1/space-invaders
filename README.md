# [The Huge update has been done] [Full first release is made] Made everything that was on my list, last commits

 * Made with Unity LTS 2017.4.34f1 (That means that the project works on all supported Unity versions 2018.4, 2019.. etc)
 * I have selected this specific stream to see how usuable Unity is for a common user to make a casual 2D game;
 * The game is made for 600 x 800 ratio, but can be set to any other ratios in the Game window (in that case black solid borders from both sides will be added)
 
 You can download theused Unity LTS stream here: https://unity3d.com/unity/qa/lts-releases
 
 # The idea behind this
 
 * The idea behind this project is to make it as unique as possible. I have conncted Space Invaders and Space Shooters in one game;
 
 * I have chosen to write this without any additional packages, only used sprites found online for graphics, and no assets used, trying to make this as raw as possible;
 
* While making this game, I decided to take some assets from the suggested ones by Unity, music, shaders, textures, models for the game itself;

# Features

Menu from feature side (Includes 4 scenes)
----------------------------------
- Menu Scene 4 buttons:
* Play
* Best Scores
* Instructions
* Quit

-- Play Scene
* Leads player to a "Loading Before Game Scene" which lets know the player that the game is about to begin.

-- Best Scores Scene
* Shows up on the Menu scene as an option, displays the top 5 scores

-- Insctruntion Scene 
* Shows up on the Menu scene as an option, displays the controls to the game and has some information about the original game's creator)

-- Quit option
* Shows up on the Menu scene as a quit option, quits the built application (will not quit in Editor)


InGame features (Includes 1 scene "Level1")
----------------------------------

--Main Gameplay
- You get to control you spaceship with Vertical/Horizontal movement a/d buttons, left/rigth sticks and shooting with space button.
- The game starts with the set waves of enemies (right now it it set to 1, but generally with the upcoming releases it will be ~6, the waves can be set manually);
 - Then the boss spawns;

 -- Powerups
  * In the "How to play" option in the Main Menu you can find the instrcutions. After picking the pickups you will need to press "Y",  "U" and "I" buttons depending on which powerup you pick:
  
 There are 3 types of powerups
 * [Y BUTTON] Ship shooting speed boost (gives you extra speed while shooting bullets, it will last/blink in the right top corner fro 10 seconds);
 * [U BUTTON] Ship speed boost (gives your spaceship extra speed and will last/blink in the right top corner fro 10 seconds);
 * [I BUTTON] Shield pickup (it will give you 2 additional health and blink in the inventory untill you have that health);

# Scripting side
--------------------------------------------------------------------

# .Json filewriter/filereader
- Main 3 classes created for it:
  * DataHandler (the most important class), SortHighscoresArray(), InsertFreshData(), IsScoreInTopList(int checkedScore) methods which is used for determining the TOP 5 players and checking if the current highscore can be applicable for TOP 5;
- To the .json DataHandler class added and two Serializable classes PlayerScore() (which contains players data for hgishcore -> name, Score, and place) and JsonWrapper which is contained as the object and has all the arrays and data inside (made this so that .json file would look better);
- DataHandler and 2 wrappers, to make Json look like most of jsomn files look;
- Implemented Highscores, .json read/write to file;
- Made sort method, which checks where;
- When the game is over, the score is passed to file writer to check if the score is writable to file;


# Game Running (Game Events)
- GameEvent GameObject has class GameEventControlelr which is responsible for how game is running;
- Enemy objects are constantly moving down to Y axis.
- After the set waves of spaceship spawn, then the boss spawns;
- The spaceship's waves speed increases with every wave
- Implemneted GameOver method, it is being ivoked when Player Object collides with enemy spacehip;
- Implemneted Winning method, it is being ivoked when Player Object defetas (the selected amount of enemy waves) and the boss;
- In EnemyController implemnted random shooting repeat rate and shooting delay with Math.Random (thi means that every enemy spaceship will have the different rates set for them defending on Math function);
- Enemies now doesnt spawn up until the starting timer is over;
- Implmented enemies waves spawn logic and spawn script with ennemies shooting and reacting;


# Collision logic and detection
- Added the collider to:
 * Player, so that he would be destroyed when triggered by enemies ship or bullets;
 * Enemy, so that he would be destroyed when triggered by players ship or players bullets, but not enemys bullets, ships or boss;
 * Boss, so that he would be destroyed when triggered by players bullets, if he has less than 1 health.
- Enemies collision and destruction is implemented (Enemies can collide with each other but they won't die, they recognize each other by tags addded to GameObjects).
- Finished boundary collision logic around the map, prolonged it, since I made Players movemnt with serilizable struct boundaries and math Clamp I let myself go wider with the boundaries (if the bullets would fly to offset).


# UI 

- Implemented "LoadOtherScene" class which laods the passed Scene.
- Level doesn't start up until the timer count to 3 (timer is displayed to the player), the classes that spawn or move objects are fronzen when the timer is ticking and then they are set to active.
- Implemented "End Game text" and depending on which state of the game is that, it shows "Game Over" if you lose or "You Win" if you win
- UI elements and being deactivated in the beginning and spawned only when necessary;
- When the game is over, added "Enter your name" inpu field. When its empty, it will not allow player to continue, but if player preses continue, it fils the field with string "Random Player" text and then player can press submit button;


# Particles, graphics and materials 

- Added music when:
 * Game is Over;
 * When the bullet is being shot;
 * In main menu Scene is active;
 * When the game is won;
 * When the agme is loading in "LoadingBeforeGame";
 
 - Added explosion particles when:
  * Players spaceship explodes;
  * Enemy spaceship explodes;


# Memory/ unused object checking in the scene

- When the Gameover is called, the code is finding active objects with set up tags left tags to enemy bullets and player bullets and free memory;
- Enemy object are put in the array and being invokeed, after collision they are being destroyed;
- Some UI objects that are not necessary in the Scene are made to prefabs and only spawned when used;
- Finding Objects with Tags and putting them into array with built in method, the destroying them;
- ClearEnemyGear() run foreach cycle with given string Tags array and uses FreeLeftObjects() method to destroy left unsued Objects.
- Made an easily expotable Explosion, Boss, Enemy, Player, Bullet prefabs, only Player object is used from these. Others -> Enemy, Boss, Bullets are exported from the prefabs folder and added to GameEventController script;


-----------------------------------------------------------------------

# Whats really missing from 
- Standarts on code
- Improved pcikups and pickup documentation


# Fixed some found bugs
- When the game ends objects with EnemyBullet tag not all the objects are fully destroyed;
- Pulsing effect text doesn't work on EndGame text in levels;
- Inventory pickups stays flashing even after player dies


# Annoying things that I have figured out figured out while doing...

Well, this took me an hour to figure out.. my movemnt of rigidbody is x y and not z, when using Vector3. This might be handy for anyone using this someday

NullReferences when calling Object when its not there. I had a ton of cases where the object created on top of its collider, but the collider was disabled, but since the script was running, it was calling the disabled object throwing the null refrence.
If you have a class (script) atatched to your GameObject and you want to use another class (script), Unity will allow you to use that in code, but if that classs(script) is not attachedto your project you will get a NullReference, what helped in that common situation is FindObjectWithTag method as it overcomes the attachemnet issue and find obejct with tags.


# Workflow itself:

- [Version control]: used Github, gitDesktop, working in cycles.
- [Tasks Cycle]: used Favro board with "To do" "Doing" "Testing" and "Done" columns in which cards with tasks were put.
- [Tests] I have not written any tests for my game.
- [Documnetation] Started documenting this from git commits and favro cards as much as I could, since there was teh lack of time, the workflow had to be pushed to nights when the documentation decreased.
