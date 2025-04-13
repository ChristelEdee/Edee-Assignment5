using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using static Edee_Assignment5.Program;

namespace Edee_Assignment5
{
    internal class Program
    {
        /*
         * Programming 2 - Assignment 5 – Winter 2025
         * Created by:      Christel Edee 6250046
         * Tested by:       Cheryl-Ann Edee
         * Relationship:    Sister
         * Date:            2025-04-13
         *
         * Description: The goal of this program is to do the * following:
         * Create an object-based approach to classes based on a basic Card game. (No gameplay)
         */

        static void Main(string[] args)
        {
            Console.WriteLine("************************************");
            Console.WriteLine("Welcome to Programming 2 - Assignment 5 - Winter 2025\n");
            Console.WriteLine("Created by CHRISTEL 6250046 on April 13th 2025");
            Console.WriteLine("************************************");

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
                else
                {
                    throw new ArgumentException("Only 53 (Red Joker) and 54 (Black Joker) are valid inputs.");
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

            public override int GetHashCode()
            {
                int hash = 17;

                hash = hash * 31 + (_rank != null ? _rank.GetHashCode() : 0);
                hash = hash * 31 + (_suit != null ? _suit.GetHashCode() : 0);
                hash = hash * 31 + (_color != null ? _color.GetHashCode() : 0);

                return hash;
            }
        }

        public class Deck
        {
            //Field:
            private List<Card> _cardsList;


            //Property:
            public int CardsLeft
            {
                get { return _cardsList.Count; } //Number of cards remaining (therefore the list count)
            }


            //Constructors:
            public Deck()
            {
                _cardsList = new List<Card>(); //This is indeed empty...I believe?
            }

            public Deck(bool hasJoker)
            {
                _cardsList = new List<Card>();

                //Populating the deck? i guess
                string[] suits = { "Diamonds", "Hearts", "Spades", "Clubs" };
                string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen", "Ace" };

                foreach (string suit in suits)
                {
                    foreach (string rank in ranks)
                    {
                        _cardsList.Add(new Card(suit, rank)); //Adding each card to the deck (They're in order at the moment)
                    }
                }

                //Checking if the user wanted jokers in the deck (boolean)
                if (hasJoker == true)
                {
                    _cardsList.Add(new Card(53)); //Red Joker (Using the Card integer constructor)
                    _cardsList.Add(new Card(54)); //Black Joker (Using the Card integer constructor)
                }
            }

            public Deck(List<Card> newCustomCards)
            {
                newCustomCards = new List<Card>(newCustomCards);
            }


            //Methods:
            public Card Draw()
            {
                //Returning null if there are no more cards in the deck
                if (_cardsList.Count == 0)
                    return null;

                Card topCard = _cardsList[0]; //Getting the first card in the deck
                _cardsList.Remove(topCard); //Removing it from the list

                return topCard; //Returning it
            }
            
            public void Shuffle()
            {
                Random random = new Random();
                int n = _cardsList.Count; //Will help to keep track of the number of shuffles

                //Shuffling process:
                while(n > 1)
                {
                    n--;
                    int i = random.Next(n + 1); //Taking a random index
                    Card tempCard = _cardsList[i]; //Picking the specific card at that index
                    _cardsList[i] = _cardsList[n]; //Switching that card for another one in the deck
                    _cardsList[n] = tempCard; 
                }
            }

            public Card Peek()
            {
                //Making sure the deck isn't empty
                if (_cardsList.Count == 0)
                    throw new ArgumentException("No cards left in the deck.");

                return _cardsList[0]; //Returning the top card of the deck (no removing)
            }

            public void PlaceOnTop(Card card)
            {
                _cardsList.Insert(0, card); //Adding a provided card to the top of the decl
            }


            //Override method:
            public override string ToString()
            {
                return $"There are {this.CardsLeft} cards left.";
            }
        }

        public class Hand
        {
            //Fields:
            private List<Card> _cardsList; //This is the hand, btw (list of cards)
            private string[] _suitPriority;
            

            //Property:
            public int Size
            {
                get { return _cardsList.Count; }
            }


            //Constructor:
            public Hand(string[] suitPriority)
            {
                this._cardsList = new List<Card>();
                this._suitPriority = suitPriority;
            }

            //Methods:
            public void AddCard(Card card)
            {
                _cardsList.Add(card);
                OrderBySuit();
            }

            public Card RemoveCard()
            {
                Card cardToRemove = _cardsList[0]; //Picks a card from the deck
                _cardsList.RemoveAt(0); //Removes it from the deck
                OrderBySuit(); //Reorders hand accordingly

                return cardToRemove; //Returns it
            }

            public bool Contains(Card target)
            {
                return _cardsList.Contains(target); //Checking if a target card is in hand (returns boolean)
            }

            public void OrderBySuit()
            {
                List<Card> sortedCards = new List<Card>(); //New hand list

                for(int i = 0; i < _suitPriority.Length; i++)
                {
                    //Taking the current index's suit
                    string currentSuit = _suitPriority[i]; 

                    //Temporary list to store the cards that have that current suit
                    List<Card> cardsWithSuit = new List<Card>();

                    //Store all cards that match the current suit
                    for(int j = 0; j <_cardsList.Count; j++)
                    {
                        if (_cardsList[j].Suit == currentSuit)
                        {
                            cardsWithSuit.Add(_cardsList[j]);
                        }
                    }

                    //Add sorted cards of this suit to the new hand list
                    sortedCards.AddRange(cardsWithSuit);
                }

                //Replace the original hand with the sorted one
                _cardsList = sortedCards;
            }


            //Override method:
            public override string ToString()
            {
                return string.Join(", ", _cardsList.Select(card => card.ToString()));
            }

        }

        public class GameState
        {
            //Fields:
            private Deck _drawDeck;
            private Deck _discardPile;
            private List<Hand> _playerHands;
            private string[] _suitPriorities;

            //Constructor:
            public GameState(string[] suitPriority, bool hasJokers)
            {
                int playerNum = 4;

                this._suitPriorities = suitPriority;
                _drawDeck = new Deck();
                _discardPile = new Deck();

                _drawDeck.Shuffle();

                _playerHands = new List<Hand>();

                for(int i = 0; i < playerNum; i++)
                {

                }
            }
        }
    }
}
