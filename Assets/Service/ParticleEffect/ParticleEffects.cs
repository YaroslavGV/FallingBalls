using UnityEngine;

public class ParticleEffects : MonoBehaviour 
{
    [SerializeField] private bool _modifyValue;
    [Range(0, 1)]
    [SerializeField] private float _value = 1;
    [Space]
    [SerializeField] private bool _modifySaturation;
    [Range(0, 1)]
    [SerializeField] private float _saturation = 1;
    [Space]
    [SerializeField] private ParticleEffectsPool _pool;
    private bool _isInit;

    [ContextMenu("Initialize")]
	public void Initialize ()
    {
        if (_isInit)
            return;
        _pool.Initialize();
        _isInit = true;
    }

    public void Play (Vector3 position)
    {
        var effect = Spawn(position);
        effect.Play();
    }

    public void Play (Vector3 position, Color color)
    {
        var effect = Spawn(position);
        effect.Play(GetModifyColor(color));
    }

    private ParticleEffect Spawn (Vector3 position)
    {
        var effect = _pool.GetElement();
        effect.transform.position = position;
        effect.Finished += OnFinish;
        return effect;
    }

    private void OnFinish (ParticleEffect effect)
    {
        effect.Finished -= OnFinish;
        _pool.ReturnToPull(effect);
    }

    private Color GetModifyColor (Color color)
    {
        if (_modifyValue == false && _modifySaturation == false)
            return color;
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        if (_modifySaturation)
            v = _saturation;
        if (_modifyValue)
            v = _value;
        return Color.HSVToRGB(h, s, v);
    }

    [ContextMenu("TestPlay")]
    private void TestPlay ()
    {
        float range = 3;
        Vector3 position;
        position.x = Random.Range(-range, range);
        position.y = Random.Range(-range, range);
        position.z = Random.Range(-range, range);
        float hue = Random.Range(0f, 1f);
        Color color = Color.HSVToRGB(hue, 1, 1);
        Play(position, color);
    }
}
