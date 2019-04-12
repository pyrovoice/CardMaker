using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardMaker
{

    enum ColorIdentity { MULTICOLORE, UG, WU, UB, UR, WG, WB, WR, GR, GB, RB, BLUE, RED, GREEN, BLACK, WHITE, COLORLESS }

    class Card
    {
        public string Name;
        public int ManaCostRed=0;
        public int ManaCostBlue = 0;
        public int ManaCostGreen = 0;
        public int ManaCostWhite = 0;
        public int ManaCostBlack = 0;
        public int? ManaCostGeneric = null;
        public string Type;
        public string Subtype;
        public string Text;
        public int? Strength = null;
        public int? Toughness = null;
        public string ArtPath;

        public Card()
        {
        }

        public Card(string name, int manaCostRed, int manaCostBlue, int manaCostGreen, int manaCostWhite, int manaCostBlack, int manaCostGeneric, string type, string subtype, string text, int strength, int toughness, string artPath)
        {
            Name = name;
            ManaCostRed = manaCostRed;
            ManaCostBlue = manaCostBlue;
            ManaCostGreen = manaCostGreen;
            ManaCostWhite = manaCostWhite;
            ManaCostBlack = manaCostBlack;
            ManaCostGeneric = manaCostGeneric;
            Type = type;
            Subtype = subtype;
            Text = text;
            Strength = strength;
            Toughness = toughness;
            ArtPath = artPath;
        }

        public ColorIdentity getCardColorIdentity()
        {
            int nbrColor = ManaCostRed > 0 ? 1 : 0 + ManaCostBlue > 0 ? 1 : 0 + ManaCostGreen > 0 ? 1 : 0 + ManaCostWhite > 0 ? 1 : 0 + ManaCostBlack > 0 ? 1 : 0;
            if (nbrColor >= 3)
                return ColorIdentity.MULTICOLORE;

            if (this.ManaCostBlue > 0 && this.ManaCostRed > 0)
            {
                return ColorIdentity.UR;
            }
            if (this.ManaCostBlue > 0 && this.ManaCostWhite > 0)
            {
                return ColorIdentity.WU;
            }
            if (this.ManaCostBlue > 0 && this.ManaCostBlack > 0)
            {
                return ColorIdentity.UB;
            }
            if (this.ManaCostBlue > 0 && this.ManaCostGreen > 0)
            {
                return ColorIdentity.UG;
            }
            if (this.ManaCostWhite > 0 && this.ManaCostRed > 0)
            {
                return ColorIdentity.WR;
            }
            if (this.ManaCostWhite > 0 && this.ManaCostBlack > 0)
            {
                return ColorIdentity.WB;
            }
            if (this.ManaCostWhite > 0 && this.ManaCostGreen > 0)
            {
                return ColorIdentity.WG;
            }
            if (this.ManaCostGreen > 0 && this.ManaCostRed > 0)
            {
                return ColorIdentity.GR;
            }
            if (this.ManaCostGreen > 0 && this.ManaCostBlack > 0)
            {
                return ColorIdentity.GB;
            }
            if (this.ManaCostBlack > 0 && this.ManaCostRed > 0)
            {
                return ColorIdentity.RB;
            }
            if (this.ManaCostBlue > 0)
            {
                return ColorIdentity.BLUE;
            }
            if (this.ManaCostRed > 0)
            {
                return ColorIdentity.RED;
            }
            if (this.ManaCostGreen > 0)
            {
                return ColorIdentity.GREEN;
            }
            if (this.ManaCostWhite > 0)
            {
                return ColorIdentity.WHITE;
            }
            if (this.ManaCostBlack > 0)
            {
                return ColorIdentity.BLACK;
            }
            return ColorIdentity.COLORLESS;
        }
    }
}
