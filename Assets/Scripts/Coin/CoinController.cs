using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CoinController : MonoBehaviour
{
    public static CoinController instance { get; private set; }
    public int coinCout;
    public Text coinText;
    public bool coinEnough;
    private CircleCollider2D CircleCollider2D;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D = GetComponent<CircleCollider2D>();
        coinEnough = false;
        CircleCollider2D.enabled = coinEnough;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + coinCout.ToString();

        CircleCollider2D.enabled = coinEnough;
    }



}
