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

namespace FWPGame.Engine
{
    public class Map
    {
        private const int MAX_TILE_SIZE = 64;
        private Vector2 myScreenSize;
        private Player myPlayer;
        private MapTile[,] mapTiles;
        private Vector2 myPosition;
        private Texture2D myTexture;
        private float myAngle = 0f;
        private Vector2 myOrigin = new Vector2(0, 0);
        private Vector2 myScale = new Vector2(1, 1);

        public int getMaxTileSize()
        {
            return MAX_TILE_SIZE;
        }

        public Map(Texture2D texture, Vector2 screenSize, Vector2 position, Player player)
        {
            if (screenSize.X > texture.Width)
            {
                myScale = new Vector2(2, 2);
            }
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

        /// <summary>
        /// change to use cursor
        /// </summary>
        /// <param name="mState"></param>
        /// <returns></returns>
        public MapTile GetTile(Cursor cursor)
        {
            int tilesX = (int) myTexture.Bounds.Width / MAX_TILE_SIZE;
            int tilesY = (int)myTexture.Bounds.Height / MAX_TILE_SIZE;
            int x = (int) cursor.myMapPosition.X / MAX_TILE_SIZE;
            int y = (int) cursor.myMapPosition.Y / MAX_TILE_SIZE;
            if (x >= tilesX)
                x = tilesX - 1;
            if (y >= tilesY)
                y = tilesY - 1;
            MapTile tile = mapTiles[x, y];
            return tile;
        }

        public void CreateTileGrid(Rectangle textureSize)
        {

            int tilesX = (int) textureSize.Width / MAX_TILE_SIZE;
            int tilesY = (int)textureSize.Height / MAX_TILE_SIZE;
            mapTiles = new MapTile[tilesX, tilesY];
            for (int i = 0; i < tilesX; i++)
            {
                for (int j = 0; j < tilesY; j++)
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
            //if to prevent error if clicking off game
            if((x >= 0) && (x < myScreenSize.X) && (y >= 0) && (y < myScreenSize.Y)){ 
                mapTiles[x, y].Add(s);
            }
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
            var xmax = (int)Math.Floor((myPlayer.myMapPosition.X + myScreenSize.X) / (MAX_TILE_SIZE * myScale.X)) - 1;
            var y = (int)Math.Floor((myPlayer.myMapPosition.Y) / (MAX_TILE_SIZE * myScale.Y));
            var ymax = (int)Math.Floor((myPlayer.myMapPosition.Y + myScreenSize.Y) / (MAX_TILE_SIZE * myScale.Y)) - 1;

            if (xmax >= mapTiles.GetLength(0))
                xmax = mapTiles.GetLength(0) - 1;
            if (ymax >= mapTiles.GetLength(1))
                ymax = mapTiles.GetLength(1) - 1;

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
