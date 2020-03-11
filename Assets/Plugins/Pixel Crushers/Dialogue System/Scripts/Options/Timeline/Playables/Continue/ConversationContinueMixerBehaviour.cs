#if USE_TIMELINE
#if UNITY_2017_1_OR_NEWER
// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem
{

    public class ContinueConversationMixerBehaviour : PlayableBehaviour
    {

        private HashSet<int> played = new HashSet<int>();

        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0.001f && !played.Contains(i))
                {
                    played.Add(i);
                    var inputPlayable = (ScriptPlayable<ContinueConversationBehaviour>)playable.GetInput(i);
                    ContinueConversationBehaviour input = inputPlayable.GetBehaviour();
                    if (Application.isPlaying)
                    {
                        switch (input.operation)
                        {
                            case ContinueConversationBehaviour.Operation.Continue:
                                DialogueManager.instance.BroadcastMessage("OnConversationContinueAll", SendMessageOptions.DontRequireReceiver);
                                break;
                            case ContinueConversationBehaviour.Operation.ClearSubtitleText:
                                var standardDialogueUI = DialogueManager.dialogueUI as StandardDialogueUI;
                                if (standardDialogueUI != null)
                                {
                                    if (input.clearAllPanels)
                                    {
                                        for (int j = 0; j < standardDialogueUI.conversationUIElements.subtitlePanels.Length; j++)
                                        {
                                            if (standardDialogueUI.conversationUIElements.subtitlePanels[j] == null) continue;
                                            standardDialogueUI.conversationUIElements.subtitlePanels[j].ClearText();
                                        }
                                    }
                                    else if (0 <= input.clearPanelNumber && input.clearPanelNumber < standardDialogueUI.conversationUIElements.subtitlePanels.Length &&
                                        standardDialogueUI.conversationUIElements.subtitlePanels[input.clearPanelNumber] != null)
                                    {
                                        standardDialogueUI.conversationUIElements.subtitlePanels[input.clearPanelNumber].ClearText();
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (input.operation)
                        {
                            case ContinueConversationBehaviour.Operation.Continue:
                                PreviewUI.ShowMessage("Continue", 3, -1);
                                break;
                            case ContinueConversationBehaviour.Operation.ClearSubtitleText:
                                break;
                        }
                    }
                }
                else if (inputWeight <= 0.001f && played.Contains(i))
                {
                    played.Remove(i);
                }
            }
        }

        public override void OnGraphStart(Playable playable)
        {
            base.OnGraphStart(playable);
            played.Clear();
        }

        public override void OnGraphStop(Playable playable)
        {
            base.OnGraphStop(playable);
            played.Clear();
        }

    }
}
#endif
#endif
