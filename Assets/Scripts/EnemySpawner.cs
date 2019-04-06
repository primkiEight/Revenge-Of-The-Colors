using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public struct IntervalValues
    {
        public float MinValue;
        public float MaxValue;
    }

    public IntervalValues IntervalValuesMinMax;

    public GameObject ObjectToSpawn;

    private Transform _playerTarget;

    public bool IsActive = true;

    private void Start()
    {
        _playerTarget = GameManager.GM.thePlayer.transform;
    }

    void OnEnable()
    {        
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (IsActive)
        {
            float randomWaitTime = Random.Range(IntervalValuesMinMax.MinValue, IntervalValuesMinMax.MaxValue);
            yield return new WaitForSeconds(randomWaitTime);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject objectClone = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
        objectClone.transform.SetParent(transform);

        FollowTarget followTarget = objectClone.GetComponent<FollowTarget>();

        if (followTarget)
        {
            followTarget.Target = _playerTarget;
        }
    }




    /*
    [System.Serializable]
    public struct IntervalValues
    {
        public float MinValue;
        public float MaxValue;
    }

    public IntervalValues IntervalValuesMinMax;

    public GameObject ObjectToSpawn;

    private float _interval;
    private float _stopwatch = 0.0f;

    private Transform _playerTarget;

    private void Start()
    {
        _interval = Random.Range(IntervalValuesMinMax.MinValue, IntervalValuesMinMax.MaxValue);

        _playerTarget = GameManager.GM.thePlayer.transform;

        SpawnObject();
    }

    private void Update()
    {
        _stopwatch += Time.deltaTime;

        if(_stopwatch >= _interval)
        {
            SpawnObject();
            _stopwatch = 0.0f;
        }
    }

    private void SpawnObject()
    {
        GameObject objectClone = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
        objectClone.transform.SetParent(transform); //spawner objekt nam je parent klona, radi bolje organizacije hijerarhije

        FollowTarget followTarget = objectClone.GetComponent<FollowTarget>();

        if(followTarget)
        {
            followTarget.Target = _playerTarget;
        }
    }*/

}