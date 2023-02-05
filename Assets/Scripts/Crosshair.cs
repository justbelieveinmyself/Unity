using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private float currentSpread = 5f;
    public float speedSpread = 20f;
    private float speed = 10f;
    public Parts[] parts;
    float t;
    float curSpread;
    [System.Serializable]
    public class Parts
    {
        public RectTransform trans;
        public Vector2 pos;
    }
    private void Update(){
        float movement = Input.GetAxis("Vertical");
        if(movement != 0) currentSpread = 1f * speed; 
        else currentSpread = 5f;
        CrosshairUpdate();
    }
    private void CrosshairUpdate(){
        t = 0.005f * speedSpread;
        curSpread = Mathf.Lerp(curSpread, currentSpread, t);
        for(int i = 0; i < parts.Length; i++){
            Parts p = parts[i];
            p.trans.anchoredPosition = p.pos * curSpread;
        }
    }
}
