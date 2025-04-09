using System.Text;

namespace Edee_Assignment5
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }

        public class Card
        {
            //Fields:
            private string _rank;
            private string _suit;
            private string _color;

            //Constructors:
            public Card(string rank, string suit)
            {
                _rank = rank;
                _suit = suit;

                if (suit == "Hearts" || suit == "Diamonds")
                    this._color = "Black";
                else if (suit == "Clubs" || suit == "Spades")
                    this._color = "Black";
                else
                    throw new ArgumentException("Invalid suit provided.");
            }

            //Constructor for Joker
            public Card(string color)
            {
                this._rank = "Joker";
                this._suit = null; //Joker has no suit
                this._color = color;
            }

            public Card(int num)
            {
                if(num == 53)
                {
                    this._rank = "Joker";
                    this._suit = null;
                    this._color = "Red";
                }
                else if(num == 54)
                {
                    this._rank = "Joker";
                    this._suit = null;
                    this._color = "Black";
                }
            }

            //Properties:
            public string Rank
            {
                get { return _rank; }
            }

            public string Suit
            {
                get { return _suit; }
            }

            public string Color
            {
                get { return _color; }
            }

            //Override methods
            public override string ToString()
            {
                Console.OutputEncoding = Encoding.UTF8;

                if (this._rank == "Joker" && this._color == "Red")
                    return "RJ";
                else if (this._rank == "Joker" && this._color == "Black")
                    return "BJ";

                if (this._suit == "Diamonds")
                    return $"{this._rank}\u2666";
                else if (this._suit == "Clubs")
                    return $"{this._rank}\u2663";
                else if (this._suit == "Spades")
                    return $"{this._rank}\u2660";
                else if (this._suit == "Hearts")
                    return $"{this._rank}\u2665";

                return "Error";
            }

            public override bool Equals(object otherCard)
            {
                if(otherCard == null || GetType() != otherCard.GetType())
                {
                    return false;
                }

                Card newCard = (Card)otherCard;

                return this._rank == newCard._rank && this._suit == newCard._suit && this._color == newCard._color;
            }
        }
    }
}
