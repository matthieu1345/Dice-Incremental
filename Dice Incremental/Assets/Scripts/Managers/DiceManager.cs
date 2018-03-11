using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : MonoBehaviour
{

	public static DiceManager m_instance = null;

	[SerializeField]
	private DiceUI m_uiFolder;
	[SerializeField]
	private Transform m_dicePrefab = null;

	[SerializeField]
	int m_startingDice = 1;

	private List<DiceStats> m_allDice = new List<DiceStats>();

	public UnityEvent m_rollEvent;

	public void AddDice(DiceStats dice)
	{
		m_allDice.Add(dice);
		dice.transform.SetParent(m_uiFolder.transform, false);
		m_uiFolder.AddDiceObject(dice.gameObject);
	}

	public void RollAll()
	{
		
	}

	public void AddPowerToAll()
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
		if (m_instance == null)
			m_instance = this;

		else if (m_instance != this)
			Destroy(this);

		for ( int i = m_uiFolder.GetDiceCount(); i < m_startingDice; i++ )
		{
			CreateDice();
		}
	}
}
