using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YEquals0vector
{
    public static Vector3 ConvertVectorToNullY(Vector3 _vector)
    {
        Vector3 vector = new Vector3(_vector.x, 0, _vector.z).normalized;
        return vector;
    }
}
