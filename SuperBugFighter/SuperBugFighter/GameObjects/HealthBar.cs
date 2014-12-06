using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bug.GameObjects
{
    class HealthBar : GameObject
    {
        //Figher that this HealthBar tracks
        Fighter f;

        Texture2D tex;

        int baseWidth;
        bool flip;

        public HealthBar(Fighter fighter, Texture2D tex_, bool flip_) : base()
        {
            f = fighter;
            tex = tex_;
            baseWidth = 100;
            flip = flip_;
        }

        public HealthBar(Fighter fighter, Texture2D tex_, bool flip_, Vector2 pos)
            : base(pos)
        {
            f = fighter;
            tex = tex_;
            baseWidth = 100;
            flip = flip_;
        }

        public HealthBar(Fighter fighter, Texture2D tex_, bool flip_, Vector2 pos, int baseWidth_)
            : base(pos)
        {
            f = fighter;
            tex = tex_;
            baseWidth = baseWidth_;
            flip = flip_;
        }

        public override void Update(GameTime t)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, GetBoundingBox(), Color.White);
        }

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Pos.X + (flip?(int)(baseWidth * (1- f.Health / 100.0)):0), (int)Pos.Y, (int)(baseWidth * f.Health / 100.0), 20);
        }
    }
}
