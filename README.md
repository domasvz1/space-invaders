# [The Huge update has been done] [Full first release is made] Made everything that was on my list, last commits

 * Made with Unity LTS 2017.4.34f1.
 * I selected this specific stream to see how usuable Unity is for the user if he would decide to make a casual 2D game;
 * The game is made for 600 x 800 ratio, but can be set to any other ratios in the Game window (in that case black solid borders from both sides will be added)
 
 
 You can download Unity LTS here: https://unity3d.com/unity/qa/lts-releases
 
 # The idea behind this
 
 The idea behind this project is to make it as unique as possible. I have conncted Space Invaders and Space Shooters in one game:
 - The game starts with the set waves of enemies (right now it it set to 1, but generally with the upcoming releases it will be ~6, the waves can be set manually);
 - Then the boss spawns;
 
 Pickups (there are 3 types of pickups, which give you slight advantge and when pickup up they show up in the right top of your corner):
 - Shield pickup (it will give you 2 additional health and blink untill you have that health)
 - Ship speed boost (gives your spaceship extra speed and will last/blink in the right top corner fro 10 seconds)
 - Ship shooting speed boost (gives you extra speed while shooting bullets, it will last/blink in the right top corner fro 10 seconds)
 
 
I have chosen to write this without any additional packages, only used sprites found online for graphics, and no assets used, trying to make this as raw as possible.
 
- While making this game, I decided to take some assets from the suggested ones by Unity, music, shaders, textures, models for the game itself.

Remaking my Unity project, this used to cars, now remaking this to space invaders, since I had somehwta good level system implemented and gameobjects/other scripts which might be useable for space invaders.


Right now the main menu is in fixing stage, fix the starting menu "Menu" Scene made it responsive


# Features

- Implemented Highscores, .json read/write to file.

- Started documenting this from git commits

Implemented UI options in level:
- Now the game doesnt begin up unti the set timer.
- The objects are fronzen when the timer is ticking and then they are set to active.
- After the game the objects are being destroyed (therefore freeing up the memory).
- Finding Objects with Tags and putting them into array with built in method, the destroying them.
- Implemneted GameOver method, it is being ivoked when Player Object collides with enemy spacehip.
- Implemented restart Game scene and quit to main menu option

- Added music to explosions and shots to players and enemies spaceships;
- Implmented enemy's spawn logic and spawn script with ennemies shooting and reacting.
- Enemies collision and destruction is implemented (Enemies can collide with each other but they won't die, they recognize each other by tags addded to GameObjects).
- Made an easily expotable Explosion, Enemy, Player, Bullet prefabs;
- Finished boundary collision logic around the map, prolonged it, since I made Players movemnt with serilizable struct boundaries and math Clamp I let myself go wider with the boundaries (if the bullets would fly to offset).
- Added GameEventController Gameobject which will run my game on the whole.

--

- Added the collider to player, so that he would die when triggered by enemies ship bullet;
- Implemented Enemy ships into GameObject array logic;
- Enemies now doesnt spawn in the begining when the timer is ticking (they are being deactivated)
- Connected Timer script to GameEventController and got rid of Timer script
- When the Gameover is called, the code is finding active objects with set up tags left tags to enemy bullets and player bullets and free memory
- Now the first shot from every enemy spaceship will be called with different fire rates
- In GameEventController implemnt random shooting rates set at Start() method
- There's an array object for deactivated object to reveal.
- Implementedactive Enem GameObject array movement down the map
- Added explosion on enemy ship collide
- Created new explosion prefab and tweaked its styling

- Player dies from catching enemies bullet and colliding with enemy sapceships;
- Array movemnt logic of the enemys spaceships


Annoying things figured out... 

Well, this took me an hour to figure out.. my movemnt of rigidbody is x y and not z, when using Vector3. This might be handy for anyone using this someday

NullReferences when calling Object when its not there. I had a ton of cases where the object created on top of its collider, but the collider was disabled, but since the script was running, it was calling the disabled object throwing the null refrence.


# Workflow itself:

- To set up workflow used Favro board with "To do" "Doing" "Testing" and "Done" columns. I have not written any tests for my game.
