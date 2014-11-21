using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace CustomContent
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class AnimationWriter : ContentTypeWriter<AnimationData>
    {
        protected override void Write(ContentWriter output, AnimationData value)
        {
            output.WriteExternalReference(value.sheetRef);
            output.Write(value.fps);
            output.Write(value.w);
            output.Write(value.h);
            output.Write(value.row);
            output.Write(value.col);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Heist.Utils.AnimatedTexture2DReader, Heist," +
               " Version=1.0.0.0, Culture=neutral";
        }
    }
}
