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
    public class TiledTexture : Drawable2D
    {
        private Texture2D texture;
        private string pathTexture;
        private Vector2 origin;
        RectangleF rect;
        FloatingPoint point;
        int X_Repeat, Y_Repeat;

        [RequiredComponent]
        Transform2D trans2D;

        public TiledTexture(string pathTexture, int x_repeat, int y_repeat)
            : base("TiledTexture", DefaultLayers.GUI)
        {
            this.pathTexture = pathTexture;
            trans2D = null;
            X_Repeat = x_repeat;
            Y_Repeat = y_repeat;
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
                var supposedTextureWidth = (rect.Width / X_Repeat);
                var supposedTextureHeight = (rect.Height / Y_Repeat);
                var scaleX = (rect.Width / X_Repeat) / texture.Width;
                var scaleY = (rect.Height / Y_Repeat) / texture.Height;
                for (float x = 0; x < rect.Width; x += supposedTextureWidth)
                {
                    for (float y = 0; y < rect.Height; y += supposedTextureHeight)
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
                            new Vector2(scaleX, scaleY),
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
}