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

            public Card(string colour)
            {
                this._rank = "Joker";
                this._color = colour;
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
        }
    }
}
