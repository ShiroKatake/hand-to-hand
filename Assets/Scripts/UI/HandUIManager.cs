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

	/// <summary>
	/// Start() is run on the frame when a script is enabled just before any of the Update methods are called for the first time. 
	/// Start() runs after Awake().
	/// </summary>
	void Start()
    {
		handController = Player.Instance.HandController;
		handController.OnWeaponChange += UpdateWeaponStatus;

		Weapon[] weapons = Player.Instance.GetComponentsInChildren<Weapon>();
		foreach (Weapon weapon in weapons)
		{
			weapon.OnWeaponChange += UpdateWeaponStatus;
		}

		UpdateWeaponStatus(EHandSide.Left);
		UpdateWeaponStatus(EHandSide.Right);
	}

	//Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update colors of dots.
	/// </summary>
	/// <param name="handSide">The side to change colors of.</param>
	public void UpdateWeaponStatus(EHandSide handSide)
	{
		List<Hand> hands = handSide == EHandSide.Left ? handController.LeftHands : handController.RightHands;
		for (int i = 0; i < System.Enum.GetValues(typeof(EWeaponType)).Length; i++)
		{
			if (i < hands.Count)
			{
				WeaponStats weaponStats = hands[i].GetComponent<WeaponStats>();
				HandUI(handSide).SetDotColor(i, weaponStats.Type, weaponStats.CurrentAmmo/weaponStats.MaxAmmo);
				Debug.Log(weaponStats.CurrentAmmo);
			}
			else
			{
				HandUI(handSide).ResetDotColor(i);
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
