
namespace tilecon.Tileset
{
    public class Tileset : ITileset
    {
        public readonly byte Size;
        public readonly short Width;
        public readonly short Height;
        public readonly string Name;

        private Tileset(byte spriteSize, short width, short height, string name)
        {
            Name = name;
            Width = width;
            Height = height;
            Size = spriteSize;
        }

        public override string ToString() => Name;
        public int SizeWidth() => Width;
        public int SizeHeight() => Height;
        public int SpriteSize() => Size;
        public string TilesetName() => Name;

        public static Tileset Custom(byte s) => new Tileset(s, -1, -1, "Custom");

        public static readonly Tileset R95 = new Tileset(32, 256, 1024, "RPG Maker 95");
        public static readonly Tileset S97 = new Tileset(32, 384, 1024, "Sim RPG Maker 97");
        public static readonly Tileset Alpha = new Tileset(16, 64, 128, "RPG Maker Alpha");

        public static readonly Tileset R2k_2k3_AnimObj = new Tileset(16, 480, 256, "RPG Maker 2000/2003 (Animated Objects)");
        public static readonly Tileset R2k_2k3_AB = new Tileset(16, 480, 256, "RPG Maker 2000/2003 (Tileset A-B)");
        public static readonly Tileset R2k_2k3_A = new Tileset(16, 480, 256, "RPG Maker 2000/2003 (Tileset A)");
        public static readonly Tileset R2k_2k3_B = new Tileset(16, 480, 256, "RPG Maker 2000/2003 (Tileset B)");
        public static readonly Tileset R2k_2k3_Auto = new Tileset(16, 480, 256, "RPG Maker 2000/2003 (Autotiles)");

        public static readonly Tileset XP_Tile = new Tileset(32, 256, -1, "RPG Maker XP");
        public static readonly Tileset XP_Auto = new Tileset(32, 96, 128, "RPG Maker XP (Autotile)");
        public static readonly Tileset XP_AnimatedAuto = new Tileset(32, 384, 128, "RPG Maker XP (Animated Autotile)");

        public static readonly Tileset VX_Ace_A12 = new Tileset(32, 512, 384, "RPG Maker VX/Ace (Tileset A1-2)");
        public static readonly Tileset VX_Ace_A3 = new Tileset(32, 512, 256, "RPG Maker VX/Ace (Tileset A3)");
        public static readonly Tileset VX_Ace_A4 = new Tileset(32, 512, 480, "RPG Maker VX/Ace (Tileset A4)");
        public static readonly Tileset VX_Ace_A5 = new Tileset(32, 256, 512, "RPG Maker VX/Ace (Tileset A5)");
        public static readonly Tileset VX_Ace_BE = new Tileset(32, 512, 512, "RPG Maker VX/Ace (Tileset B-E)");

        public static readonly Tileset MV_A12 = new Tileset(48, 768, 576, "RPG Maker MV (Tileset A1-2)");
        public static readonly Tileset MV_A3 = new Tileset(48, 768, 384, "RPG Maker MV (Tileset A3)");
        public static readonly Tileset MV_A4 = new Tileset(48, 768, 720, "RPG Maker MV (Tileset A4)");
        public static readonly Tileset MV_A5 = new Tileset(48, 384, 768, "RPG Maker MV (Tileset A5)");
        public static readonly Tileset MV_BE = new Tileset(48, 768, 768, "RPG Maker MV (Tileset B-E)");
        public static readonly Tileset MV_Other = new Tileset(48, -1, -1, "RPG Maker MV (Others)");

        public static bool IsR2k_2k3(Tileset t)
        {
            return t == R2k_2k3_A
                || t == R2k_2k3_B
                || t == R2k_2k3_AB
                || t == R2k_2k3_Auto
                || t == R2k_2k3_AnimObj;
        }

        public static bool IsMV(Tileset t)
        {
            return t == MV_A12
                || t == MV_A3
                || t == MV_A4
                || t == MV_A5
                || t == MV_BE
                || t == MV_Other;
        }
    }
}
