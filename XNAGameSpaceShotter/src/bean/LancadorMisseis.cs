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
    class LancadorMisseis : Inimigo
    {

        Texture2D imgTiro;
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

        public LancadorMisseis(GameCore game, Texture2D imgInimigo, int hp, int velocidadeInimigo, int largura, ScreenGamePlay screen)
            : base(game, imgInimigo,hp,velocidadeInimigo,largura)
        {
            positionInimigo = new Vector2(rnd.Next(mygame.Window.ClientBounds.Width), 0);
            this.screen = screen;
            imgTiro = mygame.Content.Load<Texture2D>("missel");
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
            if (volta)
            {
                positionInimigo.Y -= velocidadeInimigo *diff;
            }
            else {
                positionInimigo.Y += velocidadeInimigo *diff;
            }
            if (positionInimigo.Y < 0)
            {
                volta = false;
            }
            if (positionInimigo.Y > (mygame.Window.ClientBounds.Height / 2) && positionInimigo.X < (mygame.Window.ClientBounds.Width / 2))
            {
                segueEsqueda = true;
                volta = true;
            }

            if (positionInimigo.X < 0)
            {
                segueEsqueda = true;

            }
            if(positionInimigo.X > mygame.Window.ClientBounds.Width - largura){
                segueEsqueda = false;
            }
            
            if (segueEsqueda == true)
            {
                positionInimigo.X += velocidadeInimigo *diff ;

            }else{

                positionInimigo.X -= velocidadeInimigo * diff;
            }
            //TIRO
            if (frame == 10&& tiro == true)
            {
                screen.addComponent(new TiroInimigo(mygame, imgTiro, new Vector2(positionInimigo.X+5,positionInimigo.Y), 4,1,imgTiro.Width/4, screen));
                screen.addComponent(new TiroInimigo(mygame, imgTiro, new Vector2(positionInimigo.X + 50, positionInimigo.Y), 4, 1, imgTiro.Width / 4, screen));
                tiro = false;
            }



            if (tempo < 0)
            {
                tempo = 200;
                if (frame < 11)
                {
                    frame++;

                }
                else
                {
                    frame = 0;
                    tiro = true;
                    
                }

            }

            if (positionInimigo.Y > mygame.Window.ClientBounds.Height)
            {
                mygame.actualScreen.removeComponent(this);
            }
        }
    }
}
