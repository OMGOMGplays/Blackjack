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

        //for (int cards = 0; cards < CARDCOUNT; cards++)
        //{
        //    Card card = new();
        //    int suit = 0;
        //    int value = 1;

        //    if (suit > SUITCOUNT)
        //    {
        //        suit = 0;
        //    }

        //    if (value > VALUECOUNT + 1)
        //    {
        //        value = 1;
        //    }

        //    card.SetValueIndex(value);
        //    card.SetSuit(suit);

        //    value++;
        //    suit++;

        //    cardsRemaining[cards] = card;

        //}

        Card tempCard = null;

        for (int pack = 1; pack <= PACKCOUNT; pack++)
        {
            for (int suit = 0; suit < SUITCOUNT; suit++)
            {
                if (suit > SUITCOUNT)
                {
                    suit = 0;
                }

                for (int value = 1; value <= VALUECOUNT; value++)
                {
                    if (value > VALUECOUNT)
                    {
                        value = 1;
                    }

                    Card card = new(value, suit);
                    tempCard = new(card.GetValue(), card.GetSuit());
                }
            }
        }

        for (int i = 0; i < cardsRemaining.Length;)
        {
            if (cardsRemaining[i] == null)
            {
                cardsRemaining[i] = tempCard;

                Console.WriteLine($"Generated card #{i + 1} value & suit: {tempCard.GetValue()} : {tempCard.GetSuit()}");

                i++;
            }
        }

        Console.WriteLine($"\nGenerated {CARDCOUNT} cards!\n");
    }

    public void DealCard(int cardIndex, CardHolder receiver)
    {
        //        // Can't deal a null or already dealt card, try again with the next / prev one (depending on cardIndex)
        //        if (cardsRemaining[cardIndex] == null || cardsDealt[cardIndex] != null)
        //        {
        //            if (cardIndex == CARDCOUNT)
        //            {
        //                DealCard(cardIndex - 1, receiver);
        //            }
        //            else
        //            {
        //                DealCard(cardIndex + 1, receiver);
        //            }
        //        }

        //        cardsDealt[cardIndex] = cardsRemaining[cardIndex];
        //        cardsRemaining[cardIndex] = null;

        //        receiver.AddCard(cardsDealt[cardIndex]);
        //#if DEBUG
        //        Console.WriteLine($"Card #{cardIndex} dealt, value & suit: {Blackjack.Instance.GetValueFromIndex(cardsDealt[cardIndex].GetValueIndex())} (index {cardsDealt[cardIndex].GetValueIndex()}) : {cardsDealt[cardIndex].GetSuit()}");
        //#endif
    }
}