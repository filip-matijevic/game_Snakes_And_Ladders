using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _selfSprite;
    [SerializeField] private TextMeshPro _selfText;
    // Start is called before the first frame update
    public void SetData(Color tileColor, float tileSize, int tileIndex)
    {
        _selfSprite.color = tileColor;
        _selfSprite.size = Vector2.one * tileSize;
        _selfText.rectTransform.sizeDelta = Vector2.one * tileSize;
        _selfText.text = tileIndex.ToString();
    }
}
