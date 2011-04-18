using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project_Steel_Champions.src.bean;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNAGameSpaceShotter.src.bean;

namespace XNAGameSpaceShotter.src.view.template {
    public class Screen: Component {
        #region Lista de Componentes
        public List<Component> componentes = new List<Component>();
        protected bool componentsLoaded = false;
        #endregion

        #region Variáveis de Controle de Estado do Teclado, Mouse e Gamepad
        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;
        GamePadState currentGamePadState;
        GamePadState lastGamePadState;
        MouseState currentMouseState;
        MouseState lastMouseState;
        #endregion

        #region Construtor
        public Screen(GameCore game)
            : base(game, null) {
            //((ProjectSteelChampions)game).setScreen(novaJanela); <- como se troca de janela
        }
        #endregion

        #region Components
        public virtual void loadComponents() {
            componentsLoaded = true;
        }

        public virtual void unloadComponents() {
            int sz = componentes.Count;
            for (int i = 0; i < sz; i++) {
                removeComponentAt(0);
            }
            componentsLoaded = false;
        }

        public virtual void addComponent(Component component) {
            componentes.Add(component);
        }

        public virtual void removeComponent(Component component) {
            componentes.Remove(component);
        }

        public virtual void removeComponentAt(int component) {
            componentes.RemoveAt(component);
        }
        #endregion

        #region Content
        protected virtual void LoadContent() {
            loadComponents();
        }

        protected virtual void UnloadContent() {
            if (componentsLoaded) {
                unloadComponents();
            }
        }
        #endregion

        #region Funções Básicas (Draw e Update)
        public override void Update(GameTime gameTime) {
            for (int i = 0; i < componentes.Count; i++) {
                componentes[i].Update(gameTime);
            }
            lastKeyboardState = currentKeyboardState;
            lastGamePadState = currentGamePadState;
            lastMouseState = currentMouseState;
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentMouseState = Mouse.GetState();
            
        }
        public override void Draw(GameTime gameTime) {
            int sz = componentes.Count;
            for (int i = 0; i < sz; i++) {
                componentes[i].Draw(gameTime);
            }
        }
        #endregion

        #region Verificação de Pressionamento de Teclas e Botões
        protected bool isPressed(Keys key, Buttons button) {
            return (currentKeyboardState.IsKeyDown(key)) ||
                   (currentGamePadState.IsButtonDown(button));
        }
        #endregion
    }
}
