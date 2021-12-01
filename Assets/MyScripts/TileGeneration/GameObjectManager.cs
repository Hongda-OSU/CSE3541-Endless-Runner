using System.Collections.Generic;
using UnityEngine;

namespace EndLessRunner
{
    public class GameObjectManager : MonoBehaviour
    {
        [SerializeField] private GameObject tileGenerator;
        [SerializeField] private List<GameObject> tiles;
        [SerializeField] private List<GameObject> obstacles;

        void Start()
        {
            tileGenerator.GetComponent<TileGenerator>().tiles = tiles;
            tileGenerator.GetComponent<TileGenerator>().obstacles = obstacles;
        }

    }
}

