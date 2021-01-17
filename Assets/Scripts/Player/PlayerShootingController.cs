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
    }

    /// <summary>
    /// Checks if the player wants to and can shoot based on their input, and tells the appropriate weapon(s) to shoot if they do.
    /// </summary>
    private void CheckShooting()
    {
        if (shootLeft && ReadyToShoot(Player.Instance.HandController.LeftHand.Weapon))  Player.Instance.HandController.LeftHand.Weapon.Shoot();
        if (shootRight && ReadyToShoot(Player.Instance.HandController.RightHand.Weapon)) Player.Instance.HandController.RightHand.Weapon.Shoot();
    }

    /// <summary>
    /// Checks if the player is ready to shoot.
    /// </summary>
    /// <returns>Whether or not the player can shoot.</returns>
    private bool ReadyToShoot(Weapon weapon)
    {
        return canShoot && weapon != null && weapon.ReadyToShoot();
    }
}