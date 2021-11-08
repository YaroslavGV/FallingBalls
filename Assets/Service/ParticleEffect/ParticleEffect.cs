using System;
using System.Collections;
using UnityEngine;

public class ParticleEffect : MonoBehaviour 
{
    public event Action<ParticleEffect> Finished;

    [SerializeField] private ParticleSystem[] _playParticles;
    [SerializeField] private ParticleSystem[] _colorParticles;

    public void Play (Color color)
    {
        SetColor(color);
        Play();
    }

    public void Play ()
    {
        StopAllCoroutines();
        foreach (var particle in _colorParticles)
            particle.Play();
        StartCoroutine(Finish());
    }

    private IEnumerator Finish ()
    {
        float delay = GetMaxLifeTime();
        yield return new WaitForSeconds(delay);
        Finished?.Invoke(this);
    }

    private void SetColor (Color color)
    {
        foreach (var particle in _colorParticles)
        {
            var main = particle.main;
            main.startColor = color;
        }
    }

    private float GetMaxLifeTime ()
    {
        float maxLifeTime = 0;
        foreach (var particle in _playParticles)
        {
            float value = GetMaxLifeTime(particle);
            if (maxLifeTime < value)
                maxLifeTime = value;
        }
        foreach (var particle in _colorParticles)
        {
            float value = GetMaxLifeTime(particle);
            if (maxLifeTime < value)
                maxLifeTime = value;
        }
        return maxLifeTime;
    }

    private float GetMaxLifeTime (ParticleSystem particle)
    {
        float maxLifeTime = 0;
        var main = particle.main;
        var mode = main.startLifetime.mode;
        if (mode == ParticleSystemCurveMode.Constant)
            maxLifeTime = main.startLifetime.constant;
        else if (mode == ParticleSystemCurveMode.TwoConstants)
            maxLifeTime = main.startLifetime.constantMax;
        return maxLifeTime;
    }
}
