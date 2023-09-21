using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    //0 = up, 1 right, 2 down, 3 left
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private GameObject _body;
    [SerializeField] private TextMeshProUGUI _score;
    
    private List<GameObject> _bodyPart = new List<GameObject>();
    private BoxCollider2D _collider;

    private int direction = 0;
    private float delay = 1;
    private float _posX;
    private float _posY;
    private float startTime = 1.0f;
    private float lastTime = 0.0f;
    private int index = 0;
    private int score = 0;


    private void Start()
    {
        _bodyPart = new List<GameObject>();
        _foodSpawner.Spawn();
        _collider = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckWalls();
        DefaultMovement();
        ControlMovement();
        UpdateBodiesDirection();
    }
    
    
    //Default movement is applied when you don't press any button
    private void DefaultMovement()
    {
        _posX = transform.position.x;
        _posY = transform.position.y;
        if (startTime - delay >= lastTime)
        {
            switch (direction)
            {
                case 0:
                    _posY += 0.5f;
                    break;
                case 1:
                    _posX += 0.5f;
                    break;
                case 2:
                    _posY -= 0.5f;
                    break;
                case 3:
                    _posX -= 0.5f;
                    break;
            }

            lastTime += delay;
        }

        startTime += Time.deltaTime;

        this.transform.position = new Vector3(_posX, _posY, this.transform.position.z);
    }

    //Control movement
    private void ControlMovement()
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

    //Add new body part
    private void AddComponent()
    {
        score++;
        _score.text = score.ToString();
        GameObject part = Instantiate(_body);
        _bodyPart.Add(part);
        BodyController _bodyController = _bodyPart[index].GetComponent<BodyController>();
        BodyController _part = part.GetComponent<BodyController>();
        _part.direction = _bodyController.direction;
        index++;
    }

    
    //Update bodies direction to last position
    private async void UpdateBodiesDirection()
    {
        float x = this._posX;
        float y = this._posY;
        for (int i = 0; i < _bodyPart.Count; i++)
        {
            if (_bodyPart[i] != null)
            {
                BodyController _bodyController = _bodyPart[i].GetComponent<BodyController>();
                await Task.Delay(TimeSpan.FromSeconds(delay));
                _bodyController.move(x, y);
                x = _bodyPart[i].transform.position.x;
                y = _bodyPart[i].transform.position.y;
            }
        }
    }

    //Stop Game
    private async void EndGame()
    {
        Debug.Log("pauza");
        Time.timeScale = 0;
        await Task.Delay(TimeSpan.FromSeconds(3));
    }
    
    
    //Check collision with food or his body
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Food(Clone)")
        {
            _foodSpawner.Spawn();
            AddComponent();
            Destroy(collision.gameObject);
            if (delay > 0.1f)
                delay -= 0.05f;
        }
        else
        {
            EndGame();

        }
    }
    

    //Check if the head is at screen's edge
    private void CheckWalls()
    {
        if (transform.position.x <-9f)
        {
            direction = 3;
            this.transform.position = new Vector3(9, this.transform.position.y, this.transform.position.z);
        }
        if (transform.position.x > 9f)
        {
            direction = 1;
            this.transform.position = new Vector3(-9, this.transform.position.y, this.transform.position.z);
        }
        if (transform.position.y < -4.5f)
        {
            direction = 2;
            this.transform.position = new Vector3(this.transform.position.x, 4.5f, this.transform.position.z);

        }
        if (transform.position.y > 4.5f)
        {
            direction = 0;
            this.transform.position = new Vector3(this.transform.position.x, -4.5f, this.transform.position.z);
        }
    }
}

