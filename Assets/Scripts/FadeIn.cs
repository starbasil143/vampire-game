
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public bool toFadeIn;
    public bool fadeImmediately;
    public bool fadingNow;
    public float timeBeforeFade;
    public float fadeLength;
    private float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        if (timer >= timeBeforeFade)
        {
            fadingNow = true;
            Fade();
        }
        else
        {   
            timer += Time.deltaTime;    
        }
    }

    public void Fade()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {

        }
    }
}
