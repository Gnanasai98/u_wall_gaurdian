
using UnityEngine;

public class TransformUtilities 
{

    public static Vector2 getRoundPoint(Vector2 inputVector)
    {
        float roundedPosX, roundedPosY;
        roundedPosX = Mathf.Round(inputVector.x * 10f) / 10f;
        roundedPosY = Mathf.Round(inputVector.y * 10f) / 10f;
        Vector2  result = new Vector2(roundedPosX, roundedPosY);
        return result;
    }
}
