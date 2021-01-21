using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private Image healthBar;

	/// <summary>
	/// Set HP bar to specified amount.
	/// </summary>
	/// <param name="amount">A value between 0 and 1.</param>
	public void SetHeath(float amount)
	{
		healthBar.fillAmount = amount;
	}
}
