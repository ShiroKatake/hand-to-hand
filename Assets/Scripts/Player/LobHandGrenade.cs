using UnityEngine;

public class LobHandGrenade : MonoBehaviour
{
	[SerializeField] private float grenadeRange;
	[SerializeField] private Transform leftHandSpawn;
	[SerializeField] private Transform rightHandSpawn;

	private Rigidbody leftHandRb;
	private Rigidbody rightHandRb;

	private bool leftHandButton;
	private bool rightHandButton;

    void FixedUpdate()
    {
		GetInput();
		Launch();
	}

	private void GetInput()
	{
		leftHandButton = Input.GetButtonDown("Left Hand");
		rightHandButton = Input.GetButtonDown("Right Hand");
	}

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
