using System;
using System.Text;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace SampleApp.ViewModels;

public class MessageBoxViewModel : BindableBase, IDialogAware
{
    private string _customMessage = string.Empty;
    private int _maxHeight;
    private int _maxWidth;
    private string _title = "Notification";

    public MessageBoxViewModel()
    {
        Title = "Alert!";

        MaxHeight = 800;
        MaxWidth = 600;
    }

    public DelegateCommand<string> CmdResult => new DelegateCommand<string>((param) =>
    {
        // None = 0
        // OK = 1
        // Cancel = 2
        // Abort = 3
        // Retry = 4
        // Ignore = 5
        // Yes = 6
        // No = 7
        ButtonResult result = ButtonResult.None;

        if (int.TryParse(param, out int intResult))
            result = (ButtonResult)intResult;

        RequestClose.Invoke(result);
    });

    public string CustomMessage { get => _customMessage; set => SetProperty(ref _customMessage, value); }

    public int MaxHeight { get => _maxHeight; set => SetProperty(ref _maxHeight, value); }

    public int MaxWidth { get => _maxWidth; set => SetProperty(ref _maxWidth, value); }

    public DialogCloseListener RequestClose { get; }

    public string Title { get => _title; set => SetProperty(ref _title, value); }

    /// <summary>Allows the dialog to close.</summary>
    /// <returns>Return true to allow closing (i.e. OK button only).</returns>
    public virtual bool CanCloseDialog() => true;

    public virtual void OnDialogClosed()
    {
        // Detach custom event handlers here, etc.
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        var title = parameters.GetValue<string>("title");
        if (!string.IsNullOrEmpty(title))
            Title = title;

        CustomMessage = parameters.GetValue<string>("message");
    }
}
