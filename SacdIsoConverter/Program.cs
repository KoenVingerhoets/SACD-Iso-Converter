
using System;
using System.Windows.Forms;
namespace SacdIsoConverter;
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
