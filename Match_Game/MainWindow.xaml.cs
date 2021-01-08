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
using System.Windows.Threading;

namespace Match_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfASecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            

            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetupGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfASecondsElapsed++;
            timeTextBlock.Text = (tenthsOfASecondsElapsed / 10f).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetupGame()
        {
            List<string> animalEmojis = new List<string>()
            {
                "🐔","🐔",
                "🦊","🦊",
                "🐺","🐺",
                "🐵","🐵",
                "🐭","🐭",
                "🐲","🐲",
                "🐗","🐗",
                "🦄","🦄",
            };

            Random random = new Random();


            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmojis.Count);
                    string nextEmoji = animalEmojis[index];
                    textBlock.Text = nextEmoji;
                    animalEmojis.RemoveAt(index);
                }

            }

            timer.Start();
            tenthsOfASecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        // mouse down event method
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // textblock object that handle all textblocks
            TextBlock textBlock = sender as TextBlock;

            // if findingMatch is false, the textblock is hidden.
            // lastTextBlockClicked gets value textblock. the latest box clicked.
            // findingMatch set to true
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            // if the textblock "text" is the same has the one clicked before (a match)
            // makes the second animal invisible and unklickable.
            // resets findingMatch.
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            // animal doesnt match. will reset back to visible 
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
           

            if (matchesFound == 8)
            {
                SetupGame();
            }


        }
    }
}
