using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryNewScene : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(SceneController.Instance);
            float height = 2f * Camera.main.orthographicSize;
            Vector3 currentPosition = collision.transform.position; // Get current player position
            Vector3 sceneSize = new Vector3(0, height - 16, 0);
            SceneController.Instance.LoadScene("SampleScene", currentPosition, sceneSize);
            SceneController.Instance.test = "RuinsMap1";
        }
    }
}
