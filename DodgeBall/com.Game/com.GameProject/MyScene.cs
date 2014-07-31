#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Services;
#endregion

namespace com.GameProject
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.Teal;
            string path = "../../../Content/Export/Default/";
            string sprite_path = path + "Tiles/Castle/CastleMid.wpk";

            TileCropped tc = new TileCropped(sprite_path);
            Entity e = new Entity("OLOL")
            .AddComponent(new Sprite(sprite_path)
            {
                SourceRectangle = new Rectangle(0, 0, 200, 200),
            })
            .AddComponent(tc)
            .AddComponent(new Transform2D() { 
                X = 2 * WaveServices.Platform.ScreenWidth/2, 
                Y = WaveServices.Platform.ScreenHeight/2,
                Rotation = 2,
            });

            EntityManager.Add(e);

            TiledTexture tr2 = new TiledTexture(sprite_path, 1, 4);
            Entity f = new Entity("OLOL2")
            .AddComponent(tr2)
            .AddComponent(new Sprite(sprite_path)
            {
                SourceRectangle = new Rectangle(0, 0, 200, 200),
            })
            .AddComponent(new Transform2D()
            {
                X = WaveServices.Platform.ScreenWidth / 3,
                Y = WaveServices.Platform.ScreenHeight / 2,
                Origin = new Vector2(.5f, .5f),
                Rotation = 1,
            });

            EntityManager.Add(f);
            
            RenderManager.DebugLines = true;
        }
    }
}
