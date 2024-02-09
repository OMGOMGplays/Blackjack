public class Player : CardHolder
{
    public int money = 0;

    public override void OnStart()
    {
        base.OnStart();

        // There's most likely a much better way of doing this
        for (int i = 0; i < 2; i++)
        {
            Hit(false);
        }

        Blackjack.Instance.turn++;
    }

    public override void Update()
    {
        base.Update();

        if (!unableToPlay)
        {
            GetInput();
        }
    }

    private void GetInput()
    {
        string instructions = ""; // Instructions, temp null for checks
        string input = ""; // To get the player's input

        if (instructions == "")
        {   // Print instructions
            instructions = $"Player {holderIndex}, type H to hit, F to fold, and S to stand\nYour current cards total up to {cardSum}";
            Console.WriteLine($"\n{instructions}\n");
            Console.Write("> "); // Input flair
            input = Console.ReadLine();
        }

        if (Blackjack.Instance.CheckInput(input, "H")) // Hit
        {
            Hit();
        }
        else if (Blackjack.Instance.CheckInput(input, "F")) // Fold
        {
            Fold();
        }
        else if (Blackjack.Instance.CheckInput(input, "S")) // Stand
        {
            Stand();
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
