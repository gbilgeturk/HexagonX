using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float moveSpeed = 600f;
    float movement = 0f;
    bool flagRight;
    bool flagLeft;
    bool vib = true;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGO;
    public GameObject restartPanel;
    private bool asLost;
    public static int highScore ;
    public Camera cam;


    void Start()
    {
        scoreText.text = null;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }
    
    void Update()
    {
        if (Hezagon.score > highScore)
        {
            highScore = Hezagon.score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        
        if (asLost == false)
        {
            movement = Input.GetAxisRaw("Horizontal");
            scoreText.text = Hezagon.score.ToString();
            scoreTextGO.text = scoreText.text;
            if (flagRight)
            {
                movement = movement + 1;
            }
            if (flagLeft)
            {
                movement = movement - 1;
            }
            
        }
        else
        {
            Hezagon.score = 0;
        }
        
    }

    private void FixedUpdate()
    {
            transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);   
    }
    public void rightButtonBas()
    {
        flagRight = true;
    }
    public void rightButtonCek()
    {
        flagRight = false;
    }
    public void leftButtonBas()
    {
        flagLeft = true;
    }
    public void leftButtonCek()
    {
        flagLeft = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vib)
        {
            cam.backgroundColor = new Color(255, 0, 0, 0f);
            Handheld.Vibrate();
            vib = false;
        }
        
        asLost = true;
        //Hezagon.score = 0;
        scoreText.text = Hezagon.score.ToString();
        restartPanel.SetActive(true);
    }
    
}
