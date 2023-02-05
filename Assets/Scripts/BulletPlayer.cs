using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BulletPlayer : MonoBehaviour
{
    public GameObject particle1;
    public GameObject particle2;
    private bool canExplode = true;
    //public bool check = false;
    private GameObject enemy;
    private void Start() {
        enemy = GameObject.FindGameObjectWithTag("EnemyPlayer");
    }
    private void OnCollisionEnter(Collision other) {
        if (canExplode){
            if (other.gameObject.tag == "EnemyPlayer"){
                other.gameObject.GetComponent<Enemymove>().hpEnemy = 0;
                //check = true;
            };
            if (other.gameObject.tag == "EnemyTower"){
                try{
                other.gameObject.GetComponent<Enemymove>().hpEnemy = 0;
                }
                catch{}
                //check = true;
            };
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.tag = "xdd";
            GameObject explosion1 = Instantiate(particle1, transform.position, transform.rotation);
            GameObject explosion2 = Instantiate(particle2, transform.position, transform.rotation);
            Destroy(explosion1, 3f);
            Destroy(explosion2, 3f);
            canExplode = false;
        }
    }
}
