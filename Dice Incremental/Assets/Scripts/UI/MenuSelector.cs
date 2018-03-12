﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;


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
	Dictionary<GameObject, GameObject> m_buttonMenuPair = new Dictionary<GameObject, GameObject>();

	private GameObject m_currentActive;


	public void OpenMenu( GameObject button )
	{
		m_diceMenu.gameObject.SetActive(false);
		if (m_currentActive != null)
			m_currentActive.SetActive(false);
		m_buttonMenuPair[button].SetActive(true);
		m_currentActive = m_buttonMenuPair[button];
	}

	public void OpenDiceMenu( Dice dice )
	{
		if ( m_currentActive != null )
		{
			m_currentActive.SetActive(false);
			m_currentActive = null;
		}

		m_diceMenu.gameObject.SetActive(true);
		m_diceMenu.SelectNewDice(dice);
	}

	private void Awake()
	{
		foreach ( MenuPair menuPair in m_menuPairs )
		{
			m_buttonMenuPair.Add(menuPair.m_button, menuPair.m_menu);
			menuPair.m_menu.SetActive(false);
		}
	}

}
