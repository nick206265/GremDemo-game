/************************************************************************   
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
    // Класс для гремлина, не управляемого игроком
    class NPC : Creature
    {
        // Конструктор
        public NPC(int X, int Y, Texture2D spriteSheet, Random rnd)
            : base(X, Y, spriteSheet, rnd)
        {
            // Задаем прозрачность текстуры неигрового персонажа
            transparency = 0.6f;
        }

        // Метод, реализующий логику поведения неигрового персонажа
        public override void Update(GameTime gameTime)
        {
            // Если его координата не у правого края экрана
            if (this.drawRect.X < 700)
            {

                // Поворот текстуры направо
                isRight = true;
                // Задание максимальной скорости передвижения
                velocity.X = MAX_SPEED;
                // Проигрывание анимации движения вперед
                Move(gameTime);

            }
            else
            {
                // Поворот текстуры налево
                isRight = false;
                // Остановка персонажа
                velocity.X = 0;
                // Если персонаж разворачивается, проиграть анимацию поворота
                if (isRight != isRightPrev)
                {
                    Move(gameTime);
                }
                // Иначе проиграть анимацию Stay
                else
                {
                    Stay(gameTime);
                }
            }
            // Осуществление движения по экрану
            drawRect.X += (int)(velocity.X);
            // Запоминание предыдущей анимации
            prevAnimation = currentAnimation;

        }


    }
}
