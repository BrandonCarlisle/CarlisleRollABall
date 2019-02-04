using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text timerText;
    public Button restartGame;

    private float timer;
    private bool timerActive;
    private int count;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        timerActive = true;
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        timerText.text = "";
        
        restartGame.gameObject.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
            timer += Time.deltaTime;

        int seconds = Mathf.RoundToInt(timer % 60f);
        timerText.text = seconds.ToString();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {           
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        if (count > 12)
        {
            winText.text = "You Win Score: " + timerText.text + " seconds";
            restartGame.gameObject.SetActive(true);
            timerActive = false;
        }        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

}
