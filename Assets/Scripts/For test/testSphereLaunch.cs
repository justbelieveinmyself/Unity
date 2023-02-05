using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSphereLaunch : MonoBehaviour
{
    //public Vector3 goalangle; 
    public GameObject bull;
    public GameObject target;
    private GameObject bullet;
    public GameObject goal;
    public Vector3 inputSpeed = new Vector3(0,0,0);
    private Vector3 accel = Vector3.zero;
    private Vector3 speed = Vector3.zero;
    private float determSpeed = 100f;
    private bool start = false;
    private bool fstart = false;
    private float coordinat_x, coordinat_y, coordinat_z, scale_xz_to_y = 10;
    void Start(){
        coordinat_x = 0;
        coordinat_y = 0;
        coordinat_z = 0; 
    }
    public Vector3 set_input_for_x_y(Vector3 inputV){
        coordinat_x = Mathf.Sin(inputV.y * Mathf.PI / 180.0f) ;
        coordinat_z = Mathf.Cos(inputV.y * Mathf.PI / 180.0f) ;
        return inputSpeed = new Vector3(coordinat_x * scale_xz_to_y, coordinat_y, coordinat_z * scale_xz_to_y);
    } 
    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && fstart == false){
            start = true;
            fstart = true;
            Instantiate(bull, target.transform.position, target.transform.rotation);     
        }
        if (start){
            bullet = GameObject.FindGameObjectWithTag("bullet");
            if (fstart) {
                //set_input_for_x_y(bullet.transform.position);
                //goalangle = goal.transform.localEulerAngles;
                speed = set_input_for_x_y(goal.transform.localEulerAngles);
                fstart = false;
            }
        
            //bullet.transform.rotation = Quaternion.LookRotation(target.transform.position);
            //bullet.transform.LookAt(target.transform.position);
            //bullet.transform.position = Quaternion.Euler(0f,45f,0f) * target.transform.position;
            accel = Physics.gravity - ((speed.magnitude * Physics.gravity.magnitude) / Mathf.Pow(determSpeed, 2)) * speed;
            speed = speed + accel * Time.deltaTime;
            //for (int i = 0; i < bullet.Length; i++){
            //Destroy(bullet[i], 10f);    
            //bullet[i].transform.position = bullet[i].transform.position + speed * Time.fixedDeltaTime;
            bullet.transform.position = bullet.transform.position + speed * Time.fixedDeltaTime;
            //}  
        }
    }
}
