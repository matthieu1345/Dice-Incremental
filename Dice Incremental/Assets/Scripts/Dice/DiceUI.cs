using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUI : MonoBehaviour {

	private List<GameObject> diceObjects = new List<GameObject>();

	public void AddDiceObject(GameObject dice)
	{
		diceObjects.Add(dice);
	}
}
