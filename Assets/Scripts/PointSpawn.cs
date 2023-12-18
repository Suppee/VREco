using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PointSpawn : MonoBehaviour
{
    public GameObject riverPoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject curPoint;
        curPoint = Instantiate(riverPoint);
        curPoint.GetComponent<SplineAnimate>().Container = this.gameObject.GetComponent<SplineContainer>();
    }
}
