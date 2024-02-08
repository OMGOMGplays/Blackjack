﻿public class Blackjack
{
    public const int MAXPLAYERS = 4;

    public static Blackjack Instance;
    public static HouseCards houseCards = new();

    public bool gameOver;
    public int turn;

    private int desiredPlayers;
    private Player[] players = new Player[MAXPLAYERS];
    private Dealer dealer = new();

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
        turn = 0;

        // Set players
        SetPlayers();

        // House
        houseCards.GenerateCards();

        // Dealer
        dealer.holderIndex = 0;
        dealer.OnStart();

        // Player(s)
        for (int i = 1; i < desiredPlayers; i++)
        {
            Player player = new();
            player.OnStart();
            player.holderIndex = i;

            players[i] = player;
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
            if (result < 1)
            {
                Console.WriteLine("Too few players! There cannot be fewer than 1 player in a match.");
                SetPlayers(); // Retry setting players
            }
            else if (result > MAXPLAYERS)
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

    // Infinite loop so that the application will never end
    private void Update()
    {
        string input = "";

        // While the game isn't over, do its game loop
        while (!gameOver)
        {
            for (int player = 0; player < desiredPlayers; player++)
            {
                Player currPlayer = players[player];

                if (currPlayer != null && currPlayer.currentTurn)
                {
                    currPlayer.Update(); // If Player exists, call their Update function
                }
            }

            dealer.Update();

            if (turn > desiredPlayers)
            {
                turn = 0;
            }

            if (ShouldEndGame())
            {
                Thread.Sleep(4000); // Pause before game closes.
                gameOver = true;
            }
        }
    }

    // Handle game end requirements
    public bool ShouldEndGame(bool end = false)
    {
        foreach (Player player in players) // If all players can't play, dealer wins and game ends
        {
            if (player != null && player.unableToPlay)
            {
                Console.WriteLine("\nAll players unable to play and the dealer wins! Ending game...");
                return true;
            }
            else
            {
                break;
            }
        }

        if (dealer.CheckForBust()) // If the dealer's busted, end the game
        {
            Console.WriteLine($"\nDealer busted! They reached {dealer.cardSum}.");
            return true;
        }

        if (end)
        {
            return end;
        }

        // Shouldn't end game.
        return false;
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