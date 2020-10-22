using System.IO;
using System.Text;

namespace TT_Games_Explorer.Formats.GHG.Structure
{
    public class GhgMtlSettings
    {
        public const string KAVALUE = @"Ka 0.99609375 0.99609375 0.99609375";
        public const string KDVALUE = @"Kd 0.99609375 0.99609375 0.99609375";

        /// <summary>
        /// Writes out a material settings file (*.mtl)
        /// </summary>
        /// <param name="fullModelFilePath"></param>
        /// <returns></returns>
        public static string MtlFileContent(string fullModelFilePath)
        {
            try
            {
                //make sure a path was specified
                if (!string.IsNullOrEmpty(fullModelFilePath))
                {
                    //no point describing a model file that doesn't exist
                    if (File.Exists(fullModelFilePath))
                    {
                        //where the file content is going to be held
                        var sb = new StringBuilder();

                        //build the file
                        sb.AppendLine("newmtl " + Path.GetFileNameWithoutExtension(fullModelFilePath));
                        sb.AppendLine($"    {KAVALUE}");
                        sb.AppendLine($"    {KDVALUE}");
                        sb.AppendLine("    map_Ka " + Path.GetFullPath(fullModelFilePath));
                        sb.AppendLine("    map_Kd " + Path.GetFullPath(fullModelFilePath));
                        sb.AppendLine();

                        //return the final result
                        return sb.ToString();
                    }
                }
            }
            catch
            {
                //ignore error
            }

            //default
            return @"";
        }
    }
}