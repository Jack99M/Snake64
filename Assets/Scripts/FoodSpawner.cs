using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int gridWidth = 20;
    public int gridHeight = 20;

    private GameObject currentFood;

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        int x = Random.Range(-gridWidth / 2, gridWidth / 2);
        int y = Random.Range(-gridHeight / 2, gridHeight / 2);
        currentFood = Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
