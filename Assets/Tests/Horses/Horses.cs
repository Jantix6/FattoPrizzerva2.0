using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Horses : MonoBehaviour
{
    public int posX;
    public int posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DownLeft()
    {
        posX = posX - 1;
        posY = posY - 2;
    }

    public void DownRight()
    {
        posX = posX + 1;
        posY = posY - 2;
    }

    public void UpLeft()
    {
        posX = posX - 1;
        posY = posY + 2;
    }

    public void UpRight()
    {
        posX = posX + 1;
        posY = posY + 2;
    }

    public void LeftUp()
    {
        posX = posX - 2;
        posY = posY + 1;
    }

    public void LeftDown()
    {
        posX = posX - 2;
        posY = posY - 1;
    }

    public void RightUp()
    {
        posX = posX + 2;
        posY = posY + 1;
    }

    public void RightDown()
    {
        posX = posX + 2;
        posY = posY - 1;
    }
}
