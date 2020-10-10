using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] Transform turret;
    [HideInInspector]
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(TAGS.Enemy).transform;
    }

    // Update is called once per frame
    void Update()
    {
        turret.LookAt(target);
    }
}
