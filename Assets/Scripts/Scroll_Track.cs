using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Track : MonoBehaviour {

    [SerializeField]
    private float scrollSpeed = 0.3f;

    private float offset = 0.0f;
    private Renderer r;

    private void Start()
    {
        r = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
            offset = (offset + Time.fixedDeltaTime * scrollSpeed) % 1f;
            r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        }
    }
}
