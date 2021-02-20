using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public AllBombs allBombs;
    private int currentBomb;
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float Gravity = -10f;

    [SerializeField] private LineRenderer BombGun;
    [SerializeField] private MeshRenderer TrajectorScope;
    private CharacterController Controller;
    private Vector3 _velocity;
    private bool isGrounded = false;

    private void Awake()
    {
       BombGun_setDisable();
    }

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //ground check and gravity
        isGrounded = Controller.isGrounded;
        if (isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        //player walk
        if(Input.GetMouseButton(0) != true)
        {
            BombGun_setDisable();
            Vector3 MoveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Controller.Move(MoveVector * Time.deltaTime * MoveSpeed);
            if (MoveVector != Vector3.zero)
                transform.forward = MoveVector;
        }
        else//player stopMove
        {
           if(allBombs.bombList_counts[allBombs.currentBomb] > 0)
           {
               BombGun_setEnable();
            }
            else
           {
                BombGun_setDisable();
            }

        }



        _velocity.y += Gravity * Time.deltaTime;

        Controller.Move(_velocity * Time.deltaTime);
    }

    private void BombGun_setEnable()
    {
        TrajectorScope.enabled = true;
        BombGun.enabled = true;
    }

    private void BombGun_setDisable()
    {
        TrajectorScope.enabled = false;
        BombGun.enabled = false;
    }

}