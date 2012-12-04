using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;
using System.Diagnostics;

namespace FWPGame.Items
{
    public class Tornado : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        public Tornado(Texture2D[] animSeq, Vector2 position, Vector2 velocity, Vector2 mapPosition) :
            base(animSeq[0], position)
        {
            myMapPosition = mapPosition;
            myTexture = animSeq[0];
            myPosition = position;
            myVelocity = velocity;
            myAnimateSequence = animSeq;
            myAnimate = new Animate(animSeq);
            SetUpAnimate();
            name = "Tornado";
        }

        public void SetUpAnimate()
        {
            // Prepare the flip book sequence for expected Animate
            myAnimate.AddFrame(0, 1);
            myAnimate.AddFrame(1, 1);
            myAnimate.AddFrame(2, 1);
            myAnimate.AddFrame(3, 1);
            myAnimate.AddFrame(4, 1);
            myAnimate.AddFrame(5, 1);
            myAnimate.AddFrame(6, 1);
            myAnimate.AddFrame(7, 1);
            myAnimate.AddFrame(8, 1);
            myAnimate.AddFrame(9, 1);
            myAnimate.AddFrame(10, 1);
            myAnimate.AddFrame(11, 1);
            myAnimate.AddFrame(12, 1);
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
            Debug.WriteLine("position is at: " + pos);
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
            Debug.WriteLine("map position is at: " + pos);
        }

        public Tornado Clone()
        {
            return new Tornado(this.myAnimateSequence, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0));
        }

        public override void Update(double elapsedTime, Vector2 playerMapPos)
        {
            bool seqDone = false;
            myAnimate.Update(elapsedTime, ref seqDone);
            myMapPosition += myVelocity;
            myPosition += myVelocity;
            myPosition = myMapPosition - playerMapPos;
        }

        public override void Update(GameTime gameTime, Vector2 playerMapPos)
        {
            bool seqDone = false;
            myAnimate.Update(gameTime.TotalGameTime.Seconds, ref seqDone);
            myMapPosition += myVelocity;
            myPosition = myMapPosition - playerMapPos;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myAnimate.GetImage(), myPosition,
                    null, Color.White,
                    myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
        }

        public void printDebug()
        {
            Debug.WriteLine("map position: " + myMapPosition);
            Debug.WriteLine("relative position: " + myPosition);
        }

        class GoneState : State
        {
            public GoneState(Sprite s)
            {
                s.myMapPosition = new Vector2(0, 0);
                s.myVelocity = new Vector2(0, 0);
                s.myPosition = new Vector2(0, 0);
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

            }

            public void Draw(SpriteBatch batch)
            {
            }

            public Sprite Spread()
            {
                return null;
            }
        }
    }
}
