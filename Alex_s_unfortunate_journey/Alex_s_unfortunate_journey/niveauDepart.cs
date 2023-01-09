﻿using Microsoft.Xna.Framework;
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
            SpriteSheet spriteSheetIdle = Content.Load<SpriteSheet>("GraveRobber_idle.sf", new JsonContentLoader());
            alex = new Alex(spriteSheetIdle, 2, positionDepart);
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
            KeyboardState etatClavier = Keyboard.GetState();
            //saut
            alex._positionAlex += alex.velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                alex.velocity.X = 3f; }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
                alex.velocity.X = -3f; }
            else
            {
                alex.velocity.X = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) & alex.stateJump == false)
                {
                    alex._positionAlex.Y -= 10f;
                    alex.velocity.Y = -5f;
                    alex.stateJump = true;
                }
                if (alex.stateJump == true)
                {
                    float i = 1;
                    alex.velocity.Y += 0.15f * i;
                }
                if (alex._positionAlex.Y + 66 >= 676)
                {
                    alex.stateJump = false;
                }
                if (alex.stateJump == false)
                {
                    alex.velocity.Y = 0f;
                }

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

                    alex.Movement(direction, deltaSeconds);
                }
                // teleportation
                if (alex._positionAlex.X > 1190)
                {
                    _myGame._niveauDepart.positionDepart.X = 20;
                    _myGame._screenManager.LoadScreen(_myGame._niveauForet);

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
