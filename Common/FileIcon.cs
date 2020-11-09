using System.IO;

namespace TT_Games_Explorer.Common
{
    public static class FileIcon
    {
        public static int FileIconIndex(string fileName)
        {
            //icon assignation (default is the 'Unknown File' icon)
            var imageIndex = 1;

            //go through and attempt icon assignations
            switch (Path.GetExtension(fileName).ToLower())
            {
                //code files
                case ".txt":
                case ".csv":
                case ".sub":
                case ".bms":
                case ".sf":
                case ".scp":
                case ".cfg":
                case ".ini":
                case ".inf":
                case ".vdf":
                case ".gip":
                case ".gix":
                case ".giz":
                case ".gin":
                case ".gil":
                case ".ats":
                    imageIndex = 2;
                    break;

                //archive files
                case ".dat":
                case ".hdr":
                case ".pak":
                case ".pac":
                case ".fpk":
                case ".pkg":
                case ".zip":
                case ".rar":
                case ".tar":
                case ".gz":
                case ".7z":
                case ".arc":
                    imageIndex = 3;
                    break;

                //image files
                case ".tex":
                case ".dds":
                case ".png":
                case ".bmp":
                case ".raw":
                case ".tga":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".giff":
                case ".tif":
                case ".tiff":
                case ".wmp":
                case ".webp":
                    imageIndex = 4;
                    break;

                //executables
                case ".exe":
                case ".dll":
                case ".bat":
                case ".com":
                case ".elf":
                case ".dol":
                case ".cmd":
                case ".sh":
                case ".so":
                case ".ipa":
                case ".app":
                case ".ipsw":
                    imageIndex = 5;
                    break;
            }

            //return image index result
            return imageIndex;
        }
    }
}