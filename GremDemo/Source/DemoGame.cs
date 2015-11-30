/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : DemoGame.cs                                            *
* Programmers  : ���������� �.�. �������� �.�.                          *
* Created      : 17/11/15                                               *
* Last Revision: 30/11/15                                               *
* Comments     : MonoGame game project using DirectX                    *
*                                                                       *
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

        //Initial value of game score
        int Score = 0;
        //Score needed to win the game
        const int SCORE_TO_WIN = 60000;

        // ������������ ���������� ������ �� ������
        const int countOfStones = 10;

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
            graphics.IsFullScreen = true;

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

            // ���������� �����
            Arial = Content.Load<SpriteFont>("Arial");

            // load sprites for:
            // gremlin's animation
            hero = Content.Load<Texture2D>("GremAnim");
            // background
            back = Content.Load<Texture2D>("back");
            // stones
            stone = Content.Load<Texture2D>("stone");

            // create initial game objects
            gremlins.Add(new Gremlin(50, 400,hero,rnd));
            backGround.Add(new Background(graphics,back));
            NPCs.Add(new NPC(50,400,hero,rnd));

            // ���� ��� �������� ������
            for (int i = 0; i < countOfStones; i++)
            {
                stones.Add(new Stone(stone, rnd));
            }

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
                Score = 0;

                gremlins.Clear();
                NPCs.Clear();
                stones.Clear();

                // ������ ������� ������� �������
                for (int i = 0; i < countOfStones; i++)
                {
                    stones.Add(new Stone(stone, rnd));
                }
                
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

                // �������� �������� ����� (�� ������ �� ������ ������������, ����������� � ����)
                Score += gameTime.ElapsedGameTime.Milliseconds;

                // ����� ������ Update ��� ������� �������� �������
                gremlins[0].Update(gameTime);
                NPCs[0].Update(gameTime);

                foreach (Stone stone in stones)
                {
                    stone.Update(gameTime);
                }

                    //Win conditions
                if (Score >= SCORE_TO_WIN)
                {
                    GS = GameState.WIN;
                }

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
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);
                spriteBatch.DrawString(Arial,
"You must stay alive after one minute of the game.\n You get scores for the survival time.\n To start - press ENTER\nTo pause - press TAB\nIf you want to exit - press ESC",
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
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);

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
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60-Score/1000).ToString(), new Vector2(300, 100), Color.Black);

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

                // ������ ���� ��� ������, ������� ��� ����
                foreach (Stone stone in stones)
                {
                    if (stone.collisionRect.Intersects(gremlins[0].drawRect))
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // ��������� ������ ������ ���� ��������� ��������
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);

                spriteBatch.DrawString(Arial, "YOU LOOSE! GAME IS OVER.\nPRESS ESC TO EXIT.\nPRESS \"R\" TO RESTART", new Vector2(400, 150), Color.Red);

                // ��������� ���������
                spriteBatch.End();
            } // ����� ��������� ��������� GAMEOVER
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        } // end of Draw method
    } // end class
} // end namespace

/*    end of file DemoGame.cs */
