/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : Creature.cs                                            *
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GremDemo
{
    abstract class Creature : Entity
    {
        #region Fields


        // Physics support

        protected const float MAX_SPEED = 3f;

        // velocity information
        protected Vector2 velocity = new Vector2(0, 0);

        // Drawing support

        protected float transparency = 1f;

        // sprite sheet
        protected Texture2D spriteSheet;

        // Размеры спрайта (текстуры)
        protected const int spriteWidth = 71;
        protected const int spriteHeight = 85;

        // anim support

        // Генератор случайных чисел для анимации бездействия
        protected Random rnd;

        // count of frames in animation
        protected const int framesWalk = 9;
        protected const int framesStay = 4;
        protected const int framesTurn = 4;
        protected const int framesIdle = 10;


        // speed of animation in milliseconds (time needed for one frame)
        protected const int speedFrameWalk = 60;
        protected const int speedFrameStay = 200;
        protected const int speedFrameTurn = 70;
        protected const int speedFrameIdle = 150;

        // перечисление всех видов анимации
        protected enum Anim : int { Move = 0, Stay = 1, Turn = 2, Idle = 3 };
        // текущая анимация
        protected int currentAnimation = 1;
        // предыдущая проигрываемая анимация
        protected int prevAnimation = 1;

        protected int timeBeforeIdle = 0;     // time before question about playing Idle animation
        protected const int questionTime = 3000;    // time between each question
        // переменная для записи результата "броска кубика"
        protected int idleAnswer = 0;
        // 
        protected bool doOnce = false;

        // текущий кадр анимации
        protected int currentAnimationFrame = 0;
        // время, прошедшее со смены последнего кадра
        protected int timeSinceLastFrame = 0;
        // время, отводимое на один кадр анимации
        protected int msecPerFrame = speedFrameStay;

        // флаг (существо смотрит направо или налево)
        protected bool isRight = true;
        //  штука для запоминания предыдущего положения
        protected bool isRightPrev = true;

        #endregion

        #region Properties

        #endregion

        #region Constructors
        // Конструктор
        // Параметры: координаты существа на экране, текстура (спрайт), генератор случайных чисел для анимаций
        protected Creature(int X, int Y, Texture2D spriteSheet, Random rnd)
        {
            //
            this.spriteSheet = spriteSheet;

            // задаем координаты существа
            drawRect.X = X;
            drawRect.Y = Y;

            // приводим прямоугольник отрисовки в соответствие спрайту (по размеру)
            drawRect.Width = spriteWidth;
            drawRect.Height = spriteHeight;

            //
            this.rnd = rnd;
        }
        #endregion

        #region Public methods
        // Метод, реализующий логику обновления состояния существа
        public virtual void Update(GameTime gameTime)
        {

        }

        // Метод, отвечающий за отрисовку существа
        public void Draw(SpriteBatch spriteBatch)
        {
            // если повернут направо - нарисовать повернутым направо и т.д.
            if (isRight)
                spriteBatch.Draw(spriteSheet, new Vector2(drawRect.X, drawRect.Y), new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White * transparency);

            else
                spriteBatch.Draw(spriteSheet, drawRect, new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White * transparency, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);


        }
        #endregion

        #region Protected methods
        // метод для анимации в момент отсутствия движения существа
        protected void Stay(GameTime gameTime)
        {

            // время до попытки срабатывания анимации Idle 
            timeBeforeIdle += gameTime.ElapsedGameTime.Milliseconds;

            // если оно достигло величины, когда нужно "кинуть кости"
            if (timeBeforeIdle > questionTime)
            {

                // "кидем кости"
                if (doOnce == false)
                {
                    idleAnswer = rnd.Next(3);
                    doOnce = true;
                }
                // если "успех" (шанс 25%)
                if (idleAnswer == 0)
                {
                    // проиграть анимацию Idle
                    #region Idle
                    currentAnimation = (int)Anim.Idle;

                    // если поменяли анимацию - устанавливаем параметры для новой анимации
                    if (currentAnimation != prevAnimation)
                    {
                        //timeBeforeIdle = 0;
                        timeSinceLastFrame = 0;
                        currentAnimationFrame = 0;
                        msecPerFrame = speedFrameIdle;
                    }

                    // "накручиваем" время, прошедшее с последней смены кадра
                    timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                    // если пришло время для смены кадра
                    if (timeSinceLastFrame > msecPerFrame)
                    {
                        // обнулем счетчик прошедшего с последней смены кадра времени
                        timeSinceLastFrame = 0;

                        // если кадр не последний
                        if (currentAnimationFrame < framesIdle - 1)
                        {
                            // переключить кадр на следующий
                            currentAnimationFrame++;
                            //timeBeforeIdle = questionTime + 1;
                        }
                        else
                        {
                            // выйти из анимации Idle
                            doOnce = false;
                            timeBeforeIdle = 0;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region StayAfterQuestion
                    // иначе не проигрывать анимацию Idle
                    timeBeforeIdle = 0;
                    doOnce = false;

                    #endregion
                }
            }
            else
            {
                // проиграть анимацию Stay
                #region Stay

                currentAnimation = (int)Anim.Stay;

                // если поменяли анимацию - устанавливаем параметры для новой анимации
                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameStay;
                }

                // "накручиваем" время, прошедшее с последней смены кадра
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                // если пришло время сменить кадр
                if (timeSinceLastFrame > msecPerFrame)
                {
                    // обнуляем время с последней смены кадра
                    timeSinceLastFrame = 0;

                    // если кадр не последний
                    if (currentAnimationFrame < framesStay - 1)
                    {
                        // переключить кадр на следующий
                        currentAnimationFrame++;
                    }
                    else
                    {
                        // иначе - начать проигрывать анимацию заново (с начального кадра)
                        currentAnimationFrame = 0;
                    }
                }
                #endregion
            }

            // запоминаем предыдущий "поворот" существа
            isRightPrev = isRight;
        }

        // метод для анимации движения существа
        protected void Move(GameTime gameTime)
        {
            // если существо не разворачивалось
            if (isRight == isRightPrev)
            {
                // проиграть анимацию Move
                #region Move

                currentAnimation = (int)Anim.Move;

                // если поменяли анимацию - устанавливаем параметры для новой анимации
                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameWalk;
                }

                // "накручиваем" время, прошедшее с последней смены кадра
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                // если пришло время сменить кадр
                if (timeSinceLastFrame > msecPerFrame)
                {
                    // обнуляем время с последней смены кадра
                    timeSinceLastFrame = 0;

                    // если кадр не последний - переключить кадр на следующий
                    if (currentAnimationFrame < framesWalk - 1) currentAnimationFrame++;
                    // иначе - начать проигрывать анимацию заново (с начального кадра)
                    else currentAnimationFrame = 1;
                }

                // запоминаем предыдущий "поворот" существа
                isRightPrev = isRight;

                #endregion
            }
            else
            {
                // если существо развернулось - проиграть анимацию поворота
                #region Turn
                currentAnimation = (int)Anim.Turn;

                // если поменяли анимацию - устанавливаем параметры для новой анимации
                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameTurn;
                }

                // "накручиваем" время, прошедшее с последней смены кадра
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                // если пришло время сменить кадр
                if (timeSinceLastFrame > msecPerFrame)
                {
                    // обнуляем время с последней смены кадра
                    timeSinceLastFrame = 0;

                    // если кадр не последний
                    if (currentAnimationFrame < framesTurn - 1)
                    {
                        // переключить кадр на следующий
                        currentAnimationFrame++;
                        // "не выпускать" из этой анимации
                        isRightPrev = !isRight;
                    }
                    else
                    {
                        // иначе (когда все кадры проигрались) - выйти из анимации
                        isRightPrev = isRight;
                    }
                }
                // turn!!

                #endregion
            }
        }

        // all about velocity, collisions, gravitation etc.
        protected void Physics(GameTime gameTime)
        {
            
        }

        #endregion


    }
}
/*    end of file Creature.cs */
