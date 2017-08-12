namespace tilecon
{
    static class Vocab
    {
        public enum lang{
            ptbr, eng
        }
        public static string version = "1.8";

        public static string file;
        public static string fileExit;
        public static string convert;
        public static string editor;
        public static string language;
        public static string languageEng;
        public static string languagePtbr;
        public static string helpAbout;
        public static string help;
        public static string aboutHelpText = "2017 - Hermes Passer (hermespasser@gmail.com)";

        public static string btnOpen;
        public static string btnCut;
        public static string btnSave;

        public static string btnConvert;
        public static string btnTransparency;
        public static string cbIgnore;

        public static string btnSetTileset;
        public static string btnOutputTileset;
        public static string btnClearAndSetOutputTileset;

        public static string doneMessage;
        public static string waitMessage;
        public static string SaveErrorMessage;
        public static string errorMessage;

        public static string mode;
        
        public static string comboTopLeft;
        public static string comboTopCenter;
        public static string comboTopRight;
        public static string comboMiddleLeft;
        public static string comboMiddleCenter;
        public static string comboMiddleRight;
        public static string comboBottomLeft;
        public static string comboBottomCenter;
        public static string comboBottomRight;
        public static string comboResize;
        
        public static string groupConversion;

        public static string imageFilesText;
        public static string pgnFilesText;

        public static void changeLang(lang l)
        {
            if (l == lang.eng)
            {
                file = "File";
                fileExit = "Exit";
                convert = "Converter";
                editor = "Editor";
                language = "Language";
                languageEng = "English";
                languagePtbr = "Portuguese";
                helpAbout = "About";
                help = "Help";

                btnOpen = "Open Tileset";
                btnCut = "Save each sprite";
                btnConvert = "Convert";
                btnSave = "Salve";
                btnTransparency = "Set Transparency";
                cbIgnore = "Ignore Alpha";
                btnSetTileset = "Set Tileset";
                btnOutputTileset = "Output Tileset";
                btnClearAndSetOutputTileset = "Clear and Set Tileset";

                comboTopLeft =      "Top Left Align";
                comboTopCenter =    "Top Center Align";
                comboTopRight =     "Top Right Align";
                comboMiddleLeft =   "Middle Left Align";
                comboMiddleCenter = "Middle Center Align";
                comboMiddleRight =  "Middle Right Align";
                comboBottomLeft =   "Bottom Left Align";
                comboBottomCenter = "Bottom Center Align";
                comboBottomRight =  "Bottom Right Align";
                comboResize =       "Resize";

                waitMessage = "Wait...";
                doneMessage = "Done.";
                errorMessage = "Width or height does not match the size of the selected tileset.";
                SaveErrorMessage = "You cannot save over the image loaded in the program.";

                groupConversion = "Conversion";

                imageFilesText = "Image files";
                pgnFilesText = "Png Files";

                mode = "Mode";
            }
            else
            {
                file = "Arquivo";
                fileExit = "Sair";
                convert = "Conversor";
                editor = "Editor";
                language = "Linguagem";
                languageEng = "Inglês";
                languagePtbr = "Português";
                helpAbout = "Ajuda";
                help = "Sobre";

                btnOpen = "Abrir Tileset";
                btnCut = "Salvar cada sprite";
                btnConvert = "Converter";
                btnSave = "Salvar";
                btnTransparency = "Definir Transparência";
                cbIgnore = "Ignorar Alfa";
                btnSetTileset = "Definir Tileset";
                btnOutputTileset = "Tileset de Saída";
                btnClearAndSetOutputTileset = "Limpar e Definir Tileset";

                comboTopLeft =      "Alinhamento Superior Esquerdo";
                comboTopCenter =    "Alinhamento Superior Centro";
                comboTopRight =     "Alinhamento Superior Direito";
                comboMiddleLeft =   "Alinhamento Médio Esquerdo";
                comboMiddleCenter = "Alinhamento Médio Centro";
                comboMiddleRight =  "Alinhamento Médio Direito";
                comboBottomLeft =   "Alinhamento Inferior Esquerdo";
                comboBottomCenter = "Alinhamento Inferior Centro";
                comboBottomRight =  "Alinhamento Inferior Direito";
                comboResize =       "Redimencionar";

                waitMessage = "Espere...";
                doneMessage = "Feito.";
                errorMessage = "Largura ou altura não corresponde ao tamanho do conjunto de telas selecionado.";
                SaveErrorMessage = "Você não pode salvar por cima da imagem carregada no programa.";

                groupConversion = "Conversão";

                imageFilesText = "Arquivo de Imagem";
                pgnFilesText = "Arquivos Png";

                mode = "Modo";
            }
        }
    }
}
