using System;
namespace Pong
{
    public class Paddle
    {
        private PaddleSide _paddleSide;

        public Paddle(PaddleSide side)
        {
            _paddleSide = side;
        }
    }
}
