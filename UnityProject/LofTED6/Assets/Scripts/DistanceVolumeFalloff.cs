using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVolumeFalloff : MonoBehaviour
{

    public float MaxAudibleDistance = 8.0f;

    AudioSource aSource = null;
    GameObject player = null;

    // Start is called before the first frame update
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        aSource.volume = Mathf.Max(MaxAudibleDistance - Vector2.Distance(player.transform.position, transform.position), 0) / MaxAudibleDistance;
    }
}
