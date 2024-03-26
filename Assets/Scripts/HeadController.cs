using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefabs;

    private List<GameObject> bodyParts;
    private List<Vector3> positionHistory;
    private int Gap = 10;
    private float moveSpeed = 10f;
    private void Start()
    {
        bodyParts = new List<GameObject>();
        positionHistory = new List<Vector3>();
        for (int i = 0; i < 7; i++)
        {
            GrowSnake();
        }
    }
    private void Update()
    {
        positionHistory.Insert(0, transform.position);
        int i = 0;
        foreach(var body in bodyParts)
        {
            Vector3 point = positionHistory[Mathf.Clamp(i * Gap, 0, positionHistory.Count() - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.LookAt(point);
            body.transform.Translate(moveDirection.normalized * Time.deltaTime * moveSpeed, Space.World);
            i++;
        }
    }
    private void GrowSnake()
    {
        GameObject body = Instantiate(bodyPrefabs);
        if(bodyParts.Count() != 0) 
        {
            body.transform.position = bodyParts[0].transform.position;
        }
        else
        {
            body.transform.position = this.transform.position;
        }
        bodyParts.Add(body);
    }
}
