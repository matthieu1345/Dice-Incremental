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

	int m_rollValue = 1;

	public int GetRollValue() { return m_rollValue; }

	public int m_radius = 45;

	void Awake()
	{
		if (m_stats == null)
		{
			if (!this.GetComponentChecked<DiceStats>(ref m_stats))
			{
				m_stats = gameObject.AddComponent<DiceStats>();
			}
		}

		if (m_renderer == null)
		{
			if (!this.GetComponentChecked<LineRenderer>(ref m_renderer))
			{
				m_renderer = gameObject.AddComponent<LineRenderer>();
			}
		}

		if (m_text == null)
		{
			if (!this.GetComponentChecked<Text>(ref m_text))
			{
				m_text = gameObject.AddComponent<Text>();
			}
		}

		transform.position += new Vector3(0, 0, 1);
	}

	void Start()
	{
		if (m_stats != null)
			m_stats.m_powerAdded.AddListener(SetLineRenderer);

		if (DiceManager.m_instance != null)
			DiceManager.m_instance.m_rollEvent.AddListener(Roll);
	}

	public void SetLineRenderer(int sideCount)
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

	void Roll()
	{
		m_rollValue = Random.Range(1, m_stats.GetSides());
		m_text.text = m_rollValue.ToString();
	}
}
