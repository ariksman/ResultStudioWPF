using System.Collections.Generic;

namespace ResultStudioWPF.Application.Interfaces
{
  public interface IFileDialogProvider
  {
    List<string> GetFilePaths(string filter = "");
  }
}