public class Blackjack
{
    public const int MAXPLAYERS = 4;

    public static Blackjack Instance;
    public static HouseCards houseCards = new();

    public bool gameOver;
    public int turn;

    private int desiredPlayers;
    private Player[] players = new Player[MAXPLAYERS];
    private Dealer dealer = new();
    private CardHolder[] cardHolders = new CardHolder[MAXPLAYERS + 1];

    public static void Main()
    {
        if (Instance == null)
        {
            Instance = new Blackjack(); // Seems dodgy... But, it works

#if DEBUG
            Console.WriteLine("Instance initiated.");
#endif
        }

        Console.WriteLine("Starting Blackjack...");

        Instance.Start();
    }

    private void Start()
    {
        // Visuals
        Console.Title = "Blackjack";
        Console.SetWindowSize(160, 45); // 1280x720p

        // Turns
        turn = 1; // Start on the players

        // Set players
        SetPlayers();

        // House
        houseCards.GenerateCards();

        // Dealer
        dealer.holderIndex = 0;
        dealer.OnStart();
        cardHolders[0] = dealer;

        // Player(s)
        for (int i = 1; i <= desiredPlayers; i++)
        {
            Player player = new();
            player.OnStart();
            player.holderIndex = i - 1;

            players[i - 1] = player;
            cardHolders[i] = player;
        }

        // Start Update
        Update();
    }

    private void SetPlayers()
    {
        Console.WriteLine($"\nHow many players are there, 1-{MAXPLAYERS}? Type Q to quit.");
        Console.Write("> "); // Flair
        string input = Console.ReadLine(); // Take the players input
        desiredPlayers = 0;

        if (int.TryParse(input, out int result)) // If the input is an int
        {
            if (result < 1) // Too few players
            {
                Console.WriteLine("Too few players! There cannot be fewer than 1 player in a match.");
                SetPlayers(); // Retry setting players
            }
            else if (result > MAXPLAYERS) // Too many players
            {
                Console.WriteLine($"Too many players! There can only be a maximum of {MAXPLAYERS} players in a match.");
                SetPlayers();
            }

            desiredPlayers = result; // Succeeded! Can now make players
        }
        else // Input is text
        {
            if (input.Contains("Q") || input.Contains("q"))
            {
                Environment.Exit(0); // Quitting!
            }

            Console.WriteLine("\nInvalid input."); // Text isn't numbers, so ignore and retry
            SetPlayers();
        }
    }

    // Infinite loop so that the application will never end.
    private void Update()
    {
        string input = "";

        // While the game isn't over, do its game loop.
        while (!gameOver)
        {
            for (int player = 0; player < desiredPlayers; player++)
            {
                Player currPlayer = players[player];

                if (currPlayer != null && currPlayer.currentTurn)
                {
                    // If player exists, call their Update function.
                    currPlayer.Update();
                }
            }

            dealer.Update();

            if (turn + 1 > desiredPlayers)
            {
                turn = 0;
            }

            foreach (CardHolder holder in cardHolders)
            {
                if (holder != null)
                {
                    // Double check to make sure the right player has their turn active.
                    // (First one being inside the CardHolder class itself.)
                    if (turn == holder.holderIndex && !holder.currentTurn)
                    {
                        holder.currentTurn = true;
                    }
                    else
                    {
                        holder.currentTurn = false;
                    }
                }
            }

            if (ShouldEndGame())
            {
                // Pause before game ends / closes.
                Thread.Sleep(4000); 
                gameOver = true;
            }
        }
    }

    // Handle game end requirements
    public bool ShouldEndGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                break;
            }

            if (players[i].unableToPlay)
            {
                Console.WriteLine("\nAll players unable to play! Dealer has won, ending game...");
                return true;
            }

            if (players[i].standing)
            {
                return Compare() == null ? false : true;
            }
        }

        // If a player has won thru e.g. Blackjack (cardSum == 21)
        if (WinGame())
        {
            return true;
        }

        // Shouldn't end game.
        return false;
    }

    public bool WinGame(CardHolder winner = null, bool win = false)
    {
        if (win)
        {
            Console.WriteLine($"\n{winner} {winner.holderIndex} has won! Ending game.");
        }

        return win;
    }

    private CardHolder Compare()
    {
        int i;
        CardHolder winner = new();

        for (i = 0; i < cardHolders.Length - 1; i++)
        {
            CardHolder holder = cardHolders[i];
            CardHolder nextHolder = cardHolders[i + 1];

            if (holder != null && nextHolder != null)
            {
                if (holder.cardSum > nextHolder.cardSum)
                {
                    winner = holder;
                }
                else
                {
                    winner = nextHolder;
                }
            }
        }

        return winner;
    }

    public int GetCardValue(int index, CardHolder cardHolder)
    {
        if (index >= 1 && index <= 9) // 2-10
        {
            return index + 1;
        }

        switch (index)
        {
            case 10: // J
            case 11: // Q
            case 12: // K
                return 10;

            case 13: // A
                if ((cardHolder.cardSum + 11) > 21) // If the cardHolder's current sum + potential sum of A (11) > 21, A = 1
                {
                    return 1;
                }

                return 11; // Otherwise A = 11
        }

        return -1; // Woopsie! No proper value, mark faulty card with -1
    }

    public int GetCardValue(int index)
    {
        if (index >= 1 && index <= 9) // 2-10
        {
            return index + 1;
        }

        switch (index)
        {
            case 10: // J
            case 11: // Q
            case 12: // K
                return 10;

            case 13: // A
                return 11;
        }

        return -1; // Woopsie! No proper value, mark faulty card with -1
    }

    public bool CheckInput(string input, string button)
    {
        return input.Contains(button.ToUpper()) || input.Contains(button.ToLower());
    }
}