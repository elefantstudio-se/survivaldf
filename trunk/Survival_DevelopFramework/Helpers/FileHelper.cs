using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Storage;

namespace Survival_DevelopFramework.Helpers
{
    public static class FileHelper
    {
        public static FileStream LoadFileStream(string relativeFileName)
        {
            string fullPath = Path.Combine(
                StorageContainer.TitleLocation, relativeFileName);
            if (File.Exists(fullPath) == false)
                return null;
            else
                return File.Open(fullPath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

    }
}
