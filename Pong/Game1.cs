using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle _backgroundRectangle;
        private Texture2D _1Pixel;
        private Color _backgroundColor;
        private Paddle _leftPaddle;
        private Paddle _rightPaddle;
        private Puck _puck;
        private Net _net;
        private DigitsProvider _numbers;
        private Score _leftScore;
        private Score _rightScore;

        private const int WIDTH = 800;
        private const int HEIGHT = 700;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            _backgroundRectangle = new Rectangle(0, 0, WIDTH, HEIGHT);

            _numbers = new DigitsProvider(GraphicsDevice);
            _leftPaddle = new Paddle(GraphicsDevice, Side.Left);
            _rightPaddle = new Paddle(GraphicsDevice, Side.Right);
            _puck = new Puck(GraphicsDevice, new Vector2(WIDTH / 2, HEIGHT / 2), new Rectangle(0, 0, 10, 10), _leftPaddle, _rightPaddle);
            _net = new Net(GraphicsDevice);
            _leftScore = new Score(GraphicsDevice, Side.Left, _numbers);
            _rightScore = new Score(GraphicsDevice, Side.Right, _numbers);
            _backgroundColor = new Color(0x00, 0x00, 0x00, 0x70);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _1Pixel = new Texture2D(GraphicsDevice, 1, 1);
            _1Pixel.SetData(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
                Exit();

            _puck.Update(gameTime, kstate);
            _leftPaddle.Update(gameTime, kstate);
            _rightPaddle.Update(gameTime, kstate);
            _leftScore.SetScore(gameTime.TotalGameTime.Seconds);
            _rightScore.SetScore(gameTime.TotalGameTime.Seconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _spriteBatch.Draw(_1Pixel, _backgroundRectangle, _backgroundColor); // fade effect, instead of clear

            _leftScore.Show(_spriteBatch);
            _rightScore.Show(_spriteBatch);

            _net.Show(_spriteBatch);

            _leftPaddle.Show(_spriteBatch);
            _rightPaddle.Show(_spriteBatch);

            _puck.Show(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
