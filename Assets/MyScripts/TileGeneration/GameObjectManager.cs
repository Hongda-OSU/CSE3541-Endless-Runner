using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject tileGenerator;
    [SerializeField] private List<GameObject> tiles;

    void Start()
    {
        tileGenerator.GetComponent<TileGenerator>().tiles = tiles;
    }

}
