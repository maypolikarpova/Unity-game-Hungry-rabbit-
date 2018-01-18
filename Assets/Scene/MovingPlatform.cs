using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    public float time_to_wait;
    float delay;
    public Vector3 SpeedAndDirection;

    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        delay = time_to_wait;
    }

   bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target)<=0.2f;
    }
    
    bool toB = true;
    
    void FixedUpdate()
    {
        time_to_wait -= Time.deltaTime;
        if (time_to_wait <= 0)
        {
            if (toB)
            {
                if (isArrived(this.transform.position, this.pointB) == false)
                {
                    this.transform.Translate(SpeedAndDirection * Time.deltaTime);
                }
                else
                {
                    toB = false;
                    time_to_wait = delay;
                }
            }
            if (toB == false)
            {
                if (isArrived(this.pointA, this.transform.position) == false)
                {
                    this.transform.Translate(-SpeedAndDirection * Time.deltaTime);
                }
                else
                {
                    toB = true;
                    time_to_wait = delay;
                }
            }

        }
    }
}
