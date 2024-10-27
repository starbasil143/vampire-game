
using UnityEngine;

public class AssociatedObjectScript : MonoBehaviour
{
    public AudioSource activationSoundSource;
    public void CandleLit()
    {
        if (activationSoundSource)
        {
            activationSoundSource.Play();
        }
        gameObject.SetActive(false);
    }
}
