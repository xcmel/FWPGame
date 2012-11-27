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
    class SproutTree : Power
    {
        public SproutTree(FWPGame aGame, ArrayList aTextures, Vector2 position, Vector2 mapPosition, ArrayList animateSeq) :
            base(aGame, aTextures, position, mapPosition, animateSeq)
        {
            game = aGame;
            textures = aTextures;
            animateSequences = animateSeq;
            myPosition = position;
            myMapPosition = mapPosition;
        }

        public override void Interact(List<Sprite> sprites, MouseState mState)
        {
            foreach (Sprite s in sprites)
            {
                if (s != null)
                {
                    if (s.name.Equals("GrassSprite"))
                    {
                        //game.motherTree.Clone();
                        Tree tree = game.motherTree.Clone();
                        tree.myPosition.X = mState.X;
                        tree.myPosition.Y = mState.Y;
                        tree.myMapPosition.X = game.player.myMapPosition.X + mState.X;
                        tree.myMapPosition.Y = game.player.myMapPosition.Y + mState.Y;
                        game.map.AddToMapTile(tree);
                    }
                }
            }
        }

        public override void Interact(MapTile tile)
        {
            //intentionally empty
        }

        public override void PowerCombo(Power power2, MouseState mState)
        {
            //not currently implemented
        }

    }
}
