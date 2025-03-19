using System.Diagnostics.Metrics;
using System.IO;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Worlde___WPF.Models;

namespace Worlde___WPF
{
    
    public partial class MainWindow : Window
    {
        PlayerDBContext context = new PlayerDBContext();
        Game GameStarted;
        Random rnd;
        static List<string> wordsList = new List<string>();
        static List<TextBox> wordsTextBox = new List<TextBox>();

        public MainWindow()
        {
            context.Database.EnsureCreated();
            InitializeComponent();
            LoadWords();
        }

        #region Main Menu
        private void start_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text == "Insert Name" || Name.Text == string.Empty)
            {
                MessageBox.Show("Please insert your name!");
                return;
            }
            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Visible;
            rnd = new Random();
            Player player = new Player(Name.Text.Trim());
            GameStarted = new Game(wordsList[rnd.Next(0, wordsList.Count)], player);

            Task.Delay(200).Wait();
            pos1.Focus();

        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Game.Visibility = Visibility.Visible;
            continueMenu.Visibility = Visibility.Collapsed;
            GameStarted.Word = wordsList[rnd.Next(0, wordsList.Count)];
            pos1.Focus();
        }
        private void stats_Click(object sender, RoutedEventArgs e)
        {
            LoadStatistics();
            Menu.Visibility = Visibility.Collapsed;
            WordleStat.Visibility = Visibility.Visible;
        }
        private void database_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            WordleDatabase.Visibility = Visibility.Visible;
            wordCount.Content = "We have " + wordsList.Count + " words in our Database";

            foreach (string word in wordsList)
            {
                words.Items.Add(word);
            }
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void ChangeName_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.Text = "";
        }
        #endregion

        #region Extra buttons
        private void Back_Button_Database_Click(object sender, RoutedEventArgs e)
        {
            WordleDatabase.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Visible;
        }
        private void Back_Button_Statistics_Click(object sender, RoutedEventArgs e)
        {

            WordleStat.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Visible;
        }
        private void GoToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            wlMenu.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Visible;
        }

        #endregion

        #region Game Mechanics
        private void pos1_TextEntered(object sender, KeyEventArgs e)
        {
            pos2.Focus();
            Task.Delay(50).Wait();
        }
        private void pos2_TextEntered(object sender, KeyEventArgs e)
        {
            pos3.Focus();
            Task.Delay(50).Wait();
        }
        private void pos3_TextEntered(object sender, KeyEventArgs e)
        {
            pos4.Focus();
            Task.Delay(50).Wait();
        }
        private void pos4_TextEntered(object sender, KeyEventArgs e)
        {
            pos5.Focus();
            Task.Delay(50).Wait();
        }
        private void pos5_TextEntered(object sender, KeyEventArgs e)
        {
            Task.Delay(50).Wait();
            int correctAnswers = CheckCorrect(GameStarted);

            if ((correctAnswers == 5))
            {
                ShowContinueGameText(GameStarted);
            } else {
               
                if (GameStarted.InnerRounds < 5)
                {
                    CreateTextBox();
                    GameStarted.InnerRounds++;
                }
                else
                {
                    GameStarted.InnerRounds++;
                    ShowEndGameText(GameStarted);
                }
            }
        }
        #endregion

        #region Extra Functions
        private void ShowContinueGameText(Game game)
        {
            DisableTextBoxes();
            game.Rounds++;
            Game.Visibility = Visibility.Collapsed;
            continueMenu.Visibility = Visibility.Visible;
            searchedWord.Content = $"Word: {game.Word}";
            statusText.Content = $"You WON!";
            roundsText.Content = game.Rounds == 1 ? $"You finished {game.Rounds} round" : $"You finished {game.Rounds} rounds";

            ClearBoard();
        }
        private void ShowEndGameText(Game game)
        {
            DisableTextBoxes();
            Game.Visibility = Visibility.Collapsed;
            wlMenu.Visibility = Visibility.Visible;
            searched.Content = $"Word: {game.Word}";
            status.Content = $"You LOST!";
            rounds.Content = "Try it again!";
            points.Content = game.Rounds == 1 ? $"You finished {game.Rounds} round" : $"You finished {game.Rounds} rounds";

            game.Player.Rounds = game.Rounds;

            ClearBoard();
            
            //DB
            context.Players.Add(game.SavePlayerHighscore());
            context.SaveChanges();
        }
        private int CheckCorrect(Game game)
        {
            int counter = 0;
            string gameWord = game.Word;
            for (int i = 1; i < 6; i++)
            {
                TextBox? originalBox = (TextBox?)FindName($"pos{i}");

                if (originalBox == null || string.IsNullOrEmpty(originalBox.Text))
                {
                    continue;
                }

                char character = originalBox.Text[0];

                if (character == gameWord[i - 1])
                {
                    originalBox.BorderBrush = Brushes.Green;
                    counter++;
                }
                else if (game.Word.Contains(character))
                {
                    originalBox.BorderBrush = Brushes.Yellow;
                }
                else
                {
                    originalBox.BorderBrush = Brushes.Red;
                }
            }
            return counter;
        }
        private void ClearBoard()
        {
            for (int i = 0; i < wordsTextBox.Count; i++)
            {
                Game.Children.Remove(wordsTextBox[i]);
            }

            EnableTextBoxes();

            pos1.Text = "";
            pos2.Text = "";
            pos3.Text = "";
            pos4.Text = "";
            pos5.Text = "";

            pos1.BorderBrush = Brushes.Black;
            pos2.BorderBrush = Brushes.Black;
            pos3.BorderBrush = Brushes.Black;
            pos4.BorderBrush = Brushes.Black;
            pos5.BorderBrush = Brushes.Black;

            pos1.Margin = new Thickness(pos1.Margin.Left, 100, pos1.Margin.Right, pos1.Margin.Bottom);
            pos2.Margin = new Thickness(pos2.Margin.Left, 100, pos2.Margin.Right, pos2.Margin.Bottom);
            pos3.Margin = new Thickness(pos3.Margin.Left, 100, pos3.Margin.Right, pos3.Margin.Bottom);
            pos4.Margin = new Thickness(pos4.Margin.Left, 100, pos4.Margin.Right, pos4.Margin.Bottom);
            pos5.Margin = new Thickness(pos5.Margin.Left, 100, pos5.Margin.Right, pos5.Margin.Bottom);
        }
        private void DisableTextBoxes()
        {
            pos1.IsEnabled = false;
            pos2.IsEnabled = false;
            pos3.IsEnabled = false;
            pos4.IsEnabled = false;
            pos5.IsEnabled = false;
        }
        private void EnableTextBoxes()
        {
            pos1.IsEnabled = true;
            pos2.IsEnabled = true;
            pos3.IsEnabled = true;
            pos4.IsEnabled = true;
            pos5.IsEnabled = true;
        }
        private void CreateTextBox()
        {
            for(int i = 1; i < 6; i++)
            {
                TextBox? originalBox = (TextBox?)FindName($"pos{i}");           // We get the name of the Element in XAML

                if (originalBox == null) continue; 

                TextBox newBox = new TextBox()
                {
                    Width = 30,
                    Height = 30,
                    FontSize = originalBox.FontSize,
                    Margin = originalBox.Margin,
                    MaxLength = 1,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    BorderBrush = originalBox.BorderBrush,
                    Text = originalBox.Text,
                    IsEnabled = false
                };
                wordsTextBox.Add(newBox);                   
                Game.Children.Add(newBox);
                originalBox.IsEnabled = true;
                originalBox.Text = "";
            }
            SetBlackBorder();
            MoveTextBoxes();
            pos1.Focus();
        }
        private void MoveTextBoxes()
        {
            pos1.Margin = new Thickness(pos1.Margin.Left, pos1.Margin.Top + 50, pos1.Margin.Right, pos1.Margin.Bottom);
            pos2.Margin = new Thickness(pos2.Margin.Left, pos2.Margin.Top + 50, pos2.Margin.Right, pos2.Margin.Bottom);
            pos3.Margin = new Thickness(pos3.Margin.Left, pos3.Margin.Top + 50, pos3.Margin.Right, pos3.Margin.Bottom);
            pos4.Margin = new Thickness(pos4.Margin.Left, pos4.Margin.Top + 50, pos4.Margin.Right, pos4.Margin.Bottom);
            pos5.Margin = new Thickness(pos5.Margin.Left, pos5.Margin.Top + 50, pos5.Margin.Right, pos5.Margin.Bottom);
        }
        private void SetBlackBorder()
        {
            pos1.BorderBrush = Brushes.Black;
            pos2.BorderBrush = Brushes.Black;
            pos3.BorderBrush = Brushes.Black;
            pos4.BorderBrush = Brushes.Black;
            pos5.BorderBrush = Brushes.Black;
        }
        private void LoadStatistics()
        {
            playerList.Items.Clear();
            foreach (var player in context.Players)
            {
                playerList.Items.Add(player.ToString());
            }
        }
        private void LoadWords()
        {
            string pathWords = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "words.txt");
            using (StreamReader sr = new StreamReader(pathWords))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    wordsList.Add(line);
                }
            }
        }
        #endregion
    }
}