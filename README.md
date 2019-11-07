# UnityHiringProject Space Invaders

 Made with Unity LTS 2017.4.34f1. I selected this specific stream to see how usuable this is for a common Unity user if he would decide to make a casual, simple 2D game.
 
 
 You can download Unity LTS here: https://unity3d.com/unity/qa/lts-releases
 
 # the Idea behind this
 
 The idea behind this project is to make it as unique as possible. That is why I've chosen to write this without any additional packages, only used sprites found online for graphics, no assets used.
 
- While making this game, I decided to take some assets from the suggested ones by Unity, music, shaders, textures, models for the game itself.

Remaking my Unity project, this used to cars, now remaking this to space invaders, since I had somehwta good level system implemented and gameobjects/other scripts which might be useable for space invaders.


Right now the main menu is in fixing stage, fix the starting menu "Menu" Scene made it responsive


# Features

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


- Main menu implemented with 4 buttons:



# What needs to be done/..
- Player should also die from catching (colliding with) enemies bullet.
- Array movemnt logic of the enemys spaceships
- Timer Script should be merged to GameEventController


Annoying things... 

well this took me an hour to figure out.. my movemnt of rigidbody is x y and not z, when using Vector3. This might be handy for anyone using this someday


# Workflow itself:

- To set up workflow used Favro board with "To do" "Doing" "Testing" and "Done" columns. I have not written any tests for my game.
