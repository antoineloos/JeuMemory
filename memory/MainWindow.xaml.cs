using MaterialDesignThemes.Wpf;
using memory.Views;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace memory
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ElementGrilleView> ListImage;
        private Random randService;
        private DispatcherTimer mainTimer;
        private DispatcherTimer retournerCarteTimer;
        private DispatcherTimer supprCarte;
        public MainWindow()
        {
            InitializeComponent();
            randService = new Random();
            ListImage = new List<ElementGrilleView>();
            supprCarte = new DispatcherTimer();
            supprCarte.Tick += supprCarte_Tick;
            supprCarte.Interval = new TimeSpan(0, 0, 0, 1);
            retournerCarteTimer = new DispatcherTimer();
            retournerCarteTimer.Tick +=retournerCarteTimer_Tick;
            retournerCarteTimer.Interval = new TimeSpan(0, 0, 0, 1);
            mainTimer = new DispatcherTimer();
            mainTimer.Tick +=mainTimer_Tick;
            mainTimer.Interval = new TimeSpan(0, 0, 0, 1);
            PaletteHelper pltHelper = new PaletteHelper();
            pltHelper.ReplaceAccentColor("amber");
            pltHelper.ReplacePrimaryColor("blue");
            
            
            


            ResetGame();
            
        }


        private void ResetGame()
        {
            score.Content = 0;
            temps.Content = 50;
            mainTimer.Start();
            perdu.Visibility = System.Windows.Visibility.Collapsed;
            gagne.Visibility = System.Windows.Visibility.Collapsed;
            Shuffle(ListImage);
            blibli.Children.Clear();
            ListImage.Clear();
            var Dir = Directory.GetFiles(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Images");
            foreach (string elem in Dir)
            {
                int id = randService.Next();
                var tmp1 = new ElementGrilleView(new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(elem)) }, id);
                tmp1.Check.Unchecked += Check_Unchecked;
                var tmp2 = new ElementGrilleView(new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(elem)) }, id);
                tmp2.Check.Unchecked += Check_Unchecked;
                ListImage.Add(tmp1);
                ListImage.Add(tmp2);


            }
            Shuffle(ListImage);
            foreach (ElementGrilleView elem in ListImage)
            {
                blibli.Children.Add(elem);
            }
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        void supprCarte_Tick(object sender, EventArgs e)
        {
            score.Content = (int)score.Content + 1;
            if ((int)score.Content == 8)
            {
                gagne.Visibility = System.Windows.Visibility.Visible;
                mainTimer.Stop();
            }
            foreach (ElementGrilleView elem in ListImage)
            {
                if (elem.IsImageVisible == true)
                {
                    elem.Visibility = System.Windows.Visibility.Hidden;
                    
                }
            }
            ListImage.RemoveAll(f => f.IsImageVisible == true);
            
            supprCarte.Stop();
        }

        private void retournerCarteTimer_Tick(object sender, EventArgs e)
        {
            foreach (ElementGrilleView elem in ListImage)
            {
                elem.Check.IsChecked = true;
                elem.IsImageVisible = false;
            }
            retournerCarteTimer.Stop();
        }

        void mainTimer_Tick(object sender, EventArgs e)
        {
            
            temps.Content = (int)temps.Content-1;
            if ((int)temps.Content == 0 && (int)score.Content<8)
            {
                perdu.Visibility = System.Windows.Visibility.Visible;
                mainTimer.Stop();
            }
        }

        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            var selectedList = ListImage.Where(f => f.IsImageVisible);
            if (selectedList.Count() == 2) 
            {

                if (selectedList.ElementAt(0).identifiant == selectedList.ElementAt(1).identifiant)
                {
                    supprCarte.Start();
                }
                else
                {
                    retournerCarteTimer.Start();
                    
                }


            }
            else if (selectedList.Count() > 2) 
            {
                ((CheckBox)e.Source).IsChecked = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        
    }
}
