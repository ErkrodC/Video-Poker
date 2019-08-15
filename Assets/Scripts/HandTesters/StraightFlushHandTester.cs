public class StraightFlushHandTester : HandTester {
	public StraightFlushHandTester(RewardCurve rewardCurve) : base(rewardCurve) { }

	public override bool TestHand(Card[] hand) {
		if (!HandTesterUtil.TestHandForFlush(hand)) { return false; }
		return HandTesterUtil.TestHandForStraight(hand);
	}

	public override string GetHandName() => "Straight Flush";
}
