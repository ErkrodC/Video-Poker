using System;
using UnityEngine;
using UnityEngine.UI;

public class VisualCard : MonoBehaviour {
	public int HandIndex { get; private set; }

	private const float kVerticalHighlightOffset = 15f;
	[SerializeField] private CardDatabase cardDatabase;
	[SerializeField] private Button cardButton;
	[SerializeField] private Image cardImage;
	private float initialY;
	private Card card; // useful for debugging
	private Transform thisTransform;

	private void Start() {
		thisTransform = transform;
		initialY = thisTransform.position.y;
		cardButton.onClick.AddListener(() => GameManager.Instance.OnVisualCardButton(HandIndex));
		cardButton.onClick.AddListener(() => SetCardHighlight(GameManager.Instance.GetRedealFlag(HandIndex)));
	}

	public void InitializeCard(Card card, int handIndex) {
		HandIndex = handIndex;
		this.card = card;
		cardImage.sprite = cardDatabase.GetCardSprite(card);
	}

	public void SetCardHighlight(bool highlight) {
		cardImage.color = highlight ? Color.cyan : Color.white;
	}

	public void SetInteractable(bool interactable) {
		cardButton.interactable = interactable;
	}
}