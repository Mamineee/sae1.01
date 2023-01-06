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


namespace Alex_s_unfortunate_journey
{
    public class Alex
    {
        private AnimatedSprite _animation;
        private string _action;
        private Vector2 _positionPerso =new Vector2(0, 0); 
        const int _startPositionX = 125;
        const int _startPositionY = 245;
        const int _playerSpeed = 160;
        const int _moveUp = -1;
        const int _moveDown = 1;
        const int _moveLeft = -1;
        const int _moveRight = 1;

        enum State
        {
            Walking,
            Jumping
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;

        Vector2 mStartingPosition = Vector2.Zero;
        public void LoadContent(ContentManager theContentManager)
        {
            
        }

        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            _positionPerso += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);
            UpdateJump(aCurrentKeyboardState);

            mPreviousKeyboardState = aCurrentKeyboardState;
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Q) == true)
                {
                    mSpeed.X = _playerSpeed;
                    mDirection.X = _moveLeft;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.D) == true)
                {
                    mSpeed.X = _playerSpeed;
                    mDirection.X = _moveRight;
                }
            }
        }

        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (mCurrentState == State.Jumping)
            {
                if (mStartingPosition.Y - _positionPerso.Y > 150)
                {
                    mDirection.Y = _moveDown;
                }

                if (_positionPerso.Y > mStartingPosition.Y)
                {
                    _positionPerso.Y = mStartingPosition.Y;
                    mCurrentState = State.Walking;
                    mDirection = Vector2.Zero;
                }
            }
        }

        private void Jump()
        {
            if (mCurrentState != State.Jumping)
            {
                mCurrentState = State.Jumping;
                mStartingPosition = _positionPerso;
                mDirection.Y = _moveUp;
                mSpeed = new Vector2(_playerSpeed, _playerSpeed);
            }
        }


    }
}
