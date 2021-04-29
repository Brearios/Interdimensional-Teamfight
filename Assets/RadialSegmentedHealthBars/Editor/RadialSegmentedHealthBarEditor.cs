using System;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace RengeGames.HealthBars.Editors {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(RadialSegmentedHealthBar))]
    public class RadialSegmentedHealthBarEditor : Editor {
        #region serializedproperties
        
        private SerializedProperty parentName;
        private SerializedProperty hbName;
        private SerializedProperty overlayColor;
        private SerializedProperty innerColor;
        private SerializedProperty borderColor;
        private SerializedProperty emptyColor;
        private SerializedProperty spaceColor;
        private SerializedProperty segmentCount;
        private SerializedProperty removedSegments;
        private SerializedProperty spacing;
        private SerializedProperty radius;
        private SerializedProperty lineWidth;
        private SerializedProperty rotation;
        private SerializedProperty borderWidth;
        private SerializedProperty borderSpacing;
        private SerializedProperty removeBorder;
        private SerializedProperty overlayNoiseEnabled;
        private SerializedProperty overlayNoiseScale;
        private SerializedProperty overlayNoiseStrength;
        private SerializedProperty overlayNoiseOffset;
        private SerializedProperty emptyNoiseEnabled;
        private SerializedProperty emptyNoiseScale;
        private SerializedProperty emptyNoiseStrength;
        private SerializedProperty emptyNoiseOffset;
        private SerializedProperty contentNoiseEnabled;
        private SerializedProperty contentNoiseScale;
        private SerializedProperty contentNoiseStrength;
        private SerializedProperty contentNoiseOffset;
        private SerializedProperty pulsateWhenLow;
        private SerializedProperty pulseSpeed;
        private SerializedProperty pulseActivationThreshold;
        private SerializedProperty overlayTextureEnabled;
        private SerializedProperty overlayTexture;
        private SerializedProperty overlayTextureOpacity;
        private SerializedProperty overlayTextureTiling;
        private SerializedProperty overlayTextureOffset;
        private SerializedProperty innerTextureEnabled;
        private SerializedProperty innerTexture;
        private SerializedProperty alignInnerTexture;
        private SerializedProperty innerTextureScaleWithSegments;
        private SerializedProperty innerTextureOpacity;
        private SerializedProperty innerTextureTiling;
        private SerializedProperty innerTextureOffset;
        private SerializedProperty borderTextureEnabled;
        private SerializedProperty borderTexture;
        private SerializedProperty alignBorderTexture;
        private SerializedProperty borderTextureScaleWithSegments;
        private SerializedProperty borderTextureOpacity;
        private SerializedProperty borderTextureTiling;
        private SerializedProperty borderTextureOffset;
        private SerializedProperty emptyTextureEnabled;
        private SerializedProperty emptyTexture;
        private SerializedProperty alignEmptyTexture;
        private SerializedProperty emptyTextureScaleWithSegments;
        private SerializedProperty emptyTextureOpacity;
        private SerializedProperty emptyTextureTiling;
        private SerializedProperty emptyTextureOffset;
        private SerializedProperty spaceTextureEnabled;
        private SerializedProperty spaceTexture;
        private SerializedProperty alignSpaceTexture;
        private SerializedProperty spaceTextureOpacity;
        private SerializedProperty spaceTextureTiling;
        private SerializedProperty spaceTextureOffset;
        private SerializedProperty innerGradient;
        private SerializedProperty innerGradientEnabled;
        private SerializedProperty valueAsGradientTimeInner;
        private SerializedProperty emptyGradient;
        private SerializedProperty emptyGradientEnabled;
        private SerializedProperty valueAsGradientTimeEmpty;

        #endregion

        private GUIStyle headerStyle;
        private bool generalFoldout = true;
        private bool overlayFoldout = false;
        private bool hbFoldout = true;
        private bool borderFoldout = false;
        private bool depletedFoldout = false;
        private bool emptyFoldout = false;

        private void OnEnable() {
            parentName = serializedObject.FindProperty("parentName");
            hbName = serializedObject.FindProperty("hbName");
            overlayColor = serializedObject.FindProperty("_overlayColor");
            innerColor = serializedObject.FindProperty("_innerColor");
            borderColor = serializedObject.FindProperty("_borderColor");
            emptyColor = serializedObject.FindProperty("_emptyColor");
            spaceColor = serializedObject.FindProperty("_spaceColor");
            segmentCount = serializedObject.FindProperty("_segmentCount");
            removedSegments = serializedObject.FindProperty("_removedSegments");
            spacing = serializedObject.FindProperty("_spacing");
            radius = serializedObject.FindProperty("_radius");
            lineWidth = serializedObject.FindProperty("_lineWidth");
            rotation = serializedObject.FindProperty("_rotation");
            borderWidth = serializedObject.FindProperty("_borderWidth");
            borderSpacing = serializedObject.FindProperty("_borderSpacing");
            removeBorder = serializedObject.FindProperty("_removeBorder");
            overlayNoiseEnabled = serializedObject.FindProperty("_overlayNoiseEnabled");
            overlayNoiseScale = serializedObject.FindProperty("_overlayNoiseScale");
            overlayNoiseStrength = serializedObject.FindProperty("_overlayNoiseStrength");
            overlayNoiseOffset = serializedObject.FindProperty("_overlayNoiseOffset");
            emptyNoiseEnabled = serializedObject.FindProperty("_emptyNoiseEnabled");
            emptyNoiseScale = serializedObject.FindProperty("_emptyNoiseScale");
            emptyNoiseStrength = serializedObject.FindProperty("_emptyNoiseStrength");
            emptyNoiseOffset = serializedObject.FindProperty("_emptyNoiseOffset");
            contentNoiseEnabled = serializedObject.FindProperty("_contentNoiseEnabled");
            contentNoiseScale = serializedObject.FindProperty("_contentNoiseScale");
            contentNoiseStrength = serializedObject.FindProperty("_contentNoiseStrength");
            contentNoiseOffset = serializedObject.FindProperty("_contentNoiseOffset");
            pulsateWhenLow = serializedObject.FindProperty("_pulsateWhenLow");
            pulseSpeed = serializedObject.FindProperty("_pulseSpeed");
            pulseActivationThreshold = serializedObject.FindProperty("_pulseActivationThreshold");
            overlayTextureEnabled = serializedObject.FindProperty("_overlayTextureEnabled");
            overlayTexture = serializedObject.FindProperty("_overlayTexture");
            overlayTextureOpacity = serializedObject.FindProperty("_overlayTextureOpacity");
            overlayTextureTiling = serializedObject.FindProperty("_overlayTextureTiling");
            overlayTextureOffset = serializedObject.FindProperty("_overlayTextureOffset");
            innerTextureEnabled = serializedObject.FindProperty("_innerTextureEnabled");
            innerTexture = serializedObject.FindProperty("_innerTexture");
            alignInnerTexture = serializedObject.FindProperty("_alignInnerTexture");
            innerTextureScaleWithSegments = serializedObject.FindProperty("_innerTextureScaleWithSegments");
            innerTextureOpacity = serializedObject.FindProperty("_innerTextureOpacity");
            innerTextureTiling = serializedObject.FindProperty("_innerTextureTiling");
            innerTextureOffset = serializedObject.FindProperty("_innerTextureOffset");
            borderTextureEnabled = serializedObject.FindProperty("_borderTextureEnabled");
            borderTexture = serializedObject.FindProperty("_borderTexture");
            alignBorderTexture = serializedObject.FindProperty("_alignBorderTexture");
            borderTextureScaleWithSegments = serializedObject.FindProperty("_borderTextureScaleWithSegments");
            borderTextureOpacity = serializedObject.FindProperty("_borderTextureOpacity");
            borderTextureTiling = serializedObject.FindProperty("_borderTextureTiling");
            borderTextureOffset = serializedObject.FindProperty("_borderTextureOffset");
            emptyTextureEnabled = serializedObject.FindProperty("_emptyTextureEnabled");
            emptyTexture = serializedObject.FindProperty("_emptyTexture");
            alignEmptyTexture = serializedObject.FindProperty("_alignEmptyTexture");
            emptyTextureScaleWithSegments = serializedObject.FindProperty("_emptyTextureScaleWithSegments");
            emptyTextureOpacity = serializedObject.FindProperty("_emptyTextureOpacity");
            emptyTextureTiling = serializedObject.FindProperty("_emptyTextureTiling");
            emptyTextureOffset = serializedObject.FindProperty("_emptyTextureOffset");
            spaceTextureEnabled = serializedObject.FindProperty("_spaceTextureEnabled");
            spaceTexture = serializedObject.FindProperty("_spaceTexture");
            alignSpaceTexture = serializedObject.FindProperty("_alignSpaceTexture");
            spaceTextureOpacity = serializedObject.FindProperty("_spaceTextureOpacity");
            spaceTextureTiling = serializedObject.FindProperty("_spaceTextureTiling");
            spaceTextureOffset = serializedObject.FindProperty("_spaceTextureOffset");
            innerGradient = serializedObject.FindProperty("_innerGradient");
            innerGradientEnabled = serializedObject.FindProperty("_innerGradientEnabled");
            valueAsGradientTimeInner = serializedObject.FindProperty("_valueAsGradientTimeInner");
            emptyGradient = serializedObject.FindProperty("_emptyGradient");
            emptyGradientEnabled = serializedObject.FindProperty("_emptyGradientEnabled");
            valueAsGradientTimeEmpty = serializedObject.FindProperty("_valueAsGradientTimeEmpty");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            headerStyle = GUI.skin.label;
            headerStyle.fontStyle = FontStyle.Bold;

            GUILayout.Label("Naming and Access", headerStyle);

            EditorGUILayout.PropertyField(parentName, new GUIContent() {text = "Parent Name"});
            EditorGUILayout.PropertyField(hbName, new GUIContent() {text = "Health Bar Name"});

            GUILayout.Space(10);
            GUILayout.Label("Data", headerStyle);

            EditorGUILayout.PropertyField(segmentCount, new GUIContent() {text = "Segment Count"});
            EditorGUILayout.PropertyField(removedSegments, new GUIContent() {text = "Removed Segments"});

            GUILayout.Space(10);
            GUILayout.Label("Appearance", headerStyle);

            generalFoldout = EditorGUILayout.Foldout(generalFoldout, "General Appearance");
            if (generalFoldout) {
                EditorGUI.indentLevel++;
                SliderPropertyField("Segment Spacing", spacing, 0, 1);
                SliderPropertyField("Radius", radius, 0, 1);
                SliderPropertyField("Line Width", lineWidth, 0, 1);
                SliderPropertyField("Rotation", rotation, 0, 360);
                EditorGUI.indentLevel--;
            }

            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            {
                hbFoldout = EditorGUILayout.Foldout(hbFoldout, "Health Portion");
                EditorGUILayout.PropertyField(innerColor, new GUIContent() {text = "Health Color"});
            }
            EditorGUILayout.EndHorizontal();
            if (hbFoldout) {
                GUILayout.BeginVertical();
                {
                    EditorGUILayout.PropertyField(pulsateWhenLow, new GUIContent() {text = "Pulsate When Low"});
                    if (pulsateWhenLow.boolValue) {
                        EditorGUI.indentLevel++;
                        SliderPropertyField("Percent Health Activation", pulseActivationThreshold, 0, 1);
                        EditorGUILayout.PropertyField(pulseSpeed, new GUIContent() {text = "Pulse Speed"});
                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUILayout.PropertyField(innerTextureEnabled, new GUIContent(){text = "Use Texture"});
                    if (!innerTextureEnabled.hasMultipleDifferentValues && innerTextureEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        //Texture stuff
                        EditorGUILayout.PropertyField(innerTexture, new GUIContent(){text = "Texture"});
                        EditorGUILayout.PropertyField(innerTextureScaleWithSegments, new GUIContent() {text = "Scale Texture with Segments"});
                        EditorGUILayout.PropertyField(alignInnerTexture, new GUIContent() {text = "Align Texture"});
                        SliderPropertyField("Texture Opacity", innerTextureOpacity, 0, 1);
                        EditorGUILayout.PropertyField(innerTextureTiling, new GUIContent(){text = "Texture Tiling"});
                        EditorGUILayout.PropertyField(innerTextureOffset, new GUIContent(){text = "Texture Offset"});
                        
                        //Gradient stuff
                        EditorGUILayout.PropertyField(innerGradientEnabled, new GUIContent(){text = "Use Gradient"});
                        if (!innerGradientEnabled.hasMultipleDifferentValues && innerGradientEnabled.boolValue) {
                            EditorGUILayout.PropertyField(innerGradient, new GUIContent(){text = "Gradient"});
                            EditorGUILayout.PropertyField(valueAsGradientTimeInner, new GUIContent(){text = "Value As Gradient Time"});
                        }
                        
                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUILayout.PropertyField(contentNoiseEnabled, new GUIContent(){text = "Use Noise"});
                    if (!contentNoiseEnabled.hasMultipleDifferentValues && contentNoiseEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(contentNoiseStrength, new GUIContent() {text = "Noise Strength"});
                        EditorGUILayout.PropertyField(contentNoiseScale, new GUIContent() {text = "Noise Scale"});
                        EditorGUILayout.PropertyField(contentNoiseOffset, new GUIContent() {text = "Noise Offset"});
                        EditorGUI.indentLevel--;
                    }
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            {
                overlayFoldout = EditorGUILayout.Foldout(overlayFoldout, "Overlay Portion");
                EditorGUILayout.PropertyField(overlayColor, new GUIContent() {text = "Overlay Color"});
            }
            EditorGUILayout.EndHorizontal();
            if (overlayFoldout) {
                GUILayout.BeginVertical();
                {
                    EditorGUILayout.PropertyField(overlayTextureEnabled, new GUIContent() {text = "Use Texture"});
                    if (overlayTextureEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(overlayTexture, new GUIContent(){text = "Texture"});
                        SliderPropertyField("Texture Opacity", overlayTextureOpacity, 0, 1);
                        EditorGUILayout.PropertyField(overlayTextureTiling, new GUIContent(){text = "Texture Tiling"});
                        EditorGUILayout.PropertyField(overlayTextureOffset, new GUIContent(){text = "Texture Offset"});
                        EditorGUI.indentLevel--;
                    }
                    
                    EditorGUILayout.PropertyField(overlayNoiseEnabled, new GUIContent() {text = "Use Noise"});
                    if (overlayNoiseEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(overlayNoiseStrength, new GUIContent(){text = "Noise Strength"});
                        EditorGUILayout.PropertyField(overlayNoiseScale, new GUIContent(){text = "Noise Scale"});
                        EditorGUILayout.PropertyField(overlayNoiseOffset,new GUIContent(){text = "Noise Offset"});
                        EditorGUI.indentLevel--;
                    }
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            {
                borderFoldout = EditorGUILayout.Foldout(borderFoldout, "Border");
                EditorGUILayout.PropertyField(borderColor, new GUIContent() {text = "Border Color"});
            }
            EditorGUILayout.EndHorizontal();
            if (borderFoldout) {
                GUILayout.BeginVertical();
                {
                    SliderPropertyField("Border Width", borderWidth, 0, 1);
                    SliderPropertyField("Border Spacing", borderSpacing, 0, 1);

                    EditorGUILayout.PropertyField(borderTextureEnabled, new GUIContent(){text = "Use Texture"});
                    if (borderTextureEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(borderTexture, new GUIContent(){text = "Texture"});
                        EditorGUILayout.PropertyField(borderTextureScaleWithSegments, new GUIContent(){text = "Scale Texture with Segments"});
                        EditorGUILayout.PropertyField(alignBorderTexture, new GUIContent(){text = "Align Texture"});
                        SliderPropertyField("Texture Opacity", borderTextureOpacity, 0, 1);
                        EditorGUILayout.PropertyField(borderTextureTiling, new GUIContent(){text = "Texture Tiling"});
                        EditorGUILayout.PropertyField(borderTextureOffset, new GUIContent(){text = "Texture Offset"});
                        EditorGUI.indentLevel--;
                    }
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            {
                depletedFoldout = EditorGUILayout.Foldout(depletedFoldout, "Depleted Portion");
                EditorGUILayout.PropertyField(emptyColor, new GUIContent() {text = "Depleted Color"});
            }
            EditorGUILayout.EndHorizontal();
            if (depletedFoldout) {
                GUILayout.BeginVertical();
                {
                    SliderPropertyField("Depleted Transparency", removeBorder, 0, 1);
                    EditorGUILayout.PropertyField(emptyTextureEnabled, new GUIContent() {text = "Use Texture"});
                    if (emptyTextureEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(emptyTexture, new GUIContent(){text = "Texture"});
                        EditorGUILayout.PropertyField(emptyTextureScaleWithSegments, new GUIContent(){text = "Scale Texture with Segments"});
                        EditorGUILayout.PropertyField(alignEmptyTexture, new GUIContent(){text = "Align Texture"});
                        SliderPropertyField("Texture Opacity", emptyTextureOpacity, 0, 1);
                        EditorGUILayout.PropertyField(emptyTextureTiling, new GUIContent(){text = "Texture Tiling"});
                        EditorGUILayout.PropertyField(emptyTextureOffset, new GUIContent(){text = "Texture Offset"});
                        EditorGUI.indentLevel--;
                        
                        //Gradient stuff
                        EditorGUILayout.PropertyField(emptyGradientEnabled, new GUIContent(){text = "Use Gradient"});
                        if (!emptyGradientEnabled.hasMultipleDifferentValues && emptyGradientEnabled.boolValue) {
                            EditorGUILayout.PropertyField(emptyGradient, new GUIContent(){text = "Gradient"});
                            EditorGUILayout.PropertyField(valueAsGradientTimeEmpty, new GUIContent(){text = "Value As Gradient Time"});
                        }
                    }

                    EditorGUILayout.PropertyField(emptyNoiseEnabled, new GUIContent() {text = "Use Noise"});
                    if (emptyNoiseEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(emptyNoiseStrength, new GUIContent(){text = "Noise Strength"});
                        EditorGUILayout.PropertyField(emptyNoiseScale, new GUIContent(){text = "Noise Scale"});
                        EditorGUILayout.PropertyField(emptyNoiseOffset,new GUIContent(){text = "Noise Offset"});
                        EditorGUI.indentLevel--;
                    }
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            {
                emptyFoldout = EditorGUILayout.Foldout(emptyFoldout, "Empty Space");
                EditorGUILayout.PropertyField(spaceColor, new GUIContent() {text = "Empty Space Color"});
            }
            EditorGUILayout.EndHorizontal();
            if (emptyFoldout) {
                GUILayout.BeginVertical();
                {
                    spaceTextureEnabled.boolValue = EditorGUILayout.Toggle("Use Texture", spaceTextureEnabled.boolValue);
                    if (spaceTextureEnabled.boolValue) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(spaceTexture, new GUIContent(){text = "Texture"});
                        EditorGUILayout.PropertyField(alignSpaceTexture, new GUIContent(){text = "Align Texture"});
                        spaceTextureOpacity.floatValue = EditorGUILayout.Slider("Texture Opacity", spaceTextureOpacity.floatValue, 0, 1);
                        EditorGUILayout.PropertyField(spaceTextureTiling, new GUIContent(){text = "Texture Tiling"});
                        EditorGUILayout.PropertyField(spaceTextureOffset, new GUIContent(){text = "Texture Offset"});
                        EditorGUI.indentLevel--;
                    }
                }
                GUILayout.EndVertical();
            }

            EditorGUILayout.Separator();
            GUILayout.Label("Other", headerStyle);
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    if (GUILayout.Button("Reset to defaults")) {
                        Undo.RecordObjects(serializedObject.targetObjects, "Reset RSHB");
                        foreach (var serializedObjectTargetObject in serializedObject.targetObjects) {
                            (serializedObjectTargetObject as RadialSegmentedHealthBar)?.ResetSerializedProperties();
                        }
                    }

                    if (GUILayout.Button("Duplicate Shader")) {
                        foreach (var serializedObjectTargetObject in serializedObject.targetObjects) {
                            if (serializedObjectTargetObject != null) {
                                var obj = serializedObjectTargetObject as RadialSegmentedHealthBar;
                                GameObject.Instantiate(obj, obj.transform.parent);
                            }
                        }
                    }
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                {
                    RadialSegmentedHealthBar rshb = (RadialSegmentedHealthBar) target;
                    if (GUILayout.Button("Use Sprite Renderer")) {
                        rshb.UsingSpriteRenderer = true;
                        Debug.Log($"{rshb.ParentName}/{rshb.Name} is now using a Sprite Renderer component");
                    }

                    if (GUILayout.Button("Use Image")) {
                        rshb.UsingSpriteRenderer = false;
                        Debug.Log($"{rshb.ParentName}/{rshb.Name} is now using an Image component");
                    }

                    string currentlyUsing = "Image";
                    if (rshb.UsingSpriteRenderer)
                        currentlyUsing = "Sprite Renderer";
                    GUILayout.Label("Currently using: " + currentlyUsing);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
            //base.OnInspectorGUI();
        }

        void SliderPropertyField(string text, SerializedProperty property, float min, float max) {
            var floatVal = property.floatValue;
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            floatVal = EditorGUILayout.Slider(text, floatVal, min, max);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck()) {
                property.floatValue = floatVal;
            }
        }
    }
}