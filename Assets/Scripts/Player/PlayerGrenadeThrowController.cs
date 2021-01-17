using UnityEngine;

/// <summary>
/// A controller class for the player throwing their hand grenades.
/// </summary>
public class PlayerGrenadeThrowController : PrivateInstanceSerializableSingleton<PlayerGrenadeThrowController>
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------
	
	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private float grenadeRange;
	[SerializeField] private Transform playerCamera;

    //Non-Serialized Fields------------------------------------------------------------------------
    private bool leftHandButton;
    private bool rightHandButton;

    private Hand hand = null;

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
	/// Awake() runs before Start().
	/// </summary> 
	protected override void Awake()
	{

	}

	//Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update() is run every frame.
	/// </summary>
	private void Update()
	{
		GetInput();
	}

    /// <summary>
    /// FixedUpdate() is run at a fixed interval independant of framerate.
    /// </summary>
	void FixedUpdate()
    {
		Launch();
	}

    //Recurring Methods (Update())-------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the player's launching input.
	/// </summary>
	private void GetInput()
	{
        bool shift = Input.GetButtonDown("Shift");
		leftHandButton = shift && Input.GetButtonDown("Left Hand");
		rightHandButton = shift && Input.GetButtonDown("Right Hand");

		if (hand == null)
		{
            if (leftHandButton == true && Player.Instance.HandController.LeftHand != null)
            {
                hand = Player.Instance.HandController.LeftHand;
                Player.Instance.HandController.RemoveHand(hand);
            }
            else if (rightHandButton == true && Player.Instance.HandController.RightHand != null)
            {
                hand = Player.Instance.HandController.RightHand;
                Player.Instance.HandController.RemoveHand(hand);
            }
        }
	}

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Launch the grenade hand according to which button was pressed.
	/// </summary>
	private void Launch()
	{
		if (hand != null)
		{
			hand.Launch(playerCamera.forward, grenadeRange);
			hand = null;
		}
	}
}
