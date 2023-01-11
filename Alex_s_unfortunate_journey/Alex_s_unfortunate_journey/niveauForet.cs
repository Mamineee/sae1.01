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
using MonoGame.Extended;

namespace Alex_s_unfortunate_journey
    //t
{
    public class niveauForet : GameScreen
    {

        private Game1 _myGame;
        //map
        public static TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        //alex
        Alex alex;
        private Vector2 direction = Vector2.Zero;
        public Vector2 positionDepart;
        private Rectangle _recAlex;
        //ficelle 
        //public bool _ficelle;
        private Vector2 _positionFicelle;
        private Texture2D _textureFicelle;
        private Rectangle _recFicelle;
        //baton
        //private bool _baton;
        private Vector2 _positionBaton;
        private Texture2D _textureBaton;
        private Rectangle _recBaton;
        //Canne à pêche
        //private bool _cannap;
        private Vector2 _positionCannap;
        private Texture2D _textureCannap;
        //collision        
        private TiledMapTileLayer mapLayer;

        public niveauForet(Game1 game) : base(game)
        {
            _myGame = game;
            positionDepart = new Vector2(70, 550);
        }
        public override void LoadContent()
        {
            //map
            _tiledMap = Content.Load<TiledMap>("niveauForet3");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //collision
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Plateforme");
            //ficelle
            _textureFicelle = Content.Load<Texture2D>("fils_2_scaled_8x_pngcrushed");
            //baton
            _textureBaton = Content.Load<Texture2D>("Baton_grand_scaled_8x_pngcrushed");
            //cannap
            _textureCannap = Content.Load<Texture2D>("cannapcase");
            //Idle
            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle, 2, positionDepart);
            alex.PlayAnimation("idle");
            base.LoadContent();
        }
        public override void Initialize()
        {
            //ficelle 
            _positionFicelle = new Vector2(25, 215);
            _positionBaton = new Vector2(1020, 150);
            _positionCannap = new Vector2(20, 20);
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _recAlex = new Rectangle((int)Alex._positionAlex.X-45, (int)Alex._positionAlex.Y-40, 66, 80);
            KeyboardState keyboardState = Keyboard.GetState();
            //menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _myGame.Etat = Game1.Etats.Menu;
            //map
            _tiledMapRenderer.Update(gameTime);
            //alex
            alex.UpdateAnim(deltaSeconds);
            KeyboardState etatClavier = Keyboard.GetState();
            //saut
            alex.Jump(_tiledMap);
            
            //deplacement
            if (alex.etat == Alex.Etats.Walk)
            {
                //Vector2 direction = Vector2.Zero;

                if (etatClavier.IsKeyDown(Keys.Q) == true)
                {
                    ushort tx = (ushort)(Alex._positionAlex.X / _tiledMap.TileWidth);
                    ushort ty = (ushort)(Alex._positionAlex.Y / _tiledMap.TileHeight);
                    //marche vers la gauche
                    //direction = new Vector2(-1, 0);
                    if (!alex.etatAnimation.Equals("walk_left"))
                    {
                        SpriteSheet spriteSheetWalk = Content.Load<SpriteSheet>("MC_walk_left_2.sf", new JsonContentLoader());
                        alex._animation = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetWalk);
                        alex.PlayAnimation("walk_left");
                        alex.etatAnimation = "walk_left";
                        alex.directionRight = false;
                    }
                    
                }
                else if (etatClavier.IsKeyDown(Keys.D) == true)
                {
                    ushort tx = (ushort)(Alex._positionAlex.X / _tiledMap.TileWidth);
                    ushort ty = (ushort)(Alex._positionAlex.Y / _tiledMap.TileHeight - 1);
                    //marche vers la droite
                    //direction = new Vector2(1, 0);
                    if (!alex.etatAnimation.Equals("walk_right"))
                    {
                        SpriteSheet spriteSheetWalk = Content.Load<SpriteSheet>("GraveRobber_walk.sf", new JsonContentLoader());
                        alex._animation = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetWalk);
                        alex.PlayAnimation("walk_right");
                        alex.etatAnimation = "walk_right";
                        alex.directionRight = true;
                    }
                    //if (IsCollision(tx, ty))
                    //    alex._positionAlex.X -= alex.velocity.X;

                }
                else
                {
                    //ne marche plus
                    direction = Vector2.Zero;
                    if (!alex.etatAnimation.Equals("idle"))
                    {
                        if (alex.directionRight)
                        {
                            //derniere marche a droite
                            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
                            alex._animation = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetIdle);
                            alex.PlayAnimation("idle");
                            alex.etatAnimation = "idle";
                        }
                        else
                        {
                            //derniere marche a gauche
                            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("MC_Idle_Left.sf", new JsonContentLoader());
                            alex._animation = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetIdle);
                            alex.PlayAnimation("idle_left");
                            alex.etatAnimation = "idle";
                        }
                    }

                }
            }
            // téléportation
            if (Alex._positionAlex.X < 2)
            {
                _myGame._niveauDepart.positionDepart.X = 1160;
                _myGame._screenManager.LoadScreen(_myGame._niveauDepart);
            }
            // Controle Objets :
            //ficelle
            _recFicelle = new Rectangle((int)_positionFicelle.X, (int)_positionFicelle.Y, 64, 64);            
            if (keyboardState.IsKeyDown(Keys.E))
            {
                _myGame._ficelle = false;
            }
            if (_recAlex.Intersects(_recFicelle))
                _myGame._ficelle = true;
            //baton
            _recBaton = new Rectangle((int)_positionBaton.X, (int)_positionBaton.Y, 64, 64);
            if (keyboardState.IsKeyDown(Keys.E))
            {
                _myGame._baton = false;
            }
            if (_recAlex.Intersects(_recBaton))
                _myGame._baton = true;
            //cannap
            if (_myGame._baton == true && _myGame._ficelle == true)
                _myGame._cannap = true;

        }
        public override void Draw(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //_myGame.GraphicsDevice.Clear(Color.Red);
            //map
            _tiledMapRenderer.Draw();
            //alex
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(alex._animation, Alex._positionAlex);
            if(_myGame._ficelle == false)
                _myGame.SpriteBatch.Draw(_textureFicelle, _positionFicelle, Color.White);
            if(_myGame._baton ==false)
                _myGame.SpriteBatch.Draw(_textureBaton, _positionBaton, Color.White);
            if (_myGame._cannap == true || keyboardState.IsKeyDown(Keys.C))
                _myGame.SpriteBatch.Draw(_textureCannap, _positionCannap, Color.White);
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _myGame.SpriteBatch.DrawRectangle(_recBaton, Color.Blue);
                _myGame.SpriteBatch.DrawRectangle(_recFicelle, Color.Blue);
                _myGame.SpriteBatch.DrawRectangle(_recAlex, Color.Blue);
                if (alex.directionRight == true)
                    _myGame.SpriteBatch.DrawPoint(Alex._positionAlex + new Vector2(-15,50), Color.Blue, 5);
                else
                _myGame.SpriteBatch.DrawPoint(Alex._positionAlex + new Vector2(15, 50), Color.Blue, 5);
            }

            _myGame.SpriteBatch.End();
        }
    }

}
