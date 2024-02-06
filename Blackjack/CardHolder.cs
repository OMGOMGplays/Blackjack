public class CardHolder
{
    public virtual DeckOfCards cardDeck { get; set; } = new();
    public virtual int cardSum { get; set; }

    public bool currentTurn;
    public bool unableToPlay;

    public virtual void OnStart()
    {
        cardSum = 0;

        currentTurn = false;
        unableToPlay = false;
    }

    public virtual void Update()
    {

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
    }

    public virtual void SumCards()
    {
        //		foreach (Card card in cardDeck.cards)
        //		{
        //			cardSum += Blackjack.Instance.GetCardValue(card.GetValue(), this);
        //#if DEBUG
        //			Console.WriteLine($"cardSum: {cardSum}");
        //#endif
        //		}

        for (int i = 0; i < cardDeck.cards.Length;)
        {
            if (cardDeck.cards[i] != null)
            {
                cardSum += Blackjack.Instance.GetCardValue(cardDeck.cards[i].GetValue(), this);
#if DEBUG
                Console.WriteLine($"cardSum: {cardSum}");
#endif
                i++;
            }

            break;
        }
    }

    public virtual bool CheckForBust()
    {
        return cardSum > 21 ? true : false; // You've gone and busted my good man.
    }

    // Adds a card to the player's list
    public virtual void Hit()
    {
        Random random = new();

        if (unableToPlay || !currentTurn)
        {
            return;
        }

        Blackjack.houseCards.DealCard(random.Next(0, HouseCards.CARDCOUNT), this);

        currentTurn = false;

        Console.WriteLine("Hit! You've been dealt a card.");
    }

    // For those who've busted or folded
    public virtual void Skip()
    {
        currentTurn = false;
    }

    // No more playing for this player!
    public virtual void Fold()
    {
        if (!currentTurn)
        {
            return;
        }

        unableToPlay = true;

        Console.WriteLine("You've folded!");
    }

    // Double or nuthin'
    public virtual void DoubleDown()
    {
        if (unableToPlay || !currentTurn)
        {
            return;
        }

        currentTurn = false;

        Console.WriteLine("Doubling down!");
    }

    // Holder has busted
    public virtual void Bust()
    {
        unableToPlay = true;
        Console.WriteLine("You've busted!");
    }
}