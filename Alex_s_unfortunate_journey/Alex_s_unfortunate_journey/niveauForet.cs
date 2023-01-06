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
    public class niveauForet : GameScreen
    {

        private Game1 _myGame;
        //map
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public niveauForet(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            //map
            _tiledMap = Content.Load<TiledMap>("niveauForet");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            //map
            _tiledMapRenderer.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red);
            //map
            _tiledMapRenderer.Draw();
            
        }
    }

}
