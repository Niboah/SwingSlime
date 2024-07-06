using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int NumGoodRocks;
    public int NumBadRocks;
    public int NumWater;

    public GameObject GoodRock;
    public GameObject BadRock;
    public GameObject Water;

    public float maxX;
    public float maxY;
    public float maxZ;

    public float minX;
    public float minY;
    public float minZ;

    void Start()
    {
        for (int i = 0; i < NumGoodRocks; i++)
            Instantiate(GoodRock, GetRandomPosition(), Quaternion.identity);
        for (int i = 0; i < NumBadRocks; i++)
            Instantiate(BadRock, GetRandomPosition(), Quaternion.identity);
        for(int i = 0; i < NumWater; i++)
            Instantiate(Water, GetRandomPosition(), Quaternion.identity);
    }

    public Vector3 GetRandomPosition() 
    {
        Vector3 point = Vector3.zero;
        point.x = Random.Range(minX, maxX);
        point.y = Random.Range(minY, maxY);
        point.z = Random.Range(minZ,maxZ);
        return point;
    }
}
