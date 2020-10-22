using System.IO;

namespace TT_Games_Explorer.Formats.GHG.Structure
{
    public class GhgModelFile
    {
        public byte[] ModelData { get; set; }
        public string FileName { get; set; }
        public string ParentName { get; set; }

        /// <summary>
        /// If the data contained in this model is valid, write out the model to its specified FileName
        /// </summary>
        /// <returns>True on successful write; false on failure.</returns>
        public bool WriteOut()
        {
            try
            {
                //validate needed information
                if (ModelData != null && !string.IsNullOrEmpty(FileName))
                {
                    //write model bytes to the file
                    File.WriteAllBytes(FileName, ModelData);

                    //write was successful
                    return true;
                }
            }
            catch
            {
                //ignore all errors
            }

            //default
            return false;
        }

        /// <summary>
        /// Some models will have sub-parts that are split versions of the root model; e.g. ModelName_000001; ModelName_000002
        /// </summary>
        public GhgModelFile[] SubParts { get; set; } = { };
    }
}