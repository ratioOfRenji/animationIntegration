using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsRotation : MonoBehaviour
{
    public Transform left;
    public Animator playerAnimator;

    
    void Update()
    {
       
        CalculateLegsRotation();
    }

    void CalculateLegsRotation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 rotation = new Vector3(-horizontal, 0, -vertical);
        switch (rotation)
        {
            case Vector3 v when v.Equals(new Vector3(1, 0, 0)):
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(-1, 0, 0)):
                transform.localEulerAngles = new Vector3(0, -180, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(0, 0, -1)):
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(0, 0, 1)):
                transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(-1, 0, -1)):
                transform.localEulerAngles = new Vector3(0, -225, 0);
                break;

            case Vector3 v when v.Equals(new Vector3(1, 0, 1)):
                transform.localEulerAngles = new Vector3(0, -45, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(1, 0, -1)):
                transform.localEulerAngles = new Vector3(0, 45, 0);
                break;
            case Vector3 v when v.Equals(new Vector3(-1, 0, 1)):
                transform.localEulerAngles = new Vector3(0, -135, 0);
                break;


            default:
                break;
        }
        if (rotation == Vector3.zero)
        {
            playerAnimator.Play("Right-Idle");
        }
        if (rotation != Vector3.zero)
        {
            playerAnimator.Play("Take 001");
        }

    }
}
