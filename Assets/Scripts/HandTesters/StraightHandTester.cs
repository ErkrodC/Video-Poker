public class StraightHandTester : HandTester {
	public StraightHandTester(RewardCurve rewardCurve) : base(rewardCurve) { }

	public override bool TestHand(Card[] hand) {
		return HandTesterUtil.TestHandForStraight(hand);
	}

	public override string GetHandName() => "Straight";
}