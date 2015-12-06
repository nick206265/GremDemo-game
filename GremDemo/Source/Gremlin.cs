/************************************************************************
* Для запуска и сборки данной программы необходимо установить:          *   
*  1) MonoGame 3.4                                                      *
*  2) Microsoft .Net Framework 4                                        * 
* Решение (solution) для Visual Studio 2015 Community                   *
*************************************************************************/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GremDemo
{
    // Класс персонажа, которым управляет игрок
    class Gremlin : Creature
    {

        #region Fields
    
        //input support
        //private Keys currentKey;
        // Объект, возвращающий состояние клавиатуры
        KeyboardState kbState;
        #endregion


        #region Properties

        #endregion


        #region Constructors
        // Конструктор (см. Creature)
        public Gremlin(int X, int Y, Texture2D spriteSheet, Random rnd)
            :base(X, Y, spriteSheet, rnd)
        {

        }
        #endregion


        #region Public methods
        // Метод, реализующий логику поведения игрового персонажа
       public override void Update(GameTime gameTime)
        {
            //Moving by arrows
            //currentKey = Keyboard.GetState().GetPressedKeys().LastOrDefault();
            // Опрашиваем клавиатуру
            kbState = Keyboard.GetState();
            // Если нажата клавиша "->"
            if (kbState.IsKeyDown(Keys.Right))
            {

                // Поворот направо
                isRight = true;

                // Реализация ускорения
                if (velocity.X < MAX_SPEED)
                    velocity.X += 0.2f;
                else
                    velocity.X = MAX_SPEED;
                // Проиграть анимацию движения
                Move(gameTime);

            }
            // Если нажата клавиша "<-"
            else if (kbState.IsKeyDown(Keys.Left))
            {
                // Поворот налево
                isRight = false;
                // Реализация ускорения
                if (velocity.X > -MAX_SPEED)
                    velocity.X -= 0.2f;
                else
                    velocity.X = -MAX_SPEED;

                // Проиграть анимацию движения
                Move(gameTime);
            }
            //
            else
            {
                // Если клавиша отпущена, реализуется "торможение"   
                if (velocity.X > 0)
                    velocity.X -= 0.2f;
                else if (velocity.X < 0)
                    velocity.X += 0.2f;
                else
                    velocity.X = 0;
                // Если персонаж не поворачивается, проигрывается анимация Stay
                if (currentAnimation != (int)Anim.Turn)
                {
                    Stay(gameTime);
                }
                // Иначе - движение прямо
                else
                {
                    Move(gameTime);
                }

            }
            // Обновляем координаты персонажа
            drawRect.X += (int)(velocity.X);
            drawRect.Y += (int)(velocity.Y);    

            //Level borders
            if (drawRect.X >= 650)
            {
                drawRect.X = 650;
            }
            else if (drawRect.X <= 10)
            {
                drawRect.X = 10;
            }
            // Запоминаем предыдущую анимацию
            prevAnimation = currentAnimation;

        }

        #endregion#

        #region Private methods

        #endregion
    }
}