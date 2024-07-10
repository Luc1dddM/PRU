using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // khi video kết thúc
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu"); // Chuyển đến scene "MainMenu"
      /*  DataActionManager.instance.NewGame();
        SceneController.instance.LoadFirstScene();*/

        // Xóa file lưu trữ khi kết thúc trò chơi
        DataActionManager.instance.DeleteSaveFile();
    }
}
