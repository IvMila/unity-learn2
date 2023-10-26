using UnityEngine;

public class ParticleSystemWin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public static ParticleSystemWin ParticleSystemWinInstance;

    private void Awake()
    {
        ParticleSystemWinInstance = this;
    }

    public void PlayParticle()
    {
        _particleSystem.Play();
    }
}
