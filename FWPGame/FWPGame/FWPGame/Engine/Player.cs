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

namespace FWPGame.Engine
{
    public class Player
    {
        //screen position (relative)
        public Vector2 myPosition;
        //map position (absolute)
        public Vector2 myMapPosition;
        private Vector2 myVelocity;
        private Vector2 myScreenSize;
        private bool canGoUp
        {
            get
            {
                return myMapPosition.Y <= 0 ? false : true;
            }
        }
        private bool canGoLeft
        {
            get
            {
                return myMapPosition.X <= 0 ? false : true;
            }
        }
        private bool canGoRight
        {
            get
            {
                return myMapPosition.X + myScreenSize.X >= myMapSize.X ? false : true;
            }
        }
        private bool canGoDown
        {
            get
            {
                return myMapPosition.Y + myScreenSize.Y >= myMapSize.Y ? false : true;
            }
        }

        protected internal Cursor myCursor;
        private Vector2 myMapSize;

        public Player(Vector2 mapPos, Vector2 screenSize,
            Vector2 mapSize, Cursor cursor)
        {
            myMapPosition = mapPos;
            myMapSize = mapSize;
            myCursor = cursor;
            myScreenSize = screenSize;
            myVelocity = new Vector2(0, 0);
        }

        /// <summary>
        /// First update the map position with the velocity.
        /// Then poll input manager for a W,A,S,D keypress and check if mouse is in a "move" area. 
        /// Based on this, set a new velocity
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <param name="kState"></param>
        /// <param name="mState"></param>
        public void Update(double elapsedTime, KeyboardState kState, MouseState mState)
        {
            myMapPosition += myVelocity;
            myVelocity = new Vector2(0, 0);

            if ((kState.IsKeyDown(Keys.W) || myCursor.myPosition.Y <= 20) && canGoUp)
            {
                myVelocity.Y = -20;
            }
            else if ((kState.IsKeyDown(Keys.S) || myCursor.myPosition.Y >= (myScreenSize.Y - 20)) && canGoDown)
            {
                myVelocity.Y = 20;
            }
            if ((kState.IsKeyDown(Keys.A) || myCursor.myPosition.X <= 20) && canGoLeft)
            {
                myVelocity.X = -20;
            }
            else if ((kState.IsKeyDown(Keys.D) || myCursor.myPosition.X >= (myScreenSize.X - 20)) && canGoRight)
            {
                myVelocity.X = 20;
            }

        }
    }
}
