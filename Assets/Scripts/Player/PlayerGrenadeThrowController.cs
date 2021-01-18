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

    private bool lmbDown;
    private bool lmbUp;
    private bool rmbDown;
    private bool rmbUp;
    private bool shiftDown;
    private bool shiftUp;

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
			shiftDown = Input.GetButton("Shift");
			shiftUp = Input.GetButtonUp("Shift");
            lmbDown = Input.GetButton("Left Hand");
            lmbUp = Input.GetButtonUp("Left Hand");
            rmbDown = Input.GetButton("Right Hand");
            rmbUp = Input.GetButtonUp("Right Hand");

			if (shiftDown)
			{
				//"Subtle asks" the player via UI to choose which hand to turn into a grenade
				
				if (lmbDown)
				{
					//Switch left hand animation to grenade
				}
				if (lmbUp && Player.Instance.HandController.LeftHand != null)
				{
					//Throw left grenade
					hand = Player.Instance.HandController.LeftHand;
				}

				if (rmbDown)
				{
					//Switch right hand animation to grenade
				}
				if (rmbUp && Player.Instance.HandController.RightHand != null)
				{
					//Throw right grenade
					hand = Player.Instance.HandController.RightHand;
				}
			}

			else if (shiftUp)
			{
				//Switch hand animation back to gun
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
            Player.Instance.HandController.RemoveHand(hand);
            hand.Launch(playerCamera.forward, grenadeRange);
			hand = null;
		}
	}
}
