using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ToeJam_Earl.Graphics;

namespace ToeJam_Earl;

public class Game1 : Core
{
    private AnimatedSprite _ToeJam0;

    private Texture2D _ToeJamTexture;
    private Animation _animationUp;
    private Animation _animationDown;
    private Animation _animationLeft;
    private Animation _animationRight;
    private Animation _animationIdle;
    private Texture2D _ToeJamSneak1;
    private TextureRegion _sneakRegion;

    private List<GameObject> gameObjects;

    private Vector2 _ToeJam0Position;
    private Vector2 _ToeJam0Velocity;

    private Vector2 _giftPosition;
    private Vector2 _giftVelocity;

    private Vector2 _itemPosition;

    private bool itemsCollected = false;

    //private bool presentsMenuOpen = false;
    private bool mapMenuOpen = false;

    private bool isPaused = false;

    private float npcRotate = 0f;

    //Use bool to open the list with B and uses bool to see it if its open or not
    private bool listOpen = false;

    private Camera2D camera;

    private KeyboardState previousKeyBoardState;

    SoundEffectInstance boomSoundInstance;

    private AudioManager audioManager;
    private bool isPlaying = false;
    Audio boomInstance;
    Audio burpInstance;
    private float pitch = 0.75f;
    private float volume = 0.5f;

    SpriteFont font;

    // Speed multiplier when moving.
    private const float speedToeJam = 1.0f;

    private Texture2D NPC_1;
    private Texture2D Enemy_1;
    private Texture2D Items_1;
    private Texture2D Items_2;
    private Texture2D Elevator_1;
    private Texture2D UIelement_1;

    private Rectangle enemySourceRect = new Rectangle(8, 54, 32, 24);
    private Rectangle npcSourceRect = new Rectangle(10, 73, 59, 39);
    private Rectangle elevatorSourceRect = new Rectangle(4, 6, 32, 54);
    private Rectangle itemsSourceRect = new Rectangle(137, 10, 16, 16);
    private Rectangle itemsSourceRect_1 = new Rectangle(231, 77, 18, 11);
    private Rectangle uiSourceRect = new Rectangle(8, 88, 150, 31);
    private Rectangle uiSourceRect_1 = new Rectangle(178, 47, 172, 35);
    private Rectangle listSourceRect = new Rectangle(336, 9, 321, 115);
    // Add this constant field to the Game1 class to define MOVEMENT_SPEED
    private const float MOVEMENT_SPEED = 2.0f;

    public static float TotalSeconds { get; set; }
    public PatrolMovement MoveAI { get; private set; }

    public Game1() : base("ToeJam and Earl Project", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();

        _giftPosition = new Vector2(_ToeJam0.Width + 10, 0);

        AssignRandomGiftVelocity();

        _ToeJam0Position = new Vector2(0, 0);

        camera = new Camera2D();
    }

