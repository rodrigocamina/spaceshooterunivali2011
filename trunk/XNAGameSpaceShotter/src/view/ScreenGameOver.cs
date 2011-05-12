using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.view {
    public class ScreenGameOver :Screen {
        long timer = 13000;
        Texture2D bg;
        Texture2D title;
        List<Vector2> bgpos = new List<Vector2>();
        int szbg;
        Vector2 animacao = Vector2.Zero;
        Vector2 titlepos;
        List<Sprite> navesInimigas1 = new List<Sprite>();
        List<Sprite> navesInimigas2 = new List<Sprite>();
        List<Sprite> navesInimigas3 = new List<Sprite>();
        List<Sprite> navesInimigas4 = new List<Sprite>();
        Random rnd = new Random();
        int wheight;
        int wwidth;

        public ScreenGameOver(GameCore gamecore)
            : base(gamecore) {
            int width = wwidth = gamecore.Window.ClientBounds.Right - gamecore.Window.ClientBounds.Left;
            int height = wheight = gamecore.Window.ClientBounds.Bottom - gamecore.Window.ClientBounds.Top;
            for (int i = 0; i < 50; i++) {
                Sprite nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("mosca2"), 3, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2));
                addComponent(nave);
                navesInimigas1.Add(nave);
            }
            for (int i = 0; i < 25; i++) {
                Sprite nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("dualgun"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2));
                addComponent(nave);
                navesInimigas2.Add(nave);
            }
            for (int i = 0; i < 10; i++) {
                Sprite nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("metralhadoraST"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2 + 0.5f));
                addComponent(nave);
                navesInimigas3.Add(nave);
            }
            for (int i = 0; i < 5; i++) {
                Sprite nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("lancadorDeMisseisST"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) ));
                addComponent(nave);
                navesInimigas4.Add(nave);
            }
            bg = mygame.Content.Load<Texture2D>("bg");
            title = mygame.Content.Load<Texture2D>("labelGameOver");
            titlepos = new Vector2((width - title.Width) / 2, height+100);
            while (width > 0) {
                width -= 200;
                int htemp = height;
                while (htemp > -200) {
                    htemp -= 200;
                    bgpos.Add(new Vector2(width, htemp));
                }
            }
            szbg = bgpos.Count;
            mygame.sons.playSong(1);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            base.Update(gameTime);
            animacao.Y += gameTime.ElapsedGameTime.Milliseconds / 50.0f;
            if (animacao.Y > 200) {
                animacao.Y -= 200;
            }
            if (timer > 0) {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            } else {
                mygame.setScreen(new ScreenSplash(mygame));
            }
            for (int i = 0; i < navesInimigas1.Count; i++) {
                Sprite nave = navesInimigas1[i];
                nave.positionRect.Y += gameTime.ElapsedGameTime.Milliseconds / 5;
                if (nave.positionRect.Y > nave.positionRect.Height + wheight) {
                    removeComponent(nave);
                    navesInimigas1.Remove(nave);
                    nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("mosca2"), 3, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2));
                    addComponent(nave);
                    navesInimigas1.Add(nave);
                }
            }
            for (int i = 0; i < navesInimigas2.Count; i++) {
                Sprite nave = navesInimigas2[i];
                nave.positionRect.Y += gameTime.ElapsedGameTime.Milliseconds / 7;
                if (nave.positionRect.Y > nave.positionRect.Height + wheight) {
                    removeComponent(nave);
                    navesInimigas2.Remove(nave);
                    nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("dualgun"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2));
                    addComponent(nave);
                    navesInimigas2.Add(nave);
                }
            }
            for (int i = 0; i < navesInimigas3.Count; i++) {
                Sprite nave = navesInimigas3[i];
                nave.positionRect.Y += gameTime.ElapsedGameTime.Milliseconds / 11;
                if (nave.positionRect.Y > nave.positionRect.Height + wheight) {
                    removeComponent(nave);
                    navesInimigas3.Remove(nave);
                    nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("metralhadoraST"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2 + 0.5f));
                    addComponent(nave);
                    navesInimigas3.Add(nave);
                }
            }
            for (int i = 0; i < navesInimigas4.Count; i++) {
                Sprite nave = navesInimigas4[i];
                nave.positionRect.Y += gameTime.ElapsedGameTime.Milliseconds / 15;
                if (nave.positionRect.Y > nave.positionRect.Height + wheight) {
                    removeComponent(nave);
                    navesInimigas4.Remove(nave);
                    nave = new Sprite(mygame, mygame.Content.Load<Texture2D>("lancadorDeMisseisST"), 4, new Vector3(rnd.Next(wwidth), -rnd.Next(600), 0), (float)((rnd.NextDouble() + 1) / 2));
                    addComponent(nave);
                    navesInimigas4.Add(nave);
                }
            }
            titlepos.Y -= gameTime.ElapsedGameTime.Milliseconds / 10;
        }
        public override void Draw(GameTime gameTime) {
            for (int i = 0; i < szbg; i++) {
                mygame.spriteBatch.Draw(bg, bgpos[i]+animacao, Color.White);
            }
            base.Draw(gameTime);
            mygame.spriteBatch.Draw(title, titlepos, Color.White);
        }
    }
}
