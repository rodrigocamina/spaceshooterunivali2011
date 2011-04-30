using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNAGameSpaceShotter.src.view.template;

namespace XNAGameSpaceShotter.src.bean {
    public class Explosao : Sprite{
        Screen screen;
        public Explosao(GameCore mygame, Vector3 position, float scale,Screen screen):base(mygame, mygame.Content.Load<Texture2D>("explosao2"), 10, position, scale) {
            this.screen = screen;
            sprite = 1;
            mygame.sons.playSound(0);
        }
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if(sprite==0){
                screen.removeComponent(this);
            }
        }
    }
}
