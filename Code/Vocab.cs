using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tilecon
{
    static class Vocab
    {
        public enum lang{
            ptbr, eng
        }
        public static string version = "0.5";

        public static string archiche;
        public static string archiveExit;
        public static string convert;
        public static string language;
        public static string languageEng;
        public static string languagePtbr;
        public static string help;
        public static string helpAbout;
        public static string aboutHelpText = "2017 - Hermes Passer (gladiocitrico.blogspot.com)\nGitHub: HermesPasser/Tilecon";

        public static string btnSearch;
        public static string btnCut;
        public static string btnConvert;

        public static string doneMessage;

        public static string imageFilesText;
        public static string pgnFilesText;

        public static void changeLang(lang l)
        {
            if (l == lang.eng)
            {
                archiche = "Archive";
                archiveExit = "Exit";
                convert = "Convert";
                language = "Language";
                languageEng = "English";
                languagePtbr = "Portuguese";
                help = "About";
                helpAbout = "Help";

                btnSearch = "Search Tileset";
                btnCut = "Cut/save each sprite";
                btnConvert = "Convert and Save";

                doneMessage = "Done.";

                imageFilesText = "Image files";
                pgnFilesText = "Png Files";
            } else
            {
                archiche = "Arquivo";
                archiveExit = "Sair";
                convert = "Converter";
                language = "Linguagem";
                languageEng = "Inglês";
                languagePtbr = "Português";
                help = "Ajuda";
                helpAbout = "Sobre";

                btnSearch = "Procurar Tileset";
                btnCut = "Cortar/salvar cada sprite";
                btnConvert = "Converter e Salvar";

                doneMessage = "Feito.";

                imageFilesText = "Arquivo de Imagem";
                pgnFilesText = "Arquivos Png";
            }
        }
    }
}
