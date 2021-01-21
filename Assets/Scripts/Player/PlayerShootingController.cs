using UnityEngine;

/// <summary>
/// A controller class for the player shooting with their weapons.
/// </summary>
public class PlayerShootingController : PrivateInstanceSerializableSingleton<PlayerShootingController>
{    
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Non-Serialized Fields------------------------------------------------------------------------

    private bool canShoot = true;

	private bool animationSet;

	private bool leftHandAnimationIsSet;
	private bool rightHandAnimationIsSet;

	private bool shootLeft;
    private bool shootRight;
    private bool tab;
    private bool shift;

	private Player player;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Is the player is allowed to shoot?
    /// </summary>
    public bool CanShoot { get => canShoot; set => canShoot = value; }

	private void Start()
	{
		player = Player.Instance;
	}

	//Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update() is run every frame.
	/// </summary>
	private void Update()
    {
        GetInput();
		CheckShooting();
	}

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets the player's input from the keyboard and mouse / gamepad they're using.
    /// </summary>
    private void GetInput()
    {
        shift = Input.GetButton("Shift");
        tab = Input.GetButton("Tab");
        shootLeft = !shift && !tab && Input.GetButton("Left Hand");
        shootRight = !shift && !tab && Input.GetButton("Right Hand");
        //Debug.Log($"PlayerShootingController.GetInput(), shift: {shift}, tab: {tab}, shootLeft: {shootLeft}, leftHandWeapon.Stats.TriggerDown: {player.HandController.LeftHandWeapon?.CurrentStats.TriggerDown} shootRight: {shootRight}, rightHandWeapon.Stats.TriggerDown: {player.HandController.RightHandWeapon?.CurrentStats.TriggerDown}");
    }

    /// <summary>
    /// Checks if the player wants to and can shoot based on their input, and tells the appropriate weapon(s) to shoot if they do.
    /// </summary>
    private void CheckShooting()
    {
        if (shootLeft)
		{
			leftHandAnimationIsSet = SetAnimationToPistol(player.LeftHandAnimator, leftHandAnimationIsSet);
			if (ReadyToShoot(player.HandController.LeftHandWeapon, shootLeft)) player.HandController.LeftHandWeapon.Shoot();
        }
        else
        {
			if (player.HandController.LeftHandWeapon.CurrentStats != null)
			{
				player.HandController.LeftHandWeapon.CurrentStats.TriggerDown = false;
				leftHandAnimationIsSet = false;
			}
		}

        if (shootRight)
        {
			rightHandAnimationIsSet = SetAnimationToPistol(player.RightHandAnimator, rightHandAnimationIsSet);
			if (ReadyToShoot(player.HandController.RightHandWeapon, shootRight)) player.HandController.RightHandWeapon.Shoot();
        }
        else
        {
			if (player.HandController.RightHandWeapon.CurrentStats != null)
			{
				player.HandController.RightHandWeapon.CurrentStats.TriggerDown = false;
				rightHandAnimationIsSet = false;
			}

		}
    }

	/// <summary>
	/// Set animation state to pistol.
	/// </summary>
	/// <param name="animator">The hand's animator.</param>
	/// <param name="handAnimation">The boolean for toggling.</param>
	/// <returns></returns>
	private bool SetAnimationToPistol(Animator animator, bool handAnimation)
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
			animator.GetCurrentAnimatorStateInfo(0).IsName("Grenade"))
		{
			if (!handAnimation)
			{
				animator.SetTrigger("Pistol");
				handAnimation = true;
			}
		}

		return handAnimation;
	}

	/// <summary>
	/// Reset animation state to idle.
	/// </summary>
	/// <param name="animator">The hand's animator</param>
	/// <param name="handAnimation">The toggle boolean to reset to false.</param>
	public void ResetShooting(EHandSide handside)
	{
		Debug.Log("Reset");
		if (handside == EHandSide.Left)
		{
			leftHandAnimationIsSet = false;
		}
		else
			rightHandAnimationIsSet = false;
	}

	/// <summary>
	/// Checks if the player is able to shoot and their weapon can shoot.
	/// </summary>
	/// <param name="weapon">The weapon that the player would shoot with.</param>
	/// <param name="triggerDown">Is the player holding down the weapon's trigger?</param>
	/// <returns>Is the player able to shoot and can their weapon shoot right now?</returns>
	private bool ReadyToShoot(Weapon weapon, bool triggerDown)
    {
        //Debug.Log($"PlayerShootingController.ReadyToShoot()");
        return canShoot && weapon != null && weapon.CurrentStats != null && weapon.ReadyToShoot(triggerDown);
    }
}