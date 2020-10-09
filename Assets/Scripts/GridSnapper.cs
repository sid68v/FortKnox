using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

[DisallowMultipleComponent]
[ExecuteInEditMode]
[SelectionBase]
public class GridSnapper : MonoBehaviour
{

    WayPoint waypoint;

    // Start is called before the first frame update
    void Start()
    {
        waypoint = GetComponent<WayPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gridScale = waypoint.GetGridScale();
        Vector2 gridPosition = waypoint.GetGridPosition();

        SnapToGrid(gridScale, gridPosition);
        AssignLabel(gridScale, gridPosition);
    }

    private void AssignLabel(Vector3 gridScale,Vector2 gridPosition)
    {
        string coordinateText = gridPosition.x  + "," + gridPosition.y ;
        GetComponentInChildren<TextMesh>().text = coordinateText;

        gameObject.name = coordinateText;
    }

    private void SnapToGrid(Vector3 gridScale, Vector2 gridPosition)
    {
        Vector3 positionInNearestGrid = new Vector3(
           Mathf.RoundToInt(gridPosition.x  * gridScale.x),
           0f,
           Mathf.RoundToInt(gridPosition.y * gridScale.z)
           );

        transform.position = positionInNearestGrid;
    }
}
