//RPG Maker's dimensions

namespace tilecon
{
    public static class Maker
    {
        public enum Tileset
        {
            R95, S97, Alpha, R2000_2003_Auto, R2000_2003_A, R2000_2003_B, R2000_2003_AB, XP, XP_Auto,
            VX_Ace_A12, VX_Ace_A3, VX_Ace_A4, VX_Ace_A5, VX_BE_Ace_BC,
            MV_A12, MV_A3, MV_A4, MV_A5, MV_BC
        }

        public static bool Is2K_2K3(Tileset ver)
        {
            if (ver == Tileset.R2000_2003_A || ver == Tileset.R2000_2003_B || ver == Tileset.R2000_2003_AB || ver == Tileset.R2000_2003_Auto)
                return true;
            return false;
        }

        public static bool IsVX_Ace(Tileset ver)
        {
            if (ver == Tileset.VX_Ace_A12 || ver == Tileset.VX_Ace_A3 || ver == Tileset.VX_Ace_A4 || ver == Tileset.VX_BE_Ace_BC)
                return true;
            return false;
        }

        public static bool IsMV(Tileset ver)
        {
            if (ver == Tileset.MV_A12 || ver == Tileset.MV_A3 || ver == Tileset.MV_A4 || ver == Tileset.MV_A5 || ver == Tileset.MV_BC)
                return true;
            return false;
        }

        public static int GetSpriteSize(Tileset maker)
        {
            if (Is2K_2K3(maker))
                return R2000_2003.SPRITE_SIZE;
            else if (IsVX_Ace(maker))
                return VX_Ace.SPRITE_SIZE;

            switch (maker)
            {
                case Tileset.R95:
                    return R95.SPRITE_SIZE;
                case Tileset.S97:
                    return S97.SPRITE_SIZE;
                case Tileset.Alpha:
                    return Alpha.SPRITE_SIZE;
                case Tileset.XP:
                default:
                    return XP.SPRITE_SIZE;
            }
        }

        public static int GetSizeWidth(Tileset maker)
        {
            if (Is2K_2K3(maker))
                return R2000_2003.SIZE_WIDTH;

            switch (maker)
            {
                case Tileset.R95:
                    return R95.SIZE_WIDTH;
                case Tileset.S97:
                    return S97.SIZE_WIDTH;
                case Tileset.Alpha:
                    return Alpha.SIZE_WIDTH;
                case Tileset.VX_Ace_A12:
                    return VX_Ace.A12.SIZE_WIDTH;
                case Tileset.VX_Ace_A3:
                    return VX_Ace.A3.SIZE_WIDTH;
                case Tileset.VX_Ace_A4:
                    return VX_Ace.A4.SIZE_WIDTH;
                case Tileset.VX_Ace_A5:
                    return VX_Ace.A5.SIZE_WIDTH;
                case Tileset.VX_BE_Ace_BC:
                    return VX_Ace.BE.SIZE_WIDTH;
                case Tileset.XP:
                default:
                    return XP.SIZE_WIDTH;
            }
        }

        public static int GetSizeHeight(Tileset maker)
        {
            if (Is2K_2K3(maker))
                return R2000_2003.SIZE_HEIGHT;

            switch (maker)
            {
                case Tileset.R95:
                    return R95.SIZE_HEIGHT;
                case Tileset.S97:
                    return S97.SIZE_HEIGHT;
                case Tileset.Alpha:
                    return Alpha.SIZE_HEIGHT;
                case Tileset.VX_Ace_A12:
                    return VX_Ace.A12.SIZE_HEIGHT;
                case Tileset.VX_Ace_A3:
                    return VX_Ace.A3.SIZE_HEIGHT;
                case Tileset.VX_Ace_A4:
                    return VX_Ace.A4.SIZE_HEIGHT;
                case Tileset.VX_Ace_A5:
                    return VX_Ace.A5.SIZE_HEIGHT;
                case Tileset.VX_BE_Ace_BC:
                    return VX_Ace.BE.SIZE_WIDTH;
                case Tileset.R2000_2003_Auto:
                case Tileset.XP:
                default:
                    return -1;
            }
        }

        public static class R95
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH  = 256;
            public static readonly int SIZE_HEIGHT = 1024;
        }

        public static class S97
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH  = 384;
            public static readonly int SIZE_HEIGHT = 1024;
        }
     
        public static class Alpha
        {
            public static readonly int SPRITE_SIZE = 16;
            public static readonly int SIZE_WIDTH  = 64;
            public static readonly int SIZE_HEIGHT = 128;
        }

        public static class R2000_2003
        { 
            public static readonly int SPRITE_SIZE = 16;
            public static readonly int SIZE_WIDTH  = 480;
            public static readonly int SIZE_HEIGHT = 256;
        }

        public static class XP
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH  = 256;

            public static class Auto
            {
                public static readonly int SIZE_WIDTH = 96;
                public static readonly int SIZE_HEIGHT = 128;
            }

            public static class AnimateAuto
            {
                public static readonly int SIZE_WIDTH = 384;
                public static readonly int SIZE_HEIGHT = 128;
            }
        }

        public static class VX_Ace
        {
            public static readonly int SPRITE_SIZE = 32;

            public static class A12
            {
                public static readonly int SIZE_WIDTH = 512;
                public static readonly int SIZE_HEIGHT = 384;
            }

            public static class A3
            {
                public static readonly int SIZE_WIDTH = 512;
                public static readonly int SIZE_HEIGHT = 256;
            }

            public static class A4
            {
                public static readonly int SIZE_WIDTH = 512;
                public static readonly int SIZE_HEIGHT = 480;
            }

            public static class A5
            {
                public static readonly bool VXOnly = false;
                public static readonly int SIZE_WIDTH = 256;
                public static readonly int SIZE_HEIGHT = 512;
            }

            public static class BE
            {
                public static readonly bool VXOnly = false;
                public static readonly int SIZE_WIDTH  = 512;
                public static readonly int SIZE_HEIGHT = 512;
            }
        }
       
        public static class MV
        {
            public static readonly int SPRITE_SIZE = 48;

            public static class A12
            {
                public static readonly int SIZE_WIDTH  = 768;
                public static readonly int SIZE_HEIGHT = 576;
            }

            public static class A3
            {
                public static readonly int SIZE_WIDTH = 768;
                public static readonly int SIZE_HEIGHT = 384;
            }

            public static class A4
            {
                public static readonly int SIZE_WIDTH = 768;
                public static readonly int SIZE_HEIGHT = 720;
            }

            public static class A5
            {
                public static readonly int SIZE_WIDTH = 384;
                public static readonly int SIZE_HEIGHT = 768;
            }

            public static class BE
            {
                public static readonly int SIZE = 768;
            }
        }
    }
}
