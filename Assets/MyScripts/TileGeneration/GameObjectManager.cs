using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject tileGenerator;
    [SerializeField] private List<GameObject> tiles;
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private List<GameObject> collections;

    void Start()
    {
        tileGenerator.GetComponent<TileGenerator>().tiles = tiles;
        tileGenerator.GetComponent<TileGenerator>().obstacles = obstacles;
        tileGenerator.GetComponent<TileGenerator>().collections = collections;
    }
}
