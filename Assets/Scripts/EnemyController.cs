using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyController : MonoBehaviour
{
    [SerializeField] float dwellTime = 1f;


    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        StartCoroutine(FollowPath(pathFinder.GetCalculatedPath(), dwellTime));
    }

    IEnumerator FollowPath(List<WayPoint> path,float delay)
    {
        foreach (WayPoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(delay);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
