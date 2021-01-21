using System.Collections.Generic;
using UnityEngine;

public class HandUIManager : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private HandUI leftHandUI;
	[SerializeField] private HandUI rightHandUI;

	//Non-Serialized Fields--------------------------------------------------------------------

	private HandUI uiToChange;
	private PlayerHandController handController;

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	void Start()
    {
		handController = Player.Instance.HandController;
		handController.OnWeaponChange += UpdateWeaponStatus;

		UpdateWeaponStatus(EHandSide.Left);
		UpdateWeaponStatus(EHandSide.Right);
	}

	//Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update colors of dots.
	/// </summary>
	/// <param name="handSide">The side to change colors of.</param>
	private void UpdateWeaponStatus(EHandSide handSide)
	{
		List<Hand> hands = handSide == EHandSide.Left ? handController.LeftHands : handController.RightHands;
		for (int i = 0; i < System.Enum.GetValues(typeof(EWeaponType)).Length; i++)
		{
			if (i < hands.Count)
			{
				EWeaponType weaponType = hands[i].GetComponent<WeaponStats>().Type;
				HandUI(handSide).SetDotColor(i, weaponType);
			}
			else
			{
				HandUI(handSide).SetDotColor(i);
			}
		}
	}

	/// <summary>
	/// Determines which hand side to modify.
	/// </summary>
	/// <param name="handSide">Hand side that needs modifying.</param>
	/// <returns></returns>
	private HandUI HandUI(EHandSide handSide)
	{
		return handSide == EHandSide.Left ? leftHandUI : rightHandUI;
	}
}
