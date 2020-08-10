using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float delay;
    public GameObject[] FX;
    // Start is called before the first frame update
    void Start()
    {
        delay = 1 + (Random.Range(1, 101) / 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
        }
        else
        {
            Instantiate(FX[Random.Range(0, FX.Length)], transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
