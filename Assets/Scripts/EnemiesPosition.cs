using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesPosition : MonoBehaviour
{
    public Image arrow;
    public Transform enemy;
    public Vector3 offset;
    public GameObject BulletPl;
    private void Update() {
        float minX = arrow.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = arrow.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(enemy.position + offset);
        if(Vector3.Dot((enemy.position - transform.position), transform.forward) < 0){
            if (pos.x < Screen.width / 2) pos.x = maxX;
            else pos.x = minX;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);   
        arrow.transform.position = pos;
        /*if(enemy.gameObject.GetComponent<Enemymove>().hpEnemy == 0 && BulletPl.GetComponent<BulletPlayer>().check == true) {
            enemy.gameObject.GetComponent<Enemymove>().enabled = false;
            arrow.gameObject.SetActive(false);
    }} */
}}
