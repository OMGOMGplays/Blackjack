public class Dealer : CardHolder
{
    public override void OnStart()
    {
        base.OnStart();

        for (int i = 0; i < 2; i++)
        {
            Hit(false, false);
        }

        Blackjack.Instance.turn++;
    }

    public override void Update()
    {
        base.Update();

        if (cardSum > 17)
        {
            Stand(); // Can't hit if their sum > 17
            return;
        }

        Hit(true, false);
    }
}