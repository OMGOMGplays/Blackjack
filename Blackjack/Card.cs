public class Card
{
	private int valueIndex;
	private Suit suit;

	public void SetValueIndex(int valueIndex)
	{
		this.valueIndex = valueIndex;
	}

	public void SetSuit(int suit)
	{
		this.suit = (Suit)suit;
	}

	public int GetValueIndex()
	{
		return valueIndex;
	}

	public Suit GetSuit()
	{
		return suit;
	}
}

public enum Suit
{
	Hearts = 0,
	Diamonds,
	Clubs,
	Spades
}