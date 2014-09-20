using Bug.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.Model.Display
{
    class ColorRect : Image
    {
        private int width, height;
        private Color[] data;

        public ColorRect(int x, int y, int width_, int height_, Color color)
            : base(x, y, null)
        {
            width = width_;
            height = height_;

            data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (tex == null)
            {
                tex = new Texture2D(batch.GraphicsDevice, width, height);
                tex.SetData(data);
            }

            base.Draw(batch);
        }
    }
}
