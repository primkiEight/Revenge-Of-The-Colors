using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform Target;
    public float Speed = 1.0f;
    public float DistanceToKeep = 1.5f;

    private Transform _transform;

    private void Awake()
    {
        // ovo ispod je zapravo transform = GetComponent<Transform>
        _transform = transform; //ovo je stvar dobre prakse, mogli smo dolje i direktno mijenjati vlasiti transform.position
    }

    private void Update()
    {
        if (Target == null)
            return;

        _transform.LookAt(Target);

        float distanceToTarget = Vector3.Distance(Target.position, _transform.position);

        if (distanceToTarget > DistanceToKeep)
            _transform.position += _transform.forward * Speed * Time.deltaTime; //TRANSFORM.forward pomiče u odnosu na lokalnu, vlastitu, koordinatnu os, to nam je jedinični vektor kretanja ne u odnosu na globalne XYZ, već svoje lokalne
        else
            _transform.position -= _transform.forward * Speed * Time.deltaTime;        
    }

}