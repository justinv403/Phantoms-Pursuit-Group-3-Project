using UnityEngine;

public class Effect : MonoBehaviour
{
    AudioSource effectAudio;
    
    GameObject particleEffect;
    float volume;
    
    // Start is called before the first frame update
    private void Start()
    {
        effectAudio = gameObject.GetComponent<AudioSource>();
        volume = 0.7f;
    }

    // Update is called once per frame
    public void Play(RaycastHit hit, AudioClip hitSound, GameObject hitEffect, float effectDuration)
    {
        if (hitEffect != null)
        {
            particleEffect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particleEffect, effectDuration);
        }

        if (hitSound != null)
        {
            effectAudio.PlayOneShot(hitSound, volume);
        }
    }
}
