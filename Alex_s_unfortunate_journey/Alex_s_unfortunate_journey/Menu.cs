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

        public Menu(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[2];
            lesBoutons[0] = new Rectangle(75, 110, 640, 160);
            lesBoutons[1] = new Rectangle(75, 320, 640, 160);
        }
        public override void LoadContent()
        {
            //map
            _imageMenu = Content.Load<Texture2D>("Menu (3)_scaled_12x_pngcrushed");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
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
