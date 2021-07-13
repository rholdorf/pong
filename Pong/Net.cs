using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Net
    {
        private Texture2D _dashesTexture;
        private GraphicsDevice _graphicsDevice;
        private Rectangle _rectangle;

        public Net(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            CreateTexture();
        }

        private void CreateTexture()
        {
            _dashesTexture = new Texture2D(_graphicsDevice, 1, _graphicsDevice.Viewport.Height);
            var colorData = new Color[_dashesTexture.Width * _dashesTexture.Height];
            var switchEvery = (int)(_dashesTexture.Height / 30D / 2D);
            var color = Color.Transparent;

            for (int i = 0; i < colorData.Length; i++)
            {
                if (i % switchEvery == 0)
                {
                    if (color == Color.White)
                        color = Color.Transparent;
                    else
                        color = Color.White;
                }
                colorData[i] = color;
            }

            _dashesTexture.SetData(colorData);
            Resize();
        }

        public void Resize()
        {
            _rectangle = new Rectangle(
                _graphicsDevice.Viewport.Width / 2,
                0,
                1,
                _graphicsDevice.Viewport.Height);
        }

        public void Show(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_dashesTexture, _rectangle, Color.White);
        }
    }
}
