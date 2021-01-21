using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartAnimation : MonoBehaviour
{
	Animator animator;
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		int anim = Random.Range(0, 2);

		if (anim == 0)
			animator.SetTrigger("Pistol");
		else
			animator.SetTrigger("Grenade");
	}
}
