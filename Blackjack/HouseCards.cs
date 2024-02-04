public class HouseCards
{
	public const int CARDCOUNT = 312; // 4 types * 13 cards * 6 packs of cards
	public const int CARDVALUES = 13; // 2-A

	public Card[] cardsRemaining = new Card[CARDCOUNT];
	public Card[] cardsDealt = new Card[CARDCOUNT];

	public void SetCardValues()
	{
		for (int cards = 0; cards < CARDCOUNT; cards++)
		{
			Card card = new();

			for (int valueIndex = 1; valueIndex < CARDVALUES; valueIndex++) // Not zero-indexed, cause there aren't cards whose values are < 2
			{
				card.SetValueIndex(valueIndex);

				for (int suit = 0; suit < 4; suit++)
				{
					card.SetSuit(suit);

					cardsRemaining[cards] = card;
#if DEBUG                                            // 0-indexed, + 1 for visuals
					Console.WriteLine($"Generated new card, {cards + 1}, value & suit: {Blackjack.Instance.GetValueFromIndex(cardsRemaining[cards].GetValueIndex())} (index {cardsRemaining[cards].GetValueIndex()}) : {cardsRemaining[cards].GetSuit()}");
#endif
				}
			}
		}

		Console.WriteLine($"\nGenerated {CARDCOUNT} cards!\n");
	}

	public void DealCard(int cardIndex, CardHolder receiver)
	{
		// Can't deal a null or already dealt card, try again with the next / prev one (depending on cardIndex)
		if (cardsRemaining[cardIndex] == null || cardsDealt[cardIndex] != null)
		{
			if (cardIndex == CARDCOUNT)
			{
				DealCard(cardIndex - 1, receiver);
			}
			else
			{
				DealCard(cardIndex + 1, receiver);
			}
		}

		cardsDealt[cardIndex] = cardsRemaining[cardIndex];
		cardsRemaining[cardIndex] = null;

		receiver.AddCard(cardsDealt[cardIndex]);
#if DEBUG
		Console.WriteLine($"Card {cardIndex} has been dealt, its value & suit: {Blackjack.Instance.GetValueFromIndex(cardsDealt[cardIndex].GetValueIndex())} (index {cardsDealt[cardIndex].GetValueIndex()}) : {cardsDealt[cardIndex].GetSuit()}");
#endif
	}
}