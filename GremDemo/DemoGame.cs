using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GremDemo
{

    enum GameState
    {
        PAUSED = 2,
        WIN = 1,
        RUNNING = 0,
        GAMEOVER = -1

    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoGame : Microsoft.Xna.Framework.Game
    {

        GameState GS = GameState.PAUSED;
        int DoOnce = 0;

        int Score = 0;
        const int SCORE_TO_WIN = 3000;

        SpriteFont Arial;

        Random rnd = new Random();

        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // for background
        Texture2D back;

        // for gremlin drawing method
        Texture2D hero;
        
        // List of gremlins
        List<Gremlin> gremlins = new List<Gremlin>();
        
        // List of Static objects
        List<StaticObj> statObj = new List<StaticObj>();

        // List of Imps
        List<NPC> NPCs = new List<NPC>();

        // input support
        // mouse
        // keyboard


        public DemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        //  graphics.IsFullScreen = true;

            IsMouseVisible = true;

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

            Arial = Content.Load<SpriteFont>("Arial");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load sprites for gremlin's animation
           // gremSprites.Add(Content.Load<Texture2D>("CgremA01"));
            hero = Content.Load<Texture2D>("GremAnim");
            back = Content.Load<Texture2D>("back");
            
            // create initial game objects
            gremlins.Add(new Gremlin(50, 400,hero,rnd));
            statObj.Add(new StaticObj(graphics,back));
            NPCs.Add(new NPC(50,400,hero,rnd));

            // TODO: use this.Content to load your game content here
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (GS == GameState.PAUSED)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    GS = GameState.RUNNING;
            }

            if (GS == GameState.RUNNING)
            {
                Score += gameTime.ElapsedGameTime.Milliseconds;

                gremlins[0].Update(gameTime);
                NPCs[0].Update(gameTime);

                //Win conditions
                if (Score >= SCORE_TO_WIN && DoOnce == 0)
                {
                    GS = GameState.WIN;
                    DoOnce = 1;

                }

                // TODO: Add your update logic here
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (GS == GameState.PAUSED)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();



                statObj[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                NPCs[0].Draw(spriteBatch);


                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);
                spriteBatch.DrawString(Arial,
                    "You must stay alive after one minute of the game.\n You get scores for the survival time.\n To start - press Enter",
                    new Vector2(400, 100), Color.Black);

                spriteBatch.End();
            }

            if (GS == GameState.RUNNING)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();



                statObj[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                NPCs[0].Draw(spriteBatch);


                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);

                spriteBatch.End();
            }
            else if (GS == GameState.WIN)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
                statObj[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                NPCs[0].Draw(spriteBatch);
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60-Score/1000).ToString(), new Vector2(300, 100), Color.Black);
                spriteBatch.DrawString(Arial, "YOU WIN!\nPRESS ESC TO EXIT", new Vector2(400, 150), Color.Red);

                spriteBatch.End();
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
