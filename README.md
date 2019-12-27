# SnakesAndLadders
This is the Red team Snakes and Ladders party game.


## Graphics

All the graphics of snakes, ladders and game tiles are in the GameBoard object 

<i> (I put all the graphics and the logic in different places, so changing the graphics you just need to add some sprites under the GameBoard object)</i>

## Level Design

All the game board level design are in the GameBoard script .

To add a game tile: (Creating a snake or ladder is similar)
 
1. go to the project panel, under "Assets/ScriptableObjects/", right click, go to "Create/GameBoardItem/GameTile", name it GameTile2, GameTile3...
2. change the properties in the new scriptable object
3. Click on the GameBoard object in the scene. At the right side, under the GameBoard script, you increase the array "GameTiles" size by 1, and put the scriptable object you just created to it

To pass success or failure to the main scene
In your mini game scene, before switching to the mainscene,
1. Call SaveSystem.UpdateMiniGameData(bool isSuccessful)        This will update the data so the main game will know if the player wins or loses the mini game
2. Call LevelLoader.Instance.LoadMainGame(); .                  This will load the main game probably

I created a minigame scene called TestGameScene so you can take a look.

## Testing
Press 1 to 6 to roll whatever point you like. Press 1 to roll a 1. Press 2 to roll a 2...

<i> For the graphics, you just need to add some sprites separately in the children of GameBoard. </i>

It should work. If it doesn't just report the bugs to me (Soon) in discord. 
