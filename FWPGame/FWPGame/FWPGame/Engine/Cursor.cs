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
    public class Cursor : Sprite
    {
        private FWPGame myGame;
        private Tree tree;
        private House house;

        public Cursor(Texture2D texture, Vector2 position, FWPGame game) :
            base(texture, position)
        {
            myGame = game;
            myTexture = texture;
            myPosition = position;
        }

        public void Update(MouseState mState)
        {
            myPosition.X = mState.X;
            myPosition.Y = mState.Y;


            myMapPosition = myGame.player.myMapPosition + myPosition;

            if (mState.LeftButton == ButtonState.Pressed)
            {
                SpawnHouse(mState);
            }

            if (mState.RightButton == ButtonState.Pressed)
            {
                if (house != null)
                {
                    house.burn();
                }
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

        private void SpawnHouse(MouseState mState)
        {
            house = myGame.motherHouse.Clone();
            house.myPosition.X = mState.X;
            house.myPosition.Y = mState.Y;
            house.myMapPosition.X = myGame.player.myMapPosition.X + mState.X;
            house.myMapPosition.Y = myGame.player.myMapPosition.Y + mState.Y;
            myGame.map.AddToMapTile(house);
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
