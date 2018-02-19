using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

	public static DiceManager instance = null;

	[SerializeField]
	private Transform dicePrefab = null;

	private List<DiceStats> allDice;

	public void AddDice(DiceStats dice)
	{
		allDice.Add(dice);
	}

	public void RollAll()
	{
		for (int i = 0; i < allDice.Count; i++)
		{
			allDice[i].AddPower();
		}
	}

	public void CreateDice()
	{
		Instantiate(dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
