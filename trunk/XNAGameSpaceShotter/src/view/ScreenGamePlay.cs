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
using System.Text;
using StorageDemo;


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
        int CriaInimigoMisseis = 3000;
        int CriaInimigoFly = 6000;
        int CriaInimigoDual = 2000;
        int CriaInimigoMetralhadora = 2000;
        int indice;
        int numeroDeFly = 5;
        Texture2D imageVida;
        int criaChefe = 50000;
        Texture2D imgChefe;
        Texture2D background;
        Boolean inimigo = true;
        float diff;
        static Random rnd = new Random();
        Vector2 positionfly;
        Vector2 animacao = Vector2.Zero;
        Texture2D bg;
        List<Vector2> bgpos = new List<Vector2>();

        List<Inimigo> waves = new List<Inimigo>();
        List<Inimigo> waveAtiva = new List<Inimigo>();
        ArrayList fases = new ArrayList();
        Boolean carrega = true;

        int tempoWave = 2000;

        int szbg;
        Texture2D imageDualGuns;
        Texture2D imageMetralhadora;
        SpriteFont tipeFont;
        string scorePanel ="";
        SaveGame save;
        int play = 0;
        Boolean ativa = true;

        float temposave;

        public ScreenGamePlay(GameCore game)
            : base(game)
        {

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
            imageVida = mygame.Content.Load<Texture2D>("Player");
            imageTiroPlayer = mygame.Content.Load<Texture2D>("TiroPlayer");
            imgInimigoMisseis = mygame.Content.Load<Texture2D>("lancadorDeMisseis");
            imgInimigoFly = mygame.Content.Load<Texture2D>("mosca2");
            imgChefe = mygame.Content.Load<Texture2D>("nave");
            background = mygame.Content.Load<Texture2D>("bg");
            imageDualGuns = mygame.Content.Load<Texture2D>("dualgun");
            imageMetralhadora = mygame.Content.Load<Texture2D>("metralhadora");
            addComponent(player = new Player(mygame, imagePlayer, new Vector2(400, 400), 3, 5, 3, this));
            tipeFont = mygame.Content.Load<SpriteFont>(@"OpenFont");
            
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
                mygame.spriteBatch.DrawString(tipeFont, scorePanel, new Vector2(500, 30), Color.GreenYellow);
            }
            
        }

        private void addInimigo(Inimigo x)
        {
            addComponent(x);
            inimigos.Add(x);
        }

        public void caregainimigos() {
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            waves.Add(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            fases.Add(waves);
            
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            waves.Add(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            fases.Add(waves);
            
        }

        public override void Update(GameTime gameTime)
        {
            SaveGame.getInstance().Update();
            base.Update(gameTime);
            positionfly = new Vector2(rnd.Next(mygame.Window.ClientBounds.Width), 20);

            int tempo = (int)gameTime.ElapsedGameTime.Milliseconds;
            animacao.Y += tempo / 50.0f;
            if (animacao.Y > 200) {
                animacao.Y -= 200;
            }
            diff = gameTime.ElapsedGameTime.Milliseconds/1000.0f;

            //CriaInimigoMisseis -= tempo;
            //CriaInimigoFly -= tempo;
            //criaChefe -= tempo;
            //CriaInimigoDual -= tempo;
            //CriaInimigoMetralhadora -= tempo;

            tempoWave -= tempo;

            //inimigos-----------------------------------------------------------------------------------------
            if (carrega){
                caregainimigos();
                carrega = false;

            }
            if(ativa){
                waveAtiva = (List<Inimigo>)fases[play];
                play+=1;
                tempoWave = 2000;
                ativa = false;
            }
            if(waveAtiva != null){
                for(int i= 0;i<waveAtiva.Count;i++){
                      addInimigo(waveAtiva[i]);
                 }
                ativa = true;
            }


            
            

            //if (CriaInimigoMisseis <= 0 && inimigo == true)
            //{
              //  addInimigo(new LancadorMisseis(mygame, imgInimigoMisseis, 6, 100, 80, this, 10));
            //    CriaInimigoMisseis = 3000;

            //}
            //if (CriaInimigoFly <= 0 && inimigo == true)
            //{
            //    addInimigo(new Fly(mygame, imgInimigoFly, 3, 100.0f, imgInimigoFly.Width / 3, positionfly, player, 10));
            //    CriaInimigoFly = 1000;
            //}
            //if (CriaInimigoDual <= 0 && inimigo == true)
            //{
            //    addInimigo(new DualGuns(mygame, imageDualGuns, 10, 100, 0, 10));
            //    CriaInimigoDual = 2000;
            //}
            //if (CriaInimigoMetralhadora <= 0 && inimigo == true)
            //{
            //    addInimigo(new Metralhadora(mygame, imageMetralhadora, 10, 100, 0, this, 10));
            //    CriaInimigoMetralhadora = 2000;
            //}
            //if (criaChefe < 0 && inimigo == true)
            //{
            //    addInimigo(new Chefe(mygame, imgChefe, 50, 200, imgChefe.Width / 4, this, 100));
            //    criaChefe = 50000;
            //    inimigo = false;
            //}

            scorePanel = "Score:   " + player.score;//score player

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
            if(temposave<0){
                if (isPressed(Microsoft.Xna.Framework.Input.Keys.S, Microsoft.Xna.Framework.Input.Buttons.Back))
                {
                    Console.WriteLine("tentando salvar");
                    SaveGame.getInstance().CallSave(player.convertToSave());
                }
                temposave = 200;
            }else{
                temposave -= gameTime.ElapsedGameTime.Milliseconds;
            }

            if (delay <= 0)
            {
                if (isPressed(Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Buttons.RightTrigger))
                {
                    addComponent(new Tiro(mygame, imageTiroPlayer, player.positionPlayer + tiroAjusteEsquerda, -600, 1, this));
                    addComponent(new Tiro(mygame, imageTiroPlayer, player.positionPlayer + tiroAjusteDireita, -600, 1, this));
                    delay = 200;
                }
            }
            else
            {
                delay -= gameTime.ElapsedGameTime.Milliseconds;
            }

        }
    }
}
