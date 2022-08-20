using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeniraWalkBehavior : MonoBehaviour
{
    public bool WalkOnAwake = false;
    public List<Vector3> Points = new List<Vector3>();
    public float WalkSpeed = 5.0f;

    bool walking = false;
    int index = 1;


    // Start is called before the first frame update
    void Awake()
    {
        if(WalkOnAwake)
        {
            walking = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(walking)
        {
            if(Vector3.Distance(transform.position, Points[index - 1]) >= Vector3.Distance(Points[index - 1], Points[index]))
            {
                ++index;
                if(index >= Points.Count)
                {
                    Destroy(gameObject);
                    walking = false;
                    return;
                }
            }
            transform.position += (Points[index] - transform.position).normalized * WalkSpeed * Time.deltaTime;
        }
    }

    public void StartWalking()
    {
        walking = true;
    }
}
