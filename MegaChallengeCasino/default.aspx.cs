using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class _default : System.Web.UI.Page
    {
        Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            if (!Page.IsPostBack)
            {
                RenderImages();
                int playersMoney = 100;
                ViewState.Add("Money", playersMoney);
                moneyAmountLabel.Text = playersMoney.ToString();
            }
        }


        protected void rollButton_Click(object sender, EventArgs e)
        {
            GameLogic();
        }





            public string[] ParseImagesArray()
            {
            string path = @"D:\Programming\Challenges\MegaChallengeCasino\MegaChallengeCasino\Images";
            string[] image = Directory.GetFiles(path, "*.png");
            int imageIndex = image.Length;
            string[] images = new string[imageIndex];
            int imagesIndex = path.IndexOf("Images");
            for (int i = 0; i < image.Length; i++)
            {
                images[i] = image[i].Substring(imagesIndex);
            }

            return images;
        }

            public void RenderImages()
            {
            var images = ParseImagesArray();
            Image1.ImageUrl = images[random.Next(0, images.Length)];
            Image2.ImageUrl = images[random.Next(0, images.Length)];
            Image3.ImageUrl = images[random.Next(0, images.Length)];
        }
        

        //Taking bet balue.
        public int PlayersBet()
        {
            int bet;
            if (int.TryParse(betValueBox.Text, out bet))
            {
                return bet;
            }
            else
            {
                return 0;
            }
        }

        //Generating array with 3 random images

            public string[] GenerateRandomImages()
        {
            string[] images = new string [3]; 
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = ParseImagesArray()[random.Next(0, ParseImagesArray().Length)];
            }
            return images;
        }

        //Generate images.

            public void GenerateImages(string[] imageArray)
            {
            Image1.ImageUrl = imageArray[0];
            Image2.ImageUrl = imageArray[1];
            Image3.ImageUrl = imageArray[2];
            }

        //Checking for bars.
        public bool IsBar(string[] imageArray)
        {
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (imageArray[i].Contains("Bar")) return true;
            }
            return false;
        }
        //Checking for sevens.

        public bool IsSevens(string[] imageArray)
        {
            int counter = 0;
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (imageArray[i].Contains("Seven"))
                {
                    counter += 1;
                }
                if (counter == 3) return true;
            }
            return false;
        }

        //Count Cherries.

        public int CherryCounter(string[] imageArray)
        {
            int counter = 0;
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (imageArray[i].Contains("Cherry")) counter += 1; 
            }
            return counter;
        }

        //Multipier.

        public int Multiplier(string[] imageArray)
        {
            int multiply = 1;
            if (CherryCounter(imageArray) == 1) multiply = 2;
            if (CherryCounter(imageArray) == 2) multiply = 3;
            if (CherryCounter(imageArray) == 3) multiply = 4;
            if (IsSevens(imageArray)) multiply = 100;
            return multiply;
        }

        //Get player's money.

        public int Getplayersmoney()
        {
            int money = (int)ViewState["Money"];
            return money;
        }

        public void GameLogic()
        {
            string[] imagesArray = GenerateRandomImages();
            GenerateImages(imagesArray);
            int playersMoney = Getplayersmoney();
            if (!IsBar(imagesArray))
            {
                if(CherryCounter(imagesArray) >= 1 || IsSevens(imagesArray))
                {
                    playersMoney += PlayersBet() * Multiplier(imagesArray);
                }
                
            }
            playersMoney -= PlayersBet();
            ViewState["Money"] = playersMoney;
            moneyAmountLabel.Text = ViewState["Money"].ToString();
            MoneyLeft();
        }

        public void MoneyLeft()
        {
            int money = (int)ViewState["Money"];
            if(money <= 0)
            {
                moneyLabel.Text = "Gameover";
                moneyAmountLabel.Text = "Gameover";
            }
        }















        /*
        if (string.IsNullOrWhiteSpace(betValueBox.Text.Trim())) return;
        string[] images = RollImages();
        int bid = int.Parse(betValueBox.Text);
        RenderImages(images);
        DefinePlayer(images, bid);
        DisplayResult(images, bid);
        */


        /*
        // Defining game logic - Cherries, 7s, bar;

        // Defining if there are any bars.
        private bool IsBar(string[] images)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] == "Images /Bar.png") return true;
            }
            return false;
        }

        // Defining Jackpot.
        private bool IsJackpot(string[] images)
        {
            int counter = 0;
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] == "Images/Seven") counter += 1;
            }
            if (counter == 3) return true;
            return false;
        }

        //Defining Cherries.

        private bool IsCherries(string[] images)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] == "Images/Cherry.png") return true;
            }
            return false;
        }


        //Counting Cherries.
        private int CherryCounter(string[] images)
        {
            int counter = 0;
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] == "Images/Cherry.png") counter += 1;
            }
            return counter;
        }

        //If winning

        private bool IsWon(string[] images)
        {
            if (!IsBar(images))
            {
                if (IsCherries(images)) return true;
                if (IsJackpot(images)) return true;
            }
            return false;
        }

        // CherryMultiplier

        private int MultiplyCherry(string[] images, int bid)
        {
            int result = 0;
            if (CherryCounter(images) == 1) result = bid * 2;
            if (CherryCounter(images) == 2) result = bid * 3;
            if (CherryCounter(images) == 3) result = bid * 4;
            return result;
        }

        // Defining jackpot value
        private int MultipyJackpot(int bid)
        {
            int result = bid * 100;
            return 0;
        }

        //Counting.

        private int GlobalMultiplier(string[] images, int bid)
        {
            int result = 0;
            if (IsWon(images))
            {
                if (IsCherries(images)) result = MultiplyCherry(images, bid);
                if (IsJackpot(images)) result = MultipyJackpot(bid);
            }
            return result;
        }


        // Counting Player's Money.

        private void DefinePlayer(string[] images, int bid)
        {
            int playersMoney = (int)ViewState["PlayersMoney"];
            playersMoney -= bid;
            playersMoney += GlobalMultiplier(images, bid);
            ViewState["PlayersMoney"] = playersMoney;
        }

        // Announcing Result

        private void DisplayResult(string[] images, int bid)
        {
            if (IsWon(images)) moneyAmountLabel.Text = string.Format("Your bid was {0:C}, and you have won {1:c}", bid, GlobalMultiplier(images, bid));
            if (!IsWon(images)) moneyAmountLabel.Text = string.Format("Sorry, you've lost {0:c}, You will be lucky some day", bid);
            moneyLabel.Text = string.Format("Players Money : {0:c}", ViewState["PlayersMoney"].ToString());
        }


        // Generating Random Number
        private int GenerateNumbers(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        //Filling Images Array.
        private string[] RollImages()
        {
            string[] images = new string[] { "Images/Bar.png", "Images/Bell.png", "Images/Cherry.png", "Images/Clover.png", "Images/Diamond.png", "Images/HorseShoe.png", "Images/Lemon.png", "Images/Orange.png", "Images/Plum.png", "Images/Seven.png", "Images/Strawberry.png", "Images/Watermellon.png" };
            string[] imagesForGame = new string[3];
            for (int i = 0; i < 3; i++)
            {
                imagesForGame[i] = images[GenerateNumbers(0, images.Length)];
            }
            return imagesForGame;
        }

        // Rendering Images.
        private void RenderImages(string[] imgArr)
        {
            string[] imageArray = imgArr;
            Image1.ImageUrl = imageArray[0];
            Image2.ImageUrl = imageArray[1];
            Image3.ImageUrl = imageArray[2];
        }


    */


















        //Check if player has money
        //Generate random images
        //Check loose win combinations
        //Calculate Money



        //private string[] ImagesForGame(string[] images)
        //{
        //    string[] imageArray = images;
        //    string[] gameImages = new string[images.Length];
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        gameImages[i] = images[i];
        //    }
        //    return gameImages;
        //}
        //Rendering Images to screen

    }
}