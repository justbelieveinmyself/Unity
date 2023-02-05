using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ScrollTrackEnemy : MonoBehaviour {

    [SerializeField]
    private float scrollSpeed = 0.3f;

    private float offset = 0.0f;
    private Renderer r;
    private NavMeshAgent Enemy;

    private void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("EnemyPlayer").GetComponent<NavMeshAgent>(); 
        r = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (Enemy.enabled == true){
            offset = (offset + Time.fixedDeltaTime * scrollSpeed) % 1f;
            r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        }
    }
}
