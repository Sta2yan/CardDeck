using System;
using System.Collections.Generic;

namespace CardDeck
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Deck deck = new Deck();
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"{(int)MenuCommands.TakeCard}. {MenuCommands.TakeCard}" +
                                  $"\n{(int)MenuCommands.ChangeDeck}. {MenuCommands.ChangeDeck}" +
                                  $"\n{(int)MenuCommands.LookPlayerCard}. {MenuCommands.LookPlayerCard}" +
                                  $"\n{(int)MenuCommands.Exit}. {MenuCommands.Exit}");

                MenuCommands userInput = (MenuCommands)GetNumber(Console.ReadLine());

                switch (userInput)
                {
                    case MenuCommands.TakeCard:
                        player.TakeCard(deck);
                        break;
                    case MenuCommands.ChangeDeck:
                        deck = new Deck();
                        break;
                    case MenuCommands.LookPlayerCard:
                        player.ShowInfo();
                        break;
                    case MenuCommands.Exit:
                        isOpen = false;
                        break;
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine("Выход ...");
        }

        static int GetNumber(string textNumber)
        {
            int number;

            while (int.TryParse(textNumber, out number) == false)
            {
                Console.WriteLine("Повторите попытку:");
                textNumber = Console.ReadLine();
            }

            return number;
        }
    }

    enum MenuCommands
    {
        TakeCard = 1,
        ChangeDeck,
        LookPlayerCard,
        Exit
    }

    enum CardType
    {
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    interface IShowInfo
    {
        public void ShowInfo();
    }

    class Player : IShowInfo
    {
        private List<Card> _cards = new List<Card>();

        public void TakeCard(Deck deck)
        {
            Console.Clear();
            Card card = deck.GetCard();

            if (card != null)
            {
                _cards.Add(deck.GetCard());
            }
            else
            {
                Console.WriteLine("В этой колоде нет карт");
            }
        }

        public void ShowInfo()
        {
            Console.Clear();

            if (_cards.Count > 0)
            {
                Console.WriteLine("Ваши карты:");

                foreach (var card in _cards)
                {
                    card.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("У вас нет карт");
            }
        }
    }

    class Deck
    {
        private Stack<Card> _cards = new Stack<Card>();
        private Random _random = new Random();

        public Deck(Stack<Card> cards = null)
        {
            if (cards == null)
                SetDefaultStackCards();
        }

        public Card GetCard()
        {
            Card card;
            _cards.TryPop(out card);

            return card;
        }

        private void SetDefaultStackCards()
        {
            int maximumCard = 32;
            int maximumCardType = 8;

            for (int i = 0; i < maximumCard; i++)
            {
                _cards.Push(new Card((CardType)_random.Next(0, maximumCardType)));
            }
        }
    }

    class Card : IShowInfo
    {
        public CardType Type { get; private set; }

        public Card(CardType cardType)
        {
            Type = cardType;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Карта - {Type}");
        }
    }
}