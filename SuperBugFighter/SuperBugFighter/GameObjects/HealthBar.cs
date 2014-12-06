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

        public HealthBar(Fighter fighter, Texture2D tex) : base()
        {
            f = fighter;
            this.tex = tex;
            baseWidth = 100;
        }

        public HealthBar(Fighter fighter, Texture2D tex, Vector2 pos)
            : base(pos)
        {
            f = fighter;
            this.tex = tex;
            baseWidth = 100;
        }

        public HealthBar(Fighter fighter, Texture2D tex, Vector2 pos, int baseWidth)
            : base(pos)
        {
            f = fighter;
            this.tex = tex;
            this.baseWidth = baseWidth;
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
            return new Rectangle((int)Pos.X, (int)Pos.Y, (int)(baseWidth * f.Health / 100.0), 20);
        }
    }
}
