using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewArtifact")]
    public class ArtifactScriptableObject : ScriptableObject
    {
        public float HPPercent = 1f;
        public float AtKPercent = 1f;
        public float DefPercent = 1f;
        public float ResPercent = 1f;
    }
}

