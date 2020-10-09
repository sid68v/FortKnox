﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    [SerializeField] WayPoint startWaypoint, endWayPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>(); // the dictionary that contains the grid.
    Queue<WayPoint> pathQueue = new Queue<WayPoint>();  // the queue that contains all the waypoints used in searching to reach the endpoint.
    public List<WayPoint> path; // the list that contains only the waypoints actually needed traversing to reach the endpoint.

    bool isPathFinding = true;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };

     WayPoint currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // get the path to be traversed.
    public List<WayPoint> GetCalculatedPath()
    {
        LoadBlocks();
        SetStartAndEndWaypoints();
        FindPath();
        return path;
    }


    // Find the path using the breadth-first search algorithm.
    private void FindPath()
    {
        //add start point to queue
        pathQueue.Enqueue(startWaypoint);

        //search untill queue outputs end point as the next startpoint.
        while (pathQueue.Count > 0 && isPathFinding)
        {
            // get last entry from queue. this is our next start point of searching
            currentWaypoint = pathQueue.Dequeue();
            currentWaypoint.isExplored = true;

            // if this is the endpoint, no need to find neighbours. path is complete. check for it and stop the pathfinding if true.
            if (currentWaypoint == endWayPoint)
            {
                isPathFinding = false;
                Debug.Log($"<color=cyan> END NODE FOUND !</color>");
                CreatePath();
                return;
            }
            // if not then find the neighbours of the current start point and add them into the queue.
            else
            {
                ExploreNeighbouringBlocks();
            }

        }
    }

    // create and store the path from the queue generated by the breadth-first search algorithm.
    private void CreatePath()
    {
        // create the path from queue by going in reverse order, then reverse the list to get the forward path.
        path.Add(endWayPoint);
        WayPoint previousWaypoint = endWayPoint.exploredFrom;
        while (previousWaypoint != startWaypoint)
        {
            previousWaypoint.SetTopColor(Color.magenta);
            path.Add(previousWaypoint);
            previousWaypoint = previousWaypoint.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();

    }

    // find the neighbouring blocks of the current waypoint and add them to the queue if they are not already present.
    private void ExploreNeighbouringBlocks()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourGridPosition = currentWaypoint.GetGridPosition() + direction;

            //add the unexplored neighbours to the queue.
            try
            {
                WayPoint neighbouringWaypoint = grid[neighbourGridPosition];

                //does the neighbour already explored or currently present in the queue ? if not, add to the queue.
                if (!neighbouringWaypoint.isExplored || pathQueue.Contains(neighbouringWaypoint))
                {
                    pathQueue.Enqueue(neighbouringWaypoint);
                    // set the breadcrumb trail into the neighbour for retrieving.
                    neighbouringWaypoint.exploredFrom = currentWaypoint;
                }
            }
            catch (Exception)
            {
                Debug.Log($"no path towards {direction}");
            }

        }
    }

    //set the start and end point colors.
    private void SetStartAndEndWaypoints()
    {
        startWaypoint.SetTopColor(Color.green);
        endWayPoint.SetTopColor(Color.red);
    }


    // store all the waypoints into a dictionary : grid <Vector2Int,WayPoint>
    private void LoadBlocks()
    {
        WayPoint[] wayPoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint wayPoint in wayPoints)
        {
            if (grid.ContainsKey(wayPoint.GetGridPosition()))
            {
                Debug.LogWarning($"Skipping overlapping block {wayPoint}");
            }
            else
            {
                grid.Add(wayPoint.GetGridPosition(), wayPoint);
                //wayPoint.SetTopColor(Color.black);
            }
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
