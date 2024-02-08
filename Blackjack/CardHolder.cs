public class CardHolder
{
    public virtual DeckOfCards cardDeck { get; set; } = new();

    public bool currentTurn;
    public bool unableToPlay;
    public int holderIndex;
    public int cardSum;

    // Resets all values, as you guessed it, on start.
    public virtual void OnStart()
    {
        cardSum = 0;

        currentTurn = false;
        unableToPlay = false;
    }

    // The beating heart of the holder, allows them to play the game.
    public virtual void Update()
    {
        // Automatically sets currentTurn depending on the turn and the holderIndex.
        currentTurn = Blackjack.Instance.turn == holderIndex && !currentTurn ? true : false;

        if (cardSum == 21)
        {
            // Blackjack! The holder has won.
            Win(this);
        }
        else if (CheckForBust())
        {
            // If you reach this point, you've busted!
            Bust();
        }

        if (!currentTurn)
        {
            // If it isn't your turn, don't do anything.
            return;
        }

        if (unableToPlay && currentTurn)
        {
            // If you can't play, skip your turn.
            Blackjack.Instance.turn++;
        }
    }

    // Returns the amount of valid cards in the player's deck.
    // Was most likely temp and is left as residue of testing.
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

    // Adds a card to the players deck of cards.
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

    // Sums the holders cards up to a value.
    public virtual void SumCards(int input)
    {
        cardSum += input; // Hacky, tacky, buncha bologne if you ask me

#if DEBUG
        Console.WriteLine($"cardSum: {cardSum}");
#endif
    }

    // Does as its name suggests, if holder's cardSum is more than 21, they've busted.
    public virtual bool CheckForBust()
    {
        return cardSum > 21 ? true : false; // You've gone and busted my good man.
    }

    // Adds a card to the holder's list
    public virtual void Hit(bool incTurn = true, bool displayText = true)
    {
        Random random = new();

        Blackjack.houseCards.DealCard(random.Next(0, HouseCards.CARDCOUNT), this);

        if (incTurn)
        {
            Blackjack.Instance.turn++;
        }

        if (displayText)
        {
            Console.WriteLine("Hit! You've been dealt a card.");
        }
    }

    // No more playing for this holder!
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

    // Keep where you are.
    public virtual void Stand()
    {
        if (unableToPlay || !currentTurn)
        {
            return;
        }

        Blackjack.Instance.turn++;
        Console.WriteLine("\nStanding!");
    }

    // Holder has busted.
    public virtual void Bust()
    {
        unableToPlay = true;
        Console.WriteLine("\nYou've busted!");
    }

    // Holder has won, and the game should stop.
    private void Win(CardHolder winner)
    {
        Console.WriteLine($"{winner} {holderIndex + 1} has won! Ending game.");
        Blackjack.Instance.WinGame(true);
    }
}