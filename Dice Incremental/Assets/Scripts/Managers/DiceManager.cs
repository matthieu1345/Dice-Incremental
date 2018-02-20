using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : MonoBehaviour
{

	public static DiceManager instance = null;

	[SerializeField]
	private DiceUI m_uiFolder;
	[SerializeField]
	private Transform m_dicePrefab = null;

	private List<DiceStats> m_allDice = new List<DiceStats>();

	public void AddDice(DiceStats dice)
	{
		m_allDice.Add(dice);
	}

	public void RollAll()
	{
		for (int i = 0; i < m_allDice.Count; i++)
		{
			m_allDice[i].AddPower();
		}
	}

	public void CreateDice()
	{
		Instantiate(m_dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
