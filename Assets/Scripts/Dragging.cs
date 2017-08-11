using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    private bool onClick;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SpringJoint>().connectedBody = GameObject.FindGameObjectWithTag("Joint").GetComponent<Rigidbody>();

    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
    }

    private void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);
    }

    private void OnMouseUp()
    {
        GetComponent<SpringJoint>().connectedAnchor = new Vector3(GetComponent<SpringJoint>().connectedAnchor.x, GetComponent<SpringJoint>().connectedAnchor.y + (9 - Camera.main.WorldToScreenPoint(transform.position).z)/1.5f, GetComponent<SpringJoint>().connectedAnchor.z);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SpringJoint>().spring = 400f;
        StartCoroutine("RespawnBall");
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(3.0f);
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.AddComponent<SpringJoint>();
        GetComponent<SpringJoint>().connectedBody = GameObject.FindGameObjectWithTag("Joint").GetComponent<Rigidbody>();
        GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
        GetComponent<SpringJoint>().anchor = Vector3.zero;
        GetComponent<SpringJoint>().connectedAnchor = new Vector3(0f, 1f, 1f);
        GetComponent<SpringJoint>().spring = 0f;
        GetComponent<SpringJoint>().damper = 0f;
        GetComponent<SpringJoint>().minDistance = 0f;
        GetComponent<SpringJoint>().maxDistance = 0f;
        GetComponent<SpringJoint>().tolerance = 0.025f;
        GetComponent<SpringJoint>().breakForce = 500f;
        transform.position = new Vector3(0f, 0f, -1f);
    }

}
