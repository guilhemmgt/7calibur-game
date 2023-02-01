using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NEVER USED
public class GroundCheck_System : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Sword")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Sword")
        {
            isGrounded = false;
        }
    }
}
