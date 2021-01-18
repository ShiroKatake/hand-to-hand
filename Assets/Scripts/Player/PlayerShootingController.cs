using UnityEngine;

/// <summary>
/// A controller class for the player shooting with their weapons.
/// </summary>
public class PlayerShootingController : PrivateInstanceSerializableSingleton<PlayerShootingController>
{    
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Non-Serialized Fields------------------------------------------------------------------------

    private bool canShoot = true;
    private bool shootLeft;
    private bool shootRight;
    private bool tab;
    private bool shift;

    //Public Properties------------------------------------------------------------------------------------------------------------------------------

    //Basic Public Properties----------------------------------------------------------------------

    /// <summary>
    /// Is the player is allowed to shoot?
    /// </summary>
    public bool CanShoot { get => canShoot; set => canShoot = value; }

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
        Debug.Log($"PlayerShootingController.GetInput(), shift: {shift}, tab: {tab}, shootLeft: {shootLeft}, leftHandWeapon.Stats.TriggerDown: {Player.Instance.HandController.LeftHandWeapon?.CurrentStats.TriggerDown} shootRight: {shootRight}, rightHandWeapon.Stats.TriggerDown: {Player.Instance.HandController.RightHandWeapon?.CurrentStats.TriggerDown}");
    }

    /// <summary>
    /// Checks if the player wants to and can shoot based on their input, and tells the appropriate weapon(s) to shoot if they do.
    /// </summary>
    private void CheckShooting()
    {        
        if (shootLeft)
        {
            if (ReadyToShoot(Player.Instance.HandController.LeftHandWeapon, shootLeft)) Player.Instance.HandController.LeftHandWeapon.Shoot();
        }
        else
        {
            if (Player.Instance.HandController.LeftHandWeapon.CurrentStats != null) Player.Instance.HandController.LeftHandWeapon.CurrentStats.TriggerDown = false;
        }

        if (shootRight)
        {
            if (ReadyToShoot(Player.Instance.HandController.RightHandWeapon, shootRight)) Player.Instance.HandController.RightHandWeapon.Shoot();
        }
        else
        {
            if (Player.Instance.HandController.RightHandWeapon.CurrentStats != null) Player.Instance.HandController.RightHandWeapon.CurrentStats.TriggerDown = false;
        }
    }

    /// <summary>
    /// Checks if the player is able to shoot and their weapon can shoot.
    /// </summary>
    /// <param name="weapon">The weapon that the player would shoot with.</param>
    /// <param name="triggerDown">Is the player holding down the weapon's trigger?</param>
    /// <returns>Is the player able to shoot and can their weapon shoot right now?</returns>
    private bool ReadyToShoot(Weapon weapon, bool triggerDown)
    {
        Debug.Log($"PlayerShootingController.ReadyToShoot()");
        return canShoot && weapon != null && weapon.CurrentStats != null && weapon.ReadyToShoot(triggerDown);
    }
}