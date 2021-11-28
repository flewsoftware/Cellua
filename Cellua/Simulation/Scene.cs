using System.Numerics;
using Cellua.Api;
using Cellua.Storage;
using SFML.Graphics;

namespace Cellua.Simulation
{
    public struct SceneInfo
    {
        public uint Size;

        public SceneInfo(uint size)
        {
            Size = size;
        }
    }
    
    public class Scene
    {
        public Map<bool> ChangeMap;
        public Map<Tile> Tiles;
        private byte[] _pixels;
        
        public readonly SceneInfo SceneInfo;

        
        private void InitMaps()
        {
            ChangeMap = new Map<bool>(SceneInfo.Size);
            Tiles = new Map<Tile>(SceneInfo.Size);
            _pixels = new byte[SceneInfo.Size * SceneInfo.Size * 4];
            for (uint x = 0; x < SceneInfo.Size; x++)
            {
                for (uint y = 0; y < SceneInfo.Size; y++)
                {
                    Tiles.Data[x, y] = new Tile
                    {
                        TypeId = 0,
                        Color = Color.Black
                    };
                }
            }
        }
        
        public Scene(SceneInfo sceneInfo)
        {
            SceneInfo = sceneInfo;
            InitMaps();
        }
        
        private uint Index2dToIndex1d(uint x, uint y)
        {
            return y * SceneInfo.Size + x;
        }
        
        public void UpdatePixels(bool onlyUpdateChanged)
        {
            for (uint x = 0; x < SceneInfo.Size; x++)
            {
                for (uint y = 0; y < SceneInfo.Size; y++)
                {
                    if (onlyUpdateChanged && !ChangeMap.Data[x, y]) continue;
                    _pixels[4 * Index2dToIndex1d(x, y) + 0] = Tiles.Data[x, y].Color.R;
                    _pixels[4 * Index2dToIndex1d(x, y) + 1] = Tiles.Data[x, y].Color.G;
                    _pixels[4 * Index2dToIndex1d(x, y) + 2] = Tiles.Data[x, y].Color.B;
                    _pixels[4 * Index2dToIndex1d(x, y) + 3] = Tiles.Data[x, y].Color.A;
                    ChangeMap.Data[x, y] = false;
                } 
            }
        }

        public void UpdateTexture(Texture texture)
        {
            texture.Update(_pixels);
        }
    }
}