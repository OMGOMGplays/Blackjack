public class CardHolder
{
	public virtual DeckOfCards cardDeck { get; set; } = new();
	public virtual int totalWorth { get; set; }

	private bool currentTurn;
	private bool folded;

	public virtual void OnStart()
	{
		totalWorth = 0;

		cardDeck.OnStart();

		currentTurn = false;
		folded = false;
	}

	public virtual int GetCardCount()
	{
		int temp = 0;

		for (int i = 0; i < cardDeck.cards.Count(); i++)
		{
			if (cardDeck.cards[i] != null)
			{
				temp++;
			}
		}

		return temp;
	}

	public virtual void AddCard(Card card)
	{
		for (int i = 0; i < cardDeck.cards.Count(); i++)
		{
			if (cardDeck.cards[i] == null)
			{
				cardDeck.cards[i] = card;
			}

			continue;
		}

#if DEBUG
		Console.WriteLine($"Added new card, its value and suit is: {Blackjack.Instance.GetValueFromIndex(card.GetValueIndex(), this)} : {card.GetSuit()}");
#endif
	}

	public virtual void SumCards()
	{
		foreach (Card card in cardDeck.cards)
		{
			totalWorth += Blackjack.Instance.GetValueFromIndex(card.GetValueIndex(), this);
#if DEBUG
			Console.WriteLine($"totalWorth: {totalWorth}");
#endif
		}
	}

	public virtual bool CheckForBust()
	{
		return totalWorth > 21 ? true : false; // You've gone and busted my good man.
	}

	// Adds a card to the player's list
	public virtual void Hit()
	{
		Random random = new();

		if (folded || !currentTurn)
		{
			return;
		}

		Blackjack.houseCards.DealCard(random.Next(0, HouseCards.CARDCOUNT), this);
	}

	// Primarily for dealer, though might not be used
	public virtual void Skip()
	{

	}

	// No more playing for this player!
	public virtual void Fold()
	{
		if (!currentTurn)
		{
			return;
		}

		folded = true;
	}

	// Double or nuthin'
	public virtual void DoubleDown()
	{
		if (folded || !currentTurn)
		{
			return;
		}
	}
}