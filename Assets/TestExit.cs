using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestExit : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Move playerController = collision.GetComponent<Move>();

        if (playerController != null)
        {
            float height = 2f * Camera.main.orthographicSize;
            Vector3 currentPosition = collision.transform.position; // Get current player position
            Vector3 sceneSize = new Vector3(0, height - 20, 0);
            SceneController.Instance.LoadBackScene(currentPosition, sceneSize, playerController.jumpSpeed);
        }
    }
}
