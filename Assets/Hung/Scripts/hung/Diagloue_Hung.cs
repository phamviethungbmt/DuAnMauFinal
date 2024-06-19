using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Diaglogue_Hung : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public GameObject panelDiagloue;

    // Start is called before the first frame update
    private void Start()
    {
        panelDiagloue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        ClickNext();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
       {
            Debug.Log("va cham voi player");
            DiaglogueCollider();

       }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (panelDiagloue != null)
            {
                panelDiagloue.SetActive(false);
            }

        }
    }
    void DiaglogueCollider ()
    {
        panelDiagloue.gameObject.SetActive(true);
        textcomponent.text = string.Empty;
        starDiaglogue();
    }

    void ClickNext()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textcomponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                
                StopAllCoroutines();
                textcomponent.text = lines[index];
                
            }
        }
    }

    void starDiaglogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine ()
    {
        foreach (var a in lines[index].ToCharArray())
        {
            textcomponent.text += a;
            yield return new WaitForSeconds(textSpeed);

        }

    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        { 
            if (panelDiagloue != null)
            {
                panelDiagloue.SetActive(false);
            }    
            
        }
    }
}
