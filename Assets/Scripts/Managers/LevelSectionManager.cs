using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enables and disables sections of the level.
/// </summary>
public class LevelSectionManager : MonoBehaviour
{
    //Private Fields---------------------------------------------------------------------------------------------------------------------------------

    //Serialized Fields----------------------------------------------------------------------------

    [SerializeField] private List<GameObject> toEnable;
    [SerializeField] private List<GameObject> toDisable;

    //Triggered Methods (OnCollision)----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other} triggered {this}");

        if (other.tag == "Player")
        {
            Debug.Log($"{this} triggered by player");

            foreach (GameObject g in toDisable)
            {
                g.SetActive(false);
            }

            foreach (GameObject g in toEnable)
            {
                g.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
