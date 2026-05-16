using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
 //How far should obstacle move
    [SerializeField] Vector3 movementVector;
   
    [SerializeField] float speed;
        Vector3 startPosition;
        Vector3 endPosition;
        float movementFactor;
    
    void Start()
    {
        startPosition= transform.position;
        endPosition = startPosition + movementVector;
    }

  
    void Update()
    {
        movementFactor=Mathf.PingPong(Time.time * speed,1f);
        //Move between start and end using movementFactor
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
