using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using UnityEngine.UI;

public class CharScrollRectPopulator : MonoBehaviour
{
    #region Variables

    public Transform ContentPanel;
    public GameObject CharPortraitObj;
    public string FolderPath = "ScriptableObjects/AgentSO";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadAndPopulate();
    }

    void LoadAndPopulate()
    {
        EntityScriptableObject[] PlayableCharacters = Resources.LoadAll<EntityScriptableObject>(FolderPath);
        
        foreach (EntityScriptableObject PlayableCharacter in PlayableCharacters)
        {
            EntityScriptableObject exampleEntity = Resources.Load<EntityScriptableObject>("ScriptableObjects/AgentSO/" + PlayableCharacter.name);
            
            Debug.Log(exampleEntity);

            //Spawn the image into the scroll rect
            GameObject newChar = Instantiate(CharPortraitObj, ContentPanel);

            //Fetch the image component from the instantiated object
            Image imageComponent = newChar.GetComponent<Image>();

            //Set the sprite of the newly populated object to the entity portrait sprite
            if (imageComponent != null && PlayableCharacter !=null)
            {
                imageComponent.sprite = PlayableCharacter.EntityPortrait;
                imageComponent.name = PlayableCharacter.name;
            }   
        }
    }
}
