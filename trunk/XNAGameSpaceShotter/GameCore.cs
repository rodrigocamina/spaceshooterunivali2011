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
using XNAGameSpaceShotter.src.core;
using XNAGameSpaceShotter.src.view.template;
using XNAGameSpaceShotter.src.view;

namespace XNAGameSpaceShotter {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameCore: Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Matrix viewMatrix;
        public Matrix projectionMatrix;
        public RepositorioDeModelos modelos;
        public RepositorioDeTexturas texturas;
        public SpriteFont fonteLetras;
        public Vector3 cameraPosition = new Vector3(15000.0f, 8000.0f, GameConstants.CameraHeight);
        public Vector3 cameraFocus = new Vector3(15000.0f, 8000.0f, 0);
        public Screen actualScreen;

        public GameCore() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.ToRadians(45.0f),
                    GraphicsDevice.DisplayMode.AspectRatio,
                    GameConstants.CameraHeightMin, GameConstants.CameraHeightMax);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraFocus, Vector3.Up);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            modelos = new RepositorioDeModelos(this);
            texturas = new RepositorioDeTexturas(this);
            fonteLetras = Content.Load<SpriteFont>("OpenFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            setScreen(new ScreenSplash(this));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            actualScreen.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
            actualScreen.Draw(gameTime);
            spriteBatch.DrawString(fonteLetras, "Rodando a " + gameTime.ElapsedGameTime, new Vector2(50, 50), Color.Black);
            spriteBatch.End();
        }

        public void setScreen(Screen nextScreen) {
            if (actualScreen != null) {
                actualScreen.unloadComponents();
            }
            actualScreen = nextScreen;
            actualScreen.loadComponents();
        }
    }
}
