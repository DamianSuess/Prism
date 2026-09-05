using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SampleApp.Views;

/// <summary>MessageBox Dialog View.</summary>
public partial class MessageBoxView : UserControl
{
    public MessageBoxView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
