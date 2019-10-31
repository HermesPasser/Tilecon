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
    }   

    namespace Maker
    {
        public class Custom : ITileset
        {
            public Custom() { }

            public Custom(int spriteSize)
            {
                Custom.SPRITE_SIZE = spriteSize;
            }

            public static int SPRITE_SIZE = 0;
            public const string NAME = "Custom";

            public int SizeHeight() { return -1; }
            public int SizeWidth() { return -1; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName(){ return NAME; }
        }

        public class R95 : ITileset
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH = 256;
            public static readonly int SIZE_HEIGHT = 1024;
            public const string NAME = "RPG Maker 95";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class S97 : ITileset
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH = 384;
            public static readonly int SIZE_HEIGHT = 1024;
            public const string NAME = "Sim RPG Maker 97";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class Alpha : ITileset
        {
            public static readonly int SPRITE_SIZE = 16;
            public static readonly int SIZE_WIDTH = 64;
            public static readonly int SIZE_HEIGHT = 128;
            public const string NAME = "RPG Maker Alpha";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        #region R2k classes
        // To that all tilesets below share the same type.

        public abstract class R2k_2k3
        {
            public static readonly int SPRITE_SIZE = 16;
            public static readonly int SIZE_WIDTH = 480;
            public static readonly int SIZE_HEIGHT = 256;
        }

        public class R2k_2k3_AnimObj : R2k_2k3, ITileset
        {
            public const string NAME = "RPG Maker 2000/2003 (Animated Objects)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }

            public string TilesetName() { return NAME; }
        }

        public class R2k_2k3_AB : R2k_2k3, ITileset
        {
            public const string NAME = "RPG Maker 2000/2003 (Tileset A-B)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class R2k_2k3_A : R2k_2k3, ITileset
        {
            public const string NAME = "RPG Maker 2000/2003 (Tileset A)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class R2k_2k3_B : R2k_2k3, ITileset
        {
            public const string NAME = "RPG Maker 2000/2003 (Tileset B)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class R2k_2k3_Auto : R2k_2k3, ITileset
        {
            public const string NAME = "RPG Maker 2000/2003 (Autotiles)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }
        #endregion

        #region XP classes
        // To that all tilesets below share the same type.

        public abstract class XP
        {
            public static readonly int SPRITE_SIZE = 32;
        }

        public class XP_Tile : XP, ITileset
        {
            public static readonly int SIZE_WIDTH = 256;
            public const string NAME = "RPG Maker XP";

            public int SizeHeight() { return -1; }
            public int SizeWidth()  { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }
        
        public class XP_Auto : XP, ITileset
        {
            public static readonly int SIZE_WIDTH = 96;
            public static readonly int SIZE_HEIGHT = 128;
            public const string NAME = "RPG Maker XP (Autotile)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
            
            public class AnimateAuto
            {
                public static readonly int SIZE_WIDTH = 384;
                public static readonly int SIZE_HEIGHT = 128;
            }
        }
        #endregion

        #region VX classes
        // To that all tilesets below share the same type.

        public abstract class VX_Ace
        {
            public static readonly int SPRITE_SIZE = 32;
        }

        public class VX_Ace_A12 : VX_Ace, ITileset
        {
            
            public static readonly int SIZE_WIDTH = 512;
            public static readonly int SIZE_HEIGHT = 384;
            public const string NAME = "RPG Maker VX/Ace (Tileset A1-2)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class VX_Ace_A3 : VX_Ace, ITileset
        {
            public static readonly int SIZE_WIDTH = 512;
            public static readonly int SIZE_HEIGHT = 256;
            public const string NAME = "RPG Maker VX/Ace (Tileset A3)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class VX_Ace_A4 : VX_Ace, ITileset
        {
            public static readonly int SIZE_WIDTH = 512;
            public static readonly int SIZE_HEIGHT = 480;
            public const string NAME = "RPG Maker VX/Ace (Tileset A4)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class VX_Ace_A5 : VX_Ace, ITileset
        {
            public static readonly int SIZE_WIDTH = 256;
            public static readonly int SIZE_HEIGHT = 512;
            public const string NAME = "RPG Maker VX/Ace (Tileset A5)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class VX_Ace_BE : VX_Ace, ITileset
        {
            public static readonly int SIZE_WIDTH = 512;
            public static readonly int SIZE_HEIGHT = 512;
            public const string NAME = "RPG Maker VX/Ace (Tileset B-E)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return SPRITE_SIZE; }
            public string TilesetName() { return NAME; }
        }
        #endregion

        #region MV classes
        // All tilesets below share the same type.

        public abstract class MV
        {
            public static readonly int TILE_SIZE = 48;
        }

        public class MV_Other : MV, ITileset
        {
            public static readonly int SIZE_WIDTH = -1;
            public static readonly int SIZE_HEIGHT = -1;
            public const string NAME = "RPG Maker MV (Others)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class MV_A12 : MV, ITileset
        {
            public static readonly int SIZE_WIDTH = 768;
            public static readonly int SIZE_HEIGHT = 576;
            public const string NAME = "RPG Maker MV (Tileset A1-2)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class MV_A3 : MV, ITileset
        {
            public static readonly int SIZE_WIDTH = 768;
            public static readonly int SIZE_HEIGHT = 384;
            public const string NAME = "RPG Maker MV (Tileset A3)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class MV_A4 : MV, ITileset
        {
            public static readonly int SIZE_WIDTH = 768;
            public static readonly int SIZE_HEIGHT = 720;
            public const string NAME = "RPG Maker MV (Tileset A4)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class MV_A5 : MV, ITileset
        {
            public static readonly int SIZE_WIDTH = 384;
            public static readonly int SIZE_HEIGHT = 768;
            public const string NAME = "RPG Maker MV (Tileset A5)";

            public int SizeHeight() { return SIZE_HEIGHT; }
            public int SizeWidth() { return SIZE_WIDTH; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }

        public class MV_BE : MV, ITileset
        {
            public static readonly int SIZE = 768;
            public const string NAME = "RPG Maker MV (Tileset B-E)";

            public int SizeHeight() { return SIZE; }
            public int SizeWidth() { return SIZE; }
            public int SpriteSize() { return TILE_SIZE; }
            public string TilesetName() { return NAME; }
        }
        #endregion
    }
}
