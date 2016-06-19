using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace memory.Views
{
    /// <summary>
    /// Logique d'interaction pour ElementGrilleView.xaml
    /// </summary>
    public partial class ElementGrilleView : UserControl
    {
        
        public bool IsImageVisible;

        public int identifiant;

        public ElementGrilleView(System.Windows.Controls.Image thumb , int id)
        {
            InitializeComponent();
            this.identifiant = id;
            Check.IsChecked = true;
            IsImageVisible = false;
            Check.Content = thumb;
        }

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            IsImageVisible = false;
        }

        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            IsImageVisible = true;
        }
    }
}
