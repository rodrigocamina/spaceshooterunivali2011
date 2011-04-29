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
using XNAGameSpaceShotter.src.view;

namespace XNAGameSpaceShotter.src.bean
{
    class Player :Component
    {
        public Vector2 positionPlayer;
        public Texture2D imgPlayer;
        public int vida;
        public int hp;
        public int velocidadePlayer;
        public int score = 0;
        public ScreenGamePlay screen;
        float diff;
        int tempo;
        int frame;

        public Player(GameCore game, Texture2D imgPlayer, Vector2 positionPlayer, int vida, int hp, int velocidadePlayer, ScreenGamePlay screen)
            : base(game, imgPlayer)
        {

            this.positionPlayer = positionPlayer;
            this.imgPlayer = imgPlayer;
            this.vida = vida;
            this.hp = hp;
            this.velocidadePlayer = velocidadePlayer;
            this.screen = screen;
        }

        public override void Draw(GameTime gameTime)
        {
            mygame.spriteBatch.Draw(imgPlayer, positionPlayer, new Rectangle(frame * 54, 0, 54, 48), Color.White);
        }

        public override void Update(GameTime gameTime) 
        {
            diff = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            tempo -= gameTime.ElapsedGameTime.Milliseconds;
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
            
            colisao(screen.inimigos);
}

        public void colisao(List<Inimigo> inimigos)
        {
            for (int i = 0; i < inimigos.Count; i++)
            {
                Vector3 A = new Vector3(positionPlayer.X+5, positionPlayer.Y, 0);
                Vector3 B = new Vector3(positionPlayer.X + 44, positionPlayer.Y + imgPlayer.Height, 0);

                Vector3 C = new Vector3(inimigos[i].positionInimigo.X, inimigos[i].positionInimigo.Y+10, 0);
                Vector3 D = new Vector3(inimigos[i].positionInimigo.X + inimigos[i].largura, inimigos[i].positionInimigo.Y + inimigos[i].imgInimigo.Height-13, 0);

                BoundingBox boxPlayer = new BoundingBox(A, B);
                BoundingBox boxEnemy = new BoundingBox(C, D);

                if (boxPlayer.Intersects(boxEnemy))
                {
                    screen.removeComponent(inimigos[i]);
                    screen.inimigos.Remove(inimigos[i]);
                    screen.addComponent(new Explosao(mygame, C, 2, screen));

                    if (hp > 0)
                    {
                        hp--;
                    }
                    else {
                        vida--;
                        hp = 5;
                        screen.addComponent(new Explosao(mygame, new Vector3(screen.player.positionPlayer.X, screen.player.positionPlayer.Y, 0), 2, screen));
                    }
                    if (vida > 0)
                    {
                        vida--;
                    }
                    else {
                        screen.removeComponent(this);
                        mygame.setScreen(new ScreenGamePlay(mygame));
                    }
                    
                }
            }
        }

    }
}
