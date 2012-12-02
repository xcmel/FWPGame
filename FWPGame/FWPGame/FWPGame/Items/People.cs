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

namespace FWPGame
{
    public class People : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;

        public People(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] animateSequence, Texture2D burnt) :
            base(texture, position)
        {
            myMapPosition = mapPosition;

            myAnimateSequence = animateSequence;
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();
            myBurnt = burnt;
            myState = new RegularState(this);
        }

        public People Clone()
        {
            return new People(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), myAnimateSequence, myBurnt);
        }


        public void burn()
        {
            myState = new BurningState(this);
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }


        public void SetUpAnimate()
        {
            // Prepare the flip book sequence for expected Animate
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(1, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(2, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(3, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(4, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(5, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(6, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(7, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(8, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(9, 50);
        }


        // The Regular State
        class RegularState : State
        {
            private People people;

            public RegularState(People sprite)
            {
                people = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

                people.myPosition = people.myMapPosition - playerMapPos;
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(people.myTexture, people.myPosition,
                        null, Color.White,
                        people.myAngle, people.myOrigin, people.myScale,
                        SpriteEffects.None, 0f);
            }

        }

        // The Burning State
        class BurningState : State
        {
            private People people;

            public BurningState(People sprite)
            {
                people = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                people.myAnimate.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    people.myState = new BurntState(people);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(people.myAnimate.GetImage(), people.myPosition, null, Color.White, people.myAngle,
                        people.myOrigin, people.myScale,
                        SpriteEffects.None, 0f);
            }
        }

        // The Burnt State
        class BurntState : State
        {
            private People people;

            public BurntState(People sprite)
            {
                people = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(people.myBurnt, people.myPosition,
                    null, Color.White,
                    people.myAngle, people.myOrigin, people.myScale,
                    SpriteEffects.None, 0f);
            }

        }
    }
}
