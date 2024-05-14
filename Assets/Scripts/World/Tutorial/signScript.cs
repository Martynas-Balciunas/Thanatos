using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class signScript : MonoBehaviour
{
    [SerializeField] string textToDisplay;
    [SerializeField] TMP_Text textTarget;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textTarget.transform.parent.gameObject.SetActive(true);
            textTarget.text = textToDisplay;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textTarget.transform.parent.gameObject.SetActive(false);
        }
    }

}
