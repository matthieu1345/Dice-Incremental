using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : InstancedMonoBehaviour<DiceManager>
{
	[SerializeField]
	private DiceUI m_uiFolder;
	[SerializeField]
	private Transform m_dicePrefab = null;

	[SerializeField]
	private int m_startingDice = 1;

	private List<Dice> m_allDice = new List<Dice>();

	public List<Dice> GetDiceList() { return m_allDice; }

	public delegate void RollEvent();

	public RollEvent m_rollEvent;

	public void AddDice(Dice dice)
	{
		m_allDice.Add(dice);
		dice.transform.SetParent(m_uiFolder.transform, false);
		dice.SetIndex(m_allDice.Count - 1);
		m_uiFolder.AddDiceObject(dice.gameObject);
	}

	public void RollAll()
	{
		m_rollEvent.Invoke();
		LevelManager.GetInstance().CheckCombos();
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

	public void LoadDice(DiceStats stats)
	{
		RemoveAllDice();

		Transform dice = Instantiate(m_dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);

		dice.GetComponent<Dice>().LoadStats(stats);
	}

	protected override void Awake()
	{
		base.Awake();
		for ( int i = m_uiFolder.GetDiceCount(); i < m_startingDice; i++ )
		{
			CreateDice();
		}

		InputManager.GetInstance().m_buyDiceEvent.AddListener(BuyDice);
		InputManager.GetInstance().m_rollDiceEvent.AddListener(RollAll);
	}

	public void BuyDice()
	{
		CreateDice();
	}

	public void RemoveAllDice()
	{
		for ( int i = 0; i < m_allDice.Count; i++ )
		{
			Destroy(m_allDice[i].gameObject);
		}
		m_allDice.Clear();
		m_uiFolder.Reset();
	}
}
