using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GremDemo
{
    abstract class Creature : Entity
    {
        #region Fields

        protected Random rnd;

        // Physics support

        protected const float MAX_SPEED = 3f;

        // velocity information
        protected Vector2 velocity = new Vector2(0, 0);

        // Drawing support

        // sprite sheet
        protected Texture2D spriteSheet;
        // draw rectangle (= collision rectangle?)
       // protected Rectangle drawRect;

        protected const int spriteWidth = 71;
        protected const int spriteHeight = 85;

        // anim support

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

        protected enum Anim : int { Move = 0, Stay = 1, Turn = 2, Idle = 3 };
        protected int currentAnimation = 1;
        protected int prevAnimation = 1;

        protected int timeBeforeIdle = 0;     // time before question about playing Idle animation
        protected const int questionTime = 3000;    // time between each question
        protected int idleAnswer = 0;
        protected bool doOnce = false;

        protected int currentAnimationFrame = 0;
        protected int timeSinceLastFrame = 0;
        protected int msecPerFrame = speedFrameStay;

        protected bool isRight = true;
        protected bool isRightPrev = true;

        #endregion

        #region Properties

        #endregion

        #region Constructors
        protected Creature(int X, int Y, Texture2D spriteSheet, Random rnd)
        {
            this.spriteSheet = spriteSheet;

            drawRect.X = X;
            drawRect.Y = Y;

            drawRect.Width = spriteWidth;
            drawRect.Height = spriteHeight;

            this.rnd = rnd;
        }
        #endregion

        #region Public methods
        public virtual void Update(GameTime gameTime)
        {

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (isRight)
                spriteBatch.Draw(spriteSheet, new Vector2(drawRect.X, drawRect.Y), new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White);

            else
                spriteBatch.Draw(spriteSheet, drawRect, new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);


        }
        #endregion

        #region Protected methods
        protected void Stay(GameTime gameTime)
        {


            timeBeforeIdle += gameTime.ElapsedGameTime.Milliseconds;

            if (timeBeforeIdle > questionTime)
            {


                if (doOnce == false)
                {
                    idleAnswer = rnd.Next(3);
                    doOnce = true;
                }
                if (idleAnswer == 0)
                {
                    #region Idle
                    currentAnimation = (int)Anim.Idle;

                    if (currentAnimation != prevAnimation)
                    {
                        //timeBeforeIdle = 0;
                        timeSinceLastFrame = 0;
                        currentAnimationFrame = 0;
                        msecPerFrame = speedFrameIdle;
                    }

                    timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;


                    if (timeSinceLastFrame > msecPerFrame)
                    {
                        timeSinceLastFrame = 0;

                        if (currentAnimationFrame < framesIdle - 1)
                        {
                            currentAnimationFrame++;
                            //timeBeforeIdle = questionTime + 1;
                        }
                        else
                        {
                            doOnce = false;
                            timeBeforeIdle = 0;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region StayAfterQuestion

                    timeBeforeIdle = 0;
                    doOnce = false;

                    #endregion
                }
            }
            else
            {
                #region Stay


                currentAnimation = (int)Anim.Stay;

                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameStay;
                }

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;


                if (timeSinceLastFrame > msecPerFrame)
                {
                    timeSinceLastFrame = 0;

                    if (currentAnimationFrame < framesStay - 1)
                    {
                        currentAnimationFrame++;
                    }
                    else
                    {
                        currentAnimationFrame = 0;
                    }
                }
                #endregion
            }

            isRightPrev = isRight;

        }

        protected void Move(GameTime gameTime)
        {

            if (isRight == isRightPrev)
            {
                #region Move

                currentAnimation = (int)Anim.Move;

                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameWalk;
                }

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (timeSinceLastFrame > msecPerFrame)
                {
                    timeSinceLastFrame = 0;

                    if (currentAnimationFrame < framesWalk - 1) currentAnimationFrame++;
                    else currentAnimationFrame = 1;
                }

                isRightPrev = isRight;

                #endregion
            }
            else
            {

                #region Turn
                currentAnimation = (int)Anim.Turn;

                if (currentAnimation != prevAnimation)
                {
                    timeBeforeIdle = 0;
                    timeSinceLastFrame = 0;
                    currentAnimationFrame = 0;
                    msecPerFrame = speedFrameTurn;
                }

                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (timeSinceLastFrame > msecPerFrame)
                {
                    timeSinceLastFrame = 0;

                    if (currentAnimationFrame < framesTurn - 1)
                    {
                        currentAnimationFrame++;
                        isRightPrev = !isRight;
                        //velocity.X = 0;

                        //
                        //if (!Keyboard.GetState().IsKeyDown(Keys.Right))
                        //{ 

                        //}
                        //

                    }
                    else
                    {
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
