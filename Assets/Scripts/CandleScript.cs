
using UnityEngine;


public class CandleScript : MonoBehaviour
{
    public AudioSource revealSoundSource;
    public bool isLit = false;
    public GameObject AssociatedObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Harm") 
        && collision.gameObject.GetComponent<HarmfulObjectScript>().lightsCandles)
        {
            if (!isLit)
            {
                isLit = true;
                transform.GetChild(0).gameObject.SetActive(true);
                GetComponent<Animator>().Play("Flicker_One");
                GetComponent<Animator>().Play("Activate");

                if (AssociatedObject)
                {
                    AssociatedObject.GetComponent<AssociatedObjectScript>().CandleLit();
                    revealSoundSource.Play();
                }
            }
        }
    }
}
