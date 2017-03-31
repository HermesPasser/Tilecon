namespace tilecon
{
    static class Vocab
    {
        public enum lang{
            ptbr, eng
        }
        public static string version = "1.3";

        public static string file;
        public static string archiveExit;
        public static string convert;
        public static string language;
        public static string languageEng;
        public static string languagePtbr;
        public static string help;
        public static string helpAbout;
        public static string aboutHelpText = "2017 - Hermes Passer (gladiocitrico.blogspot.com)\nGitHub: HermesPasser/Tilecon";

        public static string btnOpen;
        public static string btnCut;
        public static string btnConvert;
        public static string btnSave;
        public static string cbIgnore;

        public static string r2kMessageConvert;
        public static string r2kMessageCut;
        public static string doneMessage;
        public static string waitMessage;
        public static string[] errorMessage = new string[3]; 

        public static string mode;

        public static string comboNone;
        public static string comboResize;
        public static string comboCentralize;
        
        public static string groupConversion;
        public static string groupUtilities;

        public static string imageFilesText;
        public static string pgnFilesText;

        public static void changeLang(lang l)
        {
            if (l == lang.eng)
            {
                file = "File";
                archiveExit = "Exit";
                convert = "Convert";
                language = "Language";
                languageEng = "English";
                languagePtbr = "Portuguese";
                help = "About";
                helpAbout = "Help";

                btnOpen = "Open Tileset";
                btnCut = "Cut/save each sprite";
                btnConvert = "Convert";
                btnSave = "Salve";
                cbIgnore = "Ignore Alpha";

                comboNone = "None";
                comboCentralize = "Centralize";
                comboResize = "Resize";

                r2kMessageCut = "The autotiles will also be saved.";
                r2kMessageConvert = "The sprites that represent the \"autotile\" are disregarded.";
                waitMessage = "Wait...";
                doneMessage = "Done.";
                errorMessage[0] = "The height of this image is different from the height for RM97 tilesets!";
                errorMessage[1] = "Height too big!";
                errorMessage[2] = "The width of this image is only convertible to Sim RM97 tilesets!";

                groupConversion = "Conversion";
                groupUtilities = "Utilities";

                imageFilesText = "Image files";
                pgnFilesText = "Png Files";

                mode = "Mode";
            } else
            {
                file = "Arquivo";
                archiveExit = "Sair";
                convert = "Converter";
                language = "Linguagem";
                languageEng = "Inglês";
                languagePtbr = "Português";
                help = "Ajuda";
                helpAbout = "Sobre";

                btnOpen = "Abrir Tileset";
                btnCut = "Cortar/salvar cada sprite";
                btnConvert = "Converter";
                btnSave = "Salvar";
                cbIgnore = "Ignorar Alfa";

                comboNone = "Nada";
                comboCentralize = "Centralizar";
                comboResize = "Redimencionar";

                r2kMessageCut = "Os autotiles também serão salvos.";
                r2kMessageConvert = "Os sprites que representam um \"autotile\" serão desconsiderados.";
                waitMessage = "Espere...";
                doneMessage = "Feito.";
                errorMessage[0] = "";
                errorMessage[1] = "Altura grande demais!";
                errorMessage[2] = "A largura desta imagem somente é convertivél para tilesets do Sim RM97!";

                groupConversion = "Conversão";
                groupUtilities = "Utilidades";

                imageFilesText = "Arquivo de Imagem";
                pgnFilesText = "Arquivos Png";

                mode = "Modo";
            }
        }
    }
}
