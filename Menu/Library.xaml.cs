using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Dictionary<int, List<Books>> dictBooks { get; set; }

        public Library(Dictionary<int, List<Books>> dict)
        {
            DataContext = this;
            dictBooks = dict;

            InitializeComponent();
        }

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ScrollBar.LineUpCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            if (e.Delta < 0)
            {
                ScrollBar.LineDownCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            e.Handled = true;
        }
    }
}
