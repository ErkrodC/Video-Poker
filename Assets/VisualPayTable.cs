using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualPayTable : MonoBehaviour {
	private const int kCreditColumns = 5;
	[SerializeField] private Text cellPrefab;
	[SerializeField] private GridLayoutGroup gridLayoutGroup;
	
	private void Start() {
		gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		gridLayoutGroup.constraintCount = kCreditColumns + 1; // add one for hand name
		
		// header row
		Instantiate(cellPrefab, gridLayoutGroup.transform).text = "Hand";

		for (int i = 0; i < kCreditColumns; i++) {
			Instantiate(cellPrefab, gridLayoutGroup.transform).text = $"{i + 1} Credit{(i == 0 ? "" : "s")}";
		}
		
		foreach (HandTester handTester in Rules.HandTestersDescWinningPriority) {
			Instantiate(cellPrefab, gridLayoutGroup.transform).text = handTester.GetHandName();

			for (int i = 0; i < kCreditColumns; i++) {
				Instantiate(cellPrefab, gridLayoutGroup.transform).text = handTester.RewardCurve.GetReward(i + 1).ToString();
			}
		}
	}
}