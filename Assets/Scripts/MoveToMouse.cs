using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
 {
     //  e.g. 0.2 = slow, 0.8 = fast, 2 = very fast, Infinity = instant
     [Tooltip("If rotationSpeed == 0.5, then it takes 2 seconds to spin 180 degrees")]
     [SerializeField] [Range(0, 10)] float rotationSpeed = 0.5f;
 
     [Tooltip("If isInstant == true, then rotationalSpeed == Infinity")]  
     [SerializeField] bool isInstant = false;
 
     Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.
 
     void Start()
     {
         _camera = Camera.main;
     }
 
     void FixedUpdate()
     {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseDirection = ray.direction;
        mouseDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(mouseDirection);   
         if (isInstant)
         {
             transform.rotation = targetRotation;
         }
         else
         {
             Quaternion currentRotation = transform.rotation;
             float angularDifference = Quaternion.Angle(currentRotation, targetRotation);
             // will always be positive (or zero)
             if (angularDifference > 0) transform.rotation = Quaternion.Slerp(
                                          currentRotation,
                                          targetRotation,
                                          (rotationSpeed * 180 * Time.fixedDeltaTime) / angularDifference
                                     );
             else transform.rotation = targetRotation;
         }
     }
 }