using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Score
    {
        private Texture2D _firstDigit;
        private Texture2D _secondDigit;
        private GraphicsDevice _graphicsDevice;
        private DigitsProvider _digitsProvider;
        private Rectangle _firstRectangle;
        private Rectangle _secondRectangle;
        private int _score = -1;

        public Score(GraphicsDevice graphicsDevice, Side side, DigitsProvider digitsProvider)
        {
            _graphicsDevice = graphicsDevice;
            _digitsProvider = digitsProvider;

            _firstRectangle = new(115, 20, 40, 115);
            _secondRectangle = new(195, 20, 40, 115);

            if (side == Side.Right)
            {
                _firstRectangle.X = 595;
                _secondRectangle.X = 675;
            }
            SetScore(0);
        }

        public void SetScore(int score)
        {
            if (score == _score)
                return;

            _score = score;
            if (score > 9)
            {
                var ss = score.ToString();
                var d1 = Convert.ToInt16(ss.Substring(0, 1));
                var d2 = Convert.ToInt16(ss.Substring(1, 1));
                _firstDigit = _digitsProvider.GetDigit(d1);
                _secondDigit = _digitsProvider.GetDigit(d2);
            }
            else
            {
                _firstDigit = null;
                _secondDigit = _digitsProvider.GetDigit(score);
            }
        }

        public void Show(SpriteBatch spriteBatch)
        {
            if (_firstDigit != null)
                spriteBatch.Draw(_firstDigit, _firstRectangle, Color.White);

            spriteBatch.Draw(_secondDigit, _secondRectangle, Color.White);
        }
    }
}
