using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public Transform[] parent;
    public GameObject explosion;
    private GameObject player;
    public Transform floor;
    public AudioClip Death;
    private bool dead = true;
    private void Start() {
        parent = GetComponentsInChildren<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate() {
        if (GetComponent<Enemymove>().hpEnemy == 0){
            if (dead) {
                player.GetComponent<move>().died++;
                AudioSource.PlayClipAtPoint(Death, transform.position);
                dead = false;
                Instantiate(explosion, transform.position, floor.transform.rotation);
                }
            for(int i = 0; i < parent.Length; i++){
                parent[i].GetComponent<Rigidbody>().isKinematic = false;
                parent[i].GetComponent<Rigidbody>().useGravity = true;
                try{
                    parent[i].GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(2f, 3f));
                    parent[i].GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(2f, 3f));
                }
                catch {}
            } 
        }
    }
    public void Parent(){
        for (int i = 0; i < parent.Length; i++){
            parent[i].SetParent(null);
        }
    }
}
