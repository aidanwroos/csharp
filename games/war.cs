using System;
using System.Collections.Generic;

public class Card
{
	public string Suit { get; set; }
	public string Rank { get; set; }
	private static Dictionary<string, int> _rankValues;
	
	static Card()
	{
		_rankValues = new Dictionary<string, int>();
		
		for(int i = 2; i <= 10; i++)
			_rankValues.Add(i.ToString(), i);
		
		_rankValues.Add("Jack", 11);
		_rankValues.Add("Queen", 12);
		_rankValues.Add("King", 13);
		_rankValues.Add("Ace", 14);
	}
	
	//return rank value for given rank
	public int Value
	{
		get { return _rankValues[Rank]; }
	}
	
	//card constructor
	public Card(string suit, string rank)
	{
		Suit = suit;
		Rank = rank;
	}
	
	//prints the card details
	public override string ToString()
	{
		return $"{Rank} of {Suit} with value {Value}";
	}
}

public class Deck
{
	private List<Card> _cards; // A complete 52 card deck
	private static Random _random = new Random(); // created once, shared
	
	//Deck constructor
	public Deck()
	{
		_cards = new List<Card>();
		BuildDeck(); //builds deck of cards
	}
	
	//method function to build the 52 card deck
	private void BuildDeck()
	{
		string[] suits = {"Hearts", "Diamonds", "Clubs", "Spades"};
		string[] ranks = {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
		
		// (4 * 13 = 52)
		foreach (string suit in suits)
			foreach (string rank in ranks)
				_cards.Add(new Card(suit, rank));
		
		Console.WriteLine($"Deck built with {_cards.Count} cards");
	}
	
	//draw a single card from the deck
	public Card Deal()
	{
		if(_cards.Count == 0)
			throw new Exception("No cards left in the deck!");
		
		Card card = _cards[0]; //draw from top of the deck
		_cards.RemoveAt(0);
		return card;           //return the card object
	}
	
	public void printDeck()
	{
		for(int i=0; i < _cards.Count; i++)
		{
			Console.WriteLine($"{_cards[i].ToString()}");
		}
	}
	
	//completely shuffle the deck
	public void Shuffle()
	{
		
		for(int i = _cards.Count - 1; i > 0; i--)
		{
			int j = _random.Next(0, i + 1); //pick random index
			Card temp = _cards[i];         //swap the cards
			_cards[i] = _cards[j];
			_cards[j] = temp;
		}
		Console.WriteLine("Deck is shuffled!");
	}
}

public class Player
{
	private string _name;
	private List<Card> _hand;
	
	//player name property (getters and setters essentially)
	public string Name
	{
		get {return _name;}
		set 
		{
			if(string.IsNullOrEmpty(value))
				throw new Exception("Player name can't be empty!");
			_name = value;
		}
	}
	
	//player hand property
	public List<Card> Hand
	{
		get {return _hand;}
	}
	
	//player constructor
	public Player(string name)
	{
		Name = name;
		_hand = new List<Card>();
		Console.WriteLine($"Player with name {name} created!");
	}
	
	//draw card method
	public void DrawCard(Card card)
	{
		_hand.Add(card);
	}
	
	//display cards in player's hand
	public void PrintHand()
	{
		Console.WriteLine("\n" + Name + "\'s hand:");  
						  
		for(int i=0; i < _hand.Count; i++)
		{
			Console.WriteLine($"{_hand[i].ToString()}");
		}
	}
}

public class Program
{
	public static void Main()
	{
		Deck deck = new Deck();
		deck.Shuffle();
		
		Player player1 = new Player("Aidan");
		Player player2 = new Player("Computer");
		
		for(int i=0; i<26; i++)
		{
			player1.DrawCard(deck.Deal());
			player2.DrawCard(deck.Deal());
		}
		
		player1.PrintHand();
		
		
		
	}
}
