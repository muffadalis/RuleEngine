using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wizard.RuleEngine.TestBed.AvalonEdit.Sample;

namespace Wizard.RuleEngine.TestBed
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {
            var win = new Window1();
            win.ShowDialog();
        }
    }
}
