using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleExplode : MonoBehaviour
{
    public static ParticleExplode Singletone;
    private ParticleSystem particle;

    public void Init()
    {
        Singletone = this;
        particle = GetComponent<ParticleSystem>();
    }

    public void EmitParcticle(Vector3 pos, Color color)
    {
        transform.position = pos;
        var mainModule = particle.main;
        mainModule.startColor = color;
        particle.Emit(10);
    }
}
