using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollowPlayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent enemy;
    //public Animator anim;
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float walkRadius = 2f;
    private Vector3 finalPosition;
    public FOVDetection FovDetect;
    public float WanderUpdatePosition = 1f;
    public float findingTime = 3f;
    private float startFindingTime;
    public bool findState = false;

    public Transform shootPosStart;
    float bulletSpeed = 1100;
    public GameObject bullet;
    bool isOnceChasePlayer = false;


    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        FovDetect = GetComponent<FOVDetection>();
        InvokeRepeating("WanderRandomPositions", 0.0f, WanderUpdatePosition);
        startFindingTime = findingTime;

    }

    // Update is called once per frame
    void Update()
    {
        if ((FovDetect.isInFov == true))
        {
            if(isOnceChasePlayer == false)
            {
                isOnceChasePlayer = true;
                StartCoroutine("ChasePlayerCorrutine"); //то преследуй
                findState = true;
            }

        }
        else if ((FovDetect.isInFov == false) && (findState == true))
        {
            FindingPlayer();//просто ходи
        }


    }


    void WanderRandomPositions()//просто ходит
    {
        if(FovDetect.isInFov != true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            finalPosition = hit.position;
            if(enemy.isActiveAndEnabled)
            {
                enemy.SetDestination(finalPosition);
                enemy.speed = walkSpeed;
            }

        }

    }


    void FindingPlayer()//ходит (ищет игрока)
    {
        findingTime -= Time.deltaTime;
        enemy.SetDestination(player.transform.position);
        enemy.speed = walkSpeed;
        if (findingTime < 0)
        {
            //если не нашел то враг просто ходит
            findingTime = startFindingTime;
            findState = false;
            InvokeRepeating("WanderRandomPositions", 0.0f, WanderUpdatePosition);
        }

    }


    IEnumerator ChasePlayerCorrutine()//расчет пути преселования игрока
    {
        enemy.SetDestination(player.transform.position);
        GameObject tempBullet = Instantiate(bullet, shootPosStart.position, shootPosStart.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
    
        //bomb.velocity = transform.forward * 100;
        yield return new WaitForSeconds(2f);
        isOnceChasePlayer = false;
    }


}
