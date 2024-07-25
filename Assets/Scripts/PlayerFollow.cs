using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject player; 
    private float targetPoseX;
    private float targetPoseY;
    private float posX;
    private float posy;
    public float dereMax;
    public float izqMax;
    public float alturaMax;
    public float alturaMin;
    public float speed;
    public bool encendida = true;
    void Awake() {
        posX= targetPoseX + dereMax;
        posy = targetPoseY + alturaMin;
        transform.position= Vector3.Lerp(transform.position, new Vector3(posX,posy,-4),1);
    }
    void MoveCam(){
        if(encendida){
            if(player){
                targetPoseX= player.transform.position.x;
                targetPoseY= player.transform.position.y;
                if(targetPoseX> dereMax && targetPoseX < izqMax){
                    posX= targetPoseX;
                }
                if(targetPoseY < alturaMax && targetPoseY > alturaMin){
                    posy= targetPoseY;
                }
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posy,-4),speed*Time.deltaTime);
        }
    }
    void LateUpdate()
    {
        MoveCam();
    }
}


