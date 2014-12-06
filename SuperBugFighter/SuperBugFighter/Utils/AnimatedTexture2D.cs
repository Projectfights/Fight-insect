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
        
        private double frameTime, elapsedTime;
        private int fps, index, w, h, row, col;

        public bool Looped { get; set; }

        public AnimatedTexture2D(Texture2D sheet_, int w_, int h_, int row_, int col_, int fps_)
        {
            sheet = sheet_;

            index = 0;
            Looped = false;
            w = w_;
            h = h_;
            row = row_;
            col = col_;
            elapsedTime = 0;

            fps = fps_;
            SetFPS(fps_);
        }

        public void SetFPS(int fps)
        {
            // The 2 is a total fudge-factor
            frameTime = 2 * 1000d / fps;
        }

        public void UpdateTime(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime > frameTime)
            {
                index++;

                if (index >= row * col)
                {
                    index = 0;
                    Looped = true;
                }

                elapsedTime = elapsedTime % frameTime;
            }
        }

        public void Reset()
        {
            index = 0;
            Looped = false;
        }

        public Texture2D GetSheet()
        {
            return sheet;
        }

        public void SetSheet(Texture2D sheet_)
        {
            sheet = sheet_;
        }

        public Rectangle GetWindow()
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


        internal double GetNumFrames()
        {
            return row * col;
        }

        internal double GetFps()
        {
            return fps;
        }
    }
}
