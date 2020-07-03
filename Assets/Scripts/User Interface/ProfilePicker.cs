using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProfilePicker : MonoBehaviour
{
    [SerializeField] private Transform[] _costumes;
    private Transform _currentCostume;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _currentCostume = _costumes[0];
        ToggleLeft();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ToggleLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ToggleRight();
        }
    }

    void ToggleLeft()
    {
        currentIndex = (currentIndex + 1) % _costumes.Length;
        if (currentIndex < 0)
        {
            currentIndex += _costumes.Length;
        }
        DisplayFromLeft(_costumes[currentIndex]);
    }

    void ToggleRight()
    {
        currentIndex = (currentIndex - 1) % _costumes.Length;
        if (currentIndex < 0)
        {
            currentIndex += _costumes.Length;
        }
        DisplayFromRight(_costumes[currentIndex]);
    }

    void DisplayFromLeft(Transform costume)
    {
        costume.gameObject.SetActive(true);
        costume.localPosition = Vector3.right * 10f;

        _currentCostume.DOLocalMoveX(-10f, 0.4f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _currentCostume.gameObject.SetActive(false);
        });
        costume.DOLocalMoveX(0, 0.4f).SetEase(Ease.OutCubic).OnComplete(() => { _currentCostume = costume; });
    }

    void DisplayFromRight(Transform costume)
    {
        costume.gameObject.SetActive(true);
        costume.localPosition = Vector3.left * 10f;

        _currentCostume.DOLocalMoveX(10f, 0.4f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _currentCostume.gameObject.SetActive(false);
        });
        costume.DOLocalMoveX(0, 0.4f).SetEase(Ease.OutCubic).OnComplete(() => { _currentCostume = costume; });
    }
}
