using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitCount : MonoBehaviour
{
    public float count = 0;
    public string BallName = "";

    public bool closeZones = false;

    int countBalls = 0;

    [SerializeField] private GameObject spawnZoneLeft, spawnZoneRight; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Substring(0, 4) == "Ball" && closeZones)
        {
            if (other.gameObject.transform.parent.gameObject.GetComponent<ballController>().speed > 0f)
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<ballController>().speed >= 14.29f)
                {
                    count += 34f;
                }
                else 
                    count += other.gameObject.transform.parent.gameObject.GetComponent<ballController>().speed;
                Debug.Log(count);
                BallName = other.name;
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!closeZones)
            countBalls = spawnZoneLeft.GetComponent<SpawnZone>().countActiveBalls + spawnZoneRight.GetComponent<SpawnZone>().countActiveBalls;
        if (!closeZones && countBalls == 3)
        {
            Vector3 SpawnZonePos = spawnZoneLeft.transform.position;
            SpawnZonePos.x = -4f;
            spawnZoneLeft.transform.position = SpawnZonePos;
            SpawnZonePos = spawnZoneRight.transform.position;
            SpawnZonePos.x = -4f;
            spawnZoneRight.transform.position = SpawnZonePos;
            closeZones = true;
        }
    }
}
