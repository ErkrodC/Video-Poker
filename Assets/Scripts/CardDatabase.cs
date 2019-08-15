using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardDB")]
public class CardDatabase : ScriptableObject {
	[SerializeField] private List<CardInfo> cardInfos;

	public Sprite GetCardSprite(Card card) {
		foreach (CardInfo cardInfo in cardInfos) {
			if (cardInfo.Card.Suit == card.Suit && cardInfo.Card.Value == card.Value) {
				return cardInfo.Sprite;
			}
		}

		return null;
	}

	[Serializable]
	public struct CardInfo {
		public Card Card;
		public Sprite Sprite;

		public CardInfo(Card card, Sprite sprite) {
			Card = card;
			Sprite = sprite;
		}
	}
}