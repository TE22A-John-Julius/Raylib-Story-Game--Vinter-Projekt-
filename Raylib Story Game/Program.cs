using System.Numerics;
using Raylib_cs;


//Game Window
Raylib.InitWindow(1200,1000, "Adventure Game");
Raylib.SetTargetFPS(60);

//Textures
Texture2D background = Raylib.LoadTexture("dungeon.png");
Texture2D spriteSheet = Raylib.LoadTexture("sprite.png");
Texture2D invertedSpriteSheet = Raylib.LoadTexture("inverted_sprite.png");
Texture2D sky = Raylib.LoadTexture ("sky.png");
Texture2D slimeSpriteSheet = Raylib.LoadTexture("slime_spritesheet.png");
Texture2D dead = Raylib.LoadTexture("BloodOverlay.png");
Texture2D newDoorTexture = Raylib.LoadTexture("door.png");

//List of weapons
string[] weapons = {"LongSword","Staff"};

//Weapon Stats
//LongSword
int lSStrength = 12;
int lSAttackSpeed = 4;

//Staff
int sStrength = 9;
int sAttackSpeed = 5;

//Weapon Draws
Rectangle longSword = new Rectangle(200,300, 32,64);
Rectangle staff = new Rectangle(500,300, 32, 64);

//Character variables
Rectangle characterRect = new Rectangle(250, 300, 64, 64);

//Movement Variables
Vector2 movement = new Vector2(0.1f,0.1f);
float speed = 8;




//Animation
//slimes
int maxSlimes = 6;
Rectangle[] slimeRects = new Rectangle[maxSlimes];
Vector2[] slimeMovements = new Vector2[maxSlimes];
int[] slimeCurrentFrames = new int[maxSlimes];
int[] slimeNumberOfFrames = new int[maxSlimes];
float[] slimeFrameTime = new float[maxSlimes];
float[] slimeFrameCounters = new float[maxSlimes];

//sprite sheet dimensions
int slimeFrameWidth = 190;
int slimeFrameHeight = 190;

//Cuties movements
for (int i = 0; i < maxSlimes; i++)
{
    float x = new Random().Next(100, Raylib.GetScreenWidth() - 100);
    float y = new Random().Next(100, Raylib.GetScreenHeight() - 100);    
    slimeRects[i] = new Rectangle(x, y, 32, 32);
    slimeMovements[i] = new Vector2((float)(new Random().NextDouble() * 2 - 1), (float)(new Random().NextDouble() * 2 - 1));
    slimeCurrentFrames[i] = 0;
    slimeNumberOfFrames[i] = 6;
    slimeFrameTime[i] = 0.4f;
    slimeFrameCounters[i] = 0.0f;
}

//sprite animation variables
int frameWidth = spriteSheet.Width / 4;
int frameHeight = spriteSheet.Height;
int currentFrame = 0;
int numberOfFrames = 4;
float frameTime = 0.1f;
float frameCounter = 0.1f;
int currentFrameLeft = 0;
float frameCounterLeft = 0.1f;
float frameTimeLeft = 0.1f;

//Glitch anim
float glitchTimer = 0;
float glitchDuration = 1f;

//Scenes
string scene = "start";

bool gameOver = false;
//Mapping
Rectangle door = new Rectangle(1000, 600, newDoorTexture.Width, newDoorTexture.Height);
Rectangle grassPlaceHolder = new Rectangle (0, 900, 1200, 200);


