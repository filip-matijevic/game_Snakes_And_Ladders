using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;

    [SerializeField] private float _horizontalPadding = 1f;

    [SerializeField] private Tile _tilePrefab;

    private const int BOARD_SIZE = 10;

    private float _boardGameSize;

    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;
    // Start is called before the first frame update
    void Start()
    {
        InitBoardVisuals();
    }

    // Update is called once per frame
    void InitBoardVisuals()
    {
        float screenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        _boardGameSize = screenWidth - 2f * _horizontalPadding;
        _backgroundSprite.size = Vector2.one * _boardGameSize;

        InitTiles(_boardGameSize / BOARD_SIZE);
    }

    void InitTiles(float tileSize)
    {
        ClearTiles();

        Vector2 firstTilePosition = new Vector2(tileSize / 2f -_boardGameSize / 2f, tileSize / 2f - _boardGameSize / 2f);
        bool pingPong = false;
        for (int y = 0; y < BOARD_SIZE; y++)
        {
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                pingPong = !pingPong;
                Tile instantiatedTile = Instantiate(_tilePrefab, _backgroundSprite.transform);
                instantiatedTile.transform.localPosition = firstTilePosition + new Vector2((y % 2 == 0 ? (x * tileSize) : ((BOARD_SIZE - x - 1) * tileSize)), y * tileSize);
                instantiatedTile.SetData(
                    pingPong ? _firstColor : _secondColor,
                    tileSize,
                    instantiatedTile.transform.GetSiblingIndex() + 1);
            }
        }

    }

    void ClearTiles()
    {
        foreach (Transform tile in _backgroundSprite.transform)
        {
            Destroy(tile.gameObject);
        }
    }
}
