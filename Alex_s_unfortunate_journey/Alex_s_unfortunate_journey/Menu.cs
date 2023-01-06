using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using MonoGame.Extended.Animations;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Screens;

namespace Alex_s_unfortunate_journey
{
    public class Menu : GameScreen
    {

        private Game1 _myGame;
        //menu 
        private Texture2D _imageMenu;
        //boutons
        private Rectangle[] lesBoutons;
        public bool DejaJouer = false;
        public Menu(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[2];
            lesBoutons[0] = new Rectangle(420, 395, 360, 100);
            lesBoutons[1] = new Rectangle(420, 525, 360, 100);

        }
        public override void LoadContent()
        {
            //map
            _imageMenu = Content.Load<Texture2D>("menu");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                        {
                            _myGame.Etat = Game1.Etats.Play;
                        }
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.Quit;
                        break;
                    }
                    
                }
                //DejaJouer = true;
            }
            //map
            //_tiledMapRenderer.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_imageMenu, new Vector2(0,0), Color.White);
            _myGame.SpriteBatch.End();

            

        }
    }

}
