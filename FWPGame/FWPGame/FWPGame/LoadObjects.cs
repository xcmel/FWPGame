//Camco
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

namespace FWPGame
{
    public partial class FWPGame : Microsoft.Xna.Framework.Game
    {
        protected internal Tree motherTree;
        protected internal House motherHouse;

        //
        protected void LoadObjects()
        {
            

            // Create a Tree instance
            Texture2D[] burnTreeSequence = {
                Content.Load<Texture2D>("burning/burn_0"),
                Content.Load<Texture2D>("burning/burn_1"),
                Content.Load<Texture2D>("burning/burn_2"),
                Content.Load<Texture2D>("burning/burn_3"),
                Content.Load<Texture2D>("burning/burn_4"),
                Content.Load<Texture2D>("burning/burn_5"),
                Content.Load<Texture2D>("burning/burn_6"),
                Content.Load<Texture2D>("burning/burn_7"),
                Content.Load<Texture2D>("burning/burn_8"),
                Content.Load<Texture2D>("burning/burn_9")
            };
            motherTree = new Tree(Content.Load<Texture2D>("tree/tree"), new Vector2(0, 0), new Vector2(0, 0),
                burnTreeSequence, Content.Load<Texture2D>("tree/burntTree"));


            // Create a House instance
            Texture2D[] burnHouseSequence = {
                Content.Load<Texture2D>("house/houseFire"),
                Content.Load<Texture2D>("burning/burn_0"),
                Content.Load<Texture2D>("burning/burn_1"),
                Content.Load<Texture2D>("burning/burn_2"),
                Content.Load<Texture2D>("burning/burn_3"),
                Content.Load<Texture2D>("burning/burn_4"),
                Content.Load<Texture2D>("burning/burn_5"),
                Content.Load<Texture2D>("burning/burn_6"),
                Content.Load<Texture2D>("burning/burn_7"),
                Content.Load<Texture2D>("burning/burn_8"),
                Content.Load<Texture2D>("burning/burn_9")
            };
            motherHouse = new House(Content.Load<Texture2D>("house/house"), new Vector2(0, 0), new Vector2(0, 0),
                burnHouseSequence, Content.Load<Texture2D>("house/houseBurnt"));

        }

    }

}
