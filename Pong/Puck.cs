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
        private float _xVelocity = 1;
        private float _yVelocity = 4;
        private GraphicsDevice _graphicsDevice;
        private Paddle _leftPaddle;
        private Paddle _rightPaddle;

        private int _leftHitLine;
        private int _rightHitLine;
        private int _leftMissLine;
        private int _rightMissLine;
        private int _netLine;
        private bool _hit = false;
        private bool _farFromPaddle = true;

        private int _paddleWidth;
        private int _paddleHeight;
        private int _width;
        private int _height;
        private float _PiDividedByFour = (float)Math.PI * 0.25F;
        private float _yBaseVelocity = 4;
        private int[] _simpleAngleLookUpTableLeft = new[] { -45, -30, -15, 0, 0, 15, 30, 45 };
        private int[] _simpleAngleLookUpTableRight = new[] { -135, -150, -165, 180, 180, 165, 150, 135 };

        public Puck(GraphicsDevice graphicsDevice, Vector2 position, Rectangle rect, Paddle leftPaddle, Paddle rightPaddle)
        {
            _graphicsDevice = graphicsDevice;
            _puckTexture = TextureMaker.CreateRect(rect, graphicsDevice, Color.White);
            _width = rect.Width;
            _height = rect.Height;
            _position = position;
            _leftPaddle = leftPaddle;
            _rightPaddle = rightPaddle;
            _leftHitLine = 75 + 7; // (left paddle X + paddle width)
            _rightHitLine = _graphicsDevice.Viewport.Width - 75 - 7; // (screen right edge - right distance from border - paddle width - puck width)
            _leftMissLine = _leftHitLine - 5;
            _rightMissLine = _rightHitLine + 5;
            _netLine = _graphicsDevice.Viewport.Width / 2;



            _paddleWidth = leftPaddle.Width;
            _paddleHeight = leftPaddle.Height;
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public void Show(SpriteBatch spriteBatch)
        {
            if (_farFromPaddle)
                spriteBatch.Draw(_puckTexture, _position, Color.Purple);
            else
                spriteBatch.Draw(_puckTexture, _position, Color.White);

            spriteBatch.Draw(_puckTexture, new Rectangle(_rightHitLine, 0, 1, _graphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Green);
            spriteBatch.Draw(_puckTexture, new Rectangle(_leftHitLine, 0, 1, _graphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Green);
            spriteBatch.Draw(_puckTexture, new Rectangle(_rightMissLine, 0, 1, _graphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Red);
            spriteBatch.Draw(_puckTexture, new Rectangle(_leftMissLine, 0, 1, _graphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Red);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Move();
            CheckPaddleCollision();
            CheckEdges();
        }

        private void Move()
        {
            _position.Y += _yVelocity;
            _position.X += _xVelocity;
        }

        private void CheckEdges()
        {
            if (_position.Y < 0 || (_position.Y + _puckTexture.Height) > _graphicsDevice.Viewport.Height)
                _yVelocity *= -1; // bounce

            if (_position.X < 0 || (_position.X + _puckTexture.Width) > _graphicsDevice.Viewport.Width)
                _xVelocity *= -1; // bounce
        }

        // The original Allan Alcorn Pong machine used hard-wired transistor
        // logic for it's components. The paddles were divided into 8 segments,
        // with the center two segments returning the ball at 90 degrees and
        // outward segments returning the ball at lesser and lesser angles.
        // Every time the ball hit a paddle, it would speed up slightly until it
        // reached the maximum speed of the machine. 
        // 
        // It was pixel-perfect collision. Depending on which sprite-segment of
        // the paddle registered the interference with the ball, the game would
        // apply the angle and speed to the ball accordingly.
        //
        // -3 --> -45°        -135° <-- -3
        // -2 --> -30°        -150° <-- -2
        // -1 --> -15°        -165° <-- -1
        //  0 -->   0°         180° <--  0
        //  0 -->   0°         180° <--  0
        //  1 -->  15°         165° <--  1
        //  2 -->  30°         150° <--  2
        //  3 -->  45°         135° <--  3
        public void CheckPaddleCollision()
        {
            var x = _position.X;
            var puckRightBorder = x + _width;
            if (x > _leftHitLine && puckRightBorder < _rightHitLine)
            {
                // too far from puck
                _farFromPaddle = true;
                return;
            }

            _farFromPaddle = false;

            var y = _position.Y;
            if (y < 20 || y > 680)
            {
                Console.WriteLine("squeeze");
                return; // puck squeezed through top or bottom gap
            }


            if (x < _leftMissLine || x > _rightMissLine)
            {
                Console.WriteLine("miss x");
                return; // miss
            }

            var isAtLeft = x < _netLine;
            int paddleY;
            if (isAtLeft)
                paddleY = (int)_leftPaddle.Position.Y;
            else
                paddleY = (int)_rightPaddle.Position.Y;


            var offset = y - paddleY;
            if (offset < 0 || offset > _paddleHeight)
            {
                Console.WriteLine("miss y: " + offset);
                return; // miss
            }

            // hit
            Console.WriteLine(offset);
            _xVelocity *= -1;

            if (isAtLeft)
            {
                _position.X = _leftHitLine + 1; // bounce
            }
            else
            {
                _position.X = _rightHitLine - 1; // bounce
            }



        }



    }
}
