using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboUIObject : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI m_comboNameText;

	[SerializeField]
	private TextMeshProUGUI m_comboDescription;

	[SerializeField]
	private TextMeshProUGUI m_comboRewardNumber;

	[SerializeField]
	private GameObject m_gold;

	[SerializeField]
	private GameObject m_XP;

	[SerializeField]
	private GameObject m_RBP;

	[SerializeField]
	private GameObject m_multiplier;

	[SerializeField]
	private GameObject m_bonus;

	public void ConnectCombo(ComboBase combo)
	{
		m_comboNameText.text = combo.GetReadableName();

		//set combo description
		m_comboDescription.text = combo.GetDescription();


		//create string of reward numbers
		string c_rewardNumber = "";

		//get gold reward else disable gold text
		if (combo.GetMoneyReward() > 0)
		{
			c_rewardNumber += combo.GetMoneyReward().ToString() + ", ";
			m_gold.SetActive(true);
		}
		else
			m_gold.SetActive(false);

		//get XP reward else disable XP text
		if (combo.GetXPReward() > 0)
		{
			c_rewardNumber += combo.GetXPReward().ToString() + ", ";
			m_XP.SetActive(true);
		}
		else
			m_XP.SetActive(false);

		//get RBP reward else disable RBP text
		if (combo.GetRollBonusPointReward() > 0)
		{
			c_rewardNumber += combo.GetRollBonusPointReward().ToString() + ", ";
			m_RBP.SetActive(true);
		}
		else
			m_RBP.SetActive(false);

		c_rewardNumber = c_rewardNumber.Substring(0, c_rewardNumber.Length-2);

		m_comboRewardNumber.text = c_rewardNumber;

		//enable correct reward type
		switch (combo.GetRewardType())
		{
			case ComboBase.EComboRewardType.CRT_NotSet:
				m_multiplier.SetActive(false);
				m_bonus.SetActive(false);
				break;
			case ComboBase.EComboRewardType.CRT_StaticAmount:
				m_multiplier.SetActive(false);
				m_bonus.SetActive(true);
				break;
			case ComboBase.EComboRewardType.CRT_ValueMultiplication:
				m_multiplier.SetActive(true);
				m_bonus.SetActive(false);
				break;
		}


	}
}
