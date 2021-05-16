using UnityEngine;
using UnityEngine.SceneManagement;

public class LvControl : MonoBehaviour
{
    public void PlayLv(int lv)
    {
        SoundManager.instance.Destroy();
        SceneManager.LoadScene(lv);
    }
}
