using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _myRigidBody;
    private HealthManager theHealthManager;
    
    public float MoveSpeed;
    public float ActiveMoveSpeed;    
    private float _moveH;
    private float _moveV;

    //ChangeAppearance
    private Color _initialMaterialColor;
    //private Material _currentMaterial;
    public Material InvincibilityAppearance;

    /*
    //JUMP V1:
    public float JumpSpeed;
    private float _jump;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask WhatIsGround; //i za JUMP V2
    private Collider[] _isGrounded;
    */

    //JUMP V2:
    public bool PlayerCanJump = false;
    private bool _jump;
    public float JumpForce;
    public LayerMask WhatIsGround; //i za JUMP V1
    private SphereCollider _collider;

    private float _speedUp;

    private float _break;
    private float _countBreakDuration;
    private bool _breaking;

    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
        theHealthManager = GetComponent<HealthManager>();

        _breaking = false;
        ActiveMoveSpeed = MoveSpeed;

        _initialMaterialColor = GetComponent<Renderer>().material.color;

        //JUMP V2:
        _collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (!theHealthManager.IsRespawning)
        {
            if (!_breaking)
            {
                _moveH = Input.GetAxisRaw("Horizontal");
                //_moveV = 1.0f;
                _moveV = Input.GetAxisRaw("Vertical");
                //_speedUp = Input.GetAxisRaw("SpeedUp");
            }
            
            if (!_breaking && PlayerCanJump)
            {
                _jump = Input.GetButtonDown("Jump");
            }
                      
            _break = Input.GetAxis("Break");
        }
    }

    private void FixedUpdate()
    {
        Vector3 _direction = new Vector3(_moveH, 0.0f, _moveV);

        //player movement on X and Z axes with _activeMoveSpeed
        _myRigidBody.AddForce(_direction * ActiveMoveSpeed);

        //_myRigidBody.velocity = new Vector3(_moveH, 0.0f, _moveV); //miče se bez sile, doslovno "fizičko" mijenjanje pozicije

        //player speeding up
        if (_speedUp > 0 && !_breaking)
        {
            _myRigidBody.AddForce(_direction, ForceMode.Impulse);
        }

        //player breaking speed
        if (_break > 0)
        {
            _breaking = true;
            _countBreakDuration = 1f;

            if (_countBreakDuration > 0)
            {
                _countBreakDuration -= Time.deltaTime;
                _myRigidBody.velocity = new Vector3(_direction.x * _countBreakDuration, _direction.y, _direction.z * _countBreakDuration);
            }

            _breaking = false;
        }

        //JUMP V1: player jumping on Y axes with JumpSpeed
        /*_isGrounded = Physics.OverlapSphere(GroundCheck.position, GroundCheckRadius, WhatIsGround);
        if (_jump > 0 && _isGrounded.Length > 0)
        {
            _myRigidBody.velocity = new Vector3(_myRigidBody.velocity.x, JumpSpeed, _myRigidBody.velocity.z);

            //_myRigidBody.AddForce(new Vector3(0f, 3f, 0f), ForceMode.Impulse); //dodavanje sile inercije
        }*/

        //JUMP V2:
        if (IsGrounded() && _jump)
        {
            _myRigidBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            //_myRigidBody.AddForce(Vector3.up, ForceMode.Impulse);
            GameManager.GM.JumpSound.Play();
        }
    }

    //JUMP V2:
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(_collider.bounds.center, new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z), _collider.radius * 0.9f, WhatIsGround);
    }

    public IEnumerator BoostPlayerCo(Vector2 BoosterSpeedAndDuration)
    {
        GameManager.GM.BackgroundAudioSource.pitch = 1.5f;
        ActiveMoveSpeed *= BoosterSpeedAndDuration.x; //Vector.x speed
        GameManager.GM.IsBoosted = true;        
        yield return new WaitForSeconds(BoosterSpeedAndDuration.y); //Vector.x duration
        ActiveMoveSpeed = MoveSpeed;
        GameManager.GM.IsBoosted = false;
        GameManager.GM.BackgroundAudioSource.pitch = 1f;
    }

   public void ChangeAppearance(string ChangeAppearance)
    {
        if (InvincibilityAppearance)
        {
            if (ChangeAppearance == "InvincibilityON")
                GetComponent<Renderer>().material.color = InvincibilityAppearance.color;
            if (ChangeAppearance == "InvincibilityOFF")
                GetComponent<Renderer>().material.color = _initialMaterialColor;
        }        
    }

    public void ResetPlayer()
    {
        _myRigidBody.velocity = Vector3.zero;
        _breaking = false;
        ActiveMoveSpeed = MoveSpeed;
        GameManager.GM.IsBoosted = false;
        GetComponent<Renderer>().material.color = _initialMaterialColor;
    }

    public void CanJump()
    {
        PlayerCanJump = true;
    }
}
