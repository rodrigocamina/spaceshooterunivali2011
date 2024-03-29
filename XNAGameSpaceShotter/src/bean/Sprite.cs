﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.bean {
    public class Sprite : Component{
        Texture2D baseImg;
        Rectangle sourceRectangle;
        public int sprite;
        int nsprites;
        int wsprites;
        int timeSprite = 100;
        long timer;
        public Rectangle positionRect;

        public Sprite(GameCore mygame, Texture2D baseImg, int nsprites, Vector3 position, float scale)
            : base(mygame, null) {
                this.baseImg = baseImg;
                this.nsprites = nsprites;
                this.wsprites = baseImg.Width / nsprites;
                this.positionRect = new Rectangle((int)position.X, (int)position.Y, (int)(scale * wsprites), (int)(scale * baseImg.Height));
                this.sourceRectangle = new Rectangle(0, 0, wsprites, baseImg.Height);
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
                sourceRectangle.X = sprite * wsprites;
            }
        }
    }
}
