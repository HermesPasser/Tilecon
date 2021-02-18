#pragma warning disable CS1591
namespace tilecon
{
    public interface ITileset
    {
        /// <summary>Get the tileset name.</summary>
        /// <returns>Tileset name.</returns>
        string TilesetName();

        /// <summary>Get the sprite size.</summary>
        /// <returns>Sprite size.</returns>
        int SpriteSize();

        /// <summary>Get the tileset width.</summary>
        /// <returns>Tileset width.</returns>
        int SizeWidth();

        /// <summary>Get the tileset height.</summary>
        /// <returns>Tileset height.</returns>
        int SizeHeight();

        //TODO: add when move to cs8
        //string Name { get; set; }
        //string ToString() => Name;
    }   
}
