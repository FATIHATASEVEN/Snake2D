using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluescript : MonoBehaviour
{
    public snakemove snakes;
    public GameObject Blue;
    private void Start()
    {
        StartCoroutine(Spawnn());
    }
    public IEnumerator Spawnn()
    {
        while (snakes.deathscreen)
        {
            Instantiate(Blue, new Vector3(Random.Range(1, 41), Random.Range(1, 22), 0), Quaternion.identity);

            yield return new WaitForSeconds(12f);
        }
    }
}
