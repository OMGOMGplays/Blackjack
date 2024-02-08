public class CardHolder
{
    public virtual DeckOfCards cardDeck { get; set; } = new();
    public virtual int cardSum { get; set; }

    public bool currentTurn;
    public bool unableToPlay;
    public int holderIndex;

    public virtual void OnStart()
    {
        cardSum = 0;

        currentTurn = false;
        unableToPlay = false;
    }

    public virtual void Update()
    {
        if (Blackjack.Instance.turn == holderIndex)
        {
            currentTurn = true;
        }

        if (cardSum == 21)
        {
            Win(this);
        }

        if (!currentTurn)
        {
            return;
        }

        if (unableToPlay && currentTurn)
        {
            Blackjack.Instance.turn++;
        }

        if (CheckForBust())
        {
            Bust();
        }
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

    public virtual void SumCards(int input)
    {
        cardSum += input; // Hacky, tacky, buncha bologne if you ask me

#if DEBUG
        Console.WriteLine($"cardSum: {cardSum}");
#endif
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
        Blackjack.Instance.turn++;

        Console.WriteLine("Hit! You've been dealt a card.");
    }

    // No more playing for this player!
    public virtual void Fold()
    {
        if (!currentTurn)
        {
            return;
        }

        Blackjack.Instance.turn++;
        Console.WriteLine("\nYou've folded!");

        unableToPlay = true;

    }

    // Keep where you are
    public virtual void Stand()
    {
        if (unableToPlay || !currentTurn)
        {
            return;
        }

        Blackjack.Instance.turn++;
        Console.WriteLine("\nStanding!");
    }

    // Holder has busted
    public virtual void Bust()
    {
        unableToPlay = true;
        Console.WriteLine("\nYou've busted!");
    }

    private void Win(CardHolder winner)
    {
        Console.WriteLine($"{winner} {holderIndex} has won! Ending game.");
        Blackjack.Instance.ShouldEndGame(true);
    }
}