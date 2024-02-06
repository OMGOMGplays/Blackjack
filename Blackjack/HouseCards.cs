public class HouseCards
{
    public const int PACKCOUNT = 6; // 6 packs of cards -- change this one primarily
    public const int SUITCOUNT = 4; // 4 suits
    public const int VALUECOUNT = 13; // 2-A
    public const int CARDCOUNT = PACKCOUNT * SUITCOUNT * VALUECOUNT; // 6 packs * 4 suits * 13 values

    public Card[] cardsRemaining = new Card[CARDCOUNT];

    public void GenerateCards()
    {
        int pack = 1;
        int suit = 0;
        int value = 1;
        Card card = new(0, 0);

#if DEBUG
        Console.WriteLine($"\nHouseCards: CARDCOUNT = {CARDCOUNT}\n");
#endif

        for (pack = 1; pack <= PACKCOUNT; pack++)
        {
            for (suit = 0; suit < SUITCOUNT; suit++)
            {
                //if (suit == SUITCOUNT + 1)
                //{
                //    suit = 0;
                //}

                for (value = 1; value <= VALUECOUNT; value++)
                {
                    //if (value == VALUECOUNT + 1)
                    //{
                    //    value = 1;
                    //}

                    card = ApplyToCard(value, suit);
                    
                    for (int i = 0; i < cardsRemaining.Length; i++)
                    {
                        if (cardsRemaining[i] == null)
                        {
                            cardsRemaining[i] = card;
                            continue;
                        }
                    }

                    //Card card = new(value, suit);
#if DEBUG
                    Console.WriteLine($"Generated card from pack #{pack} value & suit: {Blackjack.Instance.GetCardValue(card.GetValue())} (index {card.GetValue()}) : {card?.GetSuit()}");
#endif
                }
            }
        }

        Console.WriteLine($"\nGenerated {CARDCOUNT} cards!\n");
    }

    public void DealCard(int card, CardHolder receiver)
    {
        // Can't deal a null or already dealt card, try again with the next / prev one (depending on cardIndex)
        if (cardsRemaining[card] == null)
        {
            if (card == CARDCOUNT)
            {
                DealCard(card - 1, receiver);
            }
            else
            {
                DealCard(card + 1, receiver);
            }
        }

        if (cardsRemaining[card] == null) // Shouldn't be possible, but here to stop an error, for safety's sake
        {
            Console.WriteLine("Null card...\n");
            return;
        }

#if DEBUG
        Console.WriteLine($"Card #{card} dealt, value & suit: {Blackjack.Instance.GetCardValue(cardsRemaining[card].GetValue())} (index {cardsRemaining[card].GetValue()}) : {cardsRemaining[card].GetSuit()}");
#endif

        receiver.AddCard(cardsRemaining[card]);
        receiver.SumCards();

        cardsRemaining[card] = null;
    }

    private Card ApplyToCard(int value, int suit)
    {
        return new(value, suit);
    }
}