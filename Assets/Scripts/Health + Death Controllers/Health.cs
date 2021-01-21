using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

/// <summary>
/// A health component for anything that needs to track health, durability, etc.
/// </summary>
public class Health : MonoBehaviour
{
	//Private Fields---------------------------------------------------------------------------------------------------------------------------------

	//Serialized Fields----------------------------------------------------------------------------

	[SerializeField] private float maxHealth;

	//Non-Serialized Fields------------------------------------------------------------------------

    private float currentHealth;

	//Public Properties------------------------------------------------------------------------------------------------------------------------------

	//Basic Public Properties----------------------------------------------------------------------

	/// <summary>
	/// How much health, durability, etc. this entity currently has.
	/// </summary>
	public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    /// <summary>
    /// The maximum health, durability, etc. this entity can have.
    /// </summary>
    public float MaxHealth { get => maxHealth; }

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Awake() is run when the script instance is being loaded, regardless of whether or not the script is enabled. 
    /// Awake() runs before Start().
    /// </summary>
    private void Awake()
    {
		currentHealth = maxHealth;
	}

	//Triggered Methods------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// Damage the object and trigger things that would happen when the object gets damaged.
	/// </summary>
	/// <param name="amount">The amount of damage to take.</param>
	public void TakeDamage(float amount)
	{
        Debug.Log($"{this}.Health.TakeDamage()");
		currentHealth -= amount;
		currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (IsDead()) Die();
	}

	///// <summary>
	///// Heal the object and trigger things that would happen when the object gets healed.
	///// </summary>
	///// <param name="amount">The amount of healing to give.</param>
	//public void Heal(float amount)
	//{
	//	currentHealth += amount;
	//	currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
	//}

    /// <summary>
    /// Checks if health is 0 or less.
    /// </summary>
    /// <returns>Is the object this health class is a component of dead?</returns>
    private bool IsDead()
    {
        return currentHealth <= 0;
    }

    /// <summary>
    /// Handles the death or destruction of this object.
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log($"Health.Die() is not implemented yet.");
    }

    /// <summary>
    /// Resets health to its starting value.
    /// </summary>
    public void Reset()
    {
		currentHealth = maxHealth;
    }
}
