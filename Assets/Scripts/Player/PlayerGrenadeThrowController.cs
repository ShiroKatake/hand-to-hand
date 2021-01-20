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

	private Animator leftAnimator;
	private Animator rightAnimator;

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
		leftAnimator = player.LeftHandAnimator;
		rightAnimator = player.RightHandAnimator;
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
					leftAnimator.SetTrigger("Grenade");
					isHoldingLeftGrenade = true;
				}
				if (lmbUp && player.HandController.LeftHand != null)
				{
					leftAnimator.SetTrigger("PullGrenade");
					hand = player.HandController.LeftHand;
					isHoldingLeftGrenade = false;
				}

				if (rmbDown && !isHoldingRightGrenade)
				{
					rightAnimator.SetTrigger("Grenade");
					isHoldingRightGrenade = true;
				}
				if (rmbUp && player.HandController.RightHand != null)
				{
					rightAnimator.SetTrigger("PullGrenade");
					hand = player.HandController.RightHand;
					isHoldingRightGrenade = false;
				}
			}

			else if (shiftUp)
			{
				if (isHoldingLeftGrenade)
				{
					leftAnimator.SetTrigger("Pistol");
					isHoldingLeftGrenade = false;
				}
				if (isHoldingRightGrenade)
				{
					rightAnimator.SetTrigger("Pistol");
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
			if (hand.HandSide == HandSide.Left && leftAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grenade_Pull") || 
				hand.HandSide == HandSide.Right && rightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grenade_Pull"))
			{
				Player.Instance.HandController.RemoveHand(hand);
				hand.Launch(playerCamera.forward, grenadeRange);
				hand = null;
			}
		}
	}
}
