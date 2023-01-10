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
using MonoGame.Extended.Tiled;

namespace Alex_s_unfortunate_journey
{
    public class Alex
    {
        public MonoGame.Extended.Sprites.AnimatedSprite _animation;
        public static Vector2 _positionAlex = Vector2.Zero;
        public String etatAnimation;
        public Boolean directionRight;
        ushort left;
        ushort right;
        const int _vitesse = 160;
        niveauForet Foret;
        //map

        private TiledMap _tiledMap;
       
        public enum Etats
        {
            Walk,
            Jump
        }
        public Etats etat = Etats.Walk;
        

        //saut
        public Vector2 velocity;
        public bool stateJump;

        //KeyboardState ancienEtatClavier;

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

        public bool IsCollision(ushort x, ushort y, TiledMap tiledMap)
        {           
            TiledMapTileLayer mapLayer = tiledMap.GetLayer<TiledMapTileLayer>("Plateforme");

            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        public void Jump(TiledMap tiledMap)
        {  
            if(directionRight == true)
                left = (ushort)((_positionAlex.X -15)/ tiledMap.TileWidth);
            else
            {
                left = (ushort)((_positionAlex.X+15) / tiledMap.TileWidth);

            }
            ushort ty = (ushort)((_positionAlex.Y +50)/ tiledMap.TileHeight);
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
                if (IsCollision(left, ty, tiledMap) && !Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    Console.WriteLine("oui");
                    _positionAlex.Y -= velocity.Y;
                    stateJump = false;
                }
                
                float i = 1;
                velocity.Y += 0.15f * i;                
            }
            if (!IsCollision(left, ty, tiledMap) /*&& !IsCollision(right, ty, tiledMap)*/)
            {
                stateJump = true;
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
        public void Jump()
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
