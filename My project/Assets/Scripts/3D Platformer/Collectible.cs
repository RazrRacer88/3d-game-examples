using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
     public int score = 0;
     public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.CompareTag("Collectible"))
    {
        score++;
        scoreText.text = "Score" + score.ToString();
        Destroy(other.gameObject);
    }
    }
}
