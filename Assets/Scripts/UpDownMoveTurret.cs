using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMoveTurret : MonoBehaviour
{
    public Vector3 heightCrosshair;
    public Vector3 notconstant;
    public Canvas canvasH;
    public GameObject crosshair;
    public GameObject crosshairMain;
    private void Start() {
        heightCrosshair = new Vector3(0, crosshair.transform.position.y, 0.0f);
    }
    private void FixedUpdate() {
        notconstant = crosshair.transform.position;
        if (heightCrosshair.y > 590){
            if(crosshair.transform.position.y >= 621) crosshair.transform.position = new Vector3(notconstant.x, 621, notconstant.z);
            if(crosshair.transform.position.y <= 590) crosshair.transform.position = new Vector3(notconstant.x, 590, notconstant.z);
        }
        if(crosshair.transform.position.y < heightCrosshair.y ) transform.localEulerAngles = new Vector3(heightCrosshair.y - crosshair.transform.position.y, 0f);
        else transform.localEulerAngles = new Vector3((heightCrosshair.y - crosshair.transform.position.y), 0f);
        if (transform.eulerAngles.x >= 300) crosshairMain.transform.position = new Vector3(crosshairMain.transform.position.x, (360 - transform.eulerAngles.x) + 690, crosshairMain.transform.position.z);
        else crosshairMain.transform.position = new Vector3(crosshairMain.transform.position.x,  690 - transform.eulerAngles.x, crosshairMain.transform.position.z);
    }
}