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
    public class Tree : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;

        public Tree(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] animateSequence, Texture2D burnt) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
  
            myAnimateSequence = animateSequence; 
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();
            myBurnt = burnt;
            myState = new RegularState(this);
            
            
        }

        public Tree Clone()
        {
            return new Tree(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), myAnimateSequence, myBurnt);
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
            myAnimate.AddFrame(1, 300);
            myAnimate.AddFrame(2, 200);
            myAnimate.AddFrame(3, 250);
            myAnimate.AddFrame(4, 150);
            myAnimate.AddFrame(5, 90);
            myAnimate.AddFrame(6, 310);
            myAnimate.AddFrame(7, 200);
            myAnimate.AddFrame(8, 110);
            myAnimate.AddFrame(9, 350);
        }


        // The Regular State
        class RegularState : State
        {
            private Tree tree;

            public RegularState(Tree sprite)
            {
                tree = sprite;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

                tree.myPosition = tree.myMapPosition - playerMapPos;
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myTexture, tree.myPosition,
                        null, Color.White,
                        tree.myAngle, tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }

        }

        // The Burning State
        class BurningState : State
        {
            private Tree tree;

            public BurningState(Tree sprite)
            {
                tree = sprite;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                tree.myAnimate.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    tree.myState = new BurntState(tree);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myAnimate.GetImage(), tree.myPosition, null, Color.White, tree.myAngle,
                        tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }
        }

        // The Burnt State
        class BurntState : State
        {
            private Tree tree;

            public BurntState(Tree sprite)
            {
                tree = sprite;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myBurnt, tree.myPosition,
                    null, Color.White,
                    tree.myAngle, tree.myOrigin, tree.myScale,
                    SpriteEffects.None, 0f);
            }

        }
    }
}
