public class HouseCards
{
    public const int PACKCOUNT = 6; // 6 packs of cards -- change this one primarily
    public const int SUITCOUNT = 4; // 4 suits
    public const int VALUECOUNT = 13; // 2-A
    public const int CARDCOUNT = PACKCOUNT * SUITCOUNT * VALUECOUNT; // 6 packs * 4 suits * 13 values

    public Card[] cardsRemaining = new Card[CARDCOUNT];
    public Card[] cardsDealt = new Card[CARDCOUNT];

    public void SetCardValues()
    {
#if DEBUG
        Console.WriteLine($"\nHouseCards: CARDCOUNT = {CARDCOUNT}\n");
#endif

        for (int cards = 0; cards < CARDCOUNT; cards++)
        {
            Card card = new();

            cardsRemaining[cards] = card;

            for (int i = 1; i < VALUECOUNT + 1; i++)
            {
                cardsRemaining[cards].SetValueIndex(i);

                for (int j = 0; j < SUITCOUNT; j++)
                {
                    cardsRemaining[cards].SetSuit(j);
                }
            }

#if DEBUG                              // 0-indexed, + 1 for visuals
            Console.WriteLine($"Generated card #{cards + 1}, value & suit: {Blackjack.Instance.GetValueFromIndex(cardsRemaining[cards].GetValueIndex())} (index {cardsRemaining[cards].GetValueIndex()}) : {cardsRemaining[cards].GetSuit()}");
#endif
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
        Console.WriteLine($"Card #{cardIndex} dealt, value & suit: {Blackjack.Instance.GetValueFromIndex(cardsDealt[cardIndex].GetValueIndex())} (index {cardsDealt[cardIndex].GetValueIndex()}) : {cardsDealt[cardIndex].GetSuit()}");
#endif
    }
}