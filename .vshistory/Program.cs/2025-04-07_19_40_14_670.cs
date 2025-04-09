namespace Edee_Assignment5
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }

        public class Card
        {
            private string _rank;
            private string _suit;
            private string _colour;

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
