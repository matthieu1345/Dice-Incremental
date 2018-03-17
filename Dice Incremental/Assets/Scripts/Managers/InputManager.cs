using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InputManager : InstancedMonoBehaviour<InputManager>
{

	private KeyCode m_buyDiceKey = KeyCode.B;
	public UnityEvent m_buyDiceEvent = new UnityEvent();
	private KeyCode m_rollDiceKey = KeyCode.R;
	public UnityEvent m_rollDiceEvent = new UnityEvent();
	private KeyCode m_selectNextDiceKey = KeyCode.RightArrow;
	public UnityEvent m_selectNextDiceEvent = new UnityEvent();
	private KeyCode m_selectPreviousDiceKey = KeyCode.LeftArrow;
	public UnityEvent m_selectPreviousDiceEvent = new UnityEvent();

	private void Update()
	{
		if (Input.GetKeyDown(m_buyDiceKey))
			m_buyDiceEvent.Invoke();

		if (Input.GetKeyDown(m_rollDiceKey))
			m_rollDiceEvent.Invoke();

		if (Input.GetKeyDown(m_selectNextDiceKey))
			m_selectNextDiceEvent.Invoke();

		if ( Input.GetKeyDown(m_selectPreviousDiceKey))
			m_selectPreviousDiceEvent.Invoke();
	}

}
