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
    private bool throwLeft;
    private bool throwRight;
    private bool shift;

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
		if (hand == null)
		{
            shift = Input.GetButton("Shift");
            throwLeft = shift && Input.GetButtonDown("Left Hand");
            throwRight = shift && Input.GetButtonDown("Right Hand");

            if (throwLeft && Player.Instance.HandController.LeftHand != null) hand = Player.Instance.HandController.LeftHand;
            else if (throwRight && Player.Instance.HandController.RightHand != null) hand = Player.Instance.HandController.RightHand;
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
            Player.Instance.HandController.RemoveHand(hand);
            hand.Launch(playerCamera.forward, grenadeRange);
			hand = null;
		}
	}
}
