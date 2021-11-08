using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FlashNumber : MonoBehaviour 
{
    public event Action<FlashNumber> Finished;

    [SerializeField] private float _duration = 2f;
    [SerializeField] private Vector2 _offsetPosition = new Vector2(0, 32);
    [SerializeField] private AnimationCurve _scale = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField] private AnimationCurve _alpha = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField] private AnimationCurve _offset = AnimationCurve.Linear(0, 0, 1, 1);
    private RectTransform _transform;
    private Text _text;
    private IEnumerator _flashAction;

    private void OnEnable ()
    {
        if (_flashAction != null)
            StartCoroutine(_flashAction);
    }

    private void Awake ()
    {
        _transform = transform as RectTransform;
        _text = GetComponent<Text>();
    }
    
    public void Play (int number)
    {
        _text.text = number.ToString();
        _flashAction = FlashAction();
        if (gameObject.activeInHierarchy)
            StartCoroutine(_flashAction);
    }

    private IEnumerator FlashAction ()
    {
        Vector2 basePosition = _transform.anchoredPosition;
        float elapsed = 0;
        while (elapsed < _duration)
        {
            float way = elapsed/_duration;

            Color color = _text.color;
            color.a = _alpha.Evaluate(way);
            _text.color = color;

            float scale = _scale.Evaluate(way);
            _transform.localScale = new Vector3(scale, scale, scale);

            Vector2 position = basePosition+_offsetPosition*_offset.Evaluate(way);
            _transform.anchoredPosition = position;

            elapsed += Time.deltaTime;
            yield return null;
        }
        Finished?.Invoke(this);
    }
}
