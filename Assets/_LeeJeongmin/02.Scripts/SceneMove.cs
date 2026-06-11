using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    // 버튼에 연결할 함수 (public이 있어야 유니티 화면에서 보입니다)
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
