public class Player : CardHolder
{
    public int money = 0;

    public override void OnStart()
    {
        base.OnStart();

        Random random = new Random();

        // There's most likely a much better way of doing this
        for (int i = 0; i < 2; i++)
        {
            Blackjack.houseCards.DealCard(random.Next(0, HouseCards.CARDCOUNT), this);
        }
    }

    public override void Update()
    {
        if (currentTurn && !unableToPlay)
        {
            GetInput();

            if (CheckForBust()) // Check if the player has busted...
            {
                Bust();
            }
        }
    }

    private void GetInput()
    {
        string instructions = ""; // Instructions, temp null for checks
        string input = ""; // To get the player's input

        if (instructions == "")
        {   // Print instructions
            instructions = "Type H to hit, F to fold, and D to double down";
            Console.WriteLine($"\n{instructions}\n");
            Console.Write("> ");
            input = Console.ReadLine();
        }

        if (Blackjack.Instance.CheckInput(input, "h")) // Hit
        {
            Hit();
        }
        else if (Blackjack.Instance.CheckInput(input, "f")) // Fold
        {
            Fold();
        }
        else if (Blackjack.Instance.CheckInput(input, "d")) // Double down
        {
            DoubleDown();
        }
        else // No corresponding action.
        {
            Console.WriteLine("\nInvalid input.");
        }

        if (!currentTurn || unableToPlay)
        {
            // Hides instructions if it isn't the player's turn
            if (instructions != "")
            {
                instructions = "";
            }
        }
    }
}
