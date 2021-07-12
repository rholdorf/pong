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
        private float ballSpeed;
        private float xSpeed = 1;
        private float ySpeed = 4;
        private GraphicsDevice _graphicsDevice;
        private Rectangle boundingBox;

        public Puck(GraphicsDevice graphicsDevice, Vector2 position, int radius)
        {
            _graphicsDevice = graphicsDevice;
            _texture2D = TextureMaker.CreateCircle(radius, graphicsDevice, Color.White, 1, true);
            _position = position;
            ballSpeed = 100f;
        }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, _position, Color.White);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var offset = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Up))
                _position.Y -= offset;

            if (keyboardState.IsKeyDown(Keys.Down))
                _position.Y += offset;

            if (keyboardState.IsKeyDown(Keys.Left))
                _position.X -= offset;

            if (keyboardState.IsKeyDown(Keys.Right))
                _position.X += offset;

            // moving
            Move();
            CheckEdges();
        }

        private void Move()
        {
            _position.Y += ySpeed;
            _position.X += xSpeed;
        }

        private void CheckEdges()
        {
            if (_position.Y < 0 || (_position.Y + _texture2D.Height) > _graphicsDevice.Viewport.Height)
                ySpeed *= -1;

            if (_position.X < 0 || (_position.X + _texture2D.Width) > _graphicsDevice.Viewport.Width)
                xSpeed *= -1;
        }

    }
}
