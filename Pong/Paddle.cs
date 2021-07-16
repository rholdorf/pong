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
        private int _distanceFromBorder = 75;
        private Texture2D _paddleTexture;
        private Vector2 _position;
        private float _speed;
        private float _ySpeed = 0;
        private int _topLimit;
        private int _bottomLimit;
        private Side _side;

        public Paddle(GraphicsDevice graphicsDevice, Side side)
        {
            _side = side;
            _graphicsDevice = graphicsDevice;
            _paddleTexture = TextureMaker.CreateRect(new Rectangle(0, 0, WIDTH, HEIGHT), graphicsDevice, Color.White);
            _speed = 400F;
            if (side == Side.Left)
                _position = new Vector2(_distanceFromBorder, _graphicsDevice.Viewport.Height / 2 - HEIGHT / 2);
            else
                _position = new Vector2(_graphicsDevice.Viewport.Width - _distanceFromBorder - WIDTH, _graphicsDevice.Viewport.Height / 2 - HEIGHT / 2);

            _topLimit = 20;
            _bottomLimit = _graphicsDevice.Viewport.Height - 20 - HEIGHT;
        }

        public int Width { get { return WIDTH; } }
        public int Height { get { return HEIGHT; } }
        public Vector2 Position { get { return _position; } }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_paddleTexture, _position, Color.White);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var offset = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_side == Side.Left)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                    _position.Y -= offset;

                if (keyboardState.IsKeyDown(Keys.Z))
                    _position.Y += offset;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                    _position.Y -= offset;

                if (keyboardState.IsKeyDown(Keys.Down))
                    _position.Y += offset;
            }

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
