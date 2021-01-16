using UnityEngine;

/// <summary>
/// A controller class for the player throwing their hand grenades.
/// </summary>
public class PlayerGrenadeThrowController : PrivateInstanceSerializableSingleton<PlayerGrenadeThrowController>
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------
	
	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private float grenadeRange;
	[SerializeField] private Transform leftHandSpawn;
	[SerializeField] private Transform rightHandSpawn;
	[SerializeField] private Rigidbody leftHandRb;
	[SerializeField] private Rigidbody rightHandRb;

    //Non-Serialized Fields------------------------------------------------------------------------
    private bool leftHandButton;
    private bool rightHandButton;

    private Rigidbody handRb = null;
    private Transform handSpawn = null;

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
		leftHandButton = Input.GetButtonDown("Left Hand");
		rightHandButton = Input.GetButtonDown("Right Hand");

		if (handRb == null)
		{
			if (leftHandButton == true && leftHandRb != null)
			{
				Debug.Log("left");
				handRb = leftHandRb;
				handSpawn = leftHandSpawn;
				leftHandRb = null;
			}
			if (rightHandButton == true && rightHandRb != null)
			{
				Debug.Log("right");
				handRb = rightHandRb;
				handSpawn = rightHandSpawn;
				rightHandRb = null;
			}
		}
	}

    //Recurring Methods (FixedUpdate())--------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Launch the grenade hand according to which button was pressed.
	/// </summary>
	private void Launch()
	{
		if (handRb != null && handSpawn != null)
		{
			handRb.useGravity = true;
			handRb.AddForce(handSpawn.forward * grenadeRange, ForceMode.Impulse);
			handRb = null;
		}
	}

    //Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Load up an arm if collided.
	/// </summary>
	/// <param name="other">The object collided with.</param>
	private void OnCollisionEnter(Collision other)
	{
		Hand hand = other.gameObject.GetComponent<Hand>();
		if (hand != null)
		{
			switch (hand.HandSide)
			{
				case HandSide.Left:
					leftHandRb = hand.Rigidbody;
					hand.transform.parent = leftHandSpawn;
					break;
				case HandSide.Right:
					rightHandRb = hand.Rigidbody;
					hand.transform.parent = rightHandSpawn;
					break;
				default:
					break;
			}

			hand.transform.position = Vector3.zero;
			hand.transform.rotation = Quaternion.identity;
		}
	}
}
