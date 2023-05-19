using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public bool ballIsOnTheGround = true;
    
    public Text winText;
    public Text winText2;
    public Text countText;
    private Rigidbody rb;
    private int count;

    public GameObject player;
    public Transform spawnPoint;
    public GameObject sphere;

    public GameObject pikap2;

    public bool helperforjump = false;

    

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        sphere = GameObject.FindGameObjectWithTag("sphere");
        sphere.SetActive(false);

        pikap2 = GameObject.FindGameObjectWithTag("pickup2");
        pikap2.SetActive(false);
    }
    
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);
        rb.AddForce (movement * speed); 
    }

    void Update(){
        if(Input.GetButtonDown("Jump") && ballIsOnTheGround && helperforjump){
            rb.AddForce(new Vector3(0,7,0), ForceMode.Impulse);
            ballIsOnTheGround = false;
        }
        ballIsOnTheGround = true;
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("enlight"))
        {
            other.gameObject.SetActive(false);
            PositionPlayer();
            sphere.SetActive(false);
            pikap2.SetActive(true);
            helperforjump = true;
        }

        if (other.gameObject.CompareTag("pickup2"))
        {
            other.gameObject.SetActive(false);
            winText.text = "Congratz!";
            winText2.text = "You win!";
        }

    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 6)
        {
            winText.text = "Passed the first stage!";
            sphere.SetActive(true);
        }
    }


    public void PositionPlayer()
	{
		player.transform.position = spawnPoint.position;
		player.transform.rotation = spawnPoint.rotation;
	}
}
