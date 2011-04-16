using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
//using System.;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameSpaceShotter.src.bean;
using XNAGameSpaceShotter.src.Primitives3D;
using XNAGameSpaceShotter.src.core;
using XNAGameSpaceShotter.src.core.ai;
using XNAGameSpaceShotter.src.view.template;


namespace XNAGameSpaceShotter.src.view {
    class EventBox: Screen {

        #region Variáveis de Posição
        Vector2 positionDialogBox;
        Vector2 positionCharacter;
        Vector2 positionBackground;
        Vector2 PositionText;
        #endregion

        #region Variáveis de Imagens
        Texture2D imageDialogBox;
        Texture2D imageCharacter;
        Texture2D imageBackground;
        #endregion

        #region Variáveis Usadas na Carga do CSV e na Escrita do Texto
        string loadFrases;
        StreamReader file;
        Boolean write = true;
        SpriteFont tipeFont;
        int endString = 1;
        int timerTotal = 0;
        int loadFrasesTimer = 30;
        Script script;

        #endregion

        #region Construtor
        public EventBox(GameCore game, Texture2D background, Vector2 position, Script script)
            : base(game) {
            this.script = script;
            positionBackground = position;
            imageBackground = background;
            PositionText = new Vector2(10, 420);
            file = new StreamReader("Content/DataBase/teste.csv"); // arquivo que contem o csv obs: "isso vai depender do seu projeto"
            //file = new StreamReader(@"C:\SCP\Project Steel Champions\Content\DataBase\teste.csv"); 
            loadFrases = file.ReadLine();

            tipeFont = mygame.Content.Load<SpriteFont>(@"DataBase\TipeFont"); //carrega arquivo do database que comtel o modelo da fonte

            positionCharacter = new Vector2(-300, 200);
            imageCharacter = base.mygame.Content.Load<Texture2D>("Image/figurante");
            positionDialogBox = new Vector2(120, 370);
            imageDialogBox = base.mygame.Content.Load<Texture2D>("Image/DialogBox2");
        }
        #endregion

        #region Load e Unload
        public override void loadComponents() {



        }
        #endregion

        #region Funções Básicas (Draw e Update)
        public override void Draw(GameTime gameTime) {


            int dif = gameTime.ElapsedGameTime.Milliseconds;

            if (imageBackground != null) {
                mygame.spriteBatch.Draw(imageBackground, positionBackground, Color.White);
            }
            mygame.spriteBatch.Draw(imageCharacter, positionCharacter, Color.White);
            mygame.spriteBatch.Draw(imageDialogBox, new Rectangle(0, 1280, 700, 70), Color.White);

            timerTotal += dif;
            if (timerTotal > loadFrasesTimer) {
                timerTotal -= loadFrasesTimer;
                endString++;
            }
            if (endString >= loadFrases.Count()) {
                write = true;
                mygame.spriteBatch.DrawString(tipeFont, loadFrases, PositionText, Color.GreenYellow);
                while (isPressed(Microsoft.Xna.Framework.Input.Keys.A, Microsoft.Xna.Framework.Input.Buttons.A) && write == true) {
                    endString = 1;
                    write = false;
                    if (loadFrases.Substring(0, endString) == "#") {
                        if (script != null) {
                            script.execute();
                        } else {

                        }

                    } else {
                        loadFrases = file.ReadLine(); //carrega nova linha do csv
                    }
                }
            } else {
                mygame.spriteBatch.DrawString(tipeFont, loadFrases.Substring(0, endString), PositionText, Color.GreenYellow); // escreve letra por letra
            }

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (positionCharacter.X < 100) { //move personagens 
                positionCharacter.X += 10;
            }
        }
        #endregion
    }
}
