namespace Edee_Assignment5
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }

        public class Card
        {
            //Fields:
            private string _rank;
            private string _suit;
            private string _colour;

            //Constructors:
            public Card(string rank, string suit)
            {
                _rank = rank;
                _suit = suit;
            }

            public Card(string colour)
            {
                _colour = colour;
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

            public string Colour
            {
                get { return _colour; }
            }
        }
    }
}
