using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject uICameraSpawnPoint;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Camera uICamera;
    [SerializeField] private NPCDialogueScriptableObject npcDialogue;
    [SerializeField] private Enemy enemy;
    [SerializeField] private int currentText = 0;
    [SerializeField] private float timePerCharacter = 0.1f;
    [SerializeField] private float timer = 1;
    [SerializeField] private int characterIndex = 0;
    private Camera uiCameraPrefab;
    public Text promptText;
    public bool isWriting = false;

    public void ShowDialogue()
    {
        if (currentText < npcDialogue.dialogue.Length)
        {
            if (npcDialogue.dialogue[currentText].Contains("Kill"))
            {
                StartCoroutine(enemy.Attack());
            }
            promptText.text = "Press Spacebar to Continue the Conversation";
            isWriting = true;
        }
        else
        {
            StartCoroutine(enemy.EndConversation());
            currentText = 0;
            promptText.text = "Press Spacebar to Start the Conversation";
            HideUI();
            DetachCamera();
            dialogueText.text = "";
        }
        
    }

    private void Update()
    {
        if (isWriting)
        {
            WritingEffect();
        }
    }

    private void WritingEffect()
    {
        timer -= Time.deltaTime;
        while (timer <= 0f)
        {
            timer += timePerCharacter;
            dialogueText.text = npcDialogue.dialogue[currentText].Substring(0, characterIndex);
            characterIndex++;
            if (characterIndex > npcDialogue.dialogue[currentText].Length)
            {
                characterIndex = 0; 
                currentText++;
                isWriting = false;
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