while (!Raylib.WindowShouldClose())
{
    //Sprite right animation fr
     frameCounter += Raylib.GetFrameTime();
    if ((movement.Y < 0 || movement.X > 0) && frameCounter >= frameTime && movement.Length() > 0)
    {
        currentFrame = (currentFrame + 1) % numberOfFrames;
        frameCounter = 0.0f;
    }
    // inverted sprite 
    else if ((movement.Y > 0 || movement.X < 0) && frameCounter >= frameTime && movement.Length() > 0)
    {
        currentFrame = (currentFrame + 1) % numberOfFrames;
        frameCounter = 0.0f;
    }
    Rectangle sourceRec = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
    Rectangle sourceRecLeft = new Rectangle(currentFrameLeft * frameWidth, 0, frameWidth, frameHeight);
    

    
if (movement.X < 0)
{
    Raylib.DrawTextureRec(invertedSpriteSheet, sourceRecLeft, new Vector2(characterRect.X, characterRect.Y), Color.WHITE);
}
    if (scene == "game" && !gameOver)
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
        characterRect.X += (int)movement.X;
        characterRect.Y += (int)movement.Y;

        // Update Slime Movement and Animation
         for (int i = 0; i < maxSlimes; i++)
            {
              slimeFrameCounters[i] += Raylib.GetFrameTime();    
              if (slimeFrameCounters[i] >= slimeFrameTime[i])
              {
                  slimeCurrentFrames[i] = (slimeCurrentFrames[i] + 1) % slimeNumberOfFrames[i];
                  slimeFrameCounters[i] = 0.0f;

              slimeRects[i].X += slimeMovements[i].X;
              slimeRects[i].Y += slimeMovements[i].Y;
            }
        //Slime Collisions Check
         if (Raylib.CheckCollisionRecs(characterRect, slimeRects[i]))
         {
            gameOver = true;
         }

        //door collision check
        if (Raylib.CheckCollisionRecs(characterRect, door)){
            scene = "outside";
        }
            }
            
            door.X = 1000;
            door.Y = 600;
    }
    else if (scene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
            
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
   
    if (scene == "game" && !gameOver)
    {
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.ClearBackground(Color.DARKGRAY);
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.DrawText("YOU HAVE BEEN CHOSEN TO BE THIS WORLDS HERO",200, 100, 32, Color.WHITE);
        // door Texture
        Raylib.DrawTextureRec(newDoorTexture, door, new Vector2(door.X, door.Y), Color.WHITE);
       
        //character movement drawing
        if (movement.X < 0 || movement.X < 0)
        {
            Raylib.DrawTextureRec(invertedSpriteSheet, sourceRec, new Vector2(characterRect.X, characterRect.Y), Color.WHITE);
        }
        else
        {
            Raylib.DrawTextureRec(spriteSheet, sourceRec, new Vector2(characterRect.X, characterRect.Y), Color.WHITE);
        }
        // slimes movements drawing
        for (int i = 0; i < maxSlimes; i++)
        {
            slimeFrameCounters[i] += Raylib.GetFrameTime();    
            if (slimeFrameCounters[i] >= slimeFrameTime[i])
            {
                slimeCurrentFrames[i] = (slimeCurrentFrames[i] + 1) % slimeNumberOfFrames[i];
                slimeFrameCounters[i] = 0.0f;
                slimeRects[i].X += slimeMovements[i].X;
                slimeRects[i].Y += slimeMovements[i].Y;
            }
        
            // Drawing slimes
            Rectangle slimeSourceRec = new Rectangle(slimeCurrentFrames[i] * slimeFrameWidth, 0, slimeFrameWidth, slimeFrameHeight);
            Vector2 slimePosition = new Vector2(slimeRects[i].X, slimeRects[i].Y);
            Raylib.DrawTextureRec(slimeSpriteSheet, slimeSourceRec, slimePosition, Color.WHITE);
        }
    }
    else if (scene == "outside")
    {
        Raylib.DrawTexture(sky, 0,0, Color.WHITE);
        Raylib.DrawRectangleRec(grassPlaceHolder, Color.GREEN);
        Raylib.DrawText("YOUR JOURNEY BEGINS NOW",120, 500, 64, Color.WHITE);
        Raylib.DrawText("(to be continued)", 500, 700, 32, Color.BLACK);
    }

    else if (gameOver){
        Raylib.DrawTexture(dead, 0, 0, Color.RAYWHITE);
        Raylib.DrawText("THE HERO HAS FALLEN",270, 500, 64, Color.WHITE);
    }

    else if (scene == "start") {
        Raylib.ClearBackground(Color.BLACK);
        //Make the text glitchy ong
       if (Raylib.GetTime() - glitchTimer > glitchDuration)
        {
            glitchTimer = (float)Raylib.GetTime();
            
            // Invert the colors to create a glitch effect
            Color glitchColor = new Color(
                (int)Raylib.GetRandomValue(0, 255),
                (int)Raylib.GetRandomValue(0, 255),
                (int)Raylib.GetRandomValue(0, 255),
                255
            );
        
            Raylib.DrawText("PRESS SPACE TO WAKE UP", 400, 500, 32, glitchColor);
        }
    
        
    
    }
    
    Raylib.EndDrawing();
    }