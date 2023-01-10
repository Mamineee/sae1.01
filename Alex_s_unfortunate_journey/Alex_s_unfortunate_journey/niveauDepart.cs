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
        
        public Vector2 positionDepart;
        //cannap
        private Vector2 _positionCannap;
        private Texture2D _textureCannap;
        //poisson
        private Vector2 _positionPoiss;
        private Texture2D _texturePoiss;

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
            //objets
            _textureCannap = Content.Load<Texture2D>("cannapcase");
            _texturePoiss = Content.Load<Texture2D>("poissoncase");
            //alex 
            //animation de base
            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle, 2, positionDepart);
            alex.PlayAnimation("idle");
            base.LoadContent();
        }
        public override void Initialize()
        {
            _positionPoiss = new Vector2(20, 20);
            _positionCannap = new Vector2(20, 20);
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
            //saut
            alex.Jump();
            
            // deplacement
             if (alex.etat == Alex.Etats.Walk)
                {
                    //Vector2 direction = Vector2.Zero;

                    if (etatClavier.IsKeyDown(Keys.Q) == true)
                    {
                        //marche vers la gauche
                        direction = new Vector2(-1, 0);
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
                        //marche vers la droite
                        direction = new Vector2(1, 0);
                        if (!alex.etatAnimation.Equals("walk_right"))
                        {
                            SpriteSheet spriteSheetWalk = Content.Load<SpriteSheet>("GraveRobber_walk.sf", new JsonContentLoader());
                            alex._animation = new MonoGame.Extended.Sprites.AnimatedSprite(spriteSheetWalk);
                            alex.PlayAnimation("walk_right");
                            alex.etatAnimation = "walk_right";
                            alex.directionRight = true;
                        }
                    }
                    else if (etatClavier.IsKeyDown(Keys.Space) == true)
                    {
                        //saute

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

             // teleportation
             if (Alex._positionAlex.X > 1190)
             {
                    _myGame._niveauDepart.positionDepart.X = 20;

                    _myGame._screenManager.LoadScreen(_myGame._niveauForet);
             }
             if (Alex._positionAlex.X < 250)
             {
                System.Console.WriteLine("bloque");
                Alex._positionAlex.X -= alex.velocity.X;
                if (alex.directionRight == true)
                {
                    Alex._positionAlex.X += alex.velocity.X;
                }
             }
        }
        public override void Draw(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //map
            _tiledMapRenderer.Draw();
            //alex
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(alex._animation, Alex._positionAlex);
            if (_myGame._cannap == true && _myGame._poisson == false || keyboardState.IsKeyDown(Keys.C))
            {
                _myGame.SpriteBatch.Draw(_textureCannap, _positionCannap, Color.White);
                _myGame._poisson = true;
                    
            }
            if (Alex._positionAlex.X < 270 && _myGame._poisson == true)
            {
                System.Console.WriteLine("poisson");

                _myGame.SpriteBatch.Draw(_texturePoiss, _positionPoiss, Color.White);
            }
                _myGame.SpriteBatch.End();
        }
    }
    
}
