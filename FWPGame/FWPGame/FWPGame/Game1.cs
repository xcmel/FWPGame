//Camco
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FWPGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont chiF;
        private Texture2D marsLand;
        private Texture2D marsOrbit;
        private Vector2 FontPos;
        private Vector2 FontOrigin;
        private List<Sprite> mySprites;
        private MapSprite[,] mapGrid = new MapSprite[6, 6];
        public Map map;
        private String motd = "Hello Camco";

        public GrassSprite myGrass;
        public Player player;
        private Cursor cursor;
        private KeyboardState keyboardState;
        private MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            chiF = Content.Load<SpriteFont>("ChillerFont");
            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 6; j++) {
                    MapSprite ms = new MapSprite(Content.Load<Texture2D>("Maps/Mars/marsorbit-" + i + "-" + j),
                        new Vector2(GraphicsDevice.Viewport.Width * i, GraphicsDevice.Viewport.Height * j),
                        new Vector2(0, 0),
                        new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
                    mapGrid[i, j] = ms;
                }
            }

            myGrass = new GrassSprite(Content.Load<Texture2D>("grass"),
                new Vector2(0, 0),
                new Vector2(0, 0));
            cursor = new Cursor(Content.Load<Texture2D>("cursor"), new Vector2(0,0), this);
            player = new Player(new Vector2(0, 0),
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                new Vector2((float)(GraphicsDevice.Viewport.Width * (float)(Math.Sqrt(mapGrid.Length))),
                    ((float)GraphicsDevice.Viewport.Height * (float)(Math.Sqrt(mapGrid.Length)))),
                cursor);

            map = new Map(mapGrid,
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                player);

            // TODO: use this.Content to load your game content here

            // Create a Tree instance
            Texture2D[] burnSequence = {
                Content.Load<Texture2D>("burning/burn_0"),
                Content.Load<Texture2D>("burning/burn_1"),
                Content.Load<Texture2D>("burning/burn_2"),
                Content.Load<Texture2D>("burning/burn_3"),
                Content.Load<Texture2D>("burning/burn_4"),
                Content.Load<Texture2D>("burning/burn_5"),
                Content.Load<Texture2D>("burning/burn_6"),
                Content.Load<Texture2D>("burning/burn_7"),
                Content.Load<Texture2D>("burning/burn_8"),
                Content.Load<Texture2D>("burning/burn_9")
            };
            Tree motherTree = new Tree(Content.Load<Texture2D>("tree"), new Vector2(0, 0), new Vector2(0, 0),
                burnSequence, Content.Load<Texture2D>("burntTree"));


            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            HandleInput();

            cursor.Update(mouseState);
            map.Update(gameTime, mouseState, keyboardState);

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            // get the input states
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();


        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            //FontOrigin = chiF.MeasureString(motd) / 2;

            map.Draw(player.myMapPosition, spriteBatch);
            //spriteBatch.DrawString(chiF, motd, FontPos, Color.Yellow, 0, FontOrigin, 1.0f,
            //   SpriteEffects.None, 0.5f);
            cursor.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
