using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;
using System.Collections;

namespace FWPGame.Powers
{
    class GrowGrass : Power
    {
        public GrowGrass(FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
        }

        public override void Interact(MapTile tile, MouseState mState)
        {
            //intentionall empty
        }

        public override void Interact(MapTile tile)
        {
            tile.Add(game.myGrass.Clone());
            
        }

        public override void PowerCombo(Power power2, MouseState mState)
        {
            //not currently implemented
        }
    }

}
