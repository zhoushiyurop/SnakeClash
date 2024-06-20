using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeadController : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefabs;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject fullBody;
    public static HeadController instance;

    private List<GameObject> bodyParts;
    private List<Vector3> positionHistory;
    private int Gap = 5;
    public int level;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        Application.targetFrameRate = 60;
        
        bodyParts = new List<GameObject>();
        positionHistory = new List<Vector3>();

        level = 5;
        levelText.text = "Level " + level.ToString();
        for (int i = 0; i < 5; i++)
        {
            GrowSnake();
        }
    }
    private void Update()
    {
        MoveBody();
        levelText.text = "Level " + level.ToString();
    }

    private void MoveBody()
    {
        positionHistory.Insert(0, transform.position);
        int i = 0;
        foreach (var body in bodyParts)
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
            body.transform.localScale = bodyParts[0].transform.localScale;
            body.transform.position = bodyParts[0].transform.position;
        }
        else
        {
            body.transform.position = this.transform.position;
        }
        bodyParts.Insert(0,body);
        body.transform.SetParent(fullBody.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            level++;
            GrowSnake();
            SizeGrow();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            MovementController.instance.Collide();
        }
    }
    private void SizeGrow()
    {
        transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        for (int i = 0; i < bodyParts.Count(); i++)
        {
            bodyParts[i].transform.localScale = transform.localScale / 2;
        }
    }

}
