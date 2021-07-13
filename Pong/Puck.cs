using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Puck
    {
        private Texture2D _puckTexture;
        private Vector2 _position;
        private float _ballSpeed;
        private float _xSpeed = 1;
        private float _ySpeed = 4;
        private GraphicsDevice _graphicsDevice;

        public Puck(GraphicsDevice graphicsDevice, Vector2 position, Rectangle rect)
        {
            _graphicsDevice = graphicsDevice;
            _puckTexture = TextureMaker.CreateRect(rect, graphicsDevice, Color.White);
            _position = position;
            _ballSpeed = 100f;
        }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_puckTexture, _position, Color.White);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var offset = _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Up))
                _position.Y -= offset;

            if (keyboardState.IsKeyDown(Keys.Down))
                _position.Y += offset;

            if (keyboardState.IsKeyDown(Keys.Left))
                _position.X -= offset;

            if (keyboardState.IsKeyDown(Keys.Right))
                _position.X += offset;

            Move();
            CheckEdges();
        }

        private void Move()
        {
            _position.Y += _ySpeed;
            _position.X += _xSpeed;
        }

        private void CheckEdges()
        {
            if (_position.Y < 0 || (_position.Y + _puckTexture.Height) > _graphicsDevice.Viewport.Height)
                _ySpeed *= -1; // bounce

            if (_position.X < 0 || (_position.X + _puckTexture.Width) > _graphicsDevice.Viewport.Width)
                _xSpeed *= -1; // bounce
        }

    }
}
