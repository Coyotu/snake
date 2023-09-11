using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    //0 = up, 1 right, 2 down, 3 left
    private int direction=0;
    private float delay = 1;
    private float _posX;
    private float _posY;
    private float startTime = 1.0f;
    private float lastTime = 0.0f;

    [SerializeField] private GameObject _body;
 
    private List<GameObject> _bodyPart = new List<GameObject>();
    private int index = 0;


    private void Start()
    {
        _bodyPart = new List<GameObject>();
    }

    private void Update()
    {
       movement();
       ChangePosition();
       if(Input.GetKeyDown(KeyCode.E))
           AddComponent();
    }

    private void movement()
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

    private void ChangePosition()
    {
        switch (direction)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    direction = 1;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    direction = 3;
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    direction = 0;
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    direction = 2;
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    direction = 1;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    direction = 3;
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    direction = 0;
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    direction = 2;
                break;
        }
    }

    private void AddComponent()
    {
        GameObject part = Instantiate(_body);
        _bodyPart.Add(part);
        index++;
    }

    private void UpdateBodiesDirection()
    {
        for (int i = 0; i < _bodyPart.Count; i++)
        {

            BodyController _bodyController = _bodyPart[i].GetComponent<BodyController>();
            _bodyController.direction

        }
    }
}

