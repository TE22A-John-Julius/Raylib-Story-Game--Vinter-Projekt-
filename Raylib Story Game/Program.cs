using System.Numerics;
using Raylib_cs;


//Game Window
Raylib.InitWindow(1200,1000, "Adventure Game");
Raylib.SetTargetFPS(60);

//List of weapons
string[] weapons = {"LongSword","Staff"};

//Weapon Stats

//LongSword
int lSStrength = 12;
int lSAttackSpeed = 4;

//Staff
int sStrength = 9;
int sAttackSpeed = 5;

//Character variables
Rectangle characterRect = new Rectangle(200, 300, 64, 64);

//Movement Variables
Vector2 movement = new Vector2(0.1f,0.1f);
float speed = 8;

//Scenes
string scene = "start";


while (!Raylib.WindowShouldClose())
{
    
    if (scene == "game")
    {
        // Movement ONG
        movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            movement.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            movement.Y = +1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            movement.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            movement.X = +1;
        }
        
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * speed;
        }
        characterRect.X += movement.X;
        characterRect.Y += movement.Y;
    }
    else if (scene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "dialogue";
        }
    }
    else if (scene == "dialogue")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "";
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
   
    if (scene == "game")
    {
        Raylib.ClearBackground(Color.DARKGRAY);
        Raylib.DrawRectangleRec(characterRect, Color.BLUE);
    } 
    else if (scene == "start")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("PRESS SPACE TO WAKE UP", 400, 500, 32, Color.WHITE);
    }
    else if (scene == "dialogue")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawCircle(10, 300, 30,Color.WHITE);
    for (int i = 0 ; i < 10; i++)
    {
        Raylib.DrawText("W-where am I?",400, 800,32, Color.WHITE);
    }
    }
    Raylib.EndDrawing();
}

