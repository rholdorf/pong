using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class DigitsProvider
    {
        private int[][] _bits = new int[][]{
            // 0
            new int[] { 1,1,1,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1 },
            // 1
            new int[] { 0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1 },
            // 2
            new int[] { 1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        1,1,1,1,
                        1,0,0,0,
                        1,0,0,0,
                        1,0,0,0,
                        1,0,0,0,
                        1,1,1,1 },
            // 3
            new int[] { 1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        1,1,1,1 },
            // 4
            new int[] { 1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1 },
            // 5
            new int[] { 1,1,1,1,
                        1,0,0,0,
                        1,0,0,0,
                        1,0,0,0,
                        1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        1,1,1,1 },
            // 6
            new int[] { 1,0,0,0,
                        1,0,0,0,
                        1,0,0,0,
                        1,0,0,0,
                        1,1,1,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1 },
            // 7
            new int[] { 1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1 },
            // 8
            new int[] { 1,1,1,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1 },
            // 9
            new int[] { 1,1,1,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,0,0,1,
                        1,1,1,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1,
                        0,0,0,1 },
        };

        private List<Texture2D> _numberTextures = new List<Texture2D>(10);

        public DigitsProvider(GraphicsDevice graphicsDevice)
        {
            foreach (var currentNumber in _bits)
            {
                var colorData = new Color[4 * 12];
                for (int i = 0; i < currentNumber.Length; i++)
                {
                    if (currentNumber[i] == 1)
                    {
                        colorData[i] = Color.White;
                    }
                    else
                        colorData[i] = Color.Transparent;
                }

                var numberTexture = new Texture2D(graphicsDevice, 4, 12);
                numberTexture.SetData(colorData);
                _numberTextures.Add(numberTexture);
            }
        }

        public Texture2D GetDigit(int index)
        {
            return _numberTextures[index];
        }
    }
}
