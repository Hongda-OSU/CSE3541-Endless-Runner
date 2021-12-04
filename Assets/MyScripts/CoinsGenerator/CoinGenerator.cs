//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace EndLessRunner
//{
//    public class CoinGenerator : MonoBehaviour
//    {
//        [SerializeField]
//        public int minNum = 5;

//        [SerializeField]
//        public int maxNum = 10;

//        [SerializeField]
//        private Transform Parent = null;

//        private float leftPos = -2.52f;
//        private float midPos = 0f;
//        private float rightPos = 2.48f;

//        public GameObject coins;

//        private int[] availablePos = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//        // Start is called before the first frame update
//        void Start()
//        {
//            int numberOfCoins = Random.Range(5, 10);
//            for (int i = 0; i < numberOfCoins; i++)
//            {
//                int x = Random.Range(-1, 2);
//                float xPosition = midPos;
//                int zPosition = Random.Range(1, 10);
//                while (!availablePos.Contains(zPosition))
//                {
//                    zPosition = Random.Range(1, 10);
//                }
//                availablePos = availablePos.Where(val => val != zPosition).ToArray();

//                if (x == -1)
//                {
//                    xPosition = leftPos;
//                }
//                else if (x == 1)
//                {
//                    xPosition = rightPos;
//                }
//                if(zPosition != 2 && zPosition != 5 && zPosition != 7)
//                {
//                    Vector3 coinPosition = new Vector3(xPosition, coins.transform.position.y, zPosition * 10 + TileGenerator.generationCount * 110);
//                    GameObject.Instantiate<GameObject>(coins, coinPosition, coins.transform.rotation, Parent);
//                }
                
//            }
//        }

//        // Update is called once per frame
//        void Update()
//        {

//        }
//    }

//}
