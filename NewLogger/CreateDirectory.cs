using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NewLogger
{
    public class CreateDirectory
    {
        private string CurrenDirectory { get; set; }

        private string SubDirectory { get; set; }

        DateTime dt = DateTime.Now;

        public CreateDirectory()
        {
            this.CurrenDirectory = $"{Directory.GetCurrentDirectory()}/Logs/";
            this.SubDirectory = dt.ToShortDateString();

            DirectoryInfo dirInfo = new DirectoryInfo(CurrenDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(SubDirectory);

        }

        public string GetFolderPath()
        {
            return $"{SubDirectory}";
        }
    }
}
