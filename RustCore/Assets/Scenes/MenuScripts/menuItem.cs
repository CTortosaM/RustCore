using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuItem : MonoBehaviour
{
    private Text text;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color highlightColor;
    // Start is called before the first frame update
    void Start()
    {
       text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void highlight(bool highlight)
    {
        
        if (highlight)
        {
            text.color = highlightColor;
            return;
        }

        text.color = originalColor;
    }

    
}
