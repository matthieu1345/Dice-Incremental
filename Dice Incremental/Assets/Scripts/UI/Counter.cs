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



	public void UpdateValue(int newValue)
	{
		m_counter = newValue;

		if (m_counter < 0) //we can't show values under 0, so don't show those yet
			m_counter = 0;

		for ( int i = 0; i < m_childSprites.Length; i++ )
		{
			m_childSprites[m_childSprites.Length - i - 1].sprite = m_sprite[CalculateSpriteIndex(m_counter, i)];
		}
	}

	public int CalculateSpriteIndex(int value, int number)
	{
		return (int)( value / Math.Pow(10, number)) % 10;
	}

	private void Awake() { UpdateValue(0); }

}