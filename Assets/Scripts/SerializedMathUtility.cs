using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A serialized singleton class for reusable maths methods that don't seem to be included in Mathf or any other classes or libraries or that have been put together because people can't be bothered Googling the actual methods.
/// </summary>
public class SerializedMathUtility : PublicInstanceSerializableSingleton<SerializedMathUtility>
{
    [SerializeField] private Transform targeter;

    //Angles---------------------------------------------------------------------------------------

    /// <summary>
    /// Calculate the 360 degrees clockwise angle between two points.
    /// </summary>
    /// <param name="from">The point the angle is being calculated from, i.e. the centre of the 360 degrees clockwise rotation.</param>
    /// <param name="to">The point the angle is being calculated towards, i.e. the point at which the 360 degrees clockwise rotation stops.</param>
    /// <returns>The 360 degrees clockwise angle between two points.</returns>
    public float Angle(Vector2 from, Vector2 to)
    {
        targeter.position = new Vector3(from.x, 0, from.y);
        targeter.LookAt(new Vector3(to.x, 0, to.y));
        float result = targeter.rotation.eulerAngles.y;
        return result;
    }
}
