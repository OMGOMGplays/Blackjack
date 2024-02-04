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
}