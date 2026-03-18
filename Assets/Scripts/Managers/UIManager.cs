using UnityEngine;
using UserInterface;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
