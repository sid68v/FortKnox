using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WayPoint : MonoBehaviour
{
    Vector3 gridScale = new Vector3(10, 0, 10);

    public bool isExplored = false;
    public WayPoint exploredFrom;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public Vector3 GetGridScale()
    {
        return gridScale;
    }

    public Vector2Int GetGridPosition()
    {
       return new Vector2Int(
             (int)(Mathf.RoundToInt(transform.position.x / gridScale.x)),
             (int)(Mathf.RoundToInt(transform.position.z / gridScale.z) ));

    }

    public void SetTopColor(Color appliedColor)
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material.color = appliedColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
