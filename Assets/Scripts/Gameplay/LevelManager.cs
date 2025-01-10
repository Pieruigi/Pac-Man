using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PM
{
    public class LevelManager : MonoBehaviour
    {
        [System.Serializable]
        class Tile
        {
            [SerializeField]
            public bool isWalkable;
            [SerializeField]
            public GameObject tileObject;
        }

        [SerializeField]
        int width = 29;

        [SerializeField]
        int height = 32;

        [SerializeField]
        float tileSize = 1;

        [SerializeField]
        GameObject diamondPrefab;

        [SerializeField]
        GameObject playerSpawnTileObject;

        [Header("Debug")]
        [SerializeField]
        Tile[] tiles;

        [SerializeField]
        List<GameObject> diamonds = new List<GameObject>();

        private void Start()
        {
            // Fill tiles
            FillTileArray();

            // Reset player
            ResetPlayer();

            // Add diamonds
            AddDiamonds();
        }

        void ResetPlayer()
        {
            // Get spawn coordinates
            var coords = GetTileCoordinates(tiles.ToList().Find(t => t.tileObject == playerSpawnTileObject));

            float x = coords.Item1 * tileSize;  // It's the tile in the middle
            float y = coords.Item2 * tileSize - tileSize * .5f;

            FindObjectOfType<PlayerController>().Teleport(new Vector3(x, 0, y));
        }

        void FillTileArray()
        {
            // Init
            tiles = new Tile[width * height];

            float h = 5;

            // Loop
            for (int i = 0; i < height; i++) // Column
            {
                float y = i * tileSize;
                for (int j = 0; j < width; j++) // Row
                {
                    float x = j * tileSize;

                    Vector3 origin = new Vector3(x, h, y);

                    RaycastHit hit;
                    if(Physics.Raycast(origin, Vector3.down, out hit, h))
                    {
                        tiles[i*width + j] = new Tile() { isWalkable = hit.collider.CompareTag("WalkableTile"), tileObject = hit.collider.gameObject };
                    }
                    else
                    {
                        tiles[i*width + j] = new Tile() { isWalkable = false };
                    }
                }
            }
        }

        void AddDiamonds()
        {
            for (int i = 0; i < height; i++) // Column
            {
                
                for (int j = 0; j < width; j++) // Row
                {
                    var tile = tiles[i*width+j];
                    if (!tile.isWalkable || !tiles[(i-1) * width + j].isWalkable || !tiles[i * width + j + 1].isWalkable || 
                        !tiles[(i - 1) * width + j + 1].isWalkable || tile.tileObject == playerSpawnTileObject || 
                        tiles.ToList().IndexOf(tile) == tiles.ToList().FindIndex(t=>t.tileObject == playerSpawnTileObject)-1)
                        continue;

                    // Get world coordinates
                    float x = j * tileSize + tileSize * 0.5f;
                    float y = i * tileSize - tileSize * .5f;
                                       

                    // Spawn diamond
                    var diamond = Instantiate(diamondPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    diamonds.Add(diamond);
                }
            }

            
        }

        (int, int) GetTileCoordinates(Tile tile)
        {
            var index = tiles.ToList().IndexOf(tile);
            int j = index % width;
            int i = index / width;
            return (j, i);
        }

    }

}
