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

        private Puck _puck;
        private Net _net;
        private Numbers _numbers;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 630;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            _puck = new Puck(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), new Rectangle(0, 0, 10, 10));
            _net = new Net(GraphicsDevice);
            _backgroundColor = new Color(0x00, 0x00, 0x00, 0x80);

            _numbers = new Numbers(GraphicsDevice);

            Window_ClientSizeChanged(null, null);
            base.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            //var width = Window.ClientBounds.Width;
            //var height = Window.ClientBounds.Height;

            //if (height < width / (float)_graphics.PreferredBackBufferWidth * _graphics.PreferredBackBufferHeight)
            //    width = (int)(height / (float)_graphics.PreferredBackBufferHeight * _graphics.PreferredBackBufferWidth);
            //else
            //    height = (int)(width / (float)_graphics.PreferredBackBufferWidth * _graphics.PreferredBackBufferHeight);

            //var x = (Window.ClientBounds.Width - width) / 2;
            //var y = (Window.ClientBounds.Height - height) / 2;
            _backgroundRectangle = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _net.Resize();
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            _spriteBatch.Draw(_1Pixel, _backgroundRectangle, _backgroundColor); // fade effect, instead of clear

            _spriteBatch.Draw(_numbers.GetTexture(0), new Rectangle(30, 10, 40, 120), Color.White);

            _net.Show(_spriteBatch);
            _puck.Show(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
