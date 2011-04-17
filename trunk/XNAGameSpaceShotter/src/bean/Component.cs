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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNAGameSpaceShotter;


namespace XNAGameSpaceShotter.src.bean {
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Component {

        protected Texture2D texture;
        protected int alpha;//alpha de 0 a 255, onde 255 é opaco
        protected Vector3 position;
        protected GameCore mygame;
        private GameCore game;

        public Component(GameCore game, Texture2D texture) {
            this.texture = texture;
            this.mygame = game;
            this.alpha = 255;//opaco
        }
        public Component(Game game, Texture2D texture, int alpha) {
            this.texture = texture;
            this.alpha = alpha;
        }

        public Component(GameCore game)
        {
            // TODO: Complete member initialization
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}