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
using Microsoft.Xna.Framework.Storage;
using XNAGameSpaceShotter;

namespace XNAGameSpaceShotter.src.bean
{
    class Inimigo : Component
    {
        public Vector2 positionInimigo;
        public Texture2D imgInimigo;
        public int hp;
        public int velocidadeInimigo;
        public int largura;
        public int altura;
        public int score;
        static Random rnd = new Random();
        int posicaoDesenho = 0;
        int diff;
        int tempo = 200;

        public Inimigo(GameCore game, Texture2D imgInimigo, int hp, int velocidadeInimigo, int largura)
            : base(game, imgInimigo)
        {
            positionInimigo = new Vector2(rnd.Next(mygame.Window.ClientBounds.Width), -50);
            this.imgInimigo = imgInimigo;
            this.hp = hp;
            this.velocidadeInimigo = velocidadeInimigo;
            this.largura = largura;
            this.altura = imgInimigo.Height;
        }


        public override void Draw(GameTime gameTime)
        {

            mygame.spriteBatch.Draw(imgInimigo, positionInimigo, new Rectangle(posicaoDesenho * largura, 0, largura, altura), Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            diff = gameTime.ElapsedGameTime.Milliseconds;
            tempo -= diff;
            positionInimigo.Y += velocidadeInimigo;

            if (tempo < 0)
            {
                tempo = 200;
                if (posicaoDesenho < 11)
                {
                    posicaoDesenho++;

                }
                else
                {
                    posicaoDesenho = 0;
                }

            }

            if (positionInimigo.Y > mygame.Window.ClientBounds.Height)
            {
                mygame.actualScreen.removeComponent(this);
            }
        }
    }
}
