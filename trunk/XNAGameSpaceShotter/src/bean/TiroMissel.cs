

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameSpaceShotter.src.view;
using System.Collections.Generic;
namespace XNAGameSpaceShotter.src.bean
{
    class TiroInimigo : Component
    {
        public int velocidade;
        public Vector2 posicao;
        public int dano;
        int largura;
        int altura;
        public Texture2D image;
        public ScreenGamePlay screen;
        private int frame = 0;
        float diff;
        int tempo;

        public TiroInimigo(GameCore game, Texture2D image, Vector2 posicao, int velocidade, int dano,int largura, ScreenGamePlay screen)
            : base(game, image)
        {
            this.image = image;
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.dano = dano;
            this.screen = screen;
            this.largura = largura;
            this.altura = image.Height;
            mygame.sons.playSound(2);
        }

        public override void Draw(GameTime gameTime)
        {
            mygame.spriteBatch.Draw(image, posicao, new Rectangle(frame * largura, 0, largura, altura), Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            diff = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            tempo -= gameTime.ElapsedGameTime.Milliseconds;
            this.posicao.Y += this.velocidade;

            if (this.posicao.Y > mygame.Window.ClientBounds.Height + 20)
            {
                screen.removeComponent(this);
            }
            
            colisao();
            
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

            if(posicao.Y > mygame.Window.ClientBounds.Height){
                screen.removeComponent(this);
            
            }
        }

        public void colisao()
        {
            Vector3 A = new Vector3(screen.player.positionPlayer.X+5, screen.player.positionPlayer.Y+10, 0);
            Vector3 B = new Vector3(screen.player.positionPlayer.X + 44, screen.player.positionPlayer.Y + screen.player.imgPlayer.Height - 13, 0);

            Vector3 C = new Vector3(posicao.X, posicao.Y, 0);
            Vector3 D = new Vector3(posicao.X + largura, posicao.Y + image.Height, 0);

            BoundingBox boxPlayer = new BoundingBox(A, B);
            BoundingBox boxEnemy = new BoundingBox(C, D);

            if (boxEnemy.Intersects(boxPlayer))
            {
                screen.removeComponent(this);
                screen.addComponent(new Explosao(mygame, C + new Vector3(0, 10, 0), 1, screen));
                screen.player.hp--;
                if (screen.player.hp < 0) {
                    screen.addComponent(new Explosao(mygame, new Vector3(screen.player.positionPlayer.X, screen.player.positionPlayer.Y, 0), 2, screen));
                    screen.player.vida--;
                    screen.player.hp = 5;
                }
                if(screen.player.vida < 0){
                    screen.removeComponent(screen.player);
                    mygame.setScreen(new ScreenSplash(mygame));
                }
                
            }

        }
    }
}
