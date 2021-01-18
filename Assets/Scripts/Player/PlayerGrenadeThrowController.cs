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

	private bool isHoldingLeftGrenade;
	private bool isHoldingRightGrenade;

    private Hand hand = null;
	private Player player;

	//Initialization Methods-------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
	/// Awake() runs before Start().
	/// </summary> 
	protected override void Awake()
	{

	}

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
				
				if (lmbDown && !isHoldingLeftGrenade)
				{
					player.LeftHandAnimator.SetTrigger("Grenade");
					isHoldingLeftGrenade = true;
				}
				if (lmbUp && player.HandController.LeftHand != null)
				{
					hand = player.HandController.LeftHand;
					isHoldingLeftGrenade = false;
				}

				if (rmbDown && !isHoldingRightGrenade)
				{
					player.RightHandAnimator.SetTrigger("Grenade");
					isHoldingRightGrenade = true;
				}
				if (rmbUp && player.HandController.RightHand != null)
				{
					hand = player.HandController.RightHand;
					isHoldingRightGrenade = false;
				}
			}

			else if (shiftUp)
			{
				if (isHoldingLeftGrenade)
				{
					player.LeftHandAnimator.SetTrigger("Pistol");
					isHoldingLeftGrenade = false;
				}
				if (isHoldingRightGrenade)
				{
					player.RightHandAnimator.SetTrigger("Pistol");
					isHoldingRightGrenade = false;
				}

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
