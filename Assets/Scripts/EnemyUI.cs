using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject prompt;
    [SerializeField] private Text promptText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Camera uICamera;
    [SerializeField] private GameObject uICameraSpawnPoint;
    [SerializeField] private float timePerCharacter = 0.1f;
    [SerializeField] NPCDialogueScriptableObject npcDialogue;
    [SerializeField] Enemy enemy;
    private Camera uiCameraPrefab;
    private int currentText = 0;
    private int characterIndex = 0;
    private float timer = 1;

    public void ShowDialogue()
    {
        if (currentText < npcDialogue.dialogue.Length)
        {
            if (npcDialogue.dialogue[currentText].Contains("Kill"))
            {
                StartCoroutine(enemy.Attack());
            }
            dialogueText.text = npcDialogue.dialogue[currentText];
            promptText.text = "Press Spacebar to Continue the Conversation";
            currentText++;
        }
        else
        {
            StartCoroutine(enemy.EndConversation());
            currentText = 0;
            promptText.text = "Press Spacebar to Start the Conversation";
            HideUI();
            DetachCamera();
        }
        
    }

    private void Update()
    {
        //WritingEffect(npcDialogue.dialogue[currentText]);
    }

    private void WritingEffect(string textTobeWritten)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timePerCharacter;
            characterIndex++;
            dialogueText.text = textTobeWritten.Substring(0, characterIndex);
            if (characterIndex > textTobeWritten.Length)
            {
                characterIndex = 0;
                return;
            }
        }
    }

    #region Show UI 
    public void ShowUI() { textBox.gameObject.SetActive(true); }

    public void HideUI() { textBox.gameObject.SetActive(false); }
    #endregion

    #region Show Prompt
    public void ShowPrompt() { prompt.SetActive(true); }

    public void HidePrompt() { prompt.SetActive(false); }
    #endregion

    #region Attach Camera
    public void AttachCamera()
    {
        if (uICameraSpawnPoint.GetComponentInChildren<Camera>() == null)
        {
            uiCameraPrefab = Instantiate(uICamera, uICameraSpawnPoint.transform.position, Quaternion.identity, uICameraSpawnPoint.transform);
        }
    }

    public void DetachCamera() { Destroy(uiCameraPrefab.gameObject); }
    #endregion
}
