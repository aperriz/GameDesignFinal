using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    void Update()
    {
        transform.position = new Vector3(GameObject.Find("Player(Clone)").transform.position.x, GameObject.Find("Player(Clone)").transform.position.y, -100);
    }
}
