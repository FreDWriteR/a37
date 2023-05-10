using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    [SerializeField] private LayerMask activeWall;
    public int countActiveBalls = 0;
    Rigidbody RB;

    Vector3 BallPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Substring(0, 4) == "Ball" && !other.gameObject.GetComponent<Rigidbody>().useGravity
            && other.gameObject.GetComponent<Rigidbody>().constraints == (RigidbodyConstraints.FreezePositionX)) 
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            other.gameObject.layer = 3;
            countActiveBalls++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Substring(0, 4) == "Ball")
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hitBall;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hitBall);
                if (hitBall.transform.name == other.name)
                {
                    if (hitBall.transform.gameObject.GetComponent<Rigidbody>().constraints == (RigidbodyConstraints.FreezePositionZ |
                                                                                               RigidbodyConstraints.FreezePositionX))
                    {
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                    }
                }
            }
            if (!Input.GetMouseButton(0))
            {
                if (other.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezePositionX)
                {
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                }
                if (gameObject.name == "SpawnLeft" && other.gameObject.transform.position.z != -3f)
                {
                    BallPos = other.gameObject.transform.position;
                    BallPos.z = -3f;
                    other.gameObject.transform.position = BallPos;
                }
                if (gameObject.name == "SpawnRight" && other.gameObject.transform.position.z != 1.32f)
                {
                    BallPos = other.gameObject.transform.position;
                    BallPos.z = 1.32f;
                    other.gameObject.transform.position = BallPos;
                }
            }
        }
    }


}
