using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandSide
{
	Left,
	Right
}

public class Hand : MonoBehaviour
{
	[SerializeField] private HandSide handSide;
	[SerializeField] private Transform spawnPoint;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void CheckHand()
	{
		if (handSide == HandSide.Left)
		{

		}
		else if (handSide == HandSide.Right)
		{

		}
	}
}
