using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //dodajemo kako bi vrijednosti bile upravljive kroz Inspector
public class CustomRandom //: MonoBehaviour iz ove smo skripte izbrisali Monobehaviour tako da napravimo vlastitu klasu
{
    public float Min;
    public float Max;

    public float RandomValue()
    {
        return Random.Range(Min, Max);
    }
}
