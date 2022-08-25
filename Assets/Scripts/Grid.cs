using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    
    [SerializeField] private Tile2 _tilePrefab;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile2> _tiles;

    void Start() {
        GenerateGrid();
    }

    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile2>();

        for(int x = 0 ; x < _width ; x++) {
            for (int y = 0 ; y < _height ; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3((float) (0.5 + 0.75 * x), y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f, -10);
    }

    public Tile2 GetTileAtPosition(Vector2 position) {
        if (_tiles.TryGetValue(position, out var tile)) {
            return tile;
        }
        return null;
    }
}
