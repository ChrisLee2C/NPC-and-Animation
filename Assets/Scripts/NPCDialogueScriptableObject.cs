using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "npcDialogue", fileName = "npcDialogue")]
public class NPCDialogueScriptableObject : ScriptableObject
{
    public string npcName;
    public string[] dialogue;
}
