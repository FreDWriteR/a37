using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_20_9 : MonoBehaviour
{

    [SerializeField] private GameObject hitCount, center;
    // Start is called before the first frame update

    bool allowExplode = true;



    void Start()
    {
        
    }

    IEnumerator septumExplode()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var part = gameObject.transform.GetChild(i);
            var body = part.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = part.transform.position - center.transform.position;
            body.AddForceAtPosition(direction.normalized * 100f, part.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount.GetComponent<hitCount_20_9>().count >= 100f && allowExplode)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var part = gameObject.transform.GetChild(i);
                part.gameObject.AddComponent<Rigidbody>();
                part.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            allowExplode = false;
            StartCoroutine(septumExplode());
        }
    }
}
