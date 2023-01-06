﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Alex_s_unfortunate_journey
{
    public class Alex
    {
        private AnimatedSprite _animation;
        private string _action;
        private Vector2 _positionAlex =new Vector2(0, 0); 
        const int _startPositionX = 125;
        const int _startPositionY = 245;
        const int _vitesse = 160;
        const int _haut = -1;
        const int _bas = 1;
        const int _gauche = -1;
        const int _droite = 1;

        enum Etats
        {
            Walk,
            Jump
        }
        Etats etat = Etats.Walk;

        Vector2 direction = Vector2.Zero;
        Vector2 vitesse = Vector2.Zero;

        KeyboardState ancienEtatClavier;

        Vector2 positionDepart = Vector2.Zero;
        public void LoadContent()
        {
            
        }

        //public void Update(GameTime __gameTime, Vector2 __vitesse, Vector2 __direction)
        //{
        //    _positionAlex += __direction * __vitesse * (float)__gameTime.ElapsedGameTime.TotalSeconds;
        //}
        public void Update(GameTime gameTime)
        {
            KeyboardState etatClavier = Keyboard.GetState();

            //UpdateMovement(etatClavier);
            UpdateJump(etatClavier);

            ancienEtatClavier = etatClavier;
        }

        //private void UpdateMovement(KeyboardState etatClavier)
        //{
        //    if (etat == Etats.Walk)
        //    {
        //        vitesse = Vector2.Zero;
        //        direction = Vector2.Zero;

        //        if (etatClavier.IsKeyDown(Keys.Q) == true)
        //        {
        //            vitesse.X = _vitesse;
        //            direction.X = _gauche;
        //        }
        //        else if (etatClavier.IsKeyDown(Keys.D) == true)
        //        {
        //            vitesse.X = _vitesse;
        //            direction.X = _droite;
        //        }
        //    }
        //}

        private void UpdateJump(KeyboardState etatClavier)
        {
            if (etat == Etats.Walk)
            {
                if (etatClavier.IsKeyDown(Keys.Space) == true && ancienEtatClavier.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (etat == Etats.Jump)
            {
                if (positionDepart.Y - _positionAlex.Y > 150)
                {
                    direction.Y = _bas;
                }

                if (_positionAlex.Y > positionDepart.Y)
                {
                    _positionAlex.Y = positionDepart.Y;
                    etat = Etats.Walk;
                    direction = Vector2.Zero;
                }
            }
        }

        private void Jump()
        {
            if (etat != Etats.Jump)
            {
                etat = Etats.Jump;
                positionDepart = _positionAlex;
                direction.Y = _haut;
                vitesse = new Vector2(_vitesse, _vitesse);
            }
        }


    }
}
