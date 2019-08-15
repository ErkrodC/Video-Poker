public class FlushHandTester : HandTester {
	public FlushHandTester(RewardCurve rewardCurve) : base(rewardCurve) { }

	public override bool TestHand(Card[] hand) {
		return HandTesterUtil.TestHandForFlush(hand);
	}

	public override string GetHandName() => "Flush";
}