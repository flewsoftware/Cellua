using Cellua.Simulation;

namespace Cellua.Api.Common
{
    public class SceneApi
    {
        public Scene Scene;

        public SceneApi(Scene scene)
        {
            Scene = scene;
        }

        public void SetColor(uint x, uint y, byte r, byte g, byte b, byte a)
        {
            Scene.Tiles.Data[x, y].Color.R = r;
            Scene.Tiles.Data[x, y].Color.G = g;
            Scene.Tiles.Data[x, y].Color.B = b;
            Scene.Tiles.Data[x, y].Color.A = a;
            Scene.ChangeMap.Data[x, y] = true;
        }

        public void SetTileTypeId(uint x, uint y, uint typeid)
        {
            Scene.Tiles.Data[x, y].TypeId = typeid;
        }

        public uint GetTileTypeId(uint x, uint y)
        {
            return Scene.Tiles.Data[x, y].TypeId;
        }
    }
}