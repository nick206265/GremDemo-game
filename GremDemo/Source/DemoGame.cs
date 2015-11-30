/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : DemoGame.cs                                            *
* Programmers  : Колесников А.П. Кириллин С.Д.                          *
* Created      : 17/11/15                                               *
* Last Revision: 30/11/15                                               *
* Comments     : MonoGame game project using DirectX                    *
*                                                                       *
* Для запуска и сборки данной программы необходимо установить:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* Решение (solution) для Visual Studio 2015 Community                   *
*************************************************************************/


using System;
using System.Collections.Generic;

// Framework для создания игр
using Microsoft.Xna.Framework;
// Поддержка графики (DirectX - SharpDX)
using Microsoft.Xna.Framework.Graphics;
// Обработка ввода (клавиатура, мышь)
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

        // Максимальное количество камней на экране
        const int countOfStones = 10;

        //Font for all symbols in the game
        SpriteFont Arial;

        //Random generator (for Idle anim in general)
        Random rnd = new Random();

        //Screen(game window) resolution
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        
        // Объекты, инкапсулирующие методы работы с графикой
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

        // Конструктор для класса игры
        public DemoGame()
        {
            // Связывание GraphicsDeviceManager с нашей игрой
            graphics = new GraphicsDeviceManager(this);
            // Папка, откуда будут подгружаться ресурсы (картинки)
            Content.RootDirectory = "Content";

            // Устанавливаем разрешение игрового окна
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            // Включаем полноэкранный режим
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

            // Подгружаем шрифт
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

            // цикл для создания камней
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
                // Очищаем игровое поле, устанавливаем все переменные в начальные значения
                GS = GameState.PAUSED;
                Score = 0;

                gremlins.Clear();
                NPCs.Clear();
                stones.Clear();

                // Заново создаем игровые объекты
                for (int i = 0; i < countOfStones; i++)
                {
                    stones.Add(new Stone(stone, rnd));
                }
                
                gremlins.Add(new Gremlin(50, 400, hero, rnd));
                
                NPCs.Add(new NPC(50, 400, hero, rnd));
            } // end if for RESTART

            // Обработка состояния паузы игры
            if (GS == GameState.PAUSED)
            {
                // Если нажат Enter - запустить игру
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    GS = GameState.RUNNING;
            } // end if for PAUSE

            // Обработка непосредственно игрового процесса
            if (GS == GameState.RUNNING)
            {
                // gameover conditions
                foreach (Stone stone in stones)
                {
                    // Если любой из камней столкнулся с гремлином (персонажем игрока)
                    if (stone.collisionRect.Intersects(gremlins[0].drawRect))
                    {
                        // Установить состояние игры в GAMEOVER
                        GS = GameState.GAMEOVER;
                    }
                }

                // Накрутка счетчика очков (по одному за каждую миллисекунду, проведенную в игре)
                Score += gameTime.ElapsedGameTime.Milliseconds;

                // Вызов метода Update для каждого игрового объекта
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
            // Если игра на паузе
            if (GS == GameState.PAUSED)
            {
                // Очистка экрана
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // Начинаем рисование 
                spriteBatch.Begin();

                // Отрисовываем игровые объекты и фон
                // порядок важен, первый нарисованный объект перекрывается последующими
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                foreach (Stone stone in stones)
                {
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // Отрисовка текста поверх всех остальных спрайтов
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);
                spriteBatch.DrawString(Arial,
"You must stay alive after one minute of the game.\n You get scores for the survival time.\n To start - press ENTER\nTo pause - press TAB\nIf you want to exit - press ESC",
                    new Vector2(400, 100), Color.Black);

                // Заверщаем рисование
                spriteBatch.End();
            } // Конец обработки состояния PAUSE

            // Если игра запущена
            if (GS == GameState.RUNNING)
            {
                // Очистим экран
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // Начинаем отрисовку
                spriteBatch.Begin();

                // Отрисовываем игровые объекты и фон
                // порядок важен, первый нарисованный объект перекрывается последующими
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                foreach (Stone stone in stones)
                {
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // Отрисовка текста поверх всех остальных спрайтов
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);

                // завершаем отрисовку
                spriteBatch.End();
            } // Конец обработки состояния RUNNING

            // Если игрок выиграл
            if (GS == GameState.WIN)
            {
                // Очистка экрана
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // Начинаем отрисовку
                spriteBatch.Begin();

                // Рисуем все игровые объекты кроме камней
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);
                
                NPCs[0].Draw(spriteBatch);


                // Отрисовка текста поверх всех остальных спрайтов
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60-Score/1000).ToString(), new Vector2(300, 100), Color.Black);

                spriteBatch.DrawString(Arial, "YOU WIN!\nPRESS ESC TO EXIT.\nPRESS \"R\" TO RESTART", new Vector2(400, 150), Color.Red);

                // завершаем отрисовку
                spriteBatch.End();
            } // Конец обработки состояния WIN

            // Если игра проиграна
            if (GS == GameState.GAMEOVER)
            {
                // Очистка экрана
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // Начинаем отрисовку
                spriteBatch.Begin();

                // Рисуем все игровые объекты
                backGround[0].Draw(spriteBatch);
                gremlins[0].Draw(spriteBatch);

                // Рисуем лишь тот камень, который нас убил
                foreach (Stone stone in stones)
                {
                    if (stone.collisionRect.Intersects(gremlins[0].drawRect))
                    stone.Draw(spriteBatch);
                }

                NPCs[0].Draw(spriteBatch);

                // Отрисовка текста поверх всех остальных спрайтов
                spriteBatch.DrawString(Arial, "Score: " + Score.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(Arial, "Time left: " + (60 - Score / 1000).ToString(), new Vector2(300, 100), Color.Black);

                spriteBatch.DrawString(Arial, "YOU LOOSE! GAME IS OVER.\nPRESS ESC TO EXIT.\nPRESS \"R\" TO RESTART", new Vector2(400, 150), Color.Red);

                // завершаем отрисовку
                spriteBatch.End();
            } // Конец обработки состояния GAMEOVER
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        } // end of Draw method
    } // end class
} // end namespace

/*    end of file DemoGame.cs */
