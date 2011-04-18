using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.bean {
    public class Button : Component{
        Texture2D baseImg;
        Texture2D text;
        Texture2D selection;
        Rectangle sourceRectangle;
        int sprite;
        int nsprites;
        int wsprites;
        int timeSprite = 20;
        long timer;
        bool pressed = false;
        bool selected = false;
        bool done = false;
        Script script;
        Rectangle myposition;

        public Button(GameCore mygame, Texture2D baseImg, Texture2D text, Texture2D selection, int nsprites, Vector3 position, Script script, float scale)
            : base(mygame, null) {
                this.baseImg = baseImg;
                this.text = text;
                this.nsprites = nsprites;
                this.selection = selection;
                this.wsprites = baseImg.Width / nsprites;
                this.myposition = new Rectangle((int)position.X , (int)position.Y, (int)(scale * text.Width), (int)(scale * text.Height));
                this.sourceRectangle = new Rectangle(0, 0, wsprites, baseImg.Height);
                this.script = script;
        }

        public void setSelected(bool isSelected){
            selected = isSelected;
        }
        public bool isSelected() {
            return selected;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime) {
            mygame.spriteBatch.Draw(baseImg, myposition, sourceRectangle, Color.White);
            if (selected&&sprite == 0) {
                mygame.spriteBatch.Draw(selection, myposition, Color.White);
            }
            mygame.spriteBatch.Draw(text, myposition, Color.White);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            if (pressed) {
                timer += gameTime.ElapsedGameTime.Milliseconds;
                if (timer > timeSprite) {
                    timer -= timeSprite;
                    sprite++;
                    if (sprite >= nsprites) {
                        sprite = nsprites - 1;
                        pressed = false;
                        done = true;
                    }
                }
            } else {
                if (done) {
                    timer += gameTime.ElapsedGameTime.Milliseconds;
                    if (timer > 1000) {
                        sprite = 0;
                        timer = 0;
                        script.execute();
                    }
                }
            }
            sourceRectangle.X = sprite * wsprites;
        }
        public void Press() {
            pressed = true;
            timer = 0;
        }
    }
}
