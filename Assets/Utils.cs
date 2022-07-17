using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetRandomOffset(float minOffset, float maxOffset)
    {
        var xOffset = Random.Range(minOffset, maxOffset);
        var yOffset = Random.Range(minOffset, maxOffset);

        return new Vector3(xOffset, yOffset, 0);
    }
}
