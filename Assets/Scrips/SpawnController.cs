using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject powerUpPrefab;
    private GameObject instance;
    private DateTime time;
    // Start is called before the first frame update
    void Start()
    {
        time = getRamdomTime();
        InstantiatePowerUp();
    }

    // Update is called once per frame
    void Update()
    {
       InstantiatePowerUp();
    }

    void InstantiatePowerUp()
    {
        if (instance == null && DateTime.Now.Subtract(time).TotalSeconds > 25) {
            instance = Instantiate(powerUpPrefab, gameObject.transform);
            instance.transform.position = gameObject.transform.position;
            time = getRamdomTime();
        }
    }

    DateTime getRamdomTime()
    {
        var a = UnityEngine.Random.Range(1, 5);
        var b = UnityEngine.Random.Range(6, 10);
        return DateTime.Now.AddSeconds(UnityEngine.Random.Range(a, b));
    }
}
