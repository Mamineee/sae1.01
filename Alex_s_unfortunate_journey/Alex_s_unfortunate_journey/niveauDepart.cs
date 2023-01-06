using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Screens;

namespace Alex_s_unfortunate_journey
{
    public class niveauDepart : GameScreen
    {

        private Game1 _myGame;
        //map
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        //alex
        private Vector2 direction = Vector2.Zero;

        //menu
        public bool MenuReouvert = false;
        Alex alex;

        public niveauDepart(Game1 game) : base(game)
        {
            _myGame = game;

        }
        public override void LoadContent()
        {
            //map
            _tiledMap = Content.Load<TiledMap>("niveauDepart2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //alex
            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle,2);
            
            if (direction == Vector2.Zero)
            {
                alex.PlayAnimation("idle");
            }
            if (direction > Vector2.Zero)
            {

            }
            if (direction < Vector2.Zero)
            {

            }
            base.LoadContent();
        }
        public override void Initialize()
        {
            //alex
            
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //menu
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                _myGame.Etat = Game1.Etats.Menu;
            //map
            _tiledMapRenderer.Update(gameTime);
            //alex
            alex.UpdateAnim(deltaSeconds);
            KeyboardState etatClavier = Keyboard.GetState();
            if (alex.etat == Alex.Etats.Walk)
            {
                //Vector2 direction = Vector2.Zero;

                if (etatClavier.IsKeyDown(Keys.Q) == true)
                {
                    direction = new Vector2(-1, 0);
                }
                else if (etatClavier.IsKeyDown(Keys.D) == true)
                { 
                    direction = new Vector2(1, 0);
                }
                alex.Movement(direction,deltaSeconds);
            }

        }
        public override void Draw(GameTime gameTime)
        {
            //_myGame.GraphicsDevice.Clear(Color.Red);
            //map
            _tiledMapRenderer.Draw();
            //alex
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(alex._animation, alex._positionAlex);
            _myGame.SpriteBatch.End();
        }
    }
    
}
