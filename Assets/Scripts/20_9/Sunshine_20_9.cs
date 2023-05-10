using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunshine_20_9 : MonoBehaviour
{
    Color startColor, _color;
    float t = 0f;

    [SerializeField]
    private GameObject hitCount;

    bool allowSunrise = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Sunrise()
    {
        float EndPos;
        Vector3 CurPos;
        Vector3 StartPos;
        float d = 0;
        while (gameObject.transform.childCount > 0)
        {
            EndPos = gameObject.transform.position.y;
            EndPos -= 1.6f;

            CurPos = gameObject.transform.position;
            StartPos = gameObject.transform.position;
            while (Mathf.Abs(gameObject.transform.position.y - EndPos) > 0.00001f)
            {
                t += Time.deltaTime * 4;
                d += Time.deltaTime;
                CurPos.y = Mathf.Lerp(StartPos.y, EndPos, t);
                gameObject.transform.position = CurPos;
                if (d >= 0.01f)
                {
                    yield return new WaitForSeconds(d / 10f);
                    d = 0;
                }
            }
            t = 0;
            gameObject.transform.GetChild(0).gameObject.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount.GetComponent<hitCount_20_9>().count >= 100f && allowSunrise)
        {
            allowSunrise = false;
            StartCoroutine(Sunrise());
        }
    }
}
