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
    class Inimigo : Component
    {
        public Vector2 positionInimigo;
        public Texture2D imgInimigo;
        public int hp;
        public float velocidadeInimigo;
        public int largura;
        public int altura;


        public Inimigo(GameCore game, Texture2D imgInimigo, int hp, float velocidadeInimigo, int largura)
            : base(game, imgInimigo)
        {
            this.imgInimigo = imgInimigo;
            this.hp = hp;
            this.velocidadeInimigo = velocidadeInimigo;
            this.largura = largura;
            this.altura = imgInimigo.Height;
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
