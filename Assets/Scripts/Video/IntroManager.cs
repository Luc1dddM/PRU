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
        LoadNextScene();
    }

    public void SkipVideo()
    {
        LoadNextScene();
        Debug.Log("Skip button clicked");
    }

    private void LoadNextScene()
    {
            SceneController.instance.LoadFirstScene();
            Debug.Log("Loading first scene through SceneController");
    }
}