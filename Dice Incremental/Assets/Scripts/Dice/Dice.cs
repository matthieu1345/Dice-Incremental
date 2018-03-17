using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	[SerializeField]
	private DiceStats m_stats;
	[SerializeField]
	private LineRenderer m_renderer;
	[SerializeField]
	private Text m_text;

	[SerializeField]
	private LineRenderer m_outline;

	private int m_rollValue = 1;
	private int m_radius = 45;

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
			if ( !this.GetComponentChecked<LineRenderer>(ref m_renderer) )
			{
				m_renderer = gameObject.AddComponent<LineRenderer>();
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

		Button m_button = GetComponent<Button>();

		if ( m_button != null )
			m_button.onClick.AddListener(delegate { MenuSelector.GetInstance().OpenDiceMenu(this); });

	}



	private void Start()
	{
		if ( m_stats != null )
		{
			m_stats.Start();
			m_stats.m_powerAdded += SetLineRenderer;
		}

		if ( DiceManager.m_instance != null )
		{
			DiceManager.m_instance.m_rollEvent += Roll;
			DiceManager.m_instance.AddDice(this);
		}
	}

	private void SetLineRenderer(int sideCount)
	{
		m_renderer.positionCount = sideCount;
		float radius = (((RectTransform) transform).rect.width) / 2 - m_renderer.startWidth * 2;

		Vector3[] coordinates = new Vector3[sideCount];
		for (int i = 0; i < sideCount; i++)
		{
			coordinates[i].x = radius * Mathf.Cos(2 * Mathf.PI * i / sideCount);
			coordinates[i].y = radius * Mathf.Sin(2 * Mathf.PI * i / sideCount);
			coordinates[i].z = 0;
		}

		m_renderer.SetPositions(coordinates);
	}

	private void Roll()
	{
		m_rollValue = Random.Range(1, m_stats.GetSides() + 1);
		m_text.text = m_rollValue.ToString();
	}
}
