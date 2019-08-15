public class PlayerState {
	public readonly Card[] Hand;
	public readonly bool[] RedealFlags;
	public int Credits;

	public PlayerState(int handSize, int credits) {
		Hand = new Card[handSize];
		RedealFlags = new bool[handSize];
		Credits = credits;
	}
}