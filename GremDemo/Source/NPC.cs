/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : NPC.cs                                                 *
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
    // Класс для гремлина, не управляемого игроком
    class NPC : Creature
    {
        // Конструктор
        public NPC(int X, int Y, Texture2D spriteSheet, Random rnd)
            : base(X, Y, spriteSheet, rnd)
        {
            transparency = 0.6f;
        }


        public override void Update(GameTime gameTime)
        {
            if (this.drawRect.X < 700)
            {


                isRight = true;

                velocity.X = MAX_SPEED;

                Move(gameTime);

            }
            else
            {
                isRight = false;

                velocity.X = 0;

                if (isRight != isRightPrev)
                {
                    Move(gameTime);
                }
                else
                {
                    Stay(gameTime);
                }
            }

            drawRect.X += (int)(velocity.X);

            prevAnimation = currentAnimation;

        }


    }
}
/*    end of file NPC.cs */
