using Prism.Commands;
using Prism.Dialogs;
using SampleApp.Views;

namespace SampleApp.ViewModels;

public class DialogSampleViewModel : ViewModelBase
{
    private readonly IDialogService _dialog;

    private string _msgBoxTitle = string.Empty;
    private string _msgBoxMessage = string.Empty;
    private string _msgBoxResult = string.Empty;

    public DialogSampleViewModel(IDialogService dialog)
    {
        _dialog = dialog;

        MsgBoxTitle = "Sample Dialog";
        MsgBoxMessage = "Hello from the DialogSampleViewModel!";
        Title = "Dialog Sample";
    }

    public DelegateCommand CmdShowMsgBox => new(() =>
    {
        var parameters = new DialogParameters
        {
            { "title", MsgBoxTitle  },
            { "message", MsgBoxMessage },
        };

        _dialog.ShowDialog(nameof(MessageBoxView), parameters, result =>
        {
            if (result.Result == ButtonResult.OK)
                MsgBoxResult = "OK";
            else if (result.Result == ButtonResult.Cancel)
                MsgBoxResult = "Cancel";
        });
    });

    public string MsgBoxMessage { get => _msgBoxMessage; set => SetProperty(ref _msgBoxMessage, value); }

    public string MsgBoxResult { get => _msgBoxResult; set => SetProperty(ref _msgBoxResult, value); }

    public string MsgBoxTitle { get => _msgBoxTitle; set => SetProperty(ref _msgBoxTitle, value); }
}
