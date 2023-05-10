using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCracksinPoint_20_9 : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<GameObject> Cracks;

    [SerializeField] private GameObject hitCount;



    private List<GameObject> CracksTemp = new List<GameObject>();

    bool allowdDestroyCracks = true;

    private int indCrack = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount.GetComponent<hitCount_20_9>().count >= 100f && allowdDestroyCracks)
        {
            allowdDestroyCracks = false;
            foreach(GameObject Crack in CracksTemp)
            {
                Destroy(Crack);
            }
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitCount.GetComponent<hitCount_20_9>().closeZones)
        {
            var crackPos = collision.contacts[0].point;
            crackPos.y += Cracks[indCrack].GetComponent<MeshRenderer>().bounds.extents.y;
            crackPos.x += 0.5f;
            CracksTemp.Add(Instantiate(Cracks[indCrack], crackPos, Quaternion.Euler(90, 0, -90)));
            if (indCrack < 6)
                indCrack++;
            else
            {
                indCrack = 0;
            }
        }
    }
}
