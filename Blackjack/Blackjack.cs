public class Blackjack
{
	public const int MAXPLAYERS = 4;

	public static Blackjack Instance;
	public static HouseCards houseCards;

	private int desiredPlayers;
	private Player[] players = new Player[MAXPLAYERS];
	private Dealer dealer = new();
	private int playerTurnIndex;

	public static void Main()
	{
		if (Instance == null)
		{
			Instance = new Blackjack(); // Seems dodgy...

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

		// Set players
		SetPlayers();

		// House
		houseCards = new();
		houseCards.SetCardValues();

		// Dealer
		dealer.OnStart();

		// Player(s)
		for (int i = 0; i < desiredPlayers; i++)
		{
			Player player = new();
			player.OnStart();

			players[i] = player;
		}
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
				Environment.Exit(1); // Quitting!
			}

			Console.WriteLine("Input was NaN."); // Text isn't numbers, so ignore and retry
			SetPlayers();
		}
	}

	public int GetValueFromIndex(int index, CardHolder cardHolder)
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
				if ((cardHolder.totalWorth + 11) > 21) // If the cardHolder's current sum + potential sum of A > 21, A = 1
				{
					return 1;
				}

				return 11; // Otherwise A = 11
		}

		return -1; // Woopsie! No proper value, mark faulty card with -1
	}

	public int GetValueFromIndex(int index)
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
}