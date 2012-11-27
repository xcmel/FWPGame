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
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;

namespace FWPGame.Powers
{

    class Power
    {
        protected internal ArrayList textures;//Texture2D
        protected internal ArrayList animateSequences;//Texture2D[]
        protected internal Vector2 myPosition;
        protected internal Vector2 myMapPosition;
        protected internal FWPGame game;

        public Power(FWPGame game, ArrayList textures, Vector2 myPosition, Vector2 myMapPosition, ArrayList animateSequences)
        {
            this.game = game;
            this.textures = textures;
            this.animateSequences = animateSequences;
            this.myPosition = myPosition;
            this.myMapPosition = myMapPosition;
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }

        public virtual void Interact(List<Sprite> sprites, MouseState mState)
        {
        }

        public virtual void Interact(MapTile tile)
        {
        }

        public virtual void PowerCombo(Power power2, MouseState mState)
        {
        }
    }
}
