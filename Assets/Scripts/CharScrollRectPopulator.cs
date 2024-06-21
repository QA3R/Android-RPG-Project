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
        EntityScriptableObject[] CharPortraits = Resources.LoadAll<EntityScriptableObject>(FolderPath);
        
        foreach (EntityScriptableObject CharPortrait in CharPortraits)
        {
            Debug.Log(CharPortrait.Name);

            //Spawn the image into the scroll rect
            GameObject newChar = Instantiate(CharPortraitObj, ContentPanel);

            //Fetch the image component from the instantiated object
            Image imageComponent = newChar.GetComponent<Image>();

            //Set the sprite of the newly populated object to the entity portrait sprite
            if (imageComponent != null && CharPortrait !=null)
            {
                imageComponent.sprite = CharPortrait.EntityPortrait;
                imageComponent.name = CharPortrait.name + "Portrait";
            }
        }
    }
}
