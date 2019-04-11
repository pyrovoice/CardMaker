using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardMaker
{
    class Card
    {
        public string Name;
        public int ManaCostRed=0;
        public int ManaCostBlue = 0;
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

        public Card(string name, int manaCostRed, int manaCostBlue, int manaCostGeneric, string type, string subtype, string text, int strength, int toughness, string artPath)
        {
            Name = name;
            ManaCostRed = manaCostRed;
            ManaCostBlue = manaCostBlue;
            ManaCostGeneric = manaCostGeneric;
            Type = type;
            Subtype = subtype;
            Text = text;
            Strength = strength;
            Toughness = toughness;
            ArtPath = artPath;
        }
    }
}
