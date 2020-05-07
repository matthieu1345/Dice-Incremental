using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkUIObject : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_perkNameText;

	[SerializeField]
	private TextMeshProUGUI m_perkDescription;

	[SerializeField]
	private TextMeshProUGUI m_gold;

	[SerializeField]
	private TextMeshProUGUI m_dice;

	[SerializeField]
	private TextMeshProUGUI m_combo;

	[SerializeField]
	private TextMeshProUGUI m_power;

	public void ConnectPerk(Perk perk)
	{
		m_perkNameText.text = perk.GetReadableName();

		m_perkDescription.text = perk.GetDescription();

		switch (perk.GetRewardType())
		{
			case Perk.EPerkRewardType.PRT_NotSet:
				m_gold.gameObject.SetActive(false);
				m_dice.gameObject.SetActive(false);
				m_combo.gameObject.SetActive(false);
				m_power.gameObject.SetActive(false);
				break;
			case Perk.EPerkRewardType.PRT_Money:
				m_gold.gameObject.SetActive(true);
				m_dice.gameObject.SetActive(false);
				m_combo.gameObject.SetActive(false);
				m_power.gameObject.SetActive(false);

				m_gold.text = InsertReward(m_gold.text, perk.GetRewardAmount().ToString());

				break;
			case Perk.EPerkRewardType.PRT_Dice:
				m_gold.gameObject.SetActive(false);
				m_dice.gameObject.SetActive(true);
				m_combo.gameObject.SetActive(false);
				m_power.gameObject.SetActive(false);
				
				m_dice.text = InsertReward(m_dice.text, perk.GetRewardAmount().ToString());
				break;
			case Perk.EPerkRewardType.PRT_Combo:
				m_gold.gameObject.SetActive(false);
				m_dice.gameObject.SetActive(false);
				m_combo.gameObject.SetActive(true);
				m_power.gameObject.SetActive(false);

				m_combo.text = InsertReward(m_combo.text, "\"" + perk.GetComboReward().GetReadableName() + "\"") ;
				break;
			case Perk.EPerkRewardType.PRT_Power:
				m_gold.gameObject.SetActive(false);
				m_dice.gameObject.SetActive(false);
				m_combo.gameObject.SetActive(false);
				m_power.gameObject.SetActive(true);

				m_power.text = InsertReward(m_power.text, perk.GetRewardAmount().ToString());
				break;
		}
	}

	string InsertReward(string input, string rewardString)
	{
		int position = input.IndexOf("[#]");
		if (position < 0)
			return input;
		string output = input.Substring(0, position);
		output += rewardString;
		output += input.Substring(position + 3, input.Length - position - 3);

		return output;
	}
}
