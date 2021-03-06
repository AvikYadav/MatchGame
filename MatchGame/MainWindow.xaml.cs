﻿using System;
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

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Boolean randomList = true;
        public double currentTime = 0;
        public double bestTime = 0;

        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0");
            if (matchesFound == 8)
            {
                currentTime = Convert.ToDouble(timeTextBlock.Text);
                if (!Convert.ToBoolean(bestTime) || bestTime > currentTime) {
                    bestTime = currentTime;
                };

             
                bestTimeBlock.Text = Convert.ToString(bestTime);
                
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again";
                bestTimeBlock.Text = bestTimeBlock.Text + " Best Time";
                randomList = false;
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "😀","😀",
                "🐶","🐶",
                "😂","😂",
                "🐙","🐙",
                "😎","😎",
                "🦒","🦒",
                "🥳","🥳",
                "🦍","🦍",
            };

            List<string> animalEmojitwo = new List<string>()
            {
                "🤮","🤮",
                "🐬","🐬",
                "😎","😎",
                "🐢","🐢",
                "🐵","🐵",
                "🐼","🐼",
                "🐭","🐭",
                "🐔","🐔",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    if (textBlock.Name != "bestTimeBlock")
                    {
                        if (randomList != false)
                        {
                            textBlock.Visibility = Visibility.Visible;
                            int index = random.Next(animalEmoji.Count);
                            string nextEmoji = animalEmoji[index];
                            textBlock.Text = nextEmoji;
                            animalEmoji.RemoveAt(index);

                        }
                        else
                        {
                            textBlock.Visibility = Visibility.Visible;
                            int index = random.Next(animalEmojitwo.Count);
                            string nextEmoji = animalEmojitwo[index];
                            textBlock.Text = nextEmoji;
                            animalEmojitwo.RemoveAt(index);


                        }

                    }
                    

                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;



        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
        
    }
}
