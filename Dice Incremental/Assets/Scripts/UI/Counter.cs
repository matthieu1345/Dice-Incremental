using System;
using UnityEngine;
using UnityEngine.UI;


public class Counter : MonoBehaviour
{

	[SerializeField]
	private Sprite[] m_sprite;
	[SerializeField]
	private Image[] m_childSprites;
	[ReadOnly, SerializeField]
	private int m_counter;
	private int m_counterCurrent = 0;
	private int m_counterStart = 0;
	[SerializeField]
	private float updateLenght = 1;
	private float updateSpeed = 0.1f;
	private float lastUpdate = 0;

	public void FixedUpdate()
	{
		if (m_counterCurrent == m_counter)
			return;

		lastUpdate += Time.fixedDeltaTime;

		m_counterCurrent = (int)Mathf.Lerp(m_counterStart, m_counter, lastUpdate);

		UpdateSprite();
	}

	public void UpdateValue(int newValue)
	{
		m_counter = newValue;
		m_counterStart = m_counterCurrent;
		lastUpdate = 0;
	}

	void UpdateSprite()
	{
		for ( int i = 0; i < m_childSprites.Length; i++ )
		{
			m_childSprites[m_childSprites.Length - i - 1].sprite = m_sprite[CalculateSpriteIndex(m_counterCurrent, i)];
		}
	}

	public int CalculateSpriteIndex(int value, int number)
	{
		return (int)( value / Math.Pow(10, number)) % 10;
	}

	private void Awake() { UpdateValue(0); }

}