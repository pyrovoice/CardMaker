using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardMaker
{
    enum ColorIdentity { MULTICOLORE, BLUE, RED, COLORLESS }

    class Helper
    {
        private static string pathToResource = "C:\\Users\\maxim\\source\\repos\\CardMaker\\CardMaker\\Resources\\cardParts\\";
        public static ColorIdentity getCardColorIdentity(Card c)
        {
            if (c.ManaCostBlue > 0 && c.ManaCostRed > 0)
            {
                return ColorIdentity.MULTICOLORE;
            }
            if (c.ManaCostBlue > 0)
            {
                return ColorIdentity.BLUE;
            }
            if (c.ManaCostRed > 0)
            {
                return ColorIdentity.RED;
            }
            return ColorIdentity.COLORLESS;
        }

        public static string getCardBase(Card c)
        {
            ColorIdentity color = getCardColorIdentity(c);
            string extension = null;
            if (c.Type.Contains("Land"))
            {
                switch (color)
                {
                    case ColorIdentity.MULTICOLORE:
                        extension = "omlcard.jpg";
                        break;
                    case ColorIdentity.BLUE:
                        extension = "ulcard.jpg";
                        break;
                    case ColorIdentity.RED:
                        extension = "rlcard.jpg";
                        break;
                    case ColorIdentity.COLORLESS:
                        extension = "clcard.jpg";
                        break;
                }
            }
            else
            {
                switch (color)
                {
                    case ColorIdentity.MULTICOLORE:
                        extension = "urcard.png";
                        break;
                    case ColorIdentity.BLUE:
                        extension = "ucard.png";
                        break;
                    case ColorIdentity.RED:
                        extension = "rcard.png";
                        break;
                    case ColorIdentity.COLORLESS:
                        extension = "acard.jpg";
                        break;
                }
            }
            if (extension != null)
            {
                return pathToResource + extension;
            }
            else
            {
                return null;
            }
        }

        public static void DrawText(Bitmap baseMap, String text, Font font, int x, int y, int maxX)
        {
            Graphics g = Graphics.FromImage(baseMap);
            int maxY = 100;
            if(g.MeasureString(text, font).Width <= maxX)
            {
                RectangleF rectf = new RectangleF(x, y, maxX, maxY);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawString(text, font, Brushes.Black, rectf);
                return;
            }
            int actualY = y;
            String[] abilities = text.Split(new string[] { "\\n" }, StringSplitOptions.None);
            foreach (String ability in abilities)
            {
                String toWrite = "";
                foreach (String word in ability.Split(' '))
                {
                    // Measure next word's width
                    var a = g.MeasureString(word + " ", font);
                    // If adding the next word would go too far, write the line and flush before adding it
                    if (g.MeasureString(toWrite + word, font).Width > maxX)
                    {
                        RectangleF rectf = new RectangleF(x, actualY, maxX, maxY);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawString(toWrite, font, Brushes.Black, rectf);
                        toWrite = word + " ";
                        actualY += 31;
                    }
                    else
                    {
                        toWrite += word + " ";
                    }
                }
                if (!String.IsNullOrEmpty(toWrite))
                {
                    RectangleF rectf = new RectangleF(x, actualY, maxX, maxY);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawString(toWrite, font, Brushes.Black, rectf);
                }
                actualY += 42;
            }
            g.Flush();
        }

        public static string getCardPTBox(Card c)
        {
            if (c.Strength == null || c.Toughness == null)
            {
                return null;
            }
            ColorIdentity color = getCardColorIdentity(c);
            string extension = null;
            switch (color)
            {
                case ColorIdentity.MULTICOLORE:
                    extension = "mpt.png";
                    break;
                case ColorIdentity.BLUE:
                    extension = "upt.png";
                    break;
                case ColorIdentity.RED:
                    extension = "rpt.png";
                    break;
                case ColorIdentity.COLORLESS:
                    extension = "apt.png";
                    break;
                default:
                    return null;
            }
            return pathToResource + extension;
        }

        internal static Font getFont(string style, int size, FontStyle fontStyle)
        {
            var a = new FontFamily(style);
            return new Font(a, size, fontStyle, GraphicsUnit.Pixel);
        }
    }
}
