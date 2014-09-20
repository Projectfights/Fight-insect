using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Utils
{
    public class AnimatedTexture2D
    {
        private Texture2D sheet;
        
        private double frameTime;
        private int w, h, row, col;

        public AnimatedTexture2D(Texture2D sheet_, int w_, int h_, int row_, int col_, int fps)
        {
            sheet = sheet_;

            w = w_;
            h = h_;
            row = row_;
            col = col_;

            SetFPS(fps);
        }

        public void SetFPS(int fps)
        {
            frameTime = 1d / fps;
        }

        public void UpdateTime(GameTime gameTime)
        {
        }

        public Texture2D GetSheet()
        {
            return sheet;
        }

        public Rectangle GetWindow(int index)
        {
            return new Rectangle(index % col * w, index / col * h, w, h);
        }

        public int GetWidth()
        {
            return w;
        }

        public int GetHeight()
        {
            return h;
        }
    }
}
