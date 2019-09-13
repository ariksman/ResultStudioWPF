using System.Collections.Generic;
using System.Linq;
using Ookii.Dialogs.Wpf;
using ResultStudioWPF.Application.Interfaces;

namespace ResultStudioWPF.ViewModels.Services
{
  public class FileDialogProvider : IFileDialogProvider
  {
    public List<string> GetFilePaths(string filter = "")
    {
      var title = "Select Files";
      if (VistaFileDialog.IsVistaFileDialogSupported)
      {

        var fileDialog = new VistaOpenFileDialog
        {
          Multiselect = false,
          Title = title,
          CheckFileExists = true,
          CheckPathExists = true,
          ReadOnlyChecked = true,
          RestoreDirectory = true,
          Filter = filter,
        };

        return fileDialog.ShowDialog() == true ? fileDialog.FileNames.ToList() : new List<string>();
      }
      else
      {
        var ofd = new Microsoft.Win32.OpenFileDialog()
        {
          Title = title,
          CheckFileExists = false,
          RestoreDirectory = true,
          Filter = filter
        };

        if (ofd.ShowDialog() == true)
          return ofd.FileNames.ToList();
      }

      return null;
    }
  }
}