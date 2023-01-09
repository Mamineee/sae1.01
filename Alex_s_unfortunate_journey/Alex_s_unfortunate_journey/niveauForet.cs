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
        //alex
        Alex alex;
        private Vector2 direction = Vector2.Zero;
        public Vector2 positionDepart;
        
        

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

            //alex 
            //animation de base
            
            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle, 2, positionDepart);
            alex.PlayAnimation("idle");
            base.LoadContent();
        }
        public override void Initialize()
        {
            //alex._positionAlex.X = 100;
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
                alex.Movement(direction, deltaSeconds);
            }
            // test
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
            _myGame.SpriteBatch.End();
        }
    }

}
