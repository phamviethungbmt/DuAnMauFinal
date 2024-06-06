using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,TakeDamage
{
    [SerializeField] private int maxHealth;

    private int currentHealth;
    private bool canTakeDamage = true;
    private Slider healthSlider;
    Animator myAnimator;
    const string HEALTH_SLIDER_TEXT = ("Health Slider");
    void Start()
    {
        currentHealth = maxHealth;
      myAnimator=GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
         currentHealth-=damage;
         UpdateSliderHealth();
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
}
