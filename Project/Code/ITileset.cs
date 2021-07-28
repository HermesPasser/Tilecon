namespace tilecon
{
    public interface ITileset
    {
        /// <summary>Get the tileset name.</summary>
        /// <returns>Tileset name.</returns>
        string TilesetName();

        /// <summary>Get the sprite size.</summary>
        /// <returns>Sprite size.</returns>
        byte TileSize();

        /// <summary>Get the tileset width.</summary>
        /// <returns>Tileset width.</returns>
        short SizeWidth();

        /// <summary>Get the tileset height.</summary>
        /// <returns>Tileset height.</returns>
        short SizeHeight();

        //TODO: add when move to cs8
        //string Name { get; set; }
        //string ToString() => Name;
    }   
}
