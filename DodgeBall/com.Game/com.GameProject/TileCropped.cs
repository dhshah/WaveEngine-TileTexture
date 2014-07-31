using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace com.GameProject
{
    public class TileCropped : Drawable2D
    {
        private Texture2D texture;
        private string pathTexture;
        private Vector2 origin;
        RectangleF rect;
        FloatingPoint point;

        [RequiredComponent]
        Transform2D trans2D;

        public TileCropped(string pathTexture)
            : base("TileCropped", DefaultLayers.GUI)
        {
            this.pathTexture = pathTexture;
            trans2D = null;
        }

        protected override void Initialize()
        {
            base.Initialize();

            texture = Assets.LoadAsset<Texture2D>(pathTexture);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(TimeSpan gameTime)
        {
            if (trans2D != null)
            {
                rect = trans2D.Rectangle;
                for (float x = 0; x < rect.Width; x += texture.Width)
                {
                    for (float y = 0; y < rect.Height; y += texture.Height)
                    {
                        point = rotate(new FloatingPoint(x, y), trans2D.Rotation, 
                            new Vector2(trans2D.Origin.X * rect.Width, trans2D.Origin.Y * rect.Height));
                        this.layer.SpriteBatch.Draw(
                            texture,
                            new Vector2(point.X + trans2D.X, point.Y + trans2D.Y),
                            null,
                            Color.White,
                            trans2D.Rotation,
                            new Vector2(0, 0),
                            new Vector2(1, 1),
                            trans2D.Effect,
                            0);
                    }
                }
            }
        }

        private FloatingPoint rotate(FloatingPoint p, float angle, Vector2 origin)
        {
            FloatingPoint rotated = new FloatingPoint(0f, 0f);
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            p.X -= (int)origin.X;
            p.Y -= (int)origin.Y;

            rotated.X = (float)(p.X * cos - p.Y * sin);
            rotated.Y = (float)(p.X * sin + p.Y * cos);

            p.X += (int)origin.X;
            p.Y += (int)origin.Y;

            return rotated;
        }

        protected override void Dispose(bool disposing)
        {
            Assets.UnloadAsset(pathTexture);
        }
    }

    public class FloatingPoint
    {
        public float X;
        public float Y;

        public FloatingPoint()
        {
            X = 0f;
            Y = 0f;
        }

        public FloatingPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
