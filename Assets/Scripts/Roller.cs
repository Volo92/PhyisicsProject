using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
