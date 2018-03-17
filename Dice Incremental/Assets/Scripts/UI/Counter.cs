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
		for ( int i = 0; i < m_childSprites.Length; i++ )
		{
			m_childSprites[m_childSprites.Length - i - 1].sprite = m_sprite[(int)( m_counter / Math.Pow(10, i)) % 10];
		}
	}


	private void Awake() { UpdateValue(0); }

}
