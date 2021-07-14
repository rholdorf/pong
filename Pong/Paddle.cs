using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Paddle
    {
        private GraphicsDevice _graphicsDevice;
        private const int WIDTH = 7;
        private const int HEIGHT = 40;
        private Texture2D _paddleTexture;
        private Vector2 _position;
        private float _speed;
        private float _ySpeed = 0;
        private int _topLimit;
        private int _bottomLimit;

        public Paddle(GraphicsDevice graphicsDevice, PaddleSide side)
        {
            _graphicsDevice = graphicsDevice;
            _paddleTexture = TextureMaker.CreateRect(new Rectangle(0, 0, WIDTH, HEIGHT), graphicsDevice, Color.White);
            _speed = 400F;
            if (side == PaddleSide.Left)
                _position = new Vector2(20, _graphicsDevice.Viewport.Height / 2 - HEIGHT / 2);
            else
                _position = new Vector2(_graphicsDevice.Viewport.Width - 20, _graphicsDevice.Viewport.Height / 2 - HEIGHT / 2);
        }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_paddleTexture, _position, Color.White);
        }

        public void Resize()
        {
            _topLimit = 20;
            _bottomLimit = _graphicsDevice.Viewport.Height - 20 - HEIGHT;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var offset = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Up))
                _position.Y -= offset;

            if (keyboardState.IsKeyDown(Keys.Down))
                _position.Y += offset;

            Move();
            CheckEdges();
        }

        private void Move()
        {
            _position.Y += _ySpeed;
        }

        private void CheckEdges()
        {
            if (_position.Y < _topLimit)
            {
                _ySpeed = 0;
                _position.Y = _topLimit;
            }
            if (_position.Y > _bottomLimit)
            {
                _ySpeed = 0;
                _position.Y = _bottomLimit;
            }
        }
    }
}
