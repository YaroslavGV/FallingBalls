using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextIntTransition : MonoBehaviour 
{
    [SerializeField] private string _formate;
    [SerializeField] private float _transitionDuration = 0.5f;
    [SerializeField] private AnimationCurve _transitionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private Text _text;
    private int _number;
    private IEnumerator _transitionAction;

    private Text Text
    {
        get 
        {
            if (_text == null)
                _text = GetComponent<Text>();
            return _text; 
        }
    }

    private void OnEnable ()
    {
        if (_transitionAction != null)
            StartCoroutine(_transitionAction);
    }

    private void Awake ()
    {
        _text = GetComponent<Text>();
    }

    public void SetZero ()
    {
        StopAllCoroutines();
        _transitionAction = null;
        SetText(0);
    }

    public void SetNumber (int number)
    {
        StopAllCoroutines();
        _transitionAction = null;
        SetText(number);
    }

    public void TransitionToNumber (int number)
    {
        StopAllCoroutines();
        _transitionAction = Transition(number);
        if (gameObject.activeInHierarchy)
            StartCoroutine(_transitionAction);
    }

    private IEnumerator Transition (int target)
    {
        int from = _number;
        float elapsed = 0;
        while (elapsed < _transitionDuration)
        {
            float way = elapsed/_transitionDuration;
            float wayCurve = _transitionCurve.Evaluate(way);
            int number = (int)Mathf.Lerp(from, target, wayCurve);
            SetText(number);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        SetText(target);
    }

    private void SetText (int number)
    {
        _number = number;
        Text.text = _number.ToString(_formate);
    }
}
