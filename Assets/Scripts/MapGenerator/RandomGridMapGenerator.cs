using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class RandomGridMapGenerator : MonoBehaviour
{
    public Vector2 GridResolution;
    [Range(0, 1)] public float ChanceToSpawn;
    public GameObject Tile; 
    public Collider2D GenerationArea;

    public void Start()
    {
        GenerateMap();
    }

	public void GenerateMap()
	{
	    var min = GenerationArea.bounds.min;
	    var max = GenerationArea.bounds.max;
	    for (var x = (int)min.x; x < max.x; x++)
	    {
            for (var y = (int)min.y; y < max.y; y++)
            {
                var rand = Random.value;
                if (rand < ChanceToSpawn)
                {
                    Instantiate(Tile, new Vector3(x, y, 0), Tile.transform.rotation);
                }
            }
	    }
	}

}
