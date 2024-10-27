
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public GameObject FadeObject;
    public GameObject Candle;
    public void StartGameWithFadeIn()
    {
        FadeObject.SetActive(true);
    }
    public void StartGameWithCandleDim()
    {
        Candle.GetComponent<Animator>().Play("DimStart");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
