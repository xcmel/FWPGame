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

namespace FWPGame
{
    public class MapSprite : Sprite
    {
        private Vector2 myScreenSize;
        private Vector2 myMapPosition;
        public MapSprite(Texture2D texture, Vector2 myMapPosition, Vector2 position, Vector2 screenSize) :
            base(texture, position)
        {
            myPosition = position;
            myScreenSize = screenSize;
            myScale.X = myScreenSize.X / texture.Width;
            myScale.Y = myScreenSize.Y / texture.Height;
        }

        public bool WithinBounds(Vector2 playerPosition)
        {
            bool isInXBounds = ((playerPosition.X >= myMapPosition.X && playerPosition.X <= myMapPosition.X + myScreenSize.X)
                || (playerPosition.X + myScreenSize.X >= myMapPosition.X && playerPosition.X + myScreenSize.X <= myMapPosition.X + myScreenSize.X));
            bool isInYBounds = ((playerPosition.Y >= myMapPosition.Y && playerPosition.Y <= myMapPosition.Y + myScreenSize.Y)
                || (playerPosition.Y + myScreenSize.Y >= myMapPosition.Y && playerPosition.Y + myScreenSize.Y <= myMapPosition.Y + myScreenSize.Y));

            if (isInXBounds && isInYBounds)
                return true;

            return false;
        }

        public void Update(Vector2 playerPosition)
        {
            myPosition = myMapPosition - playerPosition;
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
