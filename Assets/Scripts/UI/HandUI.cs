using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields----------------------------------------------------------------------------

	[Header("Hand")]
	[SerializeField] private Image currentWeapon;
	[SerializeField] private Image firstWeapon;
	[SerializeField] private Image secondWeapon;
	[SerializeField] private Image thirdWeapon;

	[Header("Colors")]
	[SerializeField] private Color pistolColor;
	[SerializeField] private Color autoColor;
	[SerializeField] private Color burstColor;
	[SerializeField] private Color shotgunColor;
	[SerializeField] private Color disabledColor;

	//Non-Serialized Fields--------------------------------------------------------------------

	private List<Image> weapons = new List<Image>();
	
	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
	/// Awake() runs before Start().
	/// </summary>
	private void Awake()
    {
		Initialize();
	}

	private void Initialize()
	{
		weapons.Add(currentWeapon);
		weapons.Add(firstWeapon);
		weapons.Add(secondWeapon);
		weapons.Add(thirdWeapon);

		firstWeapon.color = disabledColor;
		secondWeapon.color = disabledColor;
		thirdWeapon.color = disabledColor;
	}

	//Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Change color of dots or hand depending on the index provided.
	/// </summary>
	/// <param name="weaponIndex">The index of the weapon in the list.</param>
	/// <param name="weaponType">Type of weapon to change color to.</param>
	public void SetDotColor(int weaponIndex, EWeaponType weaponType)
	{
		weapons[weaponIndex].color = WeaponColor(weaponType);
	}

	/// <summary>
	/// Change color of dots or hand to disabled color depending on the index provided.
	/// </summary>
	/// <param name="weaponIndex">The index of the weapon in the list.</param>
	public void SetDotColor(int weaponIndex)
	{
		weapons[weaponIndex].color = disabledColor;
	}

	/// <summary>
	/// Returns the color associated with the weapon type.
	/// </summary>
	/// <param name="weaponType">The weapon type.</param>
	/// <returns>Color associated with the weapon.</returns>
	private Color WeaponColor(EWeaponType weaponType)
	{
		switch (weaponType)
		{
			case EWeaponType.Pistol:
				return pistolColor;
			case EWeaponType.AutoRifle:
				return autoColor;
			case EWeaponType.BurstRifle:
				return burstColor;
			case EWeaponType.Shotgun:
				return shotgunColor;
			default:
				return disabledColor;
		}
	}
}
