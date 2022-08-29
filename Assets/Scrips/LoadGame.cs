using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGameField()
    {
        SceneManager.LoadScene("Field");
    }
}
