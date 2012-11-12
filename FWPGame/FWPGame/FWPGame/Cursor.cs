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
    class Cursor : Sprite
    {
        public Cursor(Texture2D texture, Vector2 position) :
            base(texture, position)
        {
            myTexture = texture;
            myPosition = position;
        }
    }
}
