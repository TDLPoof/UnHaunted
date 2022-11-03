using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float force, targetRotation = -1.6f/*, severThreshold = 2*/;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        if (targetRotation == -1.6f) targetRotation = rb.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.fixedDeltaTime));
        /*if (GetComponent<HingeJoint2D>().connectedBody.position.Distance(transform.position) > severThreshold)
        {
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(GetComponent<Balance>());
        }*/
    }
}
