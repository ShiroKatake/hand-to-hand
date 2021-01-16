using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobHandGrenade : MonoBehaviour
{
	[SerializeField] private float grenadeRange;
	[SerializeField] private Transform leftHandSpawn;
	[SerializeField] private Transform rightHandSpawn;
	private Hand leftHand;
	private Hand rightHand;

	private bool leftHandButton;
	private bool rightHandButton;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
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

	//TODO: Find a way to move GetComponent when the hands are picked up, not before launching
	private void Launch()
	{
		if (leftHandButton == true && leftHand != null)
		{
			rightHand.GetComponent<Rigidbody>().AddForce(leftHandSpawn.forward * grenadeRange, ForceMode.Impulse);
		}
		if (rightHandButton == true && rightHand != null)
		{
			rightHand.GetComponent<Rigidbody>().AddForce(rightHandSpawn.forward * grenadeRange, ForceMode.Impulse);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		Hand hand = other.gameObject.GetComponent<Hand>();
	}
}
