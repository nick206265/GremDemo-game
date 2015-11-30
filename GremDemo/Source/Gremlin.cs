/************************************************************************
* Project Type : MonoGame Windows project                               *
* Project Name : GremDemo                                               *
* File Name    : Gremlin.cs                                             *
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
using Microsoft.Xna.Framework.Input;

namespace GremDemo
{
    class Gremlin : Creature
    {

        #region Fields
    
        //input support
        //private Keys currentKey;
        KeyboardState kbState;
        #endregion


        #region Properties

        #endregion


        #region Constructors
        public Gremlin(int X, int Y, Texture2D spriteSheet, Random rnd)
            :base(X, Y, spriteSheet, rnd)
        {

        }
        #endregion


        #region Public methods

       public override void Update(GameTime gameTime)
        {
            //Moving by arrows
            //currentKey = Keyboard.GetState().GetPressedKeys().LastOrDefault();
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Right))
            {


                isRight = true;

                // ?
                if (velocity.X < MAX_SPEED)
                    velocity.X += 0.2f;
                else
                    velocity.X = MAX_SPEED;

                Move(gameTime);

            }

            else if (kbState.IsKeyDown(Keys.Left))
            {
                isRight = false;

                if (velocity.X > -MAX_SPEED)
                    velocity.X -= 0.2f;
                else
                    velocity.X = -MAX_SPEED;


                Move(gameTime);
            }

            else
            {

                if (velocity.X > 0)
                    velocity.X -= 0.2f;
                else if (velocity.X < 0)
                    velocity.X += 0.2f;
                else
                    velocity.X = 0;

                if (currentAnimation != (int)Anim.Turn)
                {
                    Stay(gameTime);
                }
                else
                {
                    Move(gameTime);
                }

            }

            drawRect.X += (int)(velocity.X);
            drawRect.Y += (int)(velocity.Y);    //?

            //Level borders
            if (drawRect.X >= 650)
            {
                drawRect.X = 650;
            }
            else if (drawRect.X <= 10)
            {
                drawRect.X = 10;
            }

            prevAnimation = currentAnimation;

        }

        #endregion#

        #region Private methods

        #endregion
    }
}
/*    end of file Gremlin.cs */