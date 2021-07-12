using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Puck
    {
        private Texture2D _texture2D;
        private Vector2 _position;
        private float _ballSpeed;
        private float _xSpeed = 1;
        private float _ySpeed = 4;
        private GraphicsDevice _graphicsDevice;

        public Puck(GraphicsDevice graphicsDevice, Vector2 position, int radius)
        {
            _graphicsDevice = graphicsDevice;
            _texture2D = TextureMaker.CreateCircle(radius, graphicsDevice, Color.White, 1, true);
            _position = position;
            _ballSpeed = 100f;
        }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, _position, Color.White);
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
            if (_position.Y < 0 || (_position.Y + _texture2D.Height) > _graphicsDevice.Viewport.Height)
                _ySpeed *= -1; // bounce

            if (_position.X < 0 || (_position.X + _texture2D.Width) > _graphicsDevice.Viewport.Width)
                _xSpeed *= -1; // bounce
        }

    }
}
