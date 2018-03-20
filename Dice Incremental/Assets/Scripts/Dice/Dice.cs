using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
	[SerializeField]
	private DiceStats m_stats;
	[SerializeField]
	private Shape m_renderer;
	[SerializeField]
	private Text m_text;

	[ReadOnly, SerializeField]
	private int m_index;

	public int GetIndex() { return m_index; }
	public void SetIndex( int value ) { m_index = value; }
	public DiceStats GetStats() { return m_stats;}

	public void LoadStats( DiceStats stats )
	{
		m_stats = stats;
		m_stats.m_powerAdded += SetLineRenderer;
		SetLineRenderer(m_stats.GetSides());
	}

	private int m_rollValue = 1;

	public int GetRollValue() { return m_rollValue; }

	public void AddPower() { m_stats.AddSide(); }

	public int GetSides() { return m_stats.GetSides(); }
	public int GetPower() { return m_stats.GetPower(); }
	public int GetGoal() { return m_stats.GetGoal(); }

	private void Awake()
	{
		if ( m_stats == null )
		{
			m_stats = new DiceStats();
		}

		if ( m_renderer == null )
		{
			if ( !this.GetComponentInChildrenChecked<Shape>(ref m_renderer) )
			{
				Debug.LogError("The dice prefab has no shape renderer assigned!");
			}
		}

		if ( m_text == null )
		{
			if ( !this.GetComponentChecked<Text>(ref m_text) )
			{
				m_text = gameObject.AddComponent<Text>();
			}
		}

		transform.position += new Vector3(0, 0, 1);

		Button button = GetComponent<Button>();

		if ( button != null )
			button.onClick.AddListener(delegate { MenuSelector.GetInstance().OpenDiceMenu(this); });
	}



	private void Start()
	{
		if ( m_stats != null )
		{
			m_stats.m_powerAdded += SetLineRenderer;
			SetLineRenderer(m_stats.GetSides());
		}

		if ( DiceManager.m_instance != null )
		{
			DiceManager.m_instance.m_rollEvent += Roll;
			DiceManager.m_instance.AddDice(this);
		}

	}

	private void SetLineRenderer(int sideCount)
	{

		const float radius = 0.5f;

		Vector2[] coordinates = new Vector2[sideCount];
		for (int i = 0; i < sideCount; i++)
		{
			coordinates[i].x = radius * Mathf.Cos(2 * Mathf.PI * i / sideCount);
			coordinates[i].y = radius * Mathf.Sin(2 * Mathf.PI * i / sideCount);
		}

		m_renderer.settings.polyVertices = coordinates;
		m_renderer.settings.polyVertices = m_renderer.settings.polyVertices;

	}

	private void Roll()
	{
		m_rollValue = Random.Range(1, m_stats.GetSides() + 1);
		m_text.text = m_rollValue.ToString();
	}
}
