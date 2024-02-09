public class Card
{
	private int value;
	private Suit suit;

	public Card(int value, int suit)
	{
		this.value = value;
		this.suit = (Suit)suit;
	}

	public Card(int value, Suit suit)
	{
		this.value = value;
		this.suit = suit;
	}

	public int GetValue()
	{
		return value;
	}

	public Suit GetSuit()
	{
		return suit;
	}
}

public enum Suit
{
	Hearts,
	Diamonds,
	Clubs,
	Spades
}