using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    private bool onClick;
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private GameObject SoftBall;
    [SerializeField]
    private GameObject HardBall;

    private void Awake()
    {
        SoftBall.GetComponent<MeshRenderer>().enabled = false;
        SoftBall.GetComponent<Dragging>().enabled = false;
        SoftBall.GetComponent<SphereCollider>().enabled = false;
        HardBall.GetComponent<MeshRenderer>().enabled = false;
        HardBall.GetComponent<Dragging>().enabled = false;
        HardBall.GetComponent<SphereCollider>().enabled = false;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SpringJoint>().connectedBody = GameObject.FindGameObjectWithTag("Joint").GetComponent<Rigidbody>();
        Ball = GameObject.FindGameObjectWithTag("Ball");
        SoftBall = GameObject.FindGameObjectWithTag("SoftBall");
        HardBall = GameObject.FindGameObjectWithTag("HardBall");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameObject.Equals(Ball))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                SoftBall.GetComponent<MeshRenderer>().enabled = true;
                SoftBall.GetComponent<Dragging>().enabled = true;
                SoftBall.GetComponent<SphereCollider>().enabled = true;
            } else if (gameObject.Equals(SoftBall))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                HardBall.GetComponent<MeshRenderer>().enabled = true;
                HardBall.GetComponent<Dragging>().enabled = true;
                HardBall.GetComponent<SphereCollider>().enabled = true;
            } else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Ball.GetComponent<MeshRenderer>().enabled = true;
                Ball.GetComponent<Dragging>().enabled = true;
                Ball.GetComponent<SphereCollider>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gameObject.Equals(Ball))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                HardBall.GetComponent<MeshRenderer>().enabled = true;
                HardBall.GetComponent<Dragging>().enabled = true;
                HardBall.GetComponent<SphereCollider>().enabled = true;
            }
            else if (gameObject.Equals(SoftBall))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Ball.GetComponent<MeshRenderer>().enabled = true;
                Ball.GetComponent<Dragging>().enabled = true;
                Ball.GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Dragging>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                SoftBall.GetComponent<MeshRenderer>().enabled = true;
                SoftBall.GetComponent<Dragging>().enabled = true;
                SoftBall.GetComponent<SphereCollider>().enabled = true;
            }
        }
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
        if (gameObject.Equals(SoftBall))
        {
            GetComponent<SpringJoint>().spring = 1400f;
        }
        else if (gameObject.Equals(HardBall))
        {
            GetComponent<SpringJoint>().spring = 13000f;
        } else
        {
            GetComponent<SpringJoint>().spring = 2000f;
        }
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
        if (gameObject.Equals(SoftBall))
        {
            transform.position = new Vector3(0f, -0.3f, -1f);
        }
        else if (gameObject.Equals(HardBall))
        {
            transform.position = new Vector3(0f, 0.3f, -1f);
        }
        else
        {
            transform.position = new Vector3(0f, 0f, -1f);
        }
    }

}
