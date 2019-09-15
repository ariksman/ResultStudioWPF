using System;
using System.Windows;
using Ookii.Dialogs.Wpf;
using ResultStudioWPF.Application.Interfaces;

namespace ResultStudioWPF.ViewModels.Services
{
  public class OokiiMessageDialogService : IMessageDialogService
  {
    public void ShowErrorMessage(string sender, string message, string details)
    {
      if (TaskDialog.OSSupportsTaskDialogs)
      {
        using (var dialog = new Ookii.Dialogs.Wpf.TaskDialog())
        {
          var okButton = new TaskDialogButton(ButtonType.Ok);
          dialog.Buttons.Add(okButton);
          dialog.WindowTitle = $"Error from: {sender}";
          dialog.MainInstruction = message;
          dialog.ExpandedInformation = details;
          dialog.FooterIcon = TaskDialogIcon.Information;
          dialog.MainIcon = TaskDialogIcon.Error;
          dialog.ShowDialog();
        }
      }
      else
      {
        MessageBox.Show(message, details);
      }
    }

    public void ShowWarningMessage(string sender, string message, string details)
    {
      var dialog = new Ookii.Dialogs.Wpf.TaskDialog
      {
        Buttons = { new TaskDialogButton(ButtonType.Ok) },
        WindowTitle = message,
        Content = details,
        MainIcon = TaskDialogIcon.Warning,
      };
      dialog.ShowDialog();
    }

    public void ShowUserMessage(string sender, string message, Exception ex)
    {
      var dialog = new Ookii.Dialogs.Wpf.TaskDialog
      {
        Buttons = { new TaskDialogButton(ButtonType.Ok) },
        WindowTitle = "An error occured while removing the selected item.",
        Content = ex.ToString(),
        MainIcon = TaskDialogIcon.Error,
      };
      dialog.ShowDialog();
    }

    public bool Confirm(string sender, string message, string details)
    {
      var dialog = new Ookii.Dialogs.Wpf.TaskDialog
      {
        Buttons = { new TaskDialogButton(ButtonType.Yes), new TaskDialogButton(ButtonType.No) },
        WindowTitle = message,
        Content = details,
        MainIcon = TaskDialogIcon.Information,
      };
      var result = dialog.ShowDialog();

      switch (result.ButtonType)
      {
        case ButtonType.Yes:
          return true;
        case ButtonType.No:
          return false;
        default:
          return false;
      }
    }

    public void ShowException(string sender, Exception ex)
    {
      if (TaskDialog.OSSupportsTaskDialogs)
      {
        using (var dialog = new Ookii.Dialogs.Wpf.TaskDialog())
        {
          var okButton = new TaskDialogButton(ButtonType.Ok);
          dialog.Buttons.Add(okButton);
          dialog.WindowTitle = $"Application has encoutered unexpected error and will be closed";
          dialog.ExpandedInformation = $"Unhandled exception occurred on: {sender}";
          dialog.MainInstruction = "Unhandled exception occurred on the application";
          dialog.FooterIcon = TaskDialogIcon.Information;
          dialog.MainIcon = TaskDialogIcon.Error;
          dialog.Footer = ex.Message;
          dialog.ShowDialog();
        }
      }
    }
  }
}