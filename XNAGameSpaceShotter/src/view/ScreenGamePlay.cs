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
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.bean;
using System.Collections;


namespace XNAGameSpaceShotter.src.view
{
    class ScreenGamePlay : Screen
    {
        public Player player;
        public Texture2D imagePlayer;
        public Texture2D imageTiroPlayer;
        public Vector2 tiroAjusteEsquerda = new Vector2(13, 10);
        public Vector2 tiroAjusteDireita = new Vector2(34, 10);
        public List<Inimigo> inimigos = new List<Inimigo>();
        public int delay = 200;
        public Texture2D imgInimigoMisseis;
        Texture2D imgInimigoFly;
        public int CriaInimigoMisseis = 3000;
        public int CriaInimigoFly = 6000;
        int indice;
        int numeroDeFly = 5;
        Texture2D imageVida;
        Vector2 positionfly = new Vector2(10,10);
        int criaChefe = 50000;
        Texture2D imgChefe;
        Texture2D background;
        float positionBack1;
        float positionBack2;
        float positionBack3;
        int positionBack1old;
        int positionBack2old;
        int positionBack3old;
        float diff;
        float velocidadebg = 100;

        Vector2 animacao = Vector2.Zero;
        Texture2D bg;
        List<Vector2> bgpos = new List<Vector2>();
        int szbg;

        public ScreenGamePlay(GameCore game)
            : base(game)
        {
            positionBack1old = 0;
            //positionBack2old = -mygame.Window.ClientBounds.Height;
            //positionBack3old = -mygame.Window.ClientBounds.Height - mygame.Window.ClientBounds.Height;
            positionBack1 = positionBack1old;
           // positionBack2 = positionBack2old;
           // positionBack3 = positionBack3old;

            bg = mygame.Content.Load<Texture2D>("bg");
            int width = game.Window.ClientBounds.Right - game.Window.ClientBounds.Left;
            int height = game.Window.ClientBounds.Bottom - game.Window.ClientBounds.Top;
            while (width > 0) {
                width -= 200;
                int htemp = height;
                while (htemp > -200) {
                    htemp -= 200;
                    bgpos.Add(new Vector2(width, htemp));
                }
            }
            szbg = bgpos.Count;
            mygame.sons.playSong(2);
        }


        public override void loadComponents()
        {
            imagePlayer = mygame.Content.Load<Texture2D>("naveP");
            imageVida = mygame.Content.Load<Texture2D>("naveP");
            imageTiroPlayer = mygame.Content.Load<Texture2D>("TiroPlayer");
            imgInimigoMisseis = mygame.Content.Load<Texture2D>("lancadorDeMisseis");
            imgInimigoFly = mygame.Content.Load<Texture2D>("mosca2");
            imgChefe = mygame.Content.Load<Texture2D>("nave");
            background = mygame.Content.Load<Texture2D>("bg");
            addComponent(player = new Player(mygame, imagePlayer, new Vector2(400, 400), 3, 5, 3, this));
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < szbg; i++) {
                mygame.spriteBatch.Draw(bg, bgpos[i] + animacao, Color.White);
            }
            base.Draw(gameTime);
            for (int i = 0; i < player.vida;i++ )
            {
                mygame.spriteBatch.Draw(imageVida, new Rectangle(30*i+10, mygame.Window.ClientBounds.Height - 30, 30, 30), Color.White);
      
            }
        }

        private void addInimigo(Inimigo x)
        {
            addComponent(x);
            inimigos.Add(x);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            int tempo = (int)gameTime.ElapsedGameTime.Milliseconds;
            animacao.Y += tempo / 50.0f;
            if (animacao.Y > 200) {
                animacao.Y -= 200;
            }
            diff = gameTime.ElapsedGameTime.Milliseconds/1000.0f;
            delay -= tempo;
            CriaInimigoMisseis -= tempo;
            CriaInimigoFly -= tempo;
            criaChefe -= tempo;
            //inimigos-----------------------------------------------------------------------------------------
            if (CriaInimigoMisseis <= 0)
            {
                addInimigo(new LancadorMisseis(mygame, imgInimigoMisseis,6, 100, 80, this));
                CriaInimigoMisseis = 3000;

            }
            if (CriaInimigoFly <= 0)
            {
                for(int i =0; i < numeroDeFly;i++){
                    addInimigo(new Fly(mygame, imgInimigoFly, 2, 10, 87 / 3, new Vector2(mygame.Window.ClientBounds.Width + 20 * i, 600 * i), player));
                    addInimigo(new Fly(mygame, imgInimigoFly, 2, 10, 87 / 3, positionfly, player));
                }
                CriaInimigoFly = 6000;
            }
            if(criaChefe < 0){
                addInimigo(new Chefe(mygame, imgChefe, 30, 10, imgChefe.Width/4,this));
                criaChefe = 50000;
            }
            
            //fim inimigos-------------------------------------------------------------------------------------
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Up, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickUp))
            {
                if (player.positionPlayer.Y > 0)
                {
                    player.positionPlayer.Y -= player.velocidadePlayer;
                }
            }
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Down, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickDown))
            {
                if (player.positionPlayer.Y < (mygame.Window.ClientBounds.Height - 50))
                {
                    player.positionPlayer.Y += player.velocidadePlayer;
                }
            }
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Left, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickLeft))
            {
                if (player.positionPlayer.X > 0)
                {
                    player.positionPlayer.X -= player.velocidadePlayer;
                }
            }
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Right, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickRight))
            {
                if (player.positionPlayer.X < (mygame.Window.ClientBounds.Width - 52))
                {
                    player.positionPlayer.X += player.velocidadePlayer;
                }
            }
            if (isPressed(Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Buttons.RightTrigger))
            {
                if (delay <= 0)
                {
                    addComponent(new Tiro(mygame, imageTiroPlayer, player.positionPlayer + tiroAjusteEsquerda, -600, 1, this));
                    addComponent(new Tiro(mygame, imageTiroPlayer, player.positionPlayer + tiroAjusteDireita, -600, 1, this));
                    delay = 200;
                }
            }

        }
    }
}
