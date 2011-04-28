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

        public ScreenGamePlay(GameCore game)
            : base(game)
        {

        }

        public override void loadComponents()
        {
            imagePlayer = mygame.Content.Load<Texture2D>("naveP");
            imageVida = mygame.Content.Load<Texture2D>("naveP");
            imageTiroPlayer = mygame.Content.Load<Texture2D>("TiroPlayer");
            imgInimigoMisseis = mygame.Content.Load<Texture2D>("lancadorDeMisseis");
            imgInimigoFly = mygame.Content.Load<Texture2D>("mosca2");
            addComponent(player = new Player(mygame, imagePlayer, new Vector2(400, 400), 3, 5, 3, this));
        }

        public override void Draw(GameTime gameTime)
        {
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
            int diff = (int)gameTime.ElapsedGameTime.Milliseconds;
            delay -= diff;
            CriaInimigoMisseis -= diff;
            CriaInimigoFly -= diff;
            //inimigos-----------------------------------------------------------------------------------------
            if (CriaInimigoMisseis <= 0)
            {
                addInimigo(new LancadorMisseis(mygame, imgInimigoMisseis,6, 100, 80, this));
                CriaInimigoMisseis = 3000;

            }
            if (CriaInimigoFly <= 0)
            {
                for(int i =0; i < numeroDeFly;i++){
                    addInimigo(new Fly(mygame, imgInimigoFly, 2, 100, 87 / 3, new Vector2(mygame.Window.ClientBounds.Width + 20 * i, 600 * i), player));
                    addInimigo(new Fly(mygame, imgInimigoFly, 2,100, 87 / 3, new Vector2(1 * i, 12 * i), player));
                }
                CriaInimigoFly = 6000;
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
