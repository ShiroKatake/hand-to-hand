using UnityEngine;

public class LobHandGrenade : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	private Rigidbody leftHandRb;
	private Rigidbody rightHandRb;

	private bool leftHandButton;
	private bool rightHandButton;

	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private float grenadeRange;
	[SerializeField] private Transform leftHandSpawn;
	[SerializeField] private Transform rightHandSpawn;
	
	//Core Recurring Methods-------------------------------------------------------------------------------------------------------------------------

	void FixedUpdate()
    {
		GetInput();
		Launch();
	}

	/// <summary>
	/// Gets the player's launching input.
	/// </summary>
	private void GetInput()
	{
		leftHandButton = Input.GetButtonDown("Left Hand");
		rightHandButton = Input.GetButtonDown("Right Hand");
	}

	/// <summary>
	/// Launch the grenade hand according to which button was pressed.
	/// </summary>
	private void Launch()
	{
		Rigidbody handRb = null;
		Transform handSpawn = null;

		if (leftHandButton == true && leftHandRb != null)
		{
			handRb = leftHandRb;
			handSpawn = leftHandSpawn;
		}

		if (rightHandButton == true && rightHandRb != null)
		{
			handRb = rightHandRb;
			handSpawn = rightHandSpawn;
		}

		if (handRb != null && handSpawn != null)
		{
			handRb.AddForce(handSpawn.forward * grenadeRange, ForceMode.Impulse);
			handRb = null;
		}
	}

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
