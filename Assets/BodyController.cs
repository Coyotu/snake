using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyController : MonoBehaviour
{
    public int direction=0;
    public float delay = 1;
    private float _posX;
    private float _posY;
    private float startTime = 1.0f;
    private float lastTime = 0.0f;
    
    private async void movement()
    {
        _posX = transform.position.x;
        _posY = transform.position.y;
        if (startTime - delay >= lastTime)
        {
            switch (direction)
            {
                case 0:
                    _posY += 0.3f;
                    break;
                case 1:
                    _posX += 0.3f;
                    break;
                case 2:
                    _posY -= 0.3f;
                    break;
                case 3:
                    _posX -= 0.3f;
                    break;
            }

            lastTime += delay;
        }

        startTime += Time.deltaTime;

        this.transform.position = new Vector3(_posX, _posY, this.transform.position.z);
    }

    private void Update()
    {
        movement();
    }
}
