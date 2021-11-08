using System.Collections.Generic;
using UnityEngine;

public class FlashNumbers : MonoBehaviour
{
    [SerializeField] private FlashNumbersPool _pool;
    private bool _isInit;
    private RectTransform _transform;
    private Camera _camera;
    private List<FlashNumber> _flashes;

    [ContextMenu("Initialize")]
    public void Initialize ()
    {
        if (_isInit)
            return;
        _pool.Initialize();
        _flashes = new List<FlashNumber>();
        _transform = transform as RectTransform;
        _camera = Camera.main;
        _isInit = true;
    }

    public void RemoveAll ()
    {
        for (int i = _flashes.Count-1; i > -1; i--)
            Remove(_flashes[i]);
    }

    public void PlayLocal (int number, Vector2 localPosition)
    {
        var fNumber = Spawn(localPosition);
        fNumber.Play(number);
    }

    public void PlayWorld (int number, Vector3 worldPosition)
    {
        Vector2 position = WorldToLocal(worldPosition);
        var fNumber = Spawn(position);
        fNumber.Play(number);
    }

    public Vector2 WorldToLocal (Vector3 position)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, screenPoint, null, out localPoint);
        return localPoint;
    }

    private FlashNumber Spawn (Vector3 position)
    {
        var fNumber = _pool.GetElement();
        RectTransform numberRect = fNumber.transform as RectTransform;
        numberRect.anchoredPosition = position;
        fNumber.Finished += OnFinish;
        _flashes.Add(fNumber);
        return fNumber;
    }

    private void OnFinish (FlashNumber fNumber)
    {
        Remove(fNumber);
    }

    private void Remove (FlashNumber fNumber)
    {
        fNumber.Finished -= OnFinish;
        _flashes.Remove(fNumber);
        _pool.ReturnToPull(fNumber);
    }

    [ContextMenu("TestPlay")]
    private void TestPlay ()
    {
        float range = 3;
        Vector3 position;
        position.x = Random.Range(-range, range);
        position.y = Random.Range(-range, range);
        position.z = Random.Range(-range, range);
        int number = Random.Range(0, 20)*5;
        PlayWorld(number, position);
    }
}
