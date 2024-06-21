using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class PlayerHealth : MonoBehaviour,TakeDamage
{
    [SerializeField] private int maxHealth;

    public int currentHealth;
    //private bool canTakeDamage = true;
    private Slider healthSlider;
    Animator myAnimator;
    const string HEALTH_SLIDER_TEXT = ("Health Slider");

    public static int score;
    public static int scoreTemp = 0;
    public TextMeshProUGUI scoreText;

    public bool hadKey = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            hadKey = true;
        }
        else hadKey = false;
        
        Time.timeScale = 1f;
        scoreText.text = "SCORE " + score.ToString();
        currentHealth = maxHealth;
        myAnimator=GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHeal(int addheal)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += addheal;
            UpdateSliderHealth();
        }
    }

    public void TakeScore(int addscore)
    {
        score += addscore;
        scoreText.text = "SCORE " + score;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0) 
        {
            currentHealth -= damage;
            UpdateSliderHealth();
        }
        else currentHealth = 0;
         
    }    
    private void UpdateSliderHealth()
    {
        if(healthSlider == null)
        {
            Debug.Log("slider dang null");

            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            
        }
        Debug.Log(currentHealth);
        healthSlider.maxValue = maxHealth;
        healthSlider.value=currentHealth;
        if(currentHealth<=0)
        {
            myAnimator.SetBool(AnimationString.isAlive, false);
        }
    }

    public void ResetStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Key"))
        {
            hadKey = true;
            Destroy(collision.gameObject);
        }
    }
}
