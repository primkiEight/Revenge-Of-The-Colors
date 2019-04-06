using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public GameObject ObjectToSpawn;

    //public Vector2 Interval; koristit ćemo CustomRandom vlastitu klasu (skripta CustomRandom)

    public CustomRandom Interval;

    public bool IsActive = true;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (IsActive) {
            //float randomWaitTime = Random.Range(Interval.x, Interval.y);
            float randomWaitTime = Interval.RandomValue(); //pozivamo vlastitu klasu iz objekta Interval koja nam vraća Random između svojih min i max
            yield return new WaitForSeconds(randomWaitTime);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject objectClone = Instantiate(ObjectToSpawn, transform.position, transform.rotation); //ne pišem quaterion.identity kao treći parametar, jer se ne rotira kako je predviđeno za coin
        objectClone.transform.SetParent(transform);
    }
}
