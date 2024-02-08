public class Dealer : CardHolder
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void Update()
    {
        base.Update();

        if (cardSum > 17)
        {
            Stand(); // Can't hit if their sum > 17
        }

        if (currentTurn)
        {
            for (int hitCount = 0; hitCount < 1; hitCount++)
            {
                Hit();
                return;
            }
        }
    }
}