using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour {

    public float Timer = 1.0f;

    private void Start()
    {
        Invoke("DestroyMe", Timer); //Invoke pokrene korutinu koja čeka Timer vrijeme, i onda pozove metodu
        //Postoje i CancelInvoke za prekid Invoke-a, te InvokeRepeating za ponzivanje nakon nekog vremena i dalje s nekim intervalom
        //Ako želimo randomness timera, ovime ga ne možemo postići jer će Invoke uvijek raditi s onim inicijalnim kada je funkcija prvi put pozvana
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    /*
    private float _stopwatch = 0.0f;

    private void Update()
    {
        _stopwatch += Time.deltaTime;

        if (_stopwatch >= Timer)
            Destroy(gameObject);
    }*/

}
