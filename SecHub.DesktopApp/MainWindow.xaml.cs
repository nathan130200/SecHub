using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SecHub.DesktopApp;

public partial class MainWindow : MetroWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnFunaoNaoImplementata_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        this.ShowMessageAsync("⚠ AVISO", "FUNÇÂO NÃO IMPLEMENTADA", MessageDialogStyle.Affirmative);
    }
}