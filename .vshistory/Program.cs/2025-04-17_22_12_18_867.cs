using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
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

            bool mainLoop = true; //The main loop (while true, menu continues appearing. Becomes false if user quits.)
            GameState gameState = null; //Useful for later

            while (mainLoop)
            {
                Console.WriteLine("\n");

                Console.WriteLine("Please choose below:\n");
                Console.WriteLine("1- Setup Game");
                Console.WriteLine("2- Deal Hands");
                Console.WriteLine("3- Display Gameboard");
                Console.WriteLine("4- Quit\n");
                Console.Write("Choice: ");

                byte menuChoice = MenuChoiceValidation(); //Processing the user's choice

                //The main switch loop depending on the previous user input:
                switch (menuChoice)
                {
                    case 1:
                        SetUpGame(ref gameState);         
                    break;

                    case 2:
                        //Making sure the game was actually set up:
                        if(gameState == null)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You need to set up a game first.");
                            Console.ForegroundColor= ConsoleColor.White;
                        }
                        else
                            DealHands(ref gameState);
                        
                            
                    break;

                    case 3:
                        //Making sure the game was actually set up:
                        if (gameState == null)
                        {
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You need to set up a game first.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            try
                            {
                                Console.Clear(); //Keeping the console clean

                                Console.OutputEncoding = Encoding.UTF8; //Have to put this to get the suit symbols                               
                                Console.WriteLine(gameState.ToString());

                                Console.WriteLine("\n");

                                //Discarding cards?
                                DiscardCard(ref gameState);
                                Console.WriteLine(gameState.ToString());

                                //I'm honestly not sure what else I'm supposed to do here. Try out all the class methods?
                            }
                            catch(Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;
                            }                      
                        }               
                    break;

                    case 4:
                        mainLoop = false;
                    break;
                }
            }

            Console.WriteLine(); //Space

            Console.Write("Press any key to exit the program.");
            Console.ReadLine();
        }



        /* CARD CLASS!!!
         * 
         * Algorithm:
         * The Card class represents an individual playing card.
         * It has three private fields: _rank, _suit, and _color.
         * Three constructors allow card creation based on rank/suit, color (Jokers), or special index values (53/54).
         * Properties expose the internal fields for external access.
         * ToString() returns a readable string version of the card.
         * Equals() checks if two cards are identical.
         * GetHashCode() returns a unique hash based on card attributes.
         * 
         * Parameters:
         * - string suit, rank / string color / int index depending on constructor used
         * 
         * Return Values:
         * - Varies: Card object, formatted string, boolean, or hash code
         * 
         * Exceptions:
         * - ArgumentException for invalid suit/rank/index inputs
         * 
         * Pseudocode:
         * IF using suit + rank:
         *     VALIDATE inputs
         *     SET fields
         * ELSE IF using color:
         *     SET rank to "Joker"
         *     SET suit to null
         *     SET color to input
         * ELSE IF using index:
         *     IF index == 53 -> Red Joker
         *     IF index == 54 -> Black Joker
         *     ELSE throw exception
         */
        public class Card
        {
            //Fields:
            private string _rank;
            private string _suit;
            private string _color;


            //Constructors:
            public Card(string suit, string rank)
            {
                _rank = rank;
                _suit = suit;

                if (suit == "Hearts" || suit == "Diamonds")
                    this._color = "Black";
                else if (suit == "Clubs" || suit == "Spades")
                    this._color = "Black";
                else
                    throw new ArgumentException("Invalid suit provided.");

                string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen", "Ace" };

                if(!ranks.Contains(rank ))
                    throw new ArgumentException("Invalid rank provided.");

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

                if (this._rank == "Joker" && this._color == "Red")
                    return "RJ";
                else if (this._rank == "Joker" && this._color == "Black")
                    return "BJ";

                if (this._suit == "Diamonds")
                    return $"{this._rank} of \u2666";
                else if (this._suit == "Clubs")
                    return $"{this._rank} of \u2663";
                else if (this._suit == "Spades")
                    return $"{this._rank} of \u2660";
                else if (this._suit == "Hearts")
                    return $"{this._rank} of \u2665";

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


        /* DECK CLASS!!!
         * 
         * Algorithm:
         * The Deck class represents a collection of Card objects.
         * It can be constructed as an empty list, standard deck, or custom list.
         * Cards can be drawn, peeked at, shuffled, or placed back on top.
         * 
         * Parameters:
         * - bool hasJoker / List<Card> newCustomCards
         * 
         * Return Values:
         * - Draw() returns a Card or null
         * - Peek() returns the top card
         * - ToString() returns string summary
         * 
         * Exceptions:
         * - None directly thrown in methods (nulls returned instead)
         * 
         * Pseudocode:
         * On construction:
         *     IF hasJoker true:
         *         Add all 52 standard cards + Jokers
         * Draw():
         *     RETURN top card and remove from list
         * Peek():
         *     RETURN top card without removing
         * Shuffle():
         *     RANDOMIZE the order of cards
         * PlaceOnTop():
         *     INSERT card at index 0
         */
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
                int n = _cardsList.Count; 

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
                    return null;

                return _cardsList[0]; //Returning the top card of the deck (no removing)
            }

            public void PlaceOnTop(Card card)
            {
                _cardsList.Insert(0, card); //Adding a provided card to the top of the deck
            }


            //Override method:
            public override string ToString()
            {
                return $"There are {this.CardsLeft} cards left.";
            }
        }


        /* HAND CLASS!!!
         * 
         * Algorithm:
         * Represents a player's hand.
         * Stores cards and keeps them sorted by a priority array.
         * Allows cards to be added, removed, or searched.
         * 
         * Parameters:
         * - string[] suitPriority (constructor)
         * 
         * Return Values:
         * - AddCard(): void
         * - RemoveCard(): returns Card
         * - Contains(): returns bool
         * 
         * Exceptions:
         * - None (assumes valid inputs)
         * 
         * Pseudocode:
         * AddCard():
         *     ADD to list
         *     IF hand is full -> CALL OrderBySuit()
         * RemoveCard():
         *     REMOVE first card
         *     CALL OrderBySuit()
         * Contains():
         *     RETURN if card exists in list
         * OrderBySuit():
         *     Sort based on priority array
         */
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
                _cardsList = new List<Card>();
                _suitPriority = suitPriority;
            }

            //Methods:
            public void AddCard(Card card)
            {
                _cardsList.Add(card); //Adding a card to the hand

                if(_cardsList.Count == 4)
                    OrderBySuit(); //Reordering the hand accordingly once all the cards has been distributed to the hand
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

            public void OrderBySuit() //There's a problem with this :(
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
                return string.Join(", ", _cardsList.Select(card => card.ToString())); //Displaying the hand nicely
            }

        }


        /* GAME STATE CLASS!!!
         * 
         * Algorithm:
         * Manages the entire game environment.
         * Initializes deck, discard pile, and player hands.
         * Handles dealing, drawing, discarding, and displaying the game state.
         * 
         * Parameters:
         * - bool hasJokers, string[] suitPriority (constructor)
         * 
         * Return Values:
         * - Varies: string, Card, void
         * 
         * Exceptions:
         * - Throws ArgumentException if trying to deal with not enough cards
         * - Throws ArgumentException if trying to draw from an empty deck
         * 
         * Pseudocode:
         * On setup:
         *     INIT draw deck + discard pile
         *     SHUFFLE deck
         *     INIT player hands
         * Deal():
         *     VALIDATE enough cards
         *     DEAL 4 cards per player
         * DiscardCard():
         *     PLACE card on discard pile
         * DrawCard():
         *     VALIDATE deck not empty
         *     RETURN drawn card
         * ToString():
         *     RETURN full game summary
         */
        public class GameState
        {
            //Fields:
            private Deck _drawDeck;
            private Deck _discardPile;
            private List<Hand> _playerHands;
            private string[] _suitPriorities;


            //Constructor:
            public GameState(bool hasJokers, string[] suitPriority)
            {
                int playerNum = 4;

                _suitPriorities = suitPriority;
                _drawDeck = new Deck(hasJokers);
                _discardPile = new Deck();

                _drawDeck.Shuffle();

                _playerHands = new List<Hand>();

                for(int i = 0; i < playerNum; i++)
                {
                    _playerHands.Add(new Hand(suitPriority)); //Initialize empty hands with suit priorities
                }
            }


            //Properties:
            public int CardsLeft
            {
                get { return _drawDeck.CardsLeft; }
            }

            public int DiscardPileSize
            {
                get { return _discardPile.CardsLeft; }
            }

            //public List<Hand> PlayerHands
            //{
            //    get { return _playerHands; }
            //}
             

            //Methods:
            public void Deal()
            {
                //Making sure there are enough cards for everyone:
                int cardsPerPlayerValid = _drawDeck.CardsLeft / _playerHands.Count;
                if(cardsPerPlayerValid == 0)
                {
                    throw new ArgumentException("Not enough cards to deal.");
                }

                
                int cardsPerPlayer = 4; //Deciding that each player will have 4 cards

                for (int i = 0; i < _playerHands.Count; i++)
                {
                    for(int j = 0;  j < cardsPerPlayer; j++)
                    {
                        Card dealtCard = _drawDeck.Draw(); //Drawing from the deck
                        _playerHands[i].AddCard(dealtCard); //Adding the drawn card to the player's hand
                    }
                }
            }

            public void DiscardCard(Card card)
            {
                _discardPile.PlaceOnTop(card); //Placing a card on top of the discard pile 
            }

            public Card DrawCard()
            {
                if (CardsLeft == 0)
                    throw new ArgumentException("No cards left in the draw deck.");

                Card drawnCard = _drawDeck.Draw();

                return drawnCard;
            }

            public void OrderHands()
            {
                foreach(Hand hand in _playerHands)
                {
                    hand.OrderBySuit(); //Ordering each hand by suit
                }
            }

            public void DisplayGameState()
            {
                Console.WriteLine(this.ToString()); //Displaying the game??? Do I really need anohter method for that?
            }


            //Override method:
            public override string ToString()
            {
                string gameStateDisplay = "Game State:\n"; //The main string that will be returned (we're adding to it)

                //Displaying each player's hand
                for(int i = 0; i < _playerHands.Count; i++)
                {
                    gameStateDisplay += $"Player {i + 1}'s Hand: {_playerHands[i]}\n";
                }

                //Displaying the remaining cards in the draw deck
                gameStateDisplay += $"Draw Deck: {CardsLeft} cards left.\n";

                //Display discard pile
                if (_discardPile.Peek() == null)
                    gameStateDisplay += "No cards in the discard pile.";
                else
                    gameStateDisplay += $"Discard Pile: {DiscardPileSize} cards (Top card: {_discardPile.Peek()})\n";

                return gameStateDisplay;
            }
        }


        /*SetUpGame():
         * Algorithm:
         * Initializes the game environment by creating a new GameState object using predefined values.
         * This includes enabling jokers and assigning a fixed suit priority.
         * Displays a confirmation message upon success, or an error message if initialization fails.
         * 
         * Parameters:
         * - ref GameState gameState: passed by reference and initialized inside the method
         * 
         * Return Value:
         * - None (void)
         * 
         * Exceptions:
         * - Catches and displays any exception thrown during GameState initialization
         * 
         * Pseudocode:
         * TRY:
         *     SET hasJokers to true
         *     SET suitPriority to a fixed string array
         *     CREATE new GameState with these values
         *     DISPLAY success message
         * CATCH exception:
         *     DISPLAY error message
         */
        static void SetUpGame(ref GameState gameState)
        {
            try
            {
                //Setting up the jokers (can change between true and false)
                bool hasJokers = true;

                //Seeting  up the suit priority (Options: Hearts, Diamonds, Clubs, Spades)
                string[] suitPriority = { "Diamonds", "Clubs", "Hearts", "Spades" };

                //Initializing game state with the joker boolean and the suit priority 
                gameState = new GameState(hasJokers, suitPriority);

                //Little success message:
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game successfully set up.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /*DealHands():
         * Algorithm:
         * Attempts to deal cards to all players using the Deal() method of the GameState class.
         * If successful, it shows a confirmation message. If there's an issue (like not enough cards), it catches and displays the error.
         * 
         * Parameters:
         * - ref GameState gameState: reference to the GameState object that manages players and decks
         * 
         * Return Value:
         * - None (void)
         * 
         * Exceptions:
         * - Catches and displays exceptions that might occur during the Deal() process
         * 
         * Pseudocode:
         * TRY:
         *     CALL gameState.Deal()
         *     DISPLAY success message
         * CATCH exception:
         *     DISPLAY error message
         */
        static void DealHands(ref GameState gameState)
        {
            try
            {
                //Dealing
                gameState.Deal();

                //Little success message:
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hands sucessfully dealt to all players.");
                Console.ForegroundColor = ConsoleColor.White;

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        static void DiscardCard(ref GameState gameState) //I don't understand what the discard pile does. I don't play cards. 
        {
            Card cardToDisguard1 = gameState.DrawCard();
            Card cardToDisguard2 = gameState.DrawCard();
            Card cardToDisguard3 = gameState.DrawCard();


            gameState.DiscardCard(cardToDisguard1);
            gameState.DiscardCard(cardToDisguard2);
            gameState.DiscardCard(cardToDisguard3);
        }



        /* MenuChoiceValidation():
         * Algorithm:
         * This method prompts the user to enter a menu option and ensures it is a valid byte (1–4).
         * It continues prompting until the input is valid and within range.
         * 
         * Parameters: None
         * Return Value: byte - the valid menu choice selected by the user
         * Exceptions: None (user is re-prompted on invalid input)
         * 
         * Pseudocode:
         * DO:
         *     PROMPT user for input
         *     TRY to parse input to byte
         *     IF parse succeeds AND value in range:
         *         RETURN value
         *     ELSE:
         *         DISPLAY error and retry
         */
        static byte MenuChoiceValidation()
        {
            const byte MAX_MENU_CHOICE = 4; //Last choice for main menu
            const byte MIN_CHOICE_NUM = 1;

            byte userInput;
            bool successfulConversion;

            successfulConversion = byte.TryParse(Console.ReadLine(), out userInput);

            while (successfulConversion == false || userInput > MAX_MENU_CHOICE || userInput < MIN_CHOICE_NUM)
            {
                Console.Write($"What you entered was not a valid choice. Try again: ");
                successfulConversion = byte.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }


    }
}
