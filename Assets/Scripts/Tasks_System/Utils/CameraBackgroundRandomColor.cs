using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundRandomColor : MonoBehaviour
{
    public bool enabled;
    Camera camera;

    private void Awake()
    {
        camera = this.GetComponent<Camera>();

        camera.clearFlags = CameraClearFlags.SolidColor;

        if (enabled)
            camera.backgroundColor = GenerateRandomColor();
    }
    private Color GenerateRandomColor()
    {
        Color col = new Color();

        col.r = Random.Range(0.00f, 1.00f);
        col.g = Random.Range(0.00f, 1.00f);
        col.b = Random.Range(0.00f, 1.00f);

        return col;
    }
}
