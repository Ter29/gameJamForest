using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Reference to the TextMeshPro component
    public float typingSpeed = 0.05f; // Delay between each character
    public Canvas targetCanvas;
    private string fullText; // Stores the full text to be typed out
    void Start()
    {
        // Set the full text to what the text component initially has
        fullText = textComponent.text;
        
        // Clear the text initially
        textComponent.text = "";
        targetCanvas.enabled = false;
        // Start the typing effect
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            targetCanvas.enabled = !targetCanvas.enabled;
            StartCoroutine(TypeText());
        }
    }
    IEnumerator TypeText()
    {
        // Loop through each character in the full text
        foreach (char c in fullText)
        {
            textComponent.text += c; // Append one character at a time
            yield return new WaitForSeconds(typingSpeed); // Wait for typing speed duration
        }
    }
}
