using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float raycastDistance = 3f;
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private float messageDuration = 3f;

        private Text promptText;
        private Text messageText;
        private Coroutine messageRoutine;

        private void Awake()
        {
            EnsureCameraAssigned();
            SetupUI();
        }

        private void Update()
        {
            if (playerCamera == null)
            {
                EnsureCameraAssigned();
                if (playerCamera == null)
                {
                    return;
                }
            }

            HandleInteraction();
        }

        private void HandleInteraction()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            Interactable interactable = null;

            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
            {
                interactable = hit.collider.GetComponentInParent<Interactable>();
            }

            if (interactable != null)
            {
                ShowPrompt(interactable.PromptText);
                if (Input.GetKeyDown(interactKey))
                {
                    DisplayDescription(interactable.Description);
                }
            }
            else
            {
                HidePrompt();
            }
        }

        private void ShowPrompt(string prompt)
        {
            if (promptText == null)
            {
                return;
            }

            promptText.text = $"[{interactKey}] {prompt}";
            promptText.enabled = true;
        }

        private void HidePrompt()
        {
            if (promptText != null)
            {
                promptText.enabled = false;
            }
        }

        private void DisplayDescription(string description)
        {
            if (messageText == null)
            {
                return;
            }

            if (messageRoutine != null)
            {
                StopCoroutine(messageRoutine);
            }

            messageRoutine = StartCoroutine(ShowMessageRoutine(description));
        }

        private IEnumerator ShowMessageRoutine(string description)
        {
            messageText.text = description;
            messageText.enabled = true;
            yield return new WaitForSeconds(messageDuration);
            messageText.text = string.Empty;
            messageText.enabled = false;
        }

        private void EnsureCameraAssigned()
        {
            if (playerCamera == null && Camera.main != null)
            {
                playerCamera = Camera.main;
            }
        }

        private void SetupUI()
        {
            Canvas canvas = new GameObject("PlayerInteractionCanvas").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;
            canvas.gameObject.AddComponent<CanvasScaler>();
            canvas.gameObject.AddComponent<GraphicRaycaster>();

            promptText = CreateText(canvas.transform, "InteractPrompt", new Vector2(0.5f, 0.45f));
            promptText.enabled = false;

            messageText = CreateText(canvas.transform, "InteractMessage", new Vector2(0.5f, 0.3f));
            messageText.alignment = TextAnchor.MiddleCenter;
            messageText.enabled = false;
        }

        private Text CreateText(Transform parent, string name, Vector2 anchor)
        {
            GameObject textObj = new GameObject(name);
            textObj.transform.SetParent(parent);

            RectTransform rectTransform = textObj.AddComponent<RectTransform>();
            rectTransform.anchorMin = anchor;
            rectTransform.anchorMax = anchor;
            rectTransform.pivot = anchor;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(400f, 60f);

            Text text = textObj.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.supportRichText = true;
            text.resizeTextForBestFit = true;

            return text;
        }
    }
}
