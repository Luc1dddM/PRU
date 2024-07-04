using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    /*public string nextSceneName = "RedMap";*/ // Tên scene bạn muốn chuyển tới sau khi video kết thúc

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd; // Đăng ký sự kiện khi video kết thúc
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();
    }

    public void SkipVideo()
    {
        DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();
        Debug.Log("Skip button clicked");
    }
}