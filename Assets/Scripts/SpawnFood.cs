using UnityEngine;
public class SpawnFood : MonoBehaviour
{
    [SerializeField] private int size = 300;
    [SerializeField] private GameObject foodPrefabs;
    [SerializeField] private GameObject fullFood;
    private System.Random rand;
    private float spawnCd = 0.05f;

    private void Start()
    {
        rand = new System.Random();
    }
    private void Spawn()
    {
        if(spawnCd > 0)
        {
            spawnCd -= Time.deltaTime;
        }
        else
        {
            float radius = rand.Next(10, 30) / 10;
            foodPrefabs.transform.localScale = new Vector3(radius, radius, radius);
            Vector3 randPos = new Vector3(rand.Next(-size / 2, size / 2) + (float)rand.NextDouble(), foodPrefabs.transform.localScale.x, rand.Next(-size / 2, size / 2) + (float)rand.NextDouble());
            GameObject food = Instantiate(foodPrefabs, randPos, Quaternion.identity);
            food.transform.SetParent(fullFood.transform);
            spawnCd = 0.05f;
        }
    }
    private void Update()
    {
        Spawn();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Spawn();
            Debug.Log("Spawn");
        }
    }
}
