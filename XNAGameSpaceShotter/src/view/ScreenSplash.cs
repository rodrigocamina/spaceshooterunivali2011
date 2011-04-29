using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.view {
    public class ScreenSplash: Screen {
        long timer = 5000;
        Texture2D bg;
        List<Vector2> bgpos = new List<Vector2>();
        int szbg;
        Vector2 animacao = Vector2.Zero;
        List<Sprite> naves = new List<Sprite>();
        Random rnd = new Random();
        int width;
        int height;
        Sprite passa;

        public ScreenSplash(GameCore gamecore):base(gamecore) {
            int width = this.width = gamecore.Window.ClientBounds.Right - gamecore.Window.ClientBounds.Left;
            int height = this.height = gamecore.Window.ClientBounds.Bottom - gamecore.Window.ClientBounds.Top;
            for (int i = 0; i < 30;i++ ) {
                Sprite nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("naveP"), 54, 4, new Vector3(rnd.Next(600), rnd.Next(600), 0), (float)((rnd.NextDouble() + 1)/2));
                addComponent(nave);
                naves.Add(nave);
            }
            
            bg = mygame.Content.Load<Texture2D>("bg");
            while (width > 0) {
                width -= 200;
                int htemp = height;
                while (htemp > -200) {
                    htemp -= 200;
                    bgpos.Add(new Vector2(width, htemp));
                }
            }
            szbg = bgpos.Count;
            mygame.sons.playSong(0);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            base.Update(gameTime);
            animacao.Y += gameTime.ElapsedGameTime.Milliseconds / 50.0f;
            if (animacao.Y > 200) {
                animacao.Y -= 200;
            }
            if (timer > 0) {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            } else if (timer != -500) {
                timer = -500;
                int wSprite = 25;
                passa = new Sprite(mygame, mygame.Content.Load<Texture2D>("explosao2"), wSprite, 10, new Vector3(-800, -600, 0), 100);
                mygame.sons.playSound(1);
                passa.sprite = 1;
                addComponent(passa);

            } else {
                if (passa.sprite == 6) {
                    unloadComponents();
                    addComponent(passa);
                }
                if (passa.sprite == 0) {
                    mygame.setScreen(new ScreenTitle(mygame,animacao));
                }
            }
            for (int i = 0; i < naves.Count; i++) {
                Sprite nave = naves[i];
                nave.positionRect.Y -= gameTime.ElapsedGameTime.Milliseconds / 5;
                if (nave.positionRect.Y < -nave.positionRect.Height) {
                    nave.positionRect.Y = mygame.Window.ClientBounds.Height + nave.positionRect.Height + rnd.Next(60);
                    nave.positionRect.X = rnd.Next(mygame.Window.ClientBounds.Width - nave.positionRect.Width);
                }
            }
        }
        public override void Draw(GameTime gameTime) {
            for (int i = 0; i < szbg; i++) {
                mygame.spriteBatch.Draw(bg, bgpos[i]+animacao, Color.White);
            }
            base.Draw(gameTime);
        }
    }
}
