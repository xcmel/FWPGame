using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace FWPGame
{
    class Map
    {
        private MapSprite[,] myMaps;
        private Vector2 myScreenSize;
        private Vector2 myCursorPosition;
        private bool isRightClicking;
        private Player myPlayer;

        public Map(MapSprite[,] maps, Vector2 screenSize, Player player)
        {
            myMaps = maps;
            myScreenSize = screenSize;
            myPlayer = player;
        }

        /// <summary>
        /// Update the map's relative positions with the player's position
        /// </summary>
        public void Update(GameTime gameTime, MouseState mState)
        {
            foreach (MapSprite ms in myMaps)
            {
                if (ms.WithinBounds(myPlayer.myMapPosition))
                {
                    ms.Update(myPlayer.myMapPosition);
                }
            }
        }

        /// <summary>
        /// Given the absolute position of the player, draw the map.
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            foreach (MapSprite ms in myMaps)
            {
                if (ms.WithinBounds(myPlayer.myMapPosition))
                {
                    ms.Draw(spriteBatch);
                }
            }

        }
    }
}
