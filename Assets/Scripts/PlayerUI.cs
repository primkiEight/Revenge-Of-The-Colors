using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    //public Transform Target; zašto uzimati transform, ako odmah možemo uzeti HealthManager objekta kad nam on ionako treba
    public HealthManager HMTarget;
    public Vector3 Offset;
    public Transform LookAtMe;

    public Slider HealthSlider;

    //HealthManager heatlhManager;

    private void Start()
    {
        //heatlhManager = Target.GetComponent<HealthManager>();
        HMTarget = GetComponentInParent<HealthManager>();
        LookAtMe = GameManager.GM.mainCamera.transform;
    }

    private void Update()
    {
        transform.position = HMTarget.transform.position + Offset;        
        transform.transform.rotation = LookAtMe.transform.rotation; //orijentiran kao i kamera


        //if (heatlhManager)
        //{
            HealthSlider.value = HMTarget.CurrentHP/HMTarget.MaxHP;
        //}
    }

    
}
