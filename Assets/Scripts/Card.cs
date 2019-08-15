using System;

[Serializable]
public struct Card {
	public CardSuit Suit;
	public CardValue Value;

	public Card(CardSuit suit, CardValue value) {
		Suit = suit;
		Value = value;
	}
}
