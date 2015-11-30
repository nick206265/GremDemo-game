/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : Stone.cs                                               *
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
    // Класс для реализации падающих камней в игре
    class Stone : Shell
    {
        // датчик случайных чисел для генерирования случайной начальной позиции камня
        protected Random rnd;

        // прямоугольник для обработки столкновений
        public Rectangle collisionRect;

        // размеры спрайта
        protected const int spriteWidth = 75;
        protected const int spriteHeight = 50;

        // Коструктор
        public Stone(Texture2D sprite, Random rnd)
        {
            // Размещаем камень случайным образом за краем игрового окна
            drawRect.X = rnd.Next(-60, 770);
            drawRect.Y = -rnd.Next(100, 400);

            // 
            this.sprite = sprite;

            // устанавливаем размеры прямоугольника отрисовки равными размеру спрайта(текстуры) 
            drawRect.Width = spriteWidth;
            drawRect.Height = spriteHeight;
        
            // 
            this.rnd = rnd;

            // устанавливаем размер прямоугольника для обработки столкновений
            // он немного меньше, чтобы было легче играть
            this.collisionRect.Width = spriteWidth - 60;
            
            this.collisionRect.Height = spriteHeight - 30;
        }

        // Метод, реализующий логику обновления состояния объекта
        public override void Update(GameTime gameTime)
        {
            // если камень не ушел за нижнюю границу экрана
            if (drawRect.Y <= 700)
            {
                // утановить скорость камня максимальной
                velocity.Y = MAX_SPEED;
            }
            else
            {
                // иначе снова поднять камень наверх
                drawRect.Y = -rnd.Next(100, 400);
                drawRect.X = rnd.Next(-60, 770);
            }

            // обновление координаты камня (высоты)
            drawRect.Y += (int)(velocity.Y);

            // двигаем прямоугольник столкновений за прямоугольником отрисовки
            this.collisionRect.X = this.drawRect.X + 40;
            this.collisionRect.Y = this.drawRect.Y + 15;

        }

        // Виртуальный метод, отвечающий за отрисовку объекта
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRect, Color.White);
        }



    }// end of class
} // end of namespace

/*    end of file Stone.cs */
