using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<KeyCode> left, right, up, down;

    public float strafeSpeed, jumpForce;
    public List<Rigidbody2D> moveAnchors;
    public Transform jumpAnchor;

    public List<KeyCode> pickUp;
    public Transform leftHand, rightHand;

    public Pickup heldItem;

    Animator anim;

    enum KeyPressType { Down, Hold, Up };

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    bool IsPressingKeyGroup(List<KeyCode> keyGroup, KeyPressType pressType)
    {
        if (pressType == KeyPressType.Down)
        {
            foreach (KeyCode k in keyGroup)
            {
                if (Input.GetKeyDown(k)) return true;
            }
            return false;
        }
        if (pressType == KeyPressType.Up)
        {
            foreach (KeyCode k in keyGroup)
            {
                if (Input.GetKeyUp(k)) return true;
            }
            return false;
        }
        else
        {
            foreach (KeyCode k in keyGroup)
            {
                if (Input.GetKey(k)) return true;
            }
            return false;
        }
    }

    bool IsPressingKeyGroup(List<KeyCode> keyGroup)
    {
        foreach (KeyCode k in keyGroup)
        {
            if (Input.GetKey(k)) return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement --------------------------------------------------

        if (IsPressingKeyGroup(left))
        {
            foreach (Rigidbody2D moveAnchor in moveAnchors)
            {
                anim.Play("RunLeft");
                moveAnchor.AddForce(-Vector2.right * strafeSpeed * Time.deltaTime / moveAnchors.Count);
            }
            
        }
        if (IsPressingKeyGroup(right))
        {
            foreach (Rigidbody2D moveAnchor in moveAnchors)
            {
                anim.Play("RunRight");
                moveAnchor.AddForce(Vector2.right * strafeSpeed * Time.deltaTime / moveAnchors.Count);
            }
        }


        if (!IsPressingKeyGroup(left) && !IsPressingKeyGroup(right))
        {
            anim.Play("Idle");
        }

        // Item Pickup and Handling ----------------------------------

        if (IsPressingKeyGroup(pickUp, KeyPressType.Down))
        {
            foreach (Pickup obj in GameObject.FindObjectsOfType<Pickup>())
            {
                if (Vector2.Distance(leftHand.position, obj.transform.position) < 0.5f
                 || Vector2.Distance(rightHand.position, obj.transform.position) < 0.5f)
                {
                    heldItem = obj;
                    obj.isHeld = true;
                    break;
                }
            }
        }
    }
}
