using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private float _diceLerpTime;
    [SerializeField] private float _diceHoldTime;
    [SerializeField] private int _numberOfShuffles;

    private Animator _selfAnimator;

    public static int Value = 1;
    // Start is called before the first frame update
    void Awake()
    {
        _selfAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DiceThrowCoroutine());
        }
    }

    IEnumerator DiceThrowCoroutine()
    {
        float currentTime = 0f;
        Debug.Log("Dice :");
        for (int i = 0; i < _numberOfShuffles; i++)
        {
            //Never randomly pick the same
            Value = (Value + Random.Range(1, 5)) % 6 + 1;
            Debug.Log(Value);
            _selfAnimator.SetTrigger(Value.ToString());      
            yield return new WaitForSeconds(_diceHoldTime);

        }
    }
}

