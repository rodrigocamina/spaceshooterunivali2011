using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StorageDemo;

namespace XNAGameSpaceShotter.src.view {
    public class ScreenRecords: Screen {
        List<Label> ranks = new List<Label>();
        Texture2D bg;
        List<Vector2> bgpos = new List<Vector2>();
        int szbg;
        Vector2 animacao = Vector2.Zero;
        bool opening = true;
        bool closing = false;
        Sprite recordsSprite;

        public ScreenRecords(GameCore game, Vector2 animacao) : base(game) {
            this.animacao = animacao;
            bg = mygame.Content.Load<Texture2D>("bg");
            int width = game.Window.ClientBounds.Right - game.Window.ClientBounds.Left;
            int height = game.Window.ClientBounds.Bottom - game.Window.ClientBounds.Top;
            Texture2D img = mygame.Content.Load<Texture2D>("labelRecordsTitle");
            recordsSprite = new Sprite(mygame, img, 1, new Vector3(width / 2 - img.Width / 2, 20, 0), 1);
            while (width > 0) {
                width -= 200;
                int htemp = height;
                while (htemp > -200) {
                    htemp -= 200;
                    bgpos.Add(new Vector2(width, htemp));
                }
            }
            szbg = bgpos.Count;
            while (SaveGame.getInstance().GameLoadRequested) {
                Console.WriteLine("waiting.....");
                SaveGame.getInstance().Update();
            }
            int y = 150;
            SaveGame.SavePlayer save = SaveGame.getInstance().player;
            if (save.score == null) {
                Console.WriteLine("sem registro");
            } else {
                for (int i = 0; i < 10; i++) {
                    SaveGame.SavePlayerBean bean = save.score[i];
                    Label lb = new Label(Player.completer((i + 1) + "", 2, "0") + " " + bean.date + " " + bean.name + " " + Player.completer(bean.score + "", 7, "0"), new Vector3(i * -20, y, 0), mygame);
                    lb.interChar = 0;
                    ranks.Add(lb);
                    y += 30;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime) {
            for (int i = 0; i < szbg; i++) {
                mygame.spriteBatch.Draw(bg, bgpos[i]+animacao, Color.White);
            }
            base.Draw(gameTime);
            recordsSprite.Draw(gameTime);
            int op = 0;
            for (int i = 0; i < ranks.Count; i++) {
                Label testelb = ranks[i];
                testelb.Draw(gameTime);
                if (opening) {
                    if(testelb.position.X<23){
                        testelb.position.X++;
                    }else{
                        testelb.interChar++;
                        if (testelb.interChar > 23) {
                            testelb.interChar = 23;
                            op++;
                        }
                    }
                } else if (closing) {
                    testelb.interChar--;
                    if (testelb.interChar < 0) {
                        testelb.interChar = 0;
                        mygame.setScreen(new ScreenTitle(mygame, animacao));
                    }
                }
            }
            if (op == ranks.Count) {
                opening = false;
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            animacao.Y += gameTime.ElapsedGameTime.Milliseconds / 50.0f;
            if (animacao.Y > 200) {
                animacao.Y -= 200;
            }
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Buttons.A)||
                isPressed(Microsoft.Xna.Framework.Input.Keys.Escape, Microsoft.Xna.Framework.Input.Buttons.B)) {
                    closing = true;
            }
        }


    }
}
