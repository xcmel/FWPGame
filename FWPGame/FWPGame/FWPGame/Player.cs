using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FWPGame
{
    public class Player
    {
        //screen position (relative)
        public Vector2 myPosition;
        //map position (absolute)
        public Vector2 myMapPosition;
        private Vector2 myVelocity;
        private Vector2 myScreenSize;
        private bool canGoUp;
        private bool canGoLeft;
        private bool canGoRight;
        private bool canGoDown;
        private Cursor myCursor;
        private Vector2 myMapSize;
        private State myState;

        public Player(Vector2 mapPos, Vector2 screenSize, 
            Vector2 mapSize, Cursor cursor)
        {
            canGoUp = false;
            canGoLeft = false;
            myMapPosition = mapPos;
            myMapSize = mapSize;
            myCursor = cursor;
            myScreenSize = screenSize;
            myVelocity = new Vector2(0, 0);
        }

        private void MovePlayer()
        {
            myMapPosition += myVelocity;
        }

        public void Update(double elapsedTime, KeyboardState kState, MouseState mState)
        {
            myVelocity = new Vector2(0, 0);
            if (myMapPosition.X <= 0)
            {
                canGoLeft = false;
            }
            else
            {
                canGoLeft = true;
            }

            if (myMapPosition.X + myScreenSize.X >= myMapSize.X)
            {
                canGoRight = false;
            } else {
                canGoRight = true;
            }

            if (myMapPosition.Y <= 0)
            {
                canGoUp = false;
            } else {
                canGoUp = true;
            }

            if (myMapPosition.Y + myScreenSize.Y >= myMapSize.Y)
            {
                canGoDown = false;
            } else {
                canGoDown = true;
            }

            if (kState.IsKeyDown(Keys.W) && canGoUp)
            {
                myVelocity.Y = -20;
            }
            else if (kState.IsKeyDown(Keys.S) && canGoDown)
            {
                myVelocity.Y = 20;
            }

            if (kState.IsKeyDown(Keys.A) && canGoLeft)
            {
                myVelocity.X = -20;
            }
            else if (kState.IsKeyDown(Keys.D) && canGoRight)
            {
                myVelocity.X = 20;
            }

            myMapPosition += myVelocity;

            int X = 0;
            int Y = 0;
            bool move = false;
            if (myCursor.myPosition.X <= 20 && canGoLeft)
            {
                X = -20;
                move = true;
            }
            if (myCursor.myPosition.X >= (myScreenSize.X - 20) && canGoRight)
            {
                X = 20;
                move = true;
            }
            if (myCursor.myPosition.Y <= 20 && canGoUp)
            {
                Y = -20;
                move = true;
            }
            if (myCursor.myPosition.Y >= (myScreenSize.Y - 20) && canGoDown)
            {
                Y = 20;
                move = true;
            }
                myVelocity = new Vector2(X, Y);
                MovePlayer();
        }

    }
}
