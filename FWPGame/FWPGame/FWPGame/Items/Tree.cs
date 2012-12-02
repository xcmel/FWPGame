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
using FWPGame.Engine;
using FWPGame.Powers;
using System.Collections;
using System.Diagnostics;

namespace FWPGame
{
    public class Tree : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;
        private Texture2D[] myMultiplySequence;
        private Animate myMultiply;
        private FWPGame myGame;

        public Tree(Texture2D texture, Vector2 position, Vector2 mapPosition,
            Texture2D[] animateSequence, Texture2D burnt, Texture2D[] multiplyTree,
            FWPGame game) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "Tree";
            myAnimateSequence = animateSequence;
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();
            myBurnt = burnt;
            myMultiplySequence = multiplyTree;
            myMultiply = new Animate(multiplyTree);
            SetUpMultiply();
            myGame = game;
            myState = new RegularState(this);


        }

        public Tree Clone()
        {
            return new Tree(this.myTexture, new Vector2(0, 0), new Vector2(0, 0),
                myAnimateSequence, myBurnt, myMultiplySequence, myGame);
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

        public void SetUpMultiply()
        {
            // Prepare the flip book sequence for multiply Animate
            for (int i = 0; i < 68; ++i)
            {
                myMultiply.AddFrame(i, 100);
            }
        }



        // The Regular State
        class RegularState : State
        {
            private Tree tree;

            public RegularState(Tree sprite)
            {
                tree = sprite;
            }


            public Sprite Spread()
            {

                //  tree.myPosition = tree.myMapPosition - playerMapPos;

                //    MapTile tile = tree.myGame.map.SpreadTile(Proximate());
                //    SproutTree spread = new SproutTree(tree.myTexture, tree.myGame, tile.myPosition, tile.myMapPosition);
                //    spread.Interact(tile);
                //    Tree newTree = spread.NewTree;
                //    if (newTree != null)
                //    {
                //        newTree.myState = new MultiplyState(newTree);
                //    }
                /*
               
               MapTile tile = tree.myGame.map.SpreadTile(new Vector2(0, 0));
               tile.Clear();
               GrassSprite newGrass = tree.myGame.myGrass.Clone();
               tile.Add(newGrass);
                 
               Tree newTree = tree.Clone();
               newTree.myState = new MultiplyState(newTree);
               newTree.myMapPosition = tile.myMapPosition;
               newTree.myPosition = tile.myPosition;
               newTree.myScale = tile.myScale;

               tile.Add(newTree); */


                // MapTile tile = myGame.map.SpreadTile(new Vector2(100, 100));
                // tile.Clear();

                // newTree.myMapPosition = tile.myMapPosition;
                // newTree.myPosition = tile.myPosition;
                // newTree.myScale = tile.myScale;

                // tile.mySprites.Add(newTree);

                
                    Tree newTree = tree.Clone();
                    newTree.myState = new MultiplyState(newTree);
                    return newTree;

            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
              
            }

            public void Draw(SpriteBatch batch)
            {              
                batch.Draw(tree.myTexture, tree.myPosition,
                        null, Color.White,
                        tree.myAngle, tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }

            private Vector2 Proximate()
            {
                // @@@ fill in with randomized selection from 8 surrounding tiles

                return new Vector2(tree.myMapPosition.X + Map.MAX_TILE_SIZE, tree.myMapPosition.Y);
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


            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
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
                batch.Draw(tree.myBurnt, tree.myPosition,
                    null, Color.White,
                    tree.myAngle, tree.myOrigin, tree.myScale,
                    SpriteEffects.None, 0f);
            }

        }


        // The Multiplying State
        class MultiplyState : State
        {
            private Tree tree;

            public MultiplyState(Tree sprite)
            {
                tree = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                
                tree.myMultiply.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                   tree.myState = new RegularState(tree);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myMultiply.GetImage(), tree.myPosition, null, Color.White, tree.myAngle,
                        tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }
        }
    }
}
