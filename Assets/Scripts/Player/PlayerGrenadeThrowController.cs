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
	[SerializeField] private Transform leftHandSpawn;
	[SerializeField] private Transform rightHandSpawn;
	[SerializeField] private Hand leftHand;
	[SerializeField] private Hand rightHand;

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
		base.Awake();
		if (leftHand != null)
			leftHand.Collider.enabled = false;

		if (rightHand != null)
			rightHand.Collider.enabled = false;
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
			if (leftHandButton == true && leftHand != null)
			{
				hand = leftHand;
				leftHand = null;
			}
			if (rightHandButton == true && rightHand != null)
			{
				hand = rightHand;
				rightHand = null;
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

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Load up an arm if collided.
	/// </summary>
	/// <param name="other">The object collided with.</param>
	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag("Hand"))
			return;

		Hand hand = other.gameObject.GetComponent<Hand>();
		if (hand != null)
		{
			switch (hand.HandSide)
			{
				case HandSide.Left:
					leftHand = hand;
					hand.transform.parent = leftHandSpawn;
					break;
				case HandSide.Right:
					rightHand = hand;
					hand.transform.parent = rightHandSpawn;
					break;
				default:
					break;
			}

			hand.Collider.enabled = false;
			hand.transform.position = Vector3.zero;
			hand.transform.rotation = Quaternion.identity;
		}
	}
}
