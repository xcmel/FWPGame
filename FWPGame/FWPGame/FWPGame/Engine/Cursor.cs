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
using FWPGame.Powers;
using System.Collections;

namespace FWPGame.Engine
{
    public class Cursor : Sprite
    {
        private FWPGame myGame;
        private Tree tree;
        private ArrayList myPowers;
        private List<Sprite> sprites;
        public Cursor(Texture2D texture, Vector2 position, FWPGame game, ArrayList powers) :
            base(texture, position)
        {
            myGame = game;
            myTexture = texture;
            myPosition = position;
            myPowers = powers;
            sprites = new List<Sprite>();
        }

        public List<Sprite> getTileSprites(MouseState mState)
        {
            List<Sprite> tileSprites = new List<Sprite>();
            MapTile tile = myGame.map.GetTile(this);
            if (tile != null)
            {
                tileSprites = tile.mySprites;
            }
            return tileSprites;
        }

        public void Update(MouseState mState, KeyboardState kState)
        {
            if (mState.X >= 0 && mState.X < myGame.GraphicsDevice.Viewport.Width - 1)
            {
                myPosition.X = mState.X;
            }
            if (mState.Y >= 0 && mState.Y < myGame.GraphicsDevice.Viewport.Height - 1)
            {
                myPosition.Y = mState.Y;
            }
            Array keys = kState.GetPressedKeys();
            myMapPosition = myGame.player.myMapPosition + myPosition;

            MapTile tile = myGame.map.GetTile(this);
            if (tile != null)
            {
                sprites = tile.mySprites;
            }


            //refactor all of this into a dictionary and use an input manager
            if (mState.LeftButton == ButtonState.Pressed)
            {
                Power power = (Power)myPowers[0];
                if (sprites.Count > 0)
                {
                    //SpawnGrass(mState);
                    power.Interact(tile, mState);
                }
                else
                {
                    //SpawnGrass(mState);
                    power.Interact(tile);
                    Debug.WriteLine(tile.myMapPosition);
                }
            }

            if (mState.RightButton == ButtonState.Pressed)
            {
                List<Sprite> tileSprites = getTileSprites(mState);
                Power power = (Power)myPowers[1];
                if (tileSprites.Count > 0)
                {
                    if (!(tileSprites[tileSprites.Count-1].name.Equals("Tree")))
                        power.Interact(tile);
                }
            }
            // Fire
            if(kState.IsKeyDown(Keys.D1))
            {
                Power power = (Power)myPowers[2];
                Debug.WriteLine(power.GetType());
                power.Interact(tile);
            }
            if (kState.IsKeyDown(Keys.D2))
            {
                if (tile.mySprites.Count == 0)
                    tile.Add(myGame.motherHouse.Clone());
            }
        }

        private void SpawnGrass(MouseState mState)
        {
            GrassSprite grass = myGame.myGrass.Clone();
            grass.myPosition.X = mState.X;
            grass.myPosition.Y = mState.Y;
            Debug.WriteLine("cursor grass pos: " + myPosition);
            grass.myMapPosition.X = myGame.player.myMapPosition.X + mState.X;
            grass.myMapPosition.Y = myGame.player.myMapPosition.Y + mState.Y;
            Debug.WriteLine("cursor grass map pos: " + myGame.player.myMapPosition);
            myGame.map.AddToMapTile(grass);
        }

        private void SpawnTree(MouseState mState)
        {
            tree = myGame.motherTree.Clone();
            tree.myPosition.X = mState.X;
            tree.myPosition.Y = mState.Y;
            tree.myMapPosition.X = myGame.player.myMapPosition.X + mState.X;
            tree.myMapPosition.Y = myGame.player.myMapPosition.Y + mState.Y;
            myGame.map.AddToMapTile(tree);
        }

        private void BurnTree(MouseState mState)
        {
            MapTile tile = myGame.map.GetTile(this);
            if (tile != null)
            {
                List<Sprite> sprites = tile.mySprites;
                foreach(Sprite s in sprites)
                {
                    try
                    {
                        Tree tree = (Tree)s;
                        tree.burn();
                    }
                    catch
                    {
                        //intentionally empty
                    }
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, myPosition,
                   null, Color.White,
                   myAngle, myOrigin, myScale,
                   SpriteEffects.None, 0f);
        }
    }
}
