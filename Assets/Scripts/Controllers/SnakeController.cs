using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SnakeController : MonoBehaviour
{
    private SpriteShapeController _selfShape;

    [SerializeField] private Vector3 _head;
    [SerializeField] private Vector3 _tail;

    private List<Transform> _bodyTransforms = new List<Transform>();
    private List<Vector3> _idlePositions = new List<Vector3>();

    private float currentTime = 0;

    [SerializeField] private AnimationCurve _animation;
    [SerializeField] private float _animationBounce = 2f;

    [SerializeField] private float _animationAmplitude = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _selfShape = GetComponent<SpriteShapeController>();
        GenerateSnake(_head, _tail);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _bodyTransforms.Count; i++)
        {
            if (i % 2 == 0)
            {
                _selfShape.spline.SetPosition(i + 1, _idlePositions[i] + Vector3.right * _animation.Evaluate(currentTime / _animationBounce) * _animationAmplitude);
            }
            else
            {
                _selfShape.spline.SetPosition(i + 1, _idlePositions[i] + Vector3.right * _animation.Evaluate(1f - currentTime / _animationBounce) * _animationAmplitude);
            }
        }

        currentTime += Time.deltaTime;

        if (currentTime > _animationBounce)
        {
            currentTime = 0f;
        }
    }

    void GenerateSnake(Vector3 head, Vector3 tail)
    {
        ClearBones();
        int numberOfJoints = (int) Vector3.Distance(head, tail);
        Debug.Log(numberOfJoints);
        GameObject headGO = new GameObject("head");
        headGO.transform.SetParent(transform);
        headGO.transform.localPosition = head;


        GameObject tailGO = new GameObject("tail");
        tailGO.transform.SetParent(transform);
        tailGO.transform.localPosition = tail;

        int safe = 20;


        _selfShape.spline.SetPosition(0, head);
        _selfShape.spline.SetPosition(1, tail);

        for (int i = 1; i < numberOfJoints - 2; i++)
        {
            GameObject bodyGO = new GameObject("bone " + i);
            bodyGO.transform.SetParent(transform);
            bodyGO.transform.position = Vector3.Lerp(headGO.transform.position, tailGO.transform.position, ((float) i / (numberOfJoints - 2)));
            _bodyTransforms.Add(bodyGO.transform);
            _idlePositions.Add(bodyGO.transform.localPosition);
            _selfShape.spline.InsertPointAt(i, bodyGO.transform.localPosition);
            _selfShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            _selfShape.spline.SetHeight(i, 0.7f);
        }
    }
    void ClearBones()
    {
        foreach (Transform bone in transform)
        {
            Destroy(bone.gameObject);
        }
    }
}
