namespace tilecon.Conversor
{
    static class Maker
    {
        //public static class 2000 { }
        //public static class 2003 { }

        public static class XP
        {
            public static readonly int SPRITE_SIZE = 32;
        }

        //public static class VX { }
        //public static class VXACE { }

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

    //ACE

    //A1 and A2 are 512 x 384 pixels

    //A3 is 512 x 256 pixels

    //A4 is 512 x 480 pixels

    //A5 is 256 x 512 pixels

    //B-E Tilesets are 512 x 512 pixels.
}
