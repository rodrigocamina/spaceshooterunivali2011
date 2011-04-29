

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameSpaceShotter.src.view;
using System.Collections.Generic;
namespace XNAGameSpaceShotter.src.bean
{
    class Tiro :Component
    {
        public int velocidade;
        public Vector2 posicao;
        public int dano;
        public Texture2D image;
        public ScreenGamePlay screen;
        Chefe chefe;

        public Tiro(GameCore game, Texture2D image , Vector2 posicao, int velocidade, int dano, ScreenGamePlay screen)
            : base(game, image)
        {
            this.image = image;
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.dano = dano;
            this.screen = screen;
            mygame.sons.playSound(2);
        }

        public override void Draw(GameTime gameTime)
        {
            mygame.spriteBatch.Draw(this.image, this.posicao, Color.White);
        }

        public override void Update(GameTime gameTime)
        { 
            this.posicao.Y += this.velocidade*gameTime.ElapsedGameTime.Milliseconds/1000.0f;
            if (this.posicao.Y < -10)
            {
                screen.removeComponent(this);
            }
            colisao(screen.inimigos);

        }

        public void colisao(List<Inimigo> inimigos)
        {
            for (int i = 0; i < inimigos.Count; i++)
            {
                Inimigo ini = inimigos[i];
                if (((this.posicao.X > ini.positionInimigo.X)&&(this.posicao.X < ini.positionInimigo.X + 80))&&
                    ((this.posicao.Y > ini.positionInimigo.Y) && (this.posicao.Y < ini.positionInimigo.Y + ini.imgInimigo.Height)))
                {                    screen.removeComponent(this);
                    ini.hp --;
                    if(ini.hp < 0){
                        screen.addComponent(new Explosao(mygame, new Vector3(ini.positionInimigo.X, ini.positionInimigo.Y, 0), 2, screen));
                        screen.removeComponent(ini);
                        screen.inimigos.Remove(ini);
                        
                    }
                }
            }
        }
    }
}
