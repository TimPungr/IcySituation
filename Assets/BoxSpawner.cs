using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    const float SPAWN_Y = 12f;
    const float SPAWN_MIN_X = -4.5f;
    const float SPAWN_MAX_X = 4.5f;

    [SerializeField] private float spawnTimerInit;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boxParent;
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnBox();
            spawnTimer = spawnTimerInit;
        }
    }

    private void SpawnBox()
    {
        float xPos = Random.Range(SPAWN_MIN_X, SPAWN_MAX_X);
        GameObject newBox;
        newBox = Instantiate(box, new Vector3(xPos, transform.position.y, 0), Quaternion.identity);
        newBox.transform.parent = boxParent.transform;
    }
}
