using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GremDemo
{
    class Gremlin
    {

        #region Fields

        Random rnd;

        // Physics support

        const float GREM_MAX_SPEED = 3f;

        // velocity information
        Vector2 velocity = new Vector2(0, 0);

        // Drawing support

        // sprite sheet
        Texture2D hero;
        // draw rectangle (= collision rectangle?)
        Rectangle drawRect;

        const int spriteWidth = 71;
        const int spriteHeight = 85;
        
        // anim support

        // count of frames in animation
        const int framesWalk = 9;
        const int framesStay = 4;
        const int framesTurn = 4;
        const int framesIdle = 10;


        // speed of animation in milliseconds (time needed for one frame)
        const int speedFrameWalk = 60;
        const int speedFrameStay = 200;
        const int speedFrameTurn = 70;
        const int speedFrameIdle = 150;

        enum Anim : int { Move = 0, Stay = 1, Turn = 2, Idle = 3 };
        int currentAnimation = 1;
        int prevAnimation = 1;

        int timeBeforeIdle = 0;     // time before question about playing Idle animation
        const int questionTime = 3000;    // time between each question
        int idleAnswer = 0;
        bool doOnce = false;

        int currentAnimationFrame = 0;
        int timeSinceLastFrame = 0;
        int msecPerFrame = 200;         
        
        bool isRight = true;
        bool isRightPrev = true;

        //input support
        Keys currentKey;

        #endregion


        #region Properties

        #endregion


        #region Constructors

        public Gremlin(int X, int Y, Texture2D spriteSheet, Random rnd)
        {

            this.hero = spriteSheet;

            drawRect.X = X;
            drawRect.Y = Y;

            drawRect.Width = spriteWidth;
            drawRect.Height = spriteHeight;

            this.rnd = rnd;

        }

        #endregion


        #region Public methods

        public void Update(GameTime gameTime)
        {

            currentKey = Keyboard.GetState().GetPressedKeys().LastOrDefault();

                if (currentKey == Keys.Right)
                {
                    

                        isRight = true;

                    // ?
                        if (velocity.X < GREM_MAX_SPEED)
                            velocity.X += 0.2f;
                        else
                            velocity.X = GREM_MAX_SPEED;

                        Move(gameTime);

                }

                else if (currentKey == Keys.Left)
                {
                    isRight = false;

                    if (velocity.X > -GREM_MAX_SPEED)
                        velocity.X -= 0.2f;
                    else
                        velocity.X = -GREM_MAX_SPEED;


                    Move(gameTime);
                }

                else
                {

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

            prevAnimation = currentAnimation;
                       
        }


        public void Draw(SpriteBatch spriteBatch)
        {

                if (isRight)
                    spriteBatch.Draw(hero, new Vector2(drawRect.X, drawRect.Y), new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White);

                else
                    spriteBatch.Draw(hero, drawRect, new Rectangle(spriteWidth * currentAnimationFrame, spriteHeight * currentAnimation, spriteWidth, spriteHeight), Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            

        }

        #endregion#

        #region Private methods


        private void Stay(GameTime gameTime)
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

        private void Move(GameTime gameTime)
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
        private void Physics(GameTime gameTime)
        { 
            
        }

        #endregion
    }
}
