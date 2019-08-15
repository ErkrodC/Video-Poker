public abstract class HandTester {
	public readonly RewardCurve RewardCurve;
	public abstract bool TestHand(Card[] hand);

	protected HandTester(RewardCurve rewardCurve) {
		RewardCurve = rewardCurve;
	}

	public abstract string GetHandName();
}