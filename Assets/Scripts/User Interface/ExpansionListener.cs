using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionListener : MonoBehaviour
{
    [SerializeField] private float _spacing;
    [SerializeField] private RectTransform[] _observedRectTransforms;
    [SerializeField] private Direction _expansionDirection;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < _observedRectTransforms.Length; i++)
        {
            ExpandableDrawer drawer = _observedRectTransforms[i].GetComponent<ExpandableDrawer>();
            if (drawer != null)
            {
                drawer.OnResizeUpdate += Rearange;
            }
        }
    }


    void Rearange()
    {
        float calculatedSpacing = 0;
        for (int i = 0; i < _observedRectTransforms.Length; i++)
        {
            _observedRectTransforms[i].anchoredPosition = _expansionDirection == Direction.Vertical ? Vector2.down * (calculatedSpacing) : Vector2.right * (calculatedSpacing);
            calculatedSpacing += _spacing + (_expansionDirection == Direction.Vertical ? _observedRectTransforms[i].rect.height : _observedRectTransforms[i].rect.width);
        }
    }
}
