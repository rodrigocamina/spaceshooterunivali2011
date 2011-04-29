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
using XNAGameSpaceShotter;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using Microsoft.Xna.Framework.Storage;
using XNAGameSpaceShotter.src.view;

namespace XNAGameSpaceShotter.src.bean
{
    class Fly : Inimigo
    {
        static Random rnd = new Random();
        int frame = 0;
        float diff;
        int tempo = 200;
        Screen screen;
        int segueTrajeto = 0;
        Boolean pegouTrajeto = true;
        Player player;
        Texture2D life;
        Vector2 positionfly;

        public Fly(GameCore game, Texture2D imgInimigo, int hp, float velocidadeInimigo, int largura, Vector2 positionfly, Player player)
            : base(game, imgInimigo,hp,velocidadeInimigo,largura)
        {
            this.positionInimigo = positionfly;
            this.player = player;
            life = mygame.Content.Load<Texture2D>("life");
        }

        public override void Draw(GameTime gameTime)
        {
           // mygame.spriteBatch.Draw(life, new Rectangle((int)positionInimigo.X, (int)positionInimigo.Y, hp, 20), Color.White);
            mygame.spriteBatch.Draw(imgInimigo, positionInimigo, new Rectangle(frame * largura, 0, largura, altura), Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            diff = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            tempo -= gameTime.ElapsedGameTime.Milliseconds;
            

            if (positionInimigo.X > mygame.Window.ClientBounds.Width / 2 && pegouTrajeto)
            {
                segueTrajeto = 1;
                pegouTrajeto = false;
            }

            if (segueTrajeto == 1)
            {

                positionInimigo.X -= velocidadeInimigo *diff;
                positionInimigo.Y += velocidadeInimigo * diff;
            }
            else {
                positionInimigo.Y += velocidadeInimigo * diff;
                positionInimigo.X += velocidadeInimigo * diff;
            }

            //TIRO
            //if (frame == 3)
            //{
            //   screen.addComponent(new TiroInimigo(mygame, imgTiro, positionInimigo, 4, 1, screen));
            //}
            if (tempo < 0)
            {
                tempo = 200;
                if (frame < 2)
                {
                    frame++;

                }
                else
                {
                    frame = 0;

                }

            }

            if (positionInimigo.Y > mygame.Window.ClientBounds.Height)
            {
                mygame.actualScreen.removeComponent(this);
            }
        }
    }
}
