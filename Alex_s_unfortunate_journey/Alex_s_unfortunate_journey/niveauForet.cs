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
using MonoGame.Extended.TextureAtlases;

namespace Alex_s_unfortunate_journey
{
    public class niveauForet : GameScreen
    {

        private Game1 _myGame;
        //map
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        //alex
        Alex alex;
        private Vector2 direction = Vector2.Zero;
        public Vector2 positionDepart;
        private Rectangle _recAlex;
        private Vector2 _posAlex;
        //collision
        private TiledMapTileLayer mapLayer;
        //ficelle 
        private bool _ficelle;
        private Vector2 _positionFicelle;
        private Texture2D _textureFicelle;
        private Rectangle _recFicelle;

        public niveauForet(Game1 game) : base(game)
        {
            _myGame = game;
            positionDepart = new Vector2(70, 598);
        }
        public override void LoadContent()
        {
            //map
            _tiledMap = Content.Load<TiledMap>("niveauForet2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //collision
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Plateforme");
            //alex 
            //animation de base
            //ficelle
            _textureFicelle = Content.Load<Texture2D>("fils_2_scaled_8x_pngcrushed");

            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle, 2, positionDepart);
            alex.PlayAnimation("idle");
            base.LoadContent();
        }
        public override void Initialize()
        {
            //alex rectangle
            //_recAlex = new Rectangle((int)alex._positionAlex.X, (int)alex._positionAlex.Y,96,96);
            //_posAlex = new Vector2(alex._positionAlex.X, alex._positionAlex.Y);
            //ficelle 
            _positionFicelle = new Vector2(70, 598);
            _recFicelle = new Rectangle(70, 598, 64, 64);
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
            //saut
            alex.Saut();
            // deplacement
            alex.Deplacement();
            // teleportation
            if (alex._positionAlex.X < 2)
            {
                _myGame._niveauDepart.positionDepart.X = 1160;
                _myGame._screenManager.LoadScreen(_myGame._niveauDepart);

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
            _myGame.SpriteBatch.Draw(_textureFicelle, _positionFicelle, Color.White);
            _myGame.SpriteBatch.End();
        }
    }

}
