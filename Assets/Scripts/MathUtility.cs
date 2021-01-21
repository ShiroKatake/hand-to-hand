using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A non-serialized singleton class for reusable maths methods that don't seem to be included in Mathf or any other classes or libraries or that have been put together because people can't be bothered Googling the actual methods.
/// </summary>
public class MathUtility
{

    //Initialization Methods-------------------------------------------------------------------------------------------------------------------------

    ///// <summary>
    ///// MathUtility's constructor method.
    ///// WARNING: DO NOT CALL IN-CODE. IT IS ONLY PUBLIC FOR SINGLETON.INSTANCE TO USE.
    ///// </summary>
    //public MathUtility()
    //{

    //}

    //Utility Methods--------------------------------------------------------------------------------------------------------------------------------  

    //Number Square--------------------------------------------------------------------------------

    /// <summary>
    /// Returns the square of a number.
    /// </summary>
    /// <param name="num">The number to square</param>
    /// <returns>Num squared.</returns>
    public static float Square(float num)
    {
        return num * num;
    }

    /// <summary>
    /// The 2D distance squared on the x/y plane between two positions.
    /// </summary>
    /// <param name="a">The first position</param>
    /// <param name="b">The second position.</param>
    /// <returns>The distance between the two positions on the x/y plane.</returns>
    public static float DistanceSquaredXY(Vector3 a, Vector3 b)
    {
        float deltaX = a.x - b.x;
        float deltaY = a.y - b.y;
        float distSquared = deltaX * deltaX + deltaY * deltaY;
        return distSquared;
    }

    /// <summary>
    /// The 2D distance squared on the x/z plane between two positions.
    /// </summary>
    /// <param name="a">The first position</param>
    /// <param name="b">The second position.</param>
    /// <returns>The distance between the two positions on the x/z plane.</returns>
    public static float DistanceSquaredXZ(Vector3 a, Vector3 b)
    {
        float deltaX = a.x - b.x;
        float deltaZ = a.z - b.z;
        float distSquared = deltaX * deltaX + deltaZ * deltaZ;
        return distSquared;
    }

    /// <summary>
    /// The 2D distance squared on the y/z plane between two positions.
    /// </summary>
    /// <param name="a">The first position</param>
    /// <param name="b">The second position.</param>
    /// <returns>The distance between the two positions on the y/z plane.</returns>
    public static float DistanceSquaredYZ(Vector3 a, Vector3 b)
    {
        float deltaY = a.y - b.y;
        float deltaZ = a.z - b.z;
        float distSquared = deltaY * deltaY + deltaZ * deltaZ;
        return distSquared;
    }

    //Number Magnitude-----------------------------------------------------------------------------

    /// <summary>
    /// Returns the magnitude of a number.
    /// </summary>
    /// <param name="num">The number to calculate the magnitude of.</param>
    /// <returns>The magnitude of the number.</returns>
    public static float FloatMagnitude(float num)
    {
        if (num < 0)
        {
            num *= -1;
        }

        return num;
    }

    /// <summary>
    /// Returns the magnitude of a number.
    /// </summary>
    /// <param name="num">The number to calculate the magnitude of.</param>
    /// <returns>The magnitude of the number.</returns>
    public static int IntMagnitude(int num)
    {
        if (num < 0)
        {
            num *= -1;
        }

        return num;
    }

    //Number Properties----------------------------------------------------------------------------

    /// <summary>
    /// Returns the sign of a number, i.e. +1 if it's positive or 0, and -1 if it's negative.
    /// </summary>
    /// <param name="num">The number to determine the sign of.</param>
    /// <returns>The sign (+1 or -1) of the number.</returns>
    public static int Sign(float num)
    {
        return (num < 0 ? -1 : 1);
    }

	/// <summary>
	/// Determines whether the number is odd or not.
	/// </summary>
	/// <param name="num">The number to determine.</param>
	/// <returns>whether the number is odd (true) or not (false).</returns>
	public static bool IsOdd(float num)
	{
		return (num % 2 == 1);
	}

	/// <summary>
	/// Determines whether the number is even or not.
	/// </summary>
	/// <param name="num">The number to determine.</param>
	/// <returns>whether the number is even (true) or not (false).</returns>
	public static bool IsEven(float num)
	{
		return (num % 2 == 0);
	}

    //Angles---------------------------------------------------------------------------------------

    /// <summary>
    /// Converts the provided angle to an angle between 0 degrees and 360 degrees
    /// </summary>
    /// <param name="angle">The raw angle.</param>
    /// <returns>The normalised angle.</returns>
    public static float NormaliseAngle(float angle)
    {
        while (angle > 360)
        {
            angle -= 360;
        }

        while (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }

    /// <summary>
    /// Checks if an angle is between two specified angles.
    /// </summary>
    /// <param name="angle">The angle being checked.</param>
    /// <param name="a">The counter-clockwise-most bound of what's acceptable for the angle being checked.</param>
    /// <param name="b">The clockwise-most bound of what's acceptable for the angle being checked.</param>
    /// <returns>Whether the angle being checked is between a and b.</returns>
    public static bool AngleIsBetween(float angle, float a, float b)
    {
        angle = NormaliseAngle(angle);

        if (a < b) //Range does not include 0/360 degrees, or 0/360 degrees is a or b.
        {
            return (angle >= a && angle <= b) || (angle == 0 && (a == 360 || b == 360)) || (angle == 360 && (a == 0 || b == 0));
        }
        else if (a > b) //Range does include 0/360 degrees, and 0/360 degrees doesn't need to be a or b.
        {
            return ((angle >= a && angle <= 360)) || (angle >= 0 && angle <= b);
        }
        else //a == b, range is only one angle.
        {
            return angle == a;
        }
    }
}
