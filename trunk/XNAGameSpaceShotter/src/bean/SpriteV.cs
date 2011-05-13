using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.bean {
    public class SpriteV : Component{
        Texture2D baseImg;
        Rectangle sourceRectangle;
        int sprite;
        int nsprites;
        int wsprites;
        int timeSprite = 100;
        long timer;
        public Rectangle positionRect;

        public SpriteV(GameCore mygame, Texture2D baseImg, int nsprites, Vector3 position, float scale)
            : base(mygame, null) {
                this.baseImg = baseImg;
                this.nsprites = nsprites;
                this.wsprites = baseImg.Height / nsprites;
                this.positionRect = new Rectangle((int)position.X, (int)position.Y, (int)(scale * baseImg.Width), (int)(scale * wsprites));
                this.sourceRectangle = new Rectangle(0, 0, baseImg.Width, wsprites);
        }

        public int Sprite {
            get { return sprite; }
            set { sprite = value; sourceRectangle.Y = sprite * wsprites; }
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime) {
            mygame.spriteBatch.Draw(baseImg, positionRect, sourceRectangle, Color.White);
        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Vector2 position) {
            mygame.spriteBatch.Draw(baseImg, position, sourceRectangle, Color.White);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > timeSprite) {
                timer -= timeSprite;
                sprite++;
                if (sprite >= nsprites) {
                    sprite = 0;
                }
                sourceRectangle.Y = sprite * wsprites;
            }
        }
    }
}
