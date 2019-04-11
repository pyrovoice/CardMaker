using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CardMaker
{
    public partial class Form1 : Form
    {
        private static int CARD_TITLE_X = 30;
        private static int CARD_TITLE_Y = 30;
        private static int CARD_COST_X = 620;
        private static int CARD_COST_Y = 33;
        private static int CARD_ABILITIES_X = 32;
        private static int CARD_ABILITIES_Y = 635;
        private static int CARD_STATBOX_X = 545;
        private static int CARD_STATBOX_Y = 900;
        private static int CARD_STATTEXT_X = 575;
        private static int CARD_STATTEXT_Y = 895;
        private static int CARD_TYPE_X = 30;
        private static int CARD_TYPE_Y = 565;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Card> cards = new List<Card>();
            using (StreamReader file = new StreamReader("C:\\Users\\maxim\\source\\repos\\CardMaker\\CardMaker\\Resources\\Cards\\cards.csv"))
            {
                string ln;
                //skip the first line
                file.ReadLine();
                while ((ln = file.ReadLine()) != null)
                {
                    cards.Add(stringToCard(ln));
                }
                file.Close();
            }

            foreach (Card c in cards)
            {
                generateCard(c);
            }
        }

        private Card stringToCard(string ln)
        {
            Card c = new Card();
            String[] parts = ln.Split(';');
            c.Name = parts[0];
            //Calculate the mana cost
            foreach (Char manaSymbol in parts[1])
            {
                if (manaSymbol.ToString().ToLower().Equals("u"))
                {
                    c.ManaCostBlue++;
                }
                else if (manaSymbol.ToString().ToLower().Equals("r"))
                {
                    c.ManaCostRed++;
                }
                else if (int.TryParse(Regex.Match(manaSymbol.ToString(), "[0-9]").Value, out int value))
                {
                    c.ManaCostGeneric = value;
                }
            }
            c.Type = parts[2];
            c.Subtype = parts[3];
            c.Text = parts[4];
            //Calculate power and toughness
            if (!String.IsNullOrEmpty(parts[5]))
            {
                var stats = parts[5].Split('/');
                if (stats.Length == 2)
                {
                    Int32.TryParse(stats[0], out int strength);
                    Int32.TryParse(stats[1], out int toughness);
                    c.Strength = strength;
                    c.Toughness = toughness;
                }
            }
            c.ArtPath = parts[6];

            return c;
        }

        private void generateCard(Card c)
        {
            // Base of the card
            Bitmap source1 = new Bitmap(Helper.getCardBase(c));

            var statsBoxPath = Helper.getCardPTBox(c);
            Bitmap source2 = null;
            if (statsBoxPath != null)
            {
                source2 = new Bitmap(Helper.getCardPTBox(c)); ;
            }
            // PT box
            var graphics = Graphics.FromImage(source1);
            graphics.DrawImage(source1, 0, 0);
            //Draw PT Textbox
            if (source2 != null)
            {
                graphics.DrawImage(source2, CARD_STATBOX_X, CARD_STATBOX_Y);
                Helper.DrawText(source1, c.Strength + "/" + c.Toughness, Helper.getFont("Neuton SC", 48, FontStyle.Regular), CARD_STATTEXT_X, CARD_STATTEXT_Y, 600);
            }
            //Draw name
            Helper.DrawText(source1, c.Name, Helper.getFont("Beleren", 36, FontStyle.Bold), CARD_TITLE_X, CARD_TITLE_Y, 600);
            //Draw Type
            Helper.DrawText(source1, c.Type + " — " + c.Subtype, Helper.getFont("Beleren", 30, FontStyle.Bold), CARD_TYPE_X, CARD_TYPE_Y, 600);
            //Draw Text
            Helper.DrawText(source1, c.Text, Helper.getFont("MPlantin", 34, FontStyle.Regular), CARD_ABILITIES_X, CARD_ABILITIES_Y, 600);
            //Draw Mana cost
            DrawManaCost(source1, c);

            String directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\CardMaker";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            source1.Save(directoryPath + "\\" + c.Name + ".png", ImageFormat.Png);

        }

        private void DrawManaCost(Bitmap source, Card card)
        {
            int basex = CARD_COST_X;
            int baseY = CARD_COST_Y;
            int distanceBetweenSymbols = 37;
            Graphics g = Graphics.FromImage(source);
            for (int i = 0; i < card.ManaCostBlue; i++)
            {
                Bitmap blueMana = new Bitmap("C:\\Users\\maxim\\source\\repos\\CardMaker\\CardMaker\\Resources\\roundSymbols\\mana_u_small.png");
                g.DrawImage(blueMana, basex, baseY);
                basex -= distanceBetweenSymbols;
            }
            for (int i = 0; i < card.ManaCostRed; i++)
            {
                Bitmap redMana = new Bitmap("C:\\Users\\maxim\\source\\repos\\CardMaker\\CardMaker\\Resources\\roundSymbols\\mana_r_small.png");
                g.DrawImage(redMana, basex, baseY);
                basex -= distanceBetweenSymbols;
            }
            if (card.ManaCostGeneric.HasValue)
            {
                Bitmap genericMana = new Bitmap("C:\\Users\\maxim\\source\\repos\\CardMaker\\CardMaker\\Resources\\roundSymbols\\mana_circle_small.png");
                Helper.DrawText(genericMana, "" + card.ManaCostGeneric.Value, Helper.getFont("Neuton SC", 50, FontStyle.Regular), 0, -22, 50);
                g.DrawImage(genericMana, basex, baseY);
                g.Flush();
            }
        }
    }
}
