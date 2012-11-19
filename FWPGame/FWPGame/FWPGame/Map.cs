using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using FWPGame.Engine;

namespace FWPGame
{
    public class Map
    {
        private const int MAX_TILE_SIZE = 100;
        private Vector2 myScreenSize;
        private Player myPlayer;
        private MapTile[,] mapTiles;
        private Vector2 myPosition;
        private Texture2D myTexture;
        private float myAngle = 0f;
        private Vector2 myOrigin = new Vector2(0, 0);
        private Vector2 myScale = new Vector2(1, 1);

        public Map(Texture2D texture, Vector2 screenSize, Vector2 position, Player player)
        {
            myTexture = texture;
            myPosition = position;
            myScreenSize = screenSize;
            myPlayer = player;
            CreateTileGrid(texture.Bounds);
        }

        /// <summary>
        /// Update the map's relative positions with the player's position
        /// </summary>
        public void Update(GameTime gameTime, MouseState mState, KeyboardState kState)
        {
            myPlayer.Update(gameTime.ElapsedGameTime.TotalSeconds, kState, mState);
            myPosition = -(myPlayer.myMapPosition);
            foreach (MapTile m in mapTiles)
            {
                m.Update(gameTime, myPlayer.myMapPosition);
            }
        }

        public void CreateTileGrid(Rectangle textureSize)
        {

            int tiles = (int) textureSize.Width / MAX_TILE_SIZE;
            mapTiles = new MapTile[tiles, tiles];
            for (int i = 0; i < tiles; i++)
            {
                for (int j = 0; j < tiles; j++)
                {
                    MapTile tile = new MapTile(new Vector2(i * MAX_TILE_SIZE, j * MAX_TILE_SIZE),
                        new Vector2(MAX_TILE_SIZE, MAX_TILE_SIZE));
                    mapTiles[i, j] = tile;
                }
            }
        }

        public void AddToMapTile(Sprite s)
        {
            var x = (int)(myPlayer.myCursor.myMapPosition.X / (MAX_TILE_SIZE * myScale.X));
            var y = (int)(myPlayer.myCursor.myMapPosition.Y / (MAX_TILE_SIZE * myScale.Y));
            mapTiles[x, y].Add(s);
        }

        /// <summary>
        /// Draw the map, then draw the MapTiles that are visible to the player.
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, myPosition,
                   null, Color.White,
                   myAngle, myOrigin, myScale,
                   SpriteEffects.None, 0f);

            // We only want to render the map tiles that are visible, so we need to find which ones the player can see
            var x = (int)Math.Floor((myPlayer.myMapPosition.X / (MAX_TILE_SIZE * myScale.X)));
            var xmax = (int)Math.Ceiling((myPlayer.myMapPosition.X + myScreenSize.X) / (MAX_TILE_SIZE * myScale.X));
            var y = (int)Math.Floor((myPlayer.myMapPosition.Y) / (MAX_TILE_SIZE * myScale.Y));
            var ymax = (int)Math.Ceiling((myPlayer.myMapPosition.Y + myScreenSize.Y) / (MAX_TILE_SIZE * myScale.Y));

            int xToRender = (int)Math.Floor(myScreenSize.X / MAX_TILE_SIZE);
            int yToRender = (int)Math.Floor(myScreenSize.Y / MAX_TILE_SIZE);

            for (int i = x; i < xmax; i++)
            {
                for (int j = y; j < ymax; j++)
                {
                    mapTiles[i, j].Draw(batch);
                }
            }
        }

    }
}
