using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkManager : MonoBehaviour
{
    public GameObject firework;
    public GameObject[] firePoint;
    public bool isFinished;
    private float fireDelay;
    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            if (fireDelay >= 0)
            {
                fireDelay -= Time.deltaTime;
            }
            else
            {
                Instantiate(firework, firePoint[Random.Range(0, firePoint.Length)].transform);
                fireDelay = ((Random.Range(1, 3) + (Random.Range(1, 101) / 100)) / 2);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFinished = true;
        }
    }
}
