using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public static class TextureMaker
    {
        public static Texture2D CreateCircle(int radius, GraphicsDevice graphicsDevice, Color color, int thickenes, bool fill)
        {
            var texture = new Texture2D(graphicsDevice, radius, radius);
            var colorData = new Color[radius * radius];
            if (thickenes >= radius)
                thickenes = radius - 5;

            float diam = radius / 2f;
            float diamsq = diam * diam;
            float intdiam = (radius - thickenes) / 2f;
            float intdiamsq = intdiam * intdiam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                        colorData[index] = color;
                    else
                        colorData[index] = Color.Transparent;

                    if (!fill && pos.LengthSquared() <= intdiamsq)
                        colorData[index] = Color.Transparent;
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        public static Texture2D CreateRect(Rectangle rect, GraphicsDevice graphicsDevice, Color color)
        {
            var texture = new Texture2D(graphicsDevice, rect.Width, rect.Height);
            var colorData = new Color[rect.Width * rect.Height];

            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = color;

            texture.SetData(colorData);
            return texture;
        }
    }
}
