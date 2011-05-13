using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAGameSpaceShotter.src.bean {
    public class Label : Component{
        int[] text;
        static SpriteV font;
        Vector2 positionTxt;
        public int interChar = 20;

        public Label(String text, Vector3 position, GameCore game) : base(game) {
            this.position = position;
            this.text = translate(text);
            if(font==null){
                font = new SpriteV(game,game.Content.Load<Texture2D>("letras30x24"),36,Vector3.Zero,1);
            }
        }

        public override void Draw(GameTime gameTime) {
            this.positionTxt = new Vector2(position.X, position.Y);
            int sz = text.Length;
            for (int i = 0; i < sz; i++) {
                if (text[i] != -1) {
                    font.Sprite = text[i];
                    font.Draw(gameTime, positionTxt);
                }
                positionTxt.X += interChar;
            }
        }

        public override void Update(GameTime gameTime) {
            
        }

        public int getWidth() {
            return text.Length * interChar;
        }

        private static int[] translate(String text) {
            int sz = text.Length;
            int[] result = new int[sz];
            char[] textarr = text.ToLower().ToCharArray();
            for (int i = 0; i < sz; i++) {
                result[i] = translateChar(textarr[i]);
            }
            return result;
        }
        private static int translateChar(char ch) {
            /*
            A 97
            B 98
            Z 122
            0 48
            1 49
            9 57
            */
            int intch = (int)ch;
            if (intch > 47 && intch < 58) {
                //número
                return intch - 22;
            } else if (intch > 96 && intch < 123) {
                //letra
                return intch - 97;
            }
            return -1;
        }
        /*
        public String translate(char[] text) {
            String result = "";
            int sz = text.Length;
            int a = (int)'a';
            for (int i = 0; i < sz; i++) {
                result = (((int)(text[i]))-a) + "";
            }


            return result;
        }*/
    }
}
