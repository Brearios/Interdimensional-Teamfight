#if USE_TIMELINE
#if UNITY_2017_1_OR_NEWER
// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// This MonoBehaviour is used internally by the Dialogue System's
    /// playables to show an editor representation of activity that can
    /// only be accurately viewed at runtime.
    /// </summary>
    [AddComponentMenu("")] // No menu item. Only used internally.
    [ExecuteInEditMode]
    public class PreviewUI : MonoBehaviour
    {

        private string message;
        private float endTime;
        private int lineOffset;
        private bool computedRect;
        private Rect rect;

        /// <summary>
        /// Returns a best guess of what the dialogue text will be.
        /// </summary>
        /// <param name="conversationTitle">Conversation (or bark conversation) started.</param>
        /// <param name="startingEntryID">Entry started, or -1 for beginning of conversation.</param>
        /// <param name="numContinues">Number of nodes to continue past.</param>
        /// <returns></returns>
        public static string GetDialogueText(string conversationTitle, int startingEntryID, int numContinues = 0)
        {
            var dialogueManager = FindObjectOfType<DialogueSystemController>();
            if (dialogueManager != null && dialogueManager.initialDatabase != null)
            {
                var database = dialogueManager.initialDatabase;
                var conversation = database.GetConversation(conversationTitle);
                if (conversation != null)
                {
                    DialogueEntry entry = null;
                    if (startingEntryID == -1)
                    {
                        var startNode = conversation.GetFirstDialogueEntry();
                        if (startNode != null && startNode.outgoingLinks.Count > 0)
                        {
                            entry = database.GetDialogueEntry(startNode.outgoingLinks[0]);
                        }
                    }
                    else
                    {
                        entry = database.GetDialogueEntry(conversation.id, startingEntryID);
                    }
                    for (int i = 0; i < numContinues; i++)
                    {
                        if (entry == null) break;
                        if (entry.outgoingLinks.Count == 0) { entry = null; break; }
                        entry = database.GetDialogueEntry(entry.outgoingLinks[0]);
                        int safeguard = 0;
                        while (entry != null && entry.isGroup && entry.outgoingLinks.Count > 0 && safeguard++ < 9999)
                        { // Bypass group links:
                            entry = database.GetDialogueEntry(entry.outgoingLinks[0]);
                        }
                    }
                    if (entry != null)
                    {
                        var text = !string.IsNullOrEmpty(entry.MenuText) ? entry.MenuText : entry.DialogueText;
                        return text;
                    }
                }                
            }
            return "(determined at runtime)";
        }

        public static void ShowMessage(string message, float duration, int lineOffset)
        {
            var go = new GameObject("Editor Preview UI: " + message);
            go.tag = "EditorOnly";
            go.hideFlags = HideFlags.DontSave;
            var previewUI = go.AddComponent<PreviewUI>();
            previewUI.Show(message, duration, lineOffset);
        }

        protected void Show(string message, float duration, int lineOffset)
        {
            this.message = message;
            this.lineOffset = lineOffset;
            endTime = Time.realtimeSinceStartup + (Mathf.Approximately(0, duration) ? 2 : duration);
            computedRect = false;
        }

        private void OnGUI()
        {
            if (!computedRect)
            {
                computedRect = true;
                var size = GUI.skin.label.CalcSize(new GUIContent(message));
                rect = new Rect((Screen.width - size.x) / 2, (Screen.height - size.y) / 2 + lineOffset * size.y, size.x, size.y);
            }
            GUI.Label(rect, message);
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
            else if (Time.realtimeSinceStartup >= endTime)
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}
#endif
#endif
