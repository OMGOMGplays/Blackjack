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
        string instructions = ""; // Null in string

        if (currentTurn)
        {
            string input = Console.ReadLine(); // Get player's input

            if (instructions == "")
            {   // Print instructions
                instructions = "Press H to hit, F to fold, and D to double down";
                Console.WriteLine($"\n{instructions}\n");
            }

            // Hit
            if (input.Contains("H") || input.Contains("h"))
            {
                Hit();
            }
        }
        else
        {
            // Hides instructions if it isn't the player's turn
            if (instructions != "")
            {
                instructions = "";
            }
        }
    }
}