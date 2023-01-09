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
    //personnage
        Alex alex;
        public Vector2 positionDepart;
        //menu
        public bool MenuReouvert = false;
        
        


        public niveauDepart(Game1 game) : base(game)
        {
            _myGame = game;
            positionDepart = new Vector2(330, 610);
        }



        public override void LoadContent()
        {
         //map
            _tiledMap = Content.Load<TiledMap>("niveauDepart2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            //alex 
            //animation de base
            alex = new Alex(_myGame.spriteSheetIdle, 2, positionDepart);
            alex.PlayAnimation("idle");
            base.LoadContent();
        }



        public override void Initialize()
        {
            
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
             if (alex._positionAlex.X > 1190)
             {
                    _myGame._niveauDepart.positionDepart.X = 20;
                    _myGame._screenManager.LoadScreen(_myGame._niveauForet);

             }
            //alex.Movement(alex.direction, deltaSeconds);


         // mur ocean
            if (alex._positionAlex.Y < 330)
            {
                
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
