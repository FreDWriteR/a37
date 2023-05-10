using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lotController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject ascensionBall;

    private GameObject GO;

    [SerializeField] private Camera cam;

    bool allowAscention = true;

    [SerializeField] private List<GameObject> Lots;
    [SerializeField] private GameObject septumTrigger;
    [SerializeField] private GameObject noBall, yesBall, Septum;

    private int yes, no;

    private void Start()
    {
        List<int> LostTemp = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
        int j;
        for (int i = 0; i < 6; i++)
        {
            j = UnityEngine.Random.Range(0, LostTemp.Count);
            Lots[LostTemp[j]].tag = "BallNo";
            LostTemp.RemoveAt(j);
        }
        for (int i = 6; i < 14; i++)
        {
            j = UnityEngine.Random.Range(0, LostTemp.Count);
            Lots[LostTemp[j]].tag = "BallYes";
            LostTemp.RemoveAt(j);
        }

    }

    private void showLots()
    {
        gameObject.GetComponent<ballController>().allowGame = false;
        Vector3 BallPos;
        for (int i = 0; i < 14; i++)
        {
            if (Lots[i].tag == "BallNo" && Lots[i].GetComponent<Rigidbody>().useGravity) 
            {
                GO = Instantiate(noBall, Lots[i].transform.position, Quaternion.Euler(90, 0, -90));
                GO.transform.parent = Lots[i].transform;
                BallPos = GO.transform.position;
                BallPos.x += 0.1f;
                GO.transform.position = BallPos;
            }
            if (Lots[i].tag == "BallYes" && Lots[i].GetComponent<Rigidbody>().useGravity)
            {
                GO = Instantiate(yesBall, Lots[i].transform.position, Quaternion.Euler(90, 0, -90));
                BallPos = Lots[i].transform.position;
                BallPos.x -= 4;
                Lots[i].transform.position = BallPos;
                GO.transform.parent = Lots[i].transform;
                BallPos = GO.transform.position;
                BallPos.x += 0.1f;
                GO.transform.position = BallPos;

            }
            Lots[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }

    IEnumerator Ascension()
    {
        for (int i = 0; i < 14; i++)
        {
            if (Lots[i].name == septumTrigger.GetComponent<hitCount>().BallName)
            {
                ascensionBall = Lots[i];
            }
        }

        float endPos = ascensionBall.transform.position.y + 3f;
        Vector3 startPos = ascensionBall.transform.position;
        Vector3 curPos = ascensionBall.transform.position;

        float t = 0;
        int j = 0;
        float d = 0;
        while(ascensionBall.transform.position.y != endPos)
        {
            t += Time.deltaTime;
            d += Time.deltaTime;
            curPos.y = Mathf.Lerp(startPos.y, endPos, t);
            ascensionBall.transform.position = curPos;
            j++;
            Debug.Log(j);
            if (d >= 0.01f)
            {
                yield return new WaitForSeconds(d / 1.2f);
                d = 0;
            } 
            
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (septumTrigger.GetComponent<hitCount>().count >= 100 && allowAscention)
        {
            showLots();
            StartCoroutine(Ascension());
            allowAscention = false;
            septumTrigger.GetComponent<BoxCollider>().enabled = false;
            Septum.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    
}
