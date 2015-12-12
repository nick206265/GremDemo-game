/************************************************************************
* ��� ������� � ������ ������ ��������� ���������� ����������:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* ������� (solution) ��� Visual Studio 2015 Community                   *
*************************************************************************/


using System;
using System.Collections.Generic;

// Framework ��� �������� ���
using Microsoft.Xna.Framework;
// ��������� ������� (DirectX - SharpDX)
using Microsoft.Xna.Framework.Graphics;
// ��������� ����� (����������, ����)
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Media;

namespace GremDemo
{
    //Enum class for state of the game
    enum GameState
    {        
        RUNNING,
        PAUSED,
        WIN,
        GAMEOVER

    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoGame : Microsoft.Xna.Framework.Game
    {
        //Initial state of the game - pause
        GameState GS = GameState.PAUSED;

        //Font for all symbols in the game
        SpriteFont Arial;

        //Random generator (for Idle anim in general)
        Random rnd = new Random();

        //Screen(game window) resolution
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        
        // �������, ��������������� ������ ������ � ��������
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Can also use SongCollection class later
        Song song;
        

        // for background
        Texture2D back;

        // for gremlin drawing method
        Texture2D hero;

        // for stones
        Texture2D stone;

        // Collections for game objects
        // List of gremlins
        List<Gremlin> gremlins = new List<Gremlin>();

        // List of Background objects
        List<Background> backGround = new List<Background>();

        // List of NPCs
        List<NPC> NPCs = new List<NPC>();

        // List of stones
        List<Stone> stones = new List<Stone>();

        // ����������� ��� ������ ����
        public DemoGame()
        {
            // ���������� GraphicsDeviceManager � ����� �����
            graphics = new GraphicsDeviceManager(this);
            // �����, ������ ����� ������������ ������� (��������)
            Content.RootDirectory = "Content";

            // ������������� ���������� �������� ����
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            // �������� ������������� �����
            //graphics.IsFullScreen = true;
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

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load song
            //song = Content.Load<Song>(@"Music\COMBAT04");

            //MediaPlayer.Play(song);
            song = Content.Load<Song>(@"Music\COMBAT04");
            

            MediaPlayer.Play(song);  

                // ���������� �����
            Arial = Content.Load<SpriteFont>(@"Fonts\Arial");

            // load sprites for;
            // gremlin's animation
            hero = Content.Load<Texture2D>(@"Sprites\GremAnim");
            // background
            back = Content.Load<Texture2D>(@"Sprites\back");
            // stones
            stone = Content.Load<Texture2D>(@"Sprites\stone");

            // create initial game objects
            gremlins.Add(new Gremlin(50, 400,hero,rnd));
            backGround.Add(new Background(graphics,back));
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

            //Allow the game to pause
            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                GS = GameState.PAUSED;

            //Allow the game to restart
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                // ������� ������� ����, ������������� ��� ���������� � ��������� ��������
                GS = GameState.PAUSED;

                gremlins.Clear();
                NPCs.Clear();
                stones.Clear();
                
                gremlins.Add(new Gremlin(50, 400, hero, rnd));
                
                NPCs.Add(new NPC(50, 400, hero, rnd));
            } // end if for RESTART

            // ��������� ��������� ����� ����
            if (GS == GameState.PAUSED)
            {
                // ���� ����� Enter - ��������� ����
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    GS = GameState.RUNNING;
            } // end if for PAUSE

            // ��������� ��������������� �������� ��������
            if (GS == GameState.RUNNING)
            {
                // gameover conditions
                foreach (Stone stone in stones)
                {
                    // ���� ����� �� ������ ���������� � ��������� (���������� ������)
                    if (stone.collisionRect.Intersects(gremlins[0].drawRect))
                    {
                        // ���������� ��������� ���� � GAMEOVER
                        GS = GameState.GAMEOVER;
                    }
                }


                // ����� ������ Update ��� ������� �������� �������
                gremlins[0].Update(gameTime);
                NPCs[0].Update(gameTime);

                foreach (Stone stone in stones)
                {
                    stone.Update(gameTime);
                }

                    //Win conditions


                // TODO: Add your update logic here
            } // end if for RUNNING

            base.Update(gameTime);
        } // end of Update method

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // ���� ���� �� �����
            if (GS == GameState.PAUSED)
            {
                // ������� ������
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // �������� ��������� 
                spriteBatch.Begin();

                // ������������ ������� ������� � ���
                // ������� �����, ������ ������������ ������ ������������� ������������
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                foreach (Stone stone in stones)
                {
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // ��������� ������ ������ ���� ��������� ��������
                spriteBatch.DrawString(Arial,
"To start - press ENTER\nTo pause - press TAB\nIf you want to exit - press ESC",
                    new Vector2(400, 100), Color.Black);

                // ��������� ���������
                spriteBatch.End();
            } // ����� ��������� ��������� PAUSE

            // ���� ���� ��������
            if (GS == GameState.RUNNING)
            {
                // ������� �����
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // �������� ���������
                spriteBatch.Begin();

                // ������������ ������� ������� � ���
                // ������� �����, ������ ������������ ������ ������������� ������������
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                foreach (Stone stone in stones)
                {
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // ��������� ������ ������ ���� ��������� ��������

                // ��������� ���������
                spriteBatch.End();
            } // ����� ��������� ��������� RUNNING

            // ���� ����� �������
            if (GS == GameState.WIN)
            {
                // ������� ������
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // �������� ���������
                spriteBatch.Begin();

                // ������ ��� ������� ������� ����� ������
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                NPCs[0].Draw(spriteBatch);


                // ��������� ������ ������ ���� ��������� ��������

                spriteBatch.DrawString(Arial, "YOU WIN!\nPRESS ESC TO EXIT.\nPRESS \"R\" TO RESTART", new Vector2(400, 150), Color.Red);

                // ��������� ���������
                spriteBatch.End();
            } // ����� ��������� ��������� WIN

            // ���� ���� ���������
            if (GS == GameState.GAMEOVER)
            {
                // ������� ������
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // �������� ���������
                spriteBatch.Begin();

                // ������ ��� ������� �������
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);

                // ��������� ���������� ���������
                NPCs[0].Draw(spriteBatch);

                // ��������� ������ ������ ���� ��������� ��������

                spriteBatch.DrawString(Arial, "YOU LOOSE! GAME IS OVER.\nPRESS ESC TO EXIT.\nPRESS \"R\" TO RESTART", new Vector2(400, 150), Color.Red);

                // ��������� ���������
                spriteBatch.End();
            } // ����� ��������� ��������� GAMEOVER
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        } // end of Draw method
    } // end class
} // end namespace
