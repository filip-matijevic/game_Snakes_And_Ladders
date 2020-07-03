using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Horizontal, Vertical
}
public class ExpandableDrawer : MonoBehaviour
{
    [SerializeField] private Direction _drawerDirection;

    [SerializeField] private float _contractedSize;
    [SerializeField] private float _expandedSize;
    private Tween _expansionTweener;
    private bool isExpanded = false;
    private RectTransform _selfRT;

    [SerializeField] private UnityEvent OnExpand;
    [SerializeField] private UnityEvent OnContract;
    [SerializeField] private bool _contractOnAwake;

    public Action OnResizeUpdate;
    // Start is called before the first frame update
    void Awake()
    {
        _selfRT = GetComponent<RectTransform>();
    }

    void Start()
    {
        if (_contractOnAwake)
        {
            _selfRT.sizeDelta = _drawerDirection == Direction.Vertical
                ? new Vector2(_selfRT.sizeDelta.x, _contractedSize)
                : new Vector2(_contractedSize, _selfRT.sizeDelta.y);
            OnResizeUpdate?.Invoke();
        }

    }

    public void Toggle()
    {
        if (isExpanded)
        {
            Contract();
        }
        else
        {
            Expand();
        }
    }

    public void Expand()
    {
        isExpanded = true;
        if (_expansionTweener != null)
        {
            _expansionTweener.Kill();
        }
        OnExpand.Invoke();

        _expansionTweener = _selfRT.DOSizeDelta(
            _drawerDirection == Direction.Vertical ? new Vector2(_selfRT.sizeDelta.x, _expandedSize) : new Vector2(_expandedSize, _selfRT.sizeDelta.y)
            , 0.5f).SetEase(Ease.InOutCubic).OnUpdate(
            () =>
            {
                OnResizeUpdate?.Invoke();
            });

    }

    public void Contract()
    {
        isExpanded = false;

        if (_expansionTweener != null)
        {
            _expansionTweener.Kill();
        }
        OnContract.Invoke();
        _expansionTweener = _selfRT.DOSizeDelta(
            _drawerDirection == Direction.Vertical ? new Vector2(_selfRT.sizeDelta.x, _contractedSize) : new Vector2(_contractedSize, _selfRT.sizeDelta.y)
            , 0.5f).SetEase(Ease.InOutCubic).OnUpdate(
            () =>
            {
                OnResizeUpdate?.Invoke();
            });

    }
}
