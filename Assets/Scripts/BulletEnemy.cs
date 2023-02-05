using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletEnemy : MonoBehaviour
{

    public GameObject particle1;
    public GameObject particle2;
    private bool canExplode = true;
    private GameObject player;
    //private move hpChanges;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter(Collision other) {
        if (canExplode){
            if (other.gameObject.tag == "Player"){
                player.gameObject.GetComponent<move>().hpPlayer -= 50;
                player.gameObject.GetComponent<move>().hpChanges();
            };
            if (other.gameObject.tag == "MyTurret"){
                player.gameObject.GetComponent<move>().hpPlayer -= 50;
                player.gameObject.GetComponent<move>().hpChanges();
            };
            gameObject.tag = "xdd";
            GameObject explosion1 = Instantiate(particle1, transform.position, transform.rotation);
            GameObject explosion2 = Instantiate(particle2, transform.position, transform.rotation);
            Destroy(explosion1, 3f);
            Destroy(explosion2, 3f);
            canExplode = false;
        }
    }

}