    protected override void LoadContent()
    {

        _ToeJamTexture = Content.Load<Texture2D>("images/ToeJam_Transparent");
        //_ToeJam0Position = Vector2.Zero;

        font = Content.Load<SpriteFont>("fonts/DefaultFont");

        TimeSpan delay = TimeSpan.FromMilliseconds(1000);

        _animationDown = new Animation(
            new List<TextureRegion>
            {
                new TextureRegion(_ToeJamTexture, 16, 80, 30, 42),
                new TextureRegion(_ToeJamTexture, 52, 80, 30, 40),
                new TextureRegion(_ToeJamTexture, 81, 80, 30, 36),
                new TextureRegion(_ToeJamTexture, 121, 80, 30, 40),
                new TextureRegion(_ToeJamTexture, 150, 80, 30, 40),
                new TextureRegion(_ToeJamTexture, 185, 80, 30, 36),
            },
            delay);

        _animationUp = new Animation(
            new List<TextureRegion>
            {
                new TextureRegion(_ToeJamTexture, 16, 130, 33, 37),
                new TextureRegion(_ToeJamTexture, 52, 130, 33, 36),
                new TextureRegion(_ToeJamTexture, 88, 130, 33, 38),
                new TextureRegion(_ToeJamTexture, 124, 130, 33, 37),
                new TextureRegion(_ToeJamTexture, 155, 130, 33, 37),
                new TextureRegion(_ToeJamTexture, 185, 130, 33, 37),
            },
            delay);

        _animationRight = new Animation(
            new List<TextureRegion>
            {
                new TextureRegion(_ToeJamTexture, 266, 130, 35, 37),
                new TextureRegion(_ToeJamTexture, 302, 130, 32, 35),
                new TextureRegion(_ToeJamTexture, 334, 130, 32, 35),
                new TextureRegion(_ToeJamTexture, 373, 130, 32, 35),
                new TextureRegion(_ToeJamTexture, 408, 130, 34, 36),
                //new TextureRegion(_ToeJamTexture, 443, 130, 34, 36),
            },
            delay);

        _animationLeft = new Animation(
            new List<TextureRegion>
            {
                new TextureRegion(_ToeJamTexture, 266, 80, 35, 37),
                new TextureRegion(_ToeJamTexture, 302, 80, 32, 35),
                new TextureRegion(_ToeJamTexture, 334, 80, 32, 35),
                new TextureRegion(_ToeJamTexture, 368, 80, 30, 35),
                new TextureRegion(_ToeJamTexture, 403, 80, 34, 36),
                //new TextureRegion(_ToeJamTexture, 438, 80, 34, 36),
            },
            delay);

        _animationIdle = new Animation(
            new List<TextureRegion>
            {
                new TextureRegion(_ToeJamTexture, 14, 14, 27, 29),
                new TextureRegion(_ToeJamTexture, 42, 13, 25, 31),
                new TextureRegion(_ToeJamTexture, 78, 15, 26, 29),
            },
            TimeSpan.FromSeconds(1)); // Idle animation with a long delay

        _ToeJam0 = new AnimatedSprite(_animationDown)
        {
            Scale = new Vector2(2.0f, 2.0f)
        };



        //_ToeJam0.CenterOrigin();

        _ToeJamSneak1 = Content.Load<Texture2D>("images/ToeJam_Transparent");
        Rectangle sneakRectangle = new Rectangle(19, 202, 22, 31);
        _sneakRegion = new TextureRegion(_ToeJamSneak1, sneakRectangle.X, sneakRectangle.Y, sneakRectangle.Width, sneakRectangle.Height);


        NPC_1 = Content.Load<Texture2D>("images/LawnMower_Man");
        Enemy_1 = Content.Load<Texture2D>("images/Lil_Devil");
        Items_1 = Content.Load<Texture2D>("images/items_scenery_tranparent");
        Items_2 = Content.Load<Texture2D>("images/items_scenery_tranparent");
        Elevator_1 = Content.Load<Texture2D>("images/Elevator");
        UIelement_1 = Content.Load<Texture2D>("images/HUD_Display");


        // Make the list of game objects
        gameObjects = new List<GameObject>
            {
                //new NPC(NPC_1, new Vector2(100, 200), npcSourceRect) {Scale = 3.0f},
                //new Enemy(Enemy_1, new Vector2(200, 400), enemySourceRect) {Scale = new Vector2(3.0f, 3.0f)},
                new Item(Items_1, new Vector2(400, 200), itemsSourceRect) {Scale = 3.0f},
                //new Item(Items_1, new Vector2(600, 700), itemsSourceRect_1) {Scale = 3.0f},
                new Item(Items_2, new Vector2(550, 200), itemsSourceRect_1) {Scale = 2.0f},
                new Elevator(Elevator_1, new Vector2(750, 150), elevatorSourceRect) {Scale = 4.0f},
                new UIelement(UIelement_1, new Vector2(0, 630), uiSourceRect) { Scale = 3.0f },
                new UIelement(UIelement_1, new Vector2(830, 630), uiSourceRect_1) { Scale = 3.0f }
            };

        PatrolMovement patrol = new PatrolMovement();
        patrol.AddWaypoint(new Vector2(100, 100));
        patrol.AddWaypoint(new Vector2(400, 100));
        patrol.AddWaypoint(new Vector2(400, 400));
        patrol.AddWaypoint(new Vector2(100, 400));

        Bot patrolBot = new Bot(Enemy_1, new Vector2(200, 400), enemySourceRect);
        patrolBot.AI = patrol;

        gameObjects.Add(patrolBot);

        patrolBot.Scale = new Vector2(2.0f, 2.0f);

        audioManager = new AudioManager(Content);

        audioManager.LoadSong("audio/01 - Toejam Jammin");

        audioManager.LoadSound("audio/Boom");
        audioManager.LoadSound("audio/Burp");

    }

    private void AssignRandomToe_JamVelocity()
    {
        _ToeJam0Velocity = Vector2.Zero;
        _ToeJam0Position = new Vector2(0, 0);
    }

    private void AssignRandomGiftVelocity()
    {
        // Make the enemy move horizontally at a constant speed.
        _giftVelocity = new Vector2(MOVEMENT_SPEED, 0);
    }




    protected override void Update(GameTime gameTime)
    {
        _ToeJam0.Update(gameTime);

        Rectangle screenBounds = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

        TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        if (!isPaused)
        {

            camera.Follow(
                _ToeJam0Position,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height
            );


            // Only update your game objects if not paused
            _ToeJam0.Update(gameTime);

            gameObjects[0].Update(gameTime);
            gameObjects[1].Update(gameTime);
            gameObjects[2].Update(gameTime);
            gameObjects[3].Update(gameTime);
            gameObjects[4].Update(gameTime);
            gameObjects[5].Update(gameTime);

            foreach (var obj in gameObjects)
            {
                if (obj is Bot bot)
                {
                    Rectangle toeJamRect = new Rectangle(
                        (int)_ToeJam0Position.X,
                        (int)_ToeJam0Position.Y,
                        (int)(_ToeJam0.Region.Width * _ToeJam0.Scale.X),
                        (int)(_ToeJam0.Region.Height * _ToeJam0.Scale.Y)
                    );

                    Rectangle botRect = new Rectangle(
                        (int)bot._position.X,
                        (int)bot._position.Y,
                        (int)(bot.sourceRect.Width * bot.Scale.X),
                        (int)(bot.sourceRect.Height * bot.Scale.Y)
                    );

                    if (toeJamRect.Intersects(botRect))
                    {
                        // only replace AI once
                        if (!(bot.AI is ChaseMovement))
                        {
                            bot.AI = new ChaseMovement(() => _ToeJam0Position, speed: 75f, stopDistance: 10f);
                        }
                    }

                    Vector2 itemPosition = gameObjects[1]._position;

                    Rectangle itemBounds = new Rectangle(
                        (int)itemPosition.X,
                        (int)itemPosition.Y,
                        (int)(itemsSourceRect_1.Width * 1.0f),
                        (int)(itemsSourceRect_1.Height * 1.0f)
                    );

                    if (toeJamRect.Intersects(itemBounds))
                    {
                        bot.IsActive = false;

                        if (gameObjects[2] is Item item)
                        {
                            item.IsActive = true;
                        }

                    }

                }
            }



            int frameWidth = _ToeJam0.Region.Width;
            int frameHeight = _ToeJam0.Region.Height;

            RectangleBorder _ToeJam0Bounds = new RectangleBorder
            (
                (int)_ToeJam0Position.X,
                (int)_ToeJam0Position.Y,
                (int)(frameWidth * _ToeJam0.Scale.X),
                (int)(frameHeight * _ToeJam0.Scale.Y)
            );

            if (_ToeJam0Bounds.Left < screenBounds.Left) { _ToeJam0Position.X = screenBounds.Left; }
            else if (_ToeJam0Bounds.Right > screenBounds.Right) { _ToeJam0Position.X = screenBounds.Right - _ToeJam0Bounds.Width; }

            if (_ToeJam0Bounds.Top < screenBounds.Top) { _ToeJam0Position.Y = screenBounds.Top; }
            else if (_ToeJam0Bounds.Bottom > screenBounds.Bottom) { _ToeJam0Position.Y = screenBounds.Bottom - _ToeJam0Bounds.Height; }


            Vector2 newGiftPosition = _giftPosition + _giftVelocity;

            RectangleBorder giftBounds = new RectangleBorder
            (
                (int)newGiftPosition.X,
                (int)newGiftPosition.Y,
                (int)(enemySourceRect.Width * 3.0f),
                (int)(enemySourceRect.Height * 3.0f)
            );

            Vector2 normal = Vector2.Zero;

            _giftPosition += _giftVelocity;

            gameObjects[0]._position = _giftPosition;

            if (giftBounds.Left < screenBounds.Left)
            {
                normal.X = Vector2.UnitX.X;
                newGiftPosition.X = screenBounds.Left;
            }
            else if (giftBounds.Right > screenBounds.Right)
            {
                normal.X = -Vector2.UnitX.X;
                newGiftPosition.X = screenBounds.Right - Items_1.Width;
            }

            if (giftBounds.Top < screenBounds.Top)
            {
                normal.Y = Vector2.UnitY.Y;
                newGiftPosition.Y = screenBounds.Top;
            }
            else if (giftBounds.Bottom > screenBounds.Bottom)
            {
                normal.Y = -Vector2.UnitY.Y;
                newGiftPosition.Y = screenBounds.Bottom - Items_1.Height;
            }

            if (normal != Vector2.Zero)
            {
                normal.Normalize();
                _giftVelocity = Vector2.Reflect(_giftVelocity, normal);
            }

            _giftPosition = newGiftPosition;

            /*Vector2 itemPosition = gameObjects[1]._position;

            Circle itemBounds = new Circle(
                (int)(itemPosition.X + (itemsSourceRect.Width * 3.0f) / 2),
                (int)(itemPosition.Y + (itemsSourceRect.Height * 3.0f) / 2),
                (int)((itemsSourceRect.Width * 3.0f) / 2)
                );

            Vector2 itemsVelocity = Vector2.Zero;

            if(itemsVelocity != Vector2.Zero)
            {
                itemsVelocity.Normalize();
                itemPosition += itemsVelocity;
            }*/

            if (_ToeJam0Bounds.Intersects(giftBounds))
            {
                int totalColumns = GraphicsDevice.PresentationParameters.BackBufferWidth / (int)Enemy_1.Width;
                int totalRows = GraphicsDevice.PresentationParameters.BackBufferHeight / (int)Enemy_1.Height;

                // Choose a random row and column based on the total number of each
                int column = Random.Shared.Next(0, totalColumns);
                int row = Random.Shared.Next(0, totalRows);

                _giftPosition = new Vector2(column * Enemy_1.Width, row * Enemy_1.Height);

                System.Diagnostics.Debug.WriteLine("ToeJam has collided with the Enemy!");

                // Assign a new random velocity to the bat
                AssignRandomGiftVelocity();
            }

            //rotating
            npcRotate += 0.05f;

            //dot product
            Vector2 enemyDirection = Vector2.Normalize(gameObjects[0]._position - _ToeJam0Position);
            float dotProduct = Vector2.Dot(_ToeJam0Position, enemyDirection);
            if (dotProduct > 0.5f) { System.Diagnostics.Debug.WriteLine("ToeJam is facing the Enemy!!"); }
            //cross product
            float crossProduct = (_ToeJam0Position.X * enemyDirection.Y) - (_ToeJam0Position.Y * enemyDirection.X);
            if (crossProduct > 0) { System.Diagnostics.Debug.WriteLine("Enemy is to the right of ToeJam"); }
            else if (crossProduct < 0) { System.Diagnostics.Debug.WriteLine("Enemy is to the left of ToeJam"); }
            //distance check
            float distanceToItem = Vector2.Distance(_ToeJam0Position, gameObjects[1]._position);
            if (distanceToItem < 50f) { System.Diagnostics.Debug.WriteLine("ToeJam is near the Item!"); }

            CheckKeyboardInput();

        }
        else
        {
            CheckKeyboardInput();
        }

        audioManager.Update(gameTime);

        if (!isPlaying)
        {
            audioManager.PlaySong("audio/01 - Toejam Jammin", volume: 0.5f, isLooped: true);
            isPlaying = true;
        }

        var keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.D1))
        {
            audioManager.PlaySound("audio/Boom", volume: 1.0f, isLooped: false);
        }

        if (keyboard.IsKeyDown(Keys.D2))
        {
            boomInstance = audioManager.PlaySound("audio/Boom", volume: 1.0f, isLooped: false);
            burpInstance = audioManager.PlaySound("audio/Burp", volume: 1.0f, isLooped: false);
            burpInstance.Stop();
        }

        if (boomInstance != null && boomInstance.State == SoundState.Stopped && burpInstance != null && burpInstance.State != SoundState.Playing)
        {
            burpInstance.Play();
            burpInstance = null;
        }

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            pitch += 0.1f;
            volume += 0.1f;
            pitch = MathHelper.Clamp(pitch, -1f, 1f);
            volume = MathHelper.Clamp(volume, 0f, 1f);
            if (boomInstance != null)
            {
                boomInstance.Pitch = pitch;
                boomInstance.Volume = volume;
                boomInstance.Play();
            }
        }

        if (keyboardState.IsKeyDown(Keys.D3))
        {
            boomInstance?.Pause();
            //burpInstance?.Pause();
            System.Diagnostics.Debug.WriteLine("Paused Sounds Boom and Burp");
        }


        CheckKeyboardInput();

        base.Update(gameTime);
        _ToeJam0.Update(gameTime);
    }

    private void CheckKeyboardInput()
    {
        // Get the state of keyboard input
        KeyboardState keyboardState = Keyboard.GetState();

        float speed = speedToeJam;

        Vector2 direction = Vector2.Zero;



        if (keyboardState.IsKeyDown(Keys.Up))
        {
            direction.Y -= speed;
            _ToeJam0.Animation = _animationUp;
        }

        if (keyboardState.IsKeyDown(Keys.Down))
        {
            direction.Y += speed;
            _ToeJam0.Animation = _animationDown;
        }

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            direction.X -= speed;
            _ToeJam0.Animation = _animationLeft;
        }

        if (keyboardState.IsKeyDown(Keys.Right))
        {
            direction.X += speed;
            _ToeJam0.Animation = _animationRight;
        }

        if (direction == Vector2.Zero)
        {
            _ToeJam0.Animation = _animationIdle;
            //_ToeJam0.Frame = 0; // Reset to the first frame of the idle animation
        }

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            _ToeJam0Position += direction * speed;
        }

        if (keyboardState.IsKeyDown(Keys.A))
        {
            _ToeJam0.Region = _sneakRegion;
            speed *= 0.47f;
            //_ToeJam0.Animation
        }

        //Pause menu toggle with Space
        if (keyboardState.IsKeyDown(Keys.P) && previousKeyBoardState.IsKeyUp(Keys.P))
        {
            isPaused = !isPaused;

            if (isPaused)
                System.Diagnostics.Debug.WriteLine("Paused");
            else
                System.Diagnostics.Debug.WriteLine("Unpaused");
        }

        // Toggle C → Map menu
        if (keyboardState.IsKeyDown(Keys.C) && previousKeyBoardState.IsKeyUp(Keys.C))
        {
            mapMenuOpen = !mapMenuOpen;

            if (mapMenuOpen)
                System.Diagnostics.Debug.WriteLine("Menu opened");
            else
                System.Diagnostics.Debug.WriteLine("Menu closed");
        }

        if (keyboardState.IsKeyDown(Keys.B) && previousKeyBoardState.IsKeyUp(Keys.B))
        {
            listOpen = !listOpen;

            if (!listOpen)
            { }
            else
            { }

            isPaused = !isPaused;

            if (isPaused)
                System.Diagnostics.Debug.WriteLine("Paused");
            else
                System.Diagnostics.Debug.WriteLine("Unpaused");
        }

        previousKeyBoardState = keyboardState;
    }

    protected override void UnloadContent()
    {
        audioManager.UnloadAllSounds();
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the back buffer.
        GraphicsDevice.Clear(Color.Navy);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: camera.Transform
            );

        foreach (var obj in gameObjects)
        {

            obj.Draw(SpriteBatch);
        }

        _ToeJam0.Draw(SpriteBatch, _ToeJam0Position);

        SpriteBatch.Draw(
            NPC_1,
            new Vector2(300, 200),
            npcSourceRect,
            Color.White,
            npcRotate,
            new Vector2(npcSourceRect.Width / 2, npcSourceRect.Height / 2),
            2.0f,
            SpriteEffects.None,
            0f);

        if (listOpen)
        {
            SpriteBatch.Draw(
                UIelement_1,
                new Vector2(150, 300),
                listSourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                3.0f,
                SpriteEffects.None,
                0f
            );

            SpriteBatch.Draw(
                Items_1,
                new Vector2(195, 397),
                itemsSourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                3.0f,
                SpriteEffects.None,
                0f);
        }

        // Always end the sprite batch when finished.
        SpriteBatch.End();

        SpriteBatch.Begin();

        if (isPaused)
        {
            CheckKeyboardInput();

            if (!listOpen)
                SpriteBatch.DrawString(font, "Paused", new Vector2(550, 100), Color.MonoGameOrange);
            else
                SpriteBatch.DrawString(font, "Map Menu Open", new Vector2(475, 150), Color.Black);
        }

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}