using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.IO;
using System.Xml;

namespace CustomContent
{
    public class AnimationData
    {
        public ExternalReference<TextureContent> sheetRef;
        public string sheet;
        public int fps, w, h, row, col;
        
        public AnimationData(string sheet_, int fps_, int w_, int h_, int row_, int col_)
        {
            sheet = sheet_;

            fps = fps_;
            w = w_;
            h = h_;
            row = row_;
            col = col_;
        }

    }
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".anim", DisplayName = "Animation Importer", DefaultProcessor = "AnimationProcessor")]
    public class AnimationImporter : ContentImporter<AnimationData>
    {
        public override AnimationData Import(string filename, ContentImporterContext context)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;

            string sheet = "";
            int fps = 0;
            int w = 0;
            int h = 0;
            int row = 0;
            int col = 0;

            foreach (XmlNode node in root.ChildNodes)
            {
                switch (node.Name)
                {

                    case "sheet":
                        sheet = node.InnerText;
                        break;
                    case "fps":
                        fps = Int32.Parse(node.InnerText);
                        break;
                    case "width":
                        w = Int32.Parse(node.InnerText);
                        break;
                    case "height":
                        h = Int32.Parse(node.InnerText);
                        break;
                    case "row":
                        row = Int32.Parse(node.InnerText);
                        break;
                    case "col":
                        col = Int32.Parse(node.InnerText);
                        break;
                    default:
                        throw new Exception(node.Value);
                }
            }

            return new AnimationData(sheet, fps, w, h, row, col);
        }
    }
}
