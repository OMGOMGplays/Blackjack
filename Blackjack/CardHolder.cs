public class CardHolder
{
    public virtual DeckOfCards cardDeck { get; set; } = new();

    public bool currentTurn;
    public bool standing;
    public bool unableToPlay;
    public int holderIndex;
    public int cardSum;

    // Resets all values, as you guessed it, on start.
    public virtual void OnStart()
    {
        cardSum = 0;

        standing = false;
        currentTurn = false;
        unableToPlay = false;
    }

    // The beating heart of the holder, allows them to play the game.
    public virtual void Update()
    {
        // Automatically sets currentTurn depending on the turn and the holderIndex.
        currentTurn = Blackjack.Instance.turn == holderIndex && !currentTurn ? true : false;

        // Blackjack! The holder has won.
        if (cardSum == 21)
        {
            Blackjack.Instance.WinGame(this, true);
        }

        // Holder has busted.
        if (cardSum > 21)
        {
            Bust();
        }

        // If it isn't your turn, don't do anything.
        if (!currentTurn)
        {
            return;
        }

        // If you can't play, skip your turn.
        if (unableToPlay && currentTurn)
        {
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
                break;
            }
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

    // Adds a card to the holder's list.
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
        unableToPlay = true;

        Blackjack.Instance.turn++;
        Console.WriteLine("\nYou've folded!");
    }

    // Keep where you are.
    public virtual void Stand()
    {
        standing = true;
        Blackjack.Instance.turn++;
        Console.WriteLine("\nStanding!");
    }

    // Holder has busted.
    public virtual void Bust()
    {
        unableToPlay = true;
        Console.WriteLine($"\nYou've busted!\nYour total worth ended up being {cardSum}.");
    }
}