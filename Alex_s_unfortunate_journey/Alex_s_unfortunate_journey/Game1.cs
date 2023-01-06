﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using MonoGame.Extended.Animations;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace Alex_s_unfortunate_journey
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        //sreen
        private niveauDepart _niveauDepart;
        private niveauForet _niveauForet;
        private Menu _menu;        
        //perso
        private Vector2 _positionPerso;
        private AnimatedSprite _persoIdle;
        //etat
        public enum Etats { Menu, Controls, Play, Quit };
        private Etats etat;

        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }
        public Game1()
        {
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            //map
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();
            Etat = Etats.Menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _niveauDepart = new niveauDepart(this);
            _niveauForet = new niveauForet(this);
            _menu = new Menu(this);
            _screenManager.LoadScreen(_menu);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed /*|| Keyboard.GetState().IsKeyDown(Keys.Escape)*/)
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(_niveauForet);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _screenManager.LoadScreen(_niveauDepart);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                System.Console.WriteLine("Up");
            }
            
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play && _menu.DejaJouer == false)
                    _screenManager.LoadScreen(_niveauDepart, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (keyboardState.IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Menu)
                {
                    _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
                    _menu.DejaJouer = true;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            //_screenManager.LoadScreen(_niveauDepart);
            //perso
            //_spriteBatch.Begin();
            //_spriteBatch.Draw(_persoIdle, _positionPerso);
            //_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}