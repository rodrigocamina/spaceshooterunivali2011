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
using XNAGameSpaceShotter;
using XNAGameSpaceShotter.src.view;


namespace XNAGameSpaceShotter.src.bean
{
    class Chefe:Inimigo
    {
         Texture2D imgTiro;
         Texture2D imgMisseel;
         Vector2 positionTiro;
         int score;
         static Random rnd = new Random();
         int frame = 0;
         float diff;
         int tempo = 200;
         Boolean segueEsqueda = false;
         Boolean volta = false;
         ScreenGamePlay screen;
         Boolean tiro = true;
         Texture2D life;
         int tempoTiro = 500;
         Boolean naoDesse = false;
         Boolean direita = false;

         public Chefe(GameCore game, Texture2D imgInimigo, int hp, int velocidadeInimigo, int largura, ScreenGamePlay screen)
            : base(game, imgInimigo,hp,velocidadeInimigo,largura)
        {
            positionInimigo = new Vector2(rnd.Next(mygame.Window.ClientBounds.Width), 0);
            this.screen = screen;
            imgMisseel = mygame.Content.Load<Texture2D>("missel");
            imgTiro = mygame.Content.Load<Texture2D>("TiroPlayer");
            life = mygame.Content.Load<Texture2D>("life");
        }

        public override void Draw(GameTime gameTime)
        {
            mygame.spriteBatch.Draw(imgInimigo, positionInimigo, new Rectangle(frame * largura, 0, largura, altura), Color.White);
            mygame.spriteBatch.Draw(life, new Rectangle((int)positionInimigo.X, (int)positionInimigo.Y - 20, hp, 20), Color.White);
            
        }

        public override void Update(GameTime gameTime)
        {
            diff = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            tempo -= gameTime.ElapsedGameTime.Milliseconds;
            tempoTiro -= gameTime.ElapsedGameTime.Milliseconds;
           
            //TIRO
            if (tempoTiro < 0){
                screen.addComponent(new TiroInimigo(mygame, imgTiro, new Vector2(positionInimigo.X+5,positionInimigo.Y), 4,1,imgTiro.Width/4, screen));
                screen.addComponent(new TiroInimigo(mygame, imgTiro, new Vector2(positionInimigo.X + 50, positionInimigo.Y), 4, 1, imgTiro.Width / 4, screen));
                tempoTiro = 500;
            }

            if(positionInimigo.Y > altura + 30){
                naoDesse = true;
            }
            if (!naoDesse)
            {
                positionInimigo.Y += velocidadeInimigo * diff;
            }
            if (direita)
            {
                if (positionInimigo.X < mygame.Window.ClientBounds.Width - largura)
                {
                    positionInimigo.X += velocidadeInimigo * diff;
                }
                else
                {
                    direita = false;
                }
            }
            else
            {
                if (positionInimigo.X > 0)
                {
                    positionInimigo.X -= velocidadeInimigo * diff;
                }
                else
                {
                    direita = true;
                }
            }

            if (tempo < 0)
            {
                tempo = 200;
                if (frame < 3)
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
       
