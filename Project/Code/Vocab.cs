namespace tilecon
{
    static class Vocab
    {
        public enum lang{
            ptbr, eng
        }
        public static string version = "1.5";

        public static string file;
        public static string archiveExit;
        public static string convert;
        public static string language;
        public static string languageEng;
        public static string languagePtbr;
        public static string help;
        public static string helpAbout;
        public static string aboutHelpText = "2017 - Hermes Passer\nGitHub: HermesPasser/Tilecon";

        public static string btnOpen;
        public static string btnCut;
        public static string btnConvert;
        public static string btnSave;
        public static string btnTransparency;
        public static string cbIgnore;

        public static string doneMessage;
        public static string waitMessage;
        public static string SaveErrorMessage;
        public static string errorMessage;

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
                btnTransparency = "Set Transparent Pixel";
                cbIgnore = "Ignore Alpha";

                comboNone = "None";
                comboCentralize = "Centralize";
                comboResize = "Resize";

                waitMessage = "Wait...";
                doneMessage = "Done.";
                errorMessage = "Width or height does not match the size of the selected tileset.";
                SaveErrorMessage = "You cannot save over the image loaded in the program.";

                groupConversion = "Conversion";
                groupUtilities = "Utilities";

                imageFilesText = "Image files";
                pgnFilesText = "Png Files";

                mode = "Mode";
            }
            else
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
                btnTransparency = "Definir Pixel Transparente";
                cbIgnore = "Ignorar Alfa";

                comboNone = "Nada";
                comboCentralize = "Centralizar";
                comboResize = "Redimencionar";

                waitMessage = "Espere...";
                doneMessage = "Feito.";
                errorMessage = "Largura ou altura não corresponde ao tamanho do conjunto de telas selecionado.";
                SaveErrorMessage = "Você não pode salvar por cima da imagem carregada no programa.";

                groupConversion = "Conversão";
                groupUtilities = "Utilidades";

                imageFilesText = "Arquivo de Imagem";
                pgnFilesText = "Arquivos Png";

                mode = "Modo";
            }
        }
    }
}
