using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GremDemo
{
    class NPC : Creature
    {
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
