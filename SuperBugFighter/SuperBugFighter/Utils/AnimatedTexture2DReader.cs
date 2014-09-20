using Microsoft.Xna.Framework.Content;
using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Bug.Utils
{
    class AnimatedTexture2DReader : ContentTypeReader<AnimatedTexture2D>
    {
        protected override AnimatedTexture2D Read(ContentReader input, AnimatedTexture2D existingInstance)
        {
            var sheet = input.ReadExternalReference<Texture2D>();
            int fps = input.ReadInt32();
            int w = input.ReadInt32();
            int h = input.ReadInt32();
            int row = input.ReadInt32();
            int col = input.ReadInt32();

            return new AnimatedTexture2D(sheet, w, h, row, col, fps);
        }
    }
}
