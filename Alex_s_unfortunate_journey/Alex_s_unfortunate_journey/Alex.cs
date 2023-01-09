using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;


namespace Alex_s_unfortunate_journey
{
    public class Alex
    {
        Game1 _myGame;
        public MonoGame.Extended.Sprites.AnimatedSprite _animation;
        public Vector2 _positionAlex;
        public String etatAnimation;
        public Boolean directionRight;
        const int _vitesse = 160;
        public Vector2 direction = Vector2.Zero;
        KeyboardState etatClavier = Keyboard.GetState();



        public enum Etats
        {
            Walk,
            Jump
        }
        public Etats etat = Etats.Walk;

       
        //saut
        public Vector2 velocity;
        public bool stateJump;

      

        Vector2 positionDepart = Vector2.Zero;

        public Alex(SpriteSheet spritesheet,float vitesse, Vector2 initialPos)
        {
            _animation = new MonoGame.Extended.Sprites.AnimatedSprite(spritesheet);
            _positionAlex = initialPos;
            etatAnimation = "idle";
            directionRight = true;
           
            //saut
            stateJump = true;
        }

        

        public void UpdateAnim(float deltasecond)
        {
            _animation.Update(deltasecond);
        }

        public void PlayAnimation(string nameAnim)
        {
            _animation.Play(nameAnim);
        }

        public void Movement(Vector2 _direction,float deltaSecond)
        {
            _positionAlex += _direction * _vitesse * deltaSecond;
        }

        

        //deplacement
        public void Deplacement()
        {
            if (etat == Alex.Etats.Walk)
            {
                //Vector2 direction = Vector2.Zero;

                if (etatClavier.IsKeyDown(Keys.Q) == true)
                {
                    //marche vers la gauche
                    direction = new Vector2(-1, 0);
                    if (!etatAnimation.Equals("walk_left"))
                    {
                        
                        _animation = new MonoGame.Extended.Sprites.AnimatedSprite(_myGame.spriteSheetWalk);
                        PlayAnimation("walk_left");
                        etatAnimation = "walk_left";
                        directionRight = false;
                    }
                }
                else if (etatClavier.IsKeyDown(Keys.D) == true)
                {
                    //marche vers la droite
                    direction = new Vector2(1, 0);
                    if (!etatAnimation.Equals("walk_right"))
                    {
                        
                        _animation = new MonoGame.Extended.Sprites.AnimatedSprite(_myGame.spriteSheetWalk);
                        PlayAnimation("walk_right");
                        etatAnimation = "walk_right";
                        directionRight = true;
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
                    if (!etatAnimation.Equals("idle"))
                    {
                        if (directionRight)
                        {
                            //derniere marche a droite
                            
                            _animation = new MonoGame.Extended.Sprites.AnimatedSprite(_myGame.spriteSheetIdle);
                            PlayAnimation("idle");
                            etatAnimation = "idle";
                        }
                        else
                        {
                            //derniere marche a gauche
                            
                            _animation = new MonoGame.Extended.Sprites.AnimatedSprite(_myGame.spriteSheetIdle);
                            PlayAnimation("idle_left");
                            etatAnimation = "idle";
                        }
                    }
                }

                
                //Movement(direction, deltaSeconds);
            }
        }


        //saut
       public void Saut()
        {
            
            _positionAlex += velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = 3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                velocity.X = -3f;
            }
            else
            {
                velocity.X = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) & stateJump == false)
            {
                _positionAlex.Y -= 10f;
                velocity.Y = -5f;
                stateJump = true;
            }
            if (stateJump == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }
            if (_positionAlex.Y + 66 >= 676)
            {
                stateJump = false;
            }
            if (stateJump == false)
            {
                velocity.Y = 0f;
            }
        }


    }
}
