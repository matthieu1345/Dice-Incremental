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

	public int radius = 45;

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
	}

	void Start()
	{
		if (m_stats != null)
			m_stats.powerAdded.AddListener(SetLineRenderer);
	}

	public void SetLineRenderer(int sideCount)
	{
		m_renderer.positionCount = sideCount;
		float radius = (((RectTransform) transform).rect.width) / 2 - 5;


		Vector3[] coordinates = new Vector3[sideCount];
		for (int i = 0; i < sideCount; i++)
		{
			coordinates[i].x = radius * Mathf.Cos(2 * Mathf.PI * i / sideCount);
			coordinates[i].y = radius * Mathf.Sin(2 * Mathf.PI * i / sideCount);
			coordinates[i].z = 0;
		}

		m_renderer.SetPositions(coordinates);
	}
}
