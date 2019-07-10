
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dinazor.Core.Common.Helper
{
  public  static class AssemblyHelper
    {
        public static List<Assembly> GetAllAssembliesInFolder(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));

            return assemblyFiles.Select(Assembly.LoadFile).ToList();
        }
    }
}
