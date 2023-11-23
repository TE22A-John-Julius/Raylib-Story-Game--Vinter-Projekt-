using System.Numerics;
using Raylib_cs;


//List of weapons
string[] weapons = {"LongSword","Staff"};

//Weapon Stats

//LongSword
int lSStrength = 12;
int lSAttackSpeed = 4;

//Staff
int sStrength = 9;
int sAttackSpeed = 5;




Vector2 movement = new Vector2(0,0);

//Scenes
string scene = "start";


while (!Raylib.WindowShouldClose())
{
    if (scene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }
    if (scene == "game")
    {
        // Movement ONG
        movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP | KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN | KeyboardKey.KEY_S))
        {
            movement.Y = +1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT | KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT | KeyboardKey.KEY_D))
        {
            movement.X = +1;
        }
    }

    /*
     Scene statistics
     if scene = dialogue
     then space button press change scene


    */

    /* PSEUDO CODE
    - Start Screen "Press (insert key name) to wake up"
    - Draw character dialogue screen
        "Where am I?"
        "I need to find a way out of here"
    - Draw start story scene
    - Draw the Weapon of choice screen
    - Continuing door to rest of dungeon
    */


    Raylib.BeginDrawing();
    
    

    Raylib.ClearBackground(Color.WHITE);



    Raylib.EndDrawing();
}