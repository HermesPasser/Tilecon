//RPG Maker's dimensions

namespace tilecon.Conversor
{
    static class Maker
    {
        public enum version
        {
            R95, S97, XP
        }

        public static int GetSpriteSize(version maker)
        {
            switch (maker)
            {
                case version.R95:
                    return R95.SPRITE_SIZE;
                case version.S97:
                    return S97.SPRITE_SIZE;
                case version.XP:
                    return XP.SPRITE_SIZE;
                default:
                    return XP.SPRITE_SIZE;
            }
        }

        public static int GetSizeWidth(version maker)
        {
            switch (maker)
            {
                case version.R95:
                    return R95.SIZE_WIDTH;
                case version.S97:
                    return S97.SIZE_WIDTH;
                case version.XP:
                    return XP.SIZE_WIDTH;
                default:
                    return XP.SIZE_WIDTH;
            }
        }

        public static int GetSizeHeight(version maker)
        {
            switch (maker)
            {
                case version.R95:
                    return R95.SIZE_HEIGHT;
                case version.S97:
                    return S97.SIZE_HEIGHT;
                case version.XP:
                    return -1;
                default:
                    return -1;
            }
        }

        public static class R95
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH = 256;
            public static readonly int SIZE_HEIGHT = 1024;
        }

        public static class S97
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH = 384;
            public static readonly int SIZE_HEIGHT = 1024;
        }

        //public static class R2000 { }
        //public static class R2003 { }

        public static class XP
        {
            public static readonly int SPRITE_SIZE = 32;
            public static readonly int SIZE_WIDTH = 256;
        }

        //public static class VX { }
        //public static class VXACE { }

        public static class VXAce
        {
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
                public static readonly int SIZE_WIDTH = 512;
                public static readonly int SIZE_HEIGHT = 512;
            }

            public static class BE
            {
                public static readonly int SIZE_WIDTH = 512;
                public static readonly int SIZE_HEIGHT = 512;
            }
        }

        public static class MV
        {
            public static readonly int SPRITE_SIZE = 48;

            public static class A12
            {
                public static readonly int SIZE_WIDTH = 768;
                public static readonly int SIZE_HEIGHT = 576;
            }

            public static class A3
            {
                public static readonly int SIZE_WIDTH = 768;
                public static readonly int SIZE_HEIGHT = 384;
            }

            public static class A4
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
