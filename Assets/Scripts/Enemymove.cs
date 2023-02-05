using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Enemymove : MonoBehaviour
{
    public Image arrow;
    public float hpEnemy = 101f;
    private float angle = 210f;
    private float radius = 100f;
    public GameObject new1; // muzzle turret = target
    public GameObject turret;
    public float timeLaunch = 0f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool canSeePlayer;
    public bool canShootPlayer;
    private GameObject Player;
    private GameObject EnemyPlayer;
    private NavMeshAgent Enemy;
    public GameObject bulletEnemy;
    public GameObject[] bulletsEnemy; 
    private GameObject myTurret;
    Vector3 LastMove;
    public float distance;
    Vector3 LastMoved;
    void Start() {
        EnemyPlayer = GameObject.FindGameObjectWithTag("EnemyPlayer");
        Enemy = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        myTurret = GameObject.FindGameObjectWithTag("MyTurret");
        arrow.enabled = false;
        //Enemies[] = GameObject.FindGameObjectsWithTag("EnemyPlayer");
    }
    private IEnumerator FOVRoutine(){
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true) {
            yield return wait;
            FieldOfViewCheck();
        }
   }
    private void FixedUpdate() {
        if (hpEnemy > 0 && Player.gameObject.GetComponent<move>().hpPlayer > 0){
            distance = Vector3.Distance(Player.transform.position, EnemyPlayer.transform.position);
            LastMove = LastMoved;
            if (canSeePlayer == true) {
                LastMoved = Player.transform.position;
                turret.transform.LookAt(myTurret.transform.position); 
                timeLaunch += Time.fixedDeltaTime;
                if (timeLaunch >= 5 && canSeePlayer == true) {
                    canShootPlayer = true;
                    timeLaunch = 0;
                }
            else 
                canShootPlayer = false;
            Enemy.enabled = true;
            Enemy.destination = Player.transform.position;
            if(distance <= 35f && canSeePlayer == true) Enemy.destination = Enemy.transform.position;
        }
        else {
            Enemy.enabled = true;
            Enemy.destination = LastMove;
            canShootPlayer = false;
            timeLaunch = 0;
            }
        if (canShootPlayer) {
            Instantiate(bulletEnemy, new1.transform.position, new1.transform.rotation);
            bulletsEnemy = GameObject.FindGameObjectsWithTag("EnemyBullet"); 
            for (int i = 0; i < bulletsEnemy.Length; i++){
            bulletsEnemy[i].GetComponent<Rigidbody>().velocity = turret.transform.forward * 100f;
            Destroy(bulletsEnemy[i], 4f);    
            }
        }}
        else Enemy.enabled = false;
    }
     private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                    canSeePlayer = true;
                    arrow.enabled = true;
                    }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
        }
