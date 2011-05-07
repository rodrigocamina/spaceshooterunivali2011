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
    class DualGuns : Inimigo
    {
        
        static Random rnd = new Random();
        float diff;
        int tempo = 200;
        Texture2D life;
        Sprite sprite;
        int score;

        public DualGuns(GameCore game, Texture2D imgInimigo, int hp, float velocidadeInimigo, int largura, int score)
            : base(game, imgInimigo,hp,velocidadeInimigo,largura,score)
        {
            life = mygame.Content.Load<Texture2D>("life");
            sprite = new Sprite(mygame, imgInimigo, 4, new Vector3(), 0);
            positionInimigo = new Vector2(20, 20);
            this.score = score;
            
        }

        public override void Draw(GameTime gameTime)
        {
            sprite.Draw(gameTime, positionInimigo);        
        
        }

        public override void Update(GameTime gameTime)
        {
            diff = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            tempo -= gameTime.ElapsedGameTime.Milliseconds;


            positionInimigo.Y += velocidadeInimigo * diff;
            

            sprite.Update(gameTime);


            //TIRO
            //if (frame == 3)
            //{
            //   screen.addComponent(new TiroInimigo(mygame, imgTiro, positionInimigo, 4, 1, screen));
            //}
            if (positionInimigo.Y > mygame.Window.ClientBounds.Height)
            {
                mygame.actualScreen.removeComponent(this);
            }
        }
    }
}

      