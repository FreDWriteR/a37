using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController_20_9 : MonoBehaviour
{

    private Rigidbody RB;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int coSpeed = 5;

    public float speed;

    bool allowMove = true, anchor = true, firstMem = true;

    public bool allowGame = true;

    Vector3 MCStart, MCEnd, ballPosition = Vector3.zero, posForSpeed = Vector3.zero;

    private Vector3 direct;

    private GameObject Ball;

    

    private Ray ray;

    

    private RaycastHit hitFloor, hitBall;

    float startTime;


    bool NewBall()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitBall);
        if (hitBall.transform.name != Ball.name && hitBall.transform.name.Substring(0, 4) == "Ball")
        {
            return true;
        }
        return false;
    }

    bool GetBall()
    {
        if (Input.GetMouseButton(0) && (Ball == null || NewBall()))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitBall);
            if (hitBall.transform.name.Substring(0, 4) == "Ball")
            {
                Ball = hitBall.transform.gameObject;
                RB = Ball.GetComponent<Rigidbody>();
                return true;
            }
        }
        if (Ball != null)
            return true;
        return false;
    }

    IEnumerator stayOnFinger()
    {
        
        if (Input.GetMouseButton(0) && GetBall())
        {
            Vector3 Center = Ball.GetComponent<MeshRenderer>().bounds.center;
            float Extent = Ball.GetComponent<MeshRenderer>().bounds.extents.x + 0.1f;
            if (Vector3.Distance(hitBall.point, Center) <= Extent)
            {
                anchor = false;
            }
            while (Input.mousePosition == MCStart && Input.GetMouseButton(0) && Vector3.Distance(hitBall.point, Center) <= Extent)
            {
                ballPosition = hitBall.point;
                ballPosition.x = Ball.transform.position.x;
                Ball.transform.position = ballPosition;
                speed = 0f;
                yield return new WaitForFixedUpdate();
            }
            allowMove = true;
            if (!ballPosition.Equals(Vector3.zero)) {
                 if (firstMem)
                {
                    startTime = Time.time;
                }
                if (Time.time - startTime == 0 && firstMem)
                {
                    posForSpeed = ballPosition;
                    firstMem = false;
                }
                if (Time.time - startTime >= 0.5f)
                {
                    posForSpeed = ballPosition;
                    startTime = Time.time;
                }


            } 
        }
    }

    IEnumerator allowMoveUp()
    {
        if (Input.GetMouseButtonUp(0) && GetBall())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitBall);
            speed = 0f;
            if (!posForSpeed.Equals(Vector3.zero))
            {
                speed = Vector3.Distance(hitBall.point, posForSpeed) / (Time.time - startTime);
                if (speed > 25f)
                    speed = 25f;
                //Debug.Log(speed);
            }
                
            allowMove = true;
            anchor = true;
            float time = Time.time;
            float moment = 0.1f;
            while (Time.time - time < moment)
            {
                RB.AddForce(direct.normalized * speed / coSpeed, ForceMode.VelocityChange);
                yield return new WaitForFixedUpdate();
                //Debug.Log(RB.velocity);
            }
            
            ballPosition = Vector3.zero;
        }
            
    }

    IEnumerator moveToFinger()
    {

        if (Input.GetMouseButton(0) && GetBall())
        {
            allowMove = false;
            MCStart = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitBall);
            
            if (Physics.Raycast(ray, out hitFloor) && !anchor)
            {
                direct = hitFloor.point - Ball.transform.position;
                direct.x = 0f;
                
                while (hitBall.transform.name != Ball.name && Input.GetMouseButton(0))
                {
                    RB.AddForce(direct.normalized * moveSpeed, ForceMode.VelocityChange);
                    yield return new WaitForFixedUpdate();
                    direct = hitFloor.point - Ball.transform.position;
                    direct.x = 0f;
                    Physics.Raycast(ray, out hitBall);
                }
                //direct = Vector3.zero;

                RB.velocity = Vector3.zero;
                StartCoroutine(stayOnFinger());
            }
            if (anchor)
            {
                StartCoroutine(stayOnFinger());
            }

        }
    }

    private void FixedUpdate()
    {

    }
    void Update()
    {
        if (allowGame && allowMove)
        {
            StartCoroutine(moveToFinger());
        }
        StartCoroutine(allowMoveUp());
    }
}

