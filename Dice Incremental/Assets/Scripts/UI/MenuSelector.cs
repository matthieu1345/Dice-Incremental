using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : InstancedMonoBehaviour<MenuSelector>
{

	[Serializable]
	public class MenuPair
	{

		[SerializeField]
		public GameObject m_menu;
		[SerializeField]
		public GameObject m_button;

	}

	[SerializeField]
	private MenuPair[] m_menuPairs;

	[SerializeField]
	private DiceMenu m_diceMenu;

	[SerializeField]
	private Dictionary<GameObject, MenuPair> m_buttonMenuPair = new Dictionary<GameObject, MenuPair>();

	private MenuPair m_currentActive;


	public void OpenMenu( GameObject button )
	{
		m_diceMenu.gameObject.SetActive(false);
		if (m_currentActive != null)
			m_currentActive.m_menu.SetActive(false);
		m_buttonMenuPair[button].m_menu.SetActive(true);
		m_currentActive = m_buttonMenuPair[button];
	}

	void UpdateMenu()
	{
		if (m_currentActive == null)
			return;
		
		m_currentActive.m_button.GetComponent<Button>().onClick.Invoke();
	}

	IEnumerator UpdateMenuNextTurn()
	{
		yield return null;

		UpdateMenu();
	}

	public void QueueMenuUpdate()
	{
		StartCoroutine(UpdateMenuNextTurn());
	}

	public void OpenDiceMenu( Dice dice )
	{
		if ( m_currentActive != null )
		{
			m_currentActive.m_menu.SetActive(false);
			m_currentActive = null;
		}

		m_diceMenu.gameObject.SetActive(true);
		m_diceMenu.SelectNewDice(dice);
	}

	protected override void Awake()
	{
		base.Awake();
		foreach ( MenuPair menuPair in m_menuPairs )
		{
			m_buttonMenuPair.Add(menuPair.m_button, menuPair);
			menuPair.m_menu.SetActive(false);
		}
	}

}
