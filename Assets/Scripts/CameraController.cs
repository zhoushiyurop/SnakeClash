using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 currentPos = Vector3.zero;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref currentPos, smoothTime);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position 
            + new Vector3(0, HeadController.instance.level/5, 0), ref currentPos, smoothTime);
    }
}
