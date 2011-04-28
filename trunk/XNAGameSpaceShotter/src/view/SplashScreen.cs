using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNAGameSpaceShotter.src.view
{
    public class SplashScreen : Screen
    {
        long timer = 1000;
        Button bt1;
        Button bt2;
        Texture2D bg;
        Texture2D title;
        List<Vector2> bgpos = new List<Vector2>();
        int szbg;
        Vector2 animacao = Vector2.Zero;
        Vector2 titlepos;
        public SplashScreen(GameCore gamecore)
            : base(gamecore)
        {
            int width = gamecore.Window.ClientBounds.Right - gamecore.Window.ClientBounds.Left;
            int height = gamecore.Window.ClientBounds.Bottom - gamecore.Window.ClientBounds.Top;
            bt1 = new Button(gamecore, mygame.Content.Load<Texture2D>("Button"), mygame.Content.Load<Texture2D>("labelStart"), mygame.Content.Load<Texture2D>("ButtonS2"), 23, new Vector3(width / 3 - 50, height * 4 / 5, 0), new ScriptBt1(mygame), 1.4f);
            bt2 = new Button(gamecore, mygame.Content.Load<Texture2D>("Button"), mygame.Content.Load<Texture2D>("labelEject"), mygame.Content.Load<Texture2D>("ButtonS2"), 23, new Vector3(width * 2 / 3 - 50, height * 4 / 5, 0), new ScriptBt2(mygame), 1.4f);
            addComponent(bt1);
            addComponent(bt2);
            bt1.setSelected(true);
            bg = mygame.Content.Load<Texture2D>("bg");
            title = mygame.Content.Load<Texture2D>("labelTitle");
            titlepos = new Vector2((width - title.Width) / 2, height / 5);
            while (width > 0)
            {
                width -= 200;
                int htemp = height;
                while (htemp > -200)
                {
                    htemp -= 200;
                    bgpos.Add(new Vector2(width, htemp));
                }
            }
            szbg = bgpos.Count;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            animacao.Y += gameTime.ElapsedGameTime.Milliseconds / 50.0f;
            if (animacao.Y > 200)
            {
                animacao.Y -= 200;
            }
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                if (isPressed(Microsoft.Xna.Framework.Input.Keys.Up, Microsoft.Xna.Framework.Input.Buttons.DPadUp) ||
                    isPressed(Microsoft.Xna.Framework.Input.Keys.Right, Microsoft.Xna.Framework.Input.Buttons.DPadRight) ||
                    isPressed(Microsoft.Xna.Framework.Input.Keys.Left, Microsoft.Xna.Framework.Input.Buttons.DPadLeft) ||
                    isPressed(Microsoft.Xna.Framework.Input.Keys.Down, Microsoft.Xna.Framework.Input.Buttons.DPadDown))
                {
                    bt1.setSelected(!bt1.isSelected());
                    bt2.setSelected(!bt2.isSelected());
                    timer = 200;
                }
                if (isPressed(Microsoft.Xna.Framework.Input.Keys.A, Microsoft.Xna.Framework.Input.Buttons.A))
                {
                    if (bt1.isSelected())
                    {
                        bt1.Press();
                        timer = 2000;
                    }
                    if (bt2.isSelected())
                    {
                        bt2.Press();
                        timer = 2000;
                    }
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < szbg; i++)
            {
                mygame.spriteBatch.Draw(bg, bgpos[i] + animacao, Color.White);
            }
            base.Draw(gameTime);
            mygame.spriteBatch.Draw(title, titlepos, Color.White);
        }
    }
    class ScriptBt1 : Script
    {
        GameCore g;
        public ScriptBt1(GameCore g)
        {
            this.g = g;
        }
        public override void execute()
        {
            Console.WriteLine("TESTE BOTAO 1");
            g.setScreen(new ScreenGamePlay(g));
        }
    }
    class ScriptBt2 : Script
    {
        GameCore g;
        public ScriptBt2(GameCore g)
        {
            this.g = g;
        }
        public override void execute()
        {
            g.Exit();
        }
    }
}
