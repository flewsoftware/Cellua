
using SFML.Graphics;

namespace Cellua.Storage
{
    /// <summary>
    /// Smallest unit inside a simulation
    /// </summary>
    public struct Tile
    {
        /// <summary>
        /// A ID that more than one tile will share
        /// </summary>
        public uint TypeId;
        
        public Color Color;
    }
}