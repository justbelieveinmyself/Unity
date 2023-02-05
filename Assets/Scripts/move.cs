using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;
public class move : MonoBehaviour
{
    public float hpPlayer = 201;
    private float tankSpeed = 0;
    private float tankRotate = 30;
    private GameObject turret;
    private GameObject enemy;
    private GameObject _camera;
    private float xMove;
    private float zMove;
    private float accel = 3;
    public Text textReload;
    private GameObject crosshair;
    private bool die = true;
    public GameObject tank;
    public Transform floor;
    public AudioSource player;
    public AudioClip motorOn;
    public AudioClip Death;
    public AudioClip Hit;
    public Image hpBar;
    private bool firsthit = true;
    public Transform[] parent;
    public GameObject explosion;
    public int died = 0;
    private void Start() {
        player = GetComponent<AudioSource> ();
        Application.targetFrameRate = 60; //fps 
        turret = GameObject.FindGameObjectWithTag("MyTurret");
        enemy = GameObject.FindGameObjectWithTag("EnemyPlayer");
        _camera = GameObject.Find("CMFL");
        crosshair = GameObject.Find("CrosshairMain");
    }
    private void FixedUpdate() {
        
        if (hpPlayer == 50 && firsthit) {
            firsthit = false;
            player.clip = Hit;
            player.Play();
        }
        if (hpPlayer == 201) {
            hpPlayer = hpPlayer-1;
            player.clip = motorOn;
            player.Play();
        } 
        if (hpPlayer > 0) { //если игрок жив
            xMove = Input.GetAxis("Vertical"); //отслеживает нажатия W и S и преобразует их в значение -1 от 1
            zMove = Input.GetAxis("Horizontal"); //отслеживает нажатия A и D и преобразует их в значение -1 от 1
        if (xMove != 0 && tankSpeed <= 10) tankSpeed = tankSpeed + ( accel * Time.fixedDeltaTime); //Ограничение по скорости в 10 и расчёт скорости 
        if (tankSpeed > 0 && xMove == 0) tankSpeed = tankSpeed - (accel * Time.fixedDeltaTime); //Замедление движения если игрок ничего не жмёт
        transform.Translate(0f, 0f, xMove * tankSpeed * Time.fixedDeltaTime); //передвигаю согласно установленной скорости за время
        transform.Rotate(0f, zMove * tankRotate * Time.fixedDeltaTime, 0f); //поворачиваю корпус танка с заданной скоростью
        }
        else {
            gameObject.GetComponent<Shooting>().enabled = false;
            turret.GetComponent<MoveToMouse>().enabled = false;
            enemy.GetComponent<Enemymove>().enabled = false;
            crosshair.SetActive(false);
            textReload.enabled = false;
            _camera.GetComponent<Cinemachine.CinemachineFreeLook>().m_Orbits[1].m_Height = 15f;
            _camera.GetComponent<Cinemachine.CinemachineFreeLook>().m_Orbits[1].m_Radius = 40f;
            if (die){
                //Destroy(gameObject, 0.01f);
                gameObject.SetActive(false);
                die = false;
                AudioSource.PlayClipAtPoint(Death, transform.position);
                Instantiate(explosion, transform.position, floor.transform.rotation);
                Instantiate(tank, transform.position, transform.rotation);
                parent = GetComponentsInChildren<Transform>();
                for(int i = 0; i < parent.Length; i++){
                    try{
                        parent[i].GetComponent<Rigidbody>().isKinematic = false; //включает физику
                        parent[i].GetComponent<Rigidbody>().useGravity = true;
                        parent[i].GetComponent<Rigidbody>().AddForce(transform.up * UnityEngine.Random.Range(2f, 3f));
                        parent[i].GetComponent<Rigidbody>().AddForce(transform.right * UnityEngine.Random.Range(2f, 3f));
                    }
                    catch {}
                }
            }
        }
    }
    public void Parent(){
        for (int i = 0; i < parent.Length; i++){
            parent[i].SetParent(null);
        }
    }
        public void hpChanges(){
        hpBar.fillAmount = hpPlayer/200;
    }
}