using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Food;
    public float delay = 1;
    private float startTime = 1.0f;
    private float lastTime = 0.0f;
    private double x;
    private double y;

    // Update is called once per frame
    public void Spawn()
    {
        System.Random random = new System.Random();

        x = random.NextDouble() * (9.0 - (-9.0)) + (-9.0);
        y = random.NextDouble() * (4.5 - (-4.5)) + (-4.5);
        Instantiate(Food);
        Food.transform.position = new Vector3((float)x, (float)y, this.transform.position.z);
    }
}
