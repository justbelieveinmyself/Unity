using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.SceneManagement;
public class CameraConroller : MonoBehaviour
{
    public GameObject crosshair;
    private  GameObject player;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        //System.Windows.Forms.Cursor.position
        //Cursor.visible = false;
    }
    private void FixedUpdate() {
        float y = Input.GetAxis("Mouse Y");
        crosshair.transform.position += new Vector3(0.0f, y, 0.0f);
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.6f, 0.0f);
        Vector3 crosshairOrigin = crosshair.transform.position - screenCenter;
        if (crosshairOrigin.sqrMagnitude > 10000.0f){
            crosshairOrigin.Normalize();
            crosshairOrigin *= 100.0f * Time.fixedDeltaTime;
        }
        crosshair.transform.position = crosshairOrigin + screenCenter;
        if (player.GetComponent<move>().hpPlayer <= 0 || player.GetComponent<move>().died == 2){
            Camera.main.GetComponent<EnemiesPosition>().enabled = false;
            //arrow.enabled = false;
            StartCoroutine(LoadMenu());
        } 
    }
     IEnumerator LoadMenu(){
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
     }   
        //Quaternion targetRotation = Quaternion.LookRotation(crosshair.transform.position - ship.transform.position);
        //ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, targetRotation, speed* Time.deltaTime);
}