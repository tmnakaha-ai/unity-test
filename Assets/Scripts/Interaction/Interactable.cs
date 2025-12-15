using UnityEngine;

namespace Interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private string promptText = "調べる";
        [SerializeField] [TextArea] private string description = "";

        public string PromptText => string.IsNullOrWhiteSpace(promptText) ? "調べる" : promptText;
        public string Description => description;
    }
}
