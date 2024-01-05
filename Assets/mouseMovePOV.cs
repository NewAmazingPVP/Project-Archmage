using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMovePOV : MonoBehaviour{

    public float mouseSens =  100f;
    public Transform playerBody;
    
    
    void Start(){

    }

    
    void Update(){
        
        //may be outdated prob should update to unitys new inputs 
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
