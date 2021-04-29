using System;
using System.Collections;
using System.Collections.Generic;
using RengeGames.HealthBars.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace RengeGames.HealthBars {

    public static class RadialHealthBarProperties {
        public const string OverlayColor = "OverlayColor";
        public const string InnerColor = "InnerColor";
        public const string BorderColor = "BorderColor";
        public const string EmptyColor = "EmptyColor";
        public const string SpaceColor = "SpaceColor";
        public const string SegmentCount = "SegmentCount";
        public const string RemovedSegments = "RemovedSegments";
        public const string Spacing = "Spacing";
        public const string Radius = "Radius";
        public const string LineWidth = "LineWidth";
        public const string Rotation = "Rotation";
        public const string BorderWidth = "BorderWidth";
        public const string BorderSpacing = "BorderSpacing";
        public const string RemoveBorder = "RemoveBorder";
        public const string OverlayNoiseEnabled = "OverlayNoiseEnabled";
        public const string OverlayNoiseScale = "OverlayNoiseScale";
        public const string OverlayNoiseStrength = "OverlayNoiseStrength";
        public const string OverlayNoiseOffset = "OverlayNoiseOffset";
        public const string EmptyNoiseEnabled = "EmptyNoiseEnabled";
        public const string EmptyNoiseScale = "EmptyNoiseScale";
        public const string EmptyNoiseStrength = "EmptyNoiseStrength";
        public const string EmptyNoiseOffset = "EmptyNoiseOffset";
        public const string ContentNoiseEnabled = "ContentNoiseEnabled";
        public const string ContentNoiseScale = "ContentNoiseScale";
        public const string ContentNoiseStrength = "ContentNoiseStrength";
        public const string ContentNoiseOffset = "ContentNoiseOffset";
        public const string PulsateWhenLow = "PulsateWhenLow";
        public const string PulseSpeed = "PulseSpeed";
        public const string PulseActivationThreshold = "PulseActivationThreshold";
        public const string OverlayTexture = "OverlayTexture";
        public const string OverlayTextureOpacity = "OverlayTextureOpacity";
        public const string OverlayTextureTiling = "OverlayTextureTiling";
        public const string OverlayTextureOffset = "OverlayTextureOffset";
        public const string InnerTexture = "InnerTexture";
        public const string AlignInnerTexture = "AlignInnerTexture";
        public const string InnerTextureScaleWithSegments = "InnerTextureScaleWithSegments";
        public const string InnerTextureOpacity = "InnerTextureOpacity";
        public const string InnerTextureTiling = "InnerTextureTiling";
        public const string InnerTextureOffset = "InnerTextureOffset";
        public const string BorderTexture = "BorderTexture";
        public const string AlignBorderTexture = "AlignBorderTexture";
        public const string BorderTextureScaleWithSegments = "BorderTextureScaleWithSegments";
        public const string BorderTextureOpacity = "BorderTextureOpacity";
        public const string BorderTextureTiling = "BorderTextureTiling";
        public const string BorderTextureOffset = "BorderTextureOffset";
        public const string EmptyTexture = "EmptyTexture";
        public const string AlignEmptyTexture = "AlignEmptyTexture";
        public const string EmptyTextureScaleWithSegments = "EmptyTextureScaleWithSegments";
        public const string EmptyTextureOpacity = "EmptyTextureOpacity";
        public const string EmptyTextureTiling = "EmptyTextureTiling";
        public const string EmptyTextureOffset = "EmptyTextureOffset";
        public const string SpaceTexture = "SpaceTexture";
        public const string AlignSpaceTexture = "AlignSpaceTexture";
        public const string SpaceTextureOpacity = "SpaceTextureOpacity";
        public const string SpaceTextureTiling = "SpaceTextureTiling";
        public const string SpaceTextureOffset = "SpaceTextureOffset";
        public const string InnerGradient = "InnerGradient";
        public const string InnerGradientEnabled = "InnerGradientEnabled";
        public const string ValueAsGradientTimeInner = "ValueAsGradientTimeInner";
        public const string EmptyGradient = "EmptyGradient";
        public const string EmptyGradientEnabled = "EmptyGradientEnabled";
        public const string ValueAsGradientTimeEmpty = "ValueAsGradientTimeEmpty";
    }

    public static class RadialHealthBarKeywords {
        public const string OverlayTextureEnabled = "OverlayTextureEnabled";
        public const string InnerTextureEnabled = "InnerTextureEnabled";
        public const string BorderTextureEnabled = "BorderTextureEnabled";
        public const string EmptyTextureEnabled = "EmptyTextureEnabled";
        public const string SpaceTextureEnabled = "SpaceTextureEnabled";
    }

    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("Health Bars/Radial Segmented Health Bar")]
    public class RadialSegmentedHealthBar : MonoBehaviour, ISegmentedHealthBar {
        private string oldParentName = "Player";
        [SerializeField] private string parentName = "Player";

        public string ParentName {
            get => parentName;
            set {
                if (Application.isPlaying)
                    StatusBarsManager.RemoveHealthBar(this, false);
                parentName = value;
                if (Application.isPlaying)
                    StatusBarsManager.AddHealthBar(this);
            }
        }

        private string oldHbName = "Primary";
        [SerializeField] private string hbName = "Primary";

        public string Name {
            get => hbName;
            set {
                if (Application.isPlaying)
                    StatusBarsManager.RemoveHealthBar(this, false);
                hbName = value;
                if (Application.isPlaying)
                    StatusBarsManager.AddHealthBar(this);
            }
        }

        [SerializeField] private bool usingSpriteRenderer;

        public bool UsingSpriteRenderer {
            get => usingSpriteRenderer;
            set {
                usingSpriteRenderer = value;
                GenerateRequiredComponents(false);
            }
        }

        #region Properties

        public ShaderProperty<Color> OverlayColor { get; private set; }
        public ShaderProperty<Color> InnerColor { get; private set; }
        public ShaderProperty<Color> BorderColor { get; private set; }
        public ShaderProperty<Color> EmptyColor { get; private set; }
        public ShaderProperty<Color> SpaceColor { get; private set; }
        public ShaderProperty<float> SegmentCount { get; private set; }
        public ShaderProperty<float> RemovedSegments { get; private set; }
        public ShaderProperty<float> Spacing { get; private set; }
        public ShaderProperty<float> Radius { get; private set; }
        public ShaderProperty<float> LineWidth { get; private set; }
        public ShaderProperty<float> Rotation { get; private set; }
        public ShaderProperty<float> BorderWidth { get; private set; }
        public ShaderProperty<float> BorderSpacing { get; private set; }
        public ShaderProperty<float> RemoveBorder { get; private set; }
        public ShaderProperty<bool> OverlayNoiseEnabled { get; private set; }
        public ShaderProperty<float> OverlayNoiseScale { get; private set; }
        public ShaderProperty<float> OverlayNoiseStrength { get; private set; }
        public ShaderProperty<Vector2> OverlayNoiseOffset { get; private set; }
        public ShaderProperty<bool> EmptyNoiseEnabled { get; private set; }
        public ShaderProperty<float> EmptyNoiseScale { get; private set; }
        public ShaderProperty<float> EmptyNoiseStrength { get; private set; }
        public ShaderProperty<Vector2> EmptyNoiseOffset { get; private set; }
        public ShaderProperty<bool> ContentNoiseEnabled { get; private set; }
        public ShaderProperty<float> ContentNoiseScale { get; private set; }
        public ShaderProperty<float> ContentNoiseStrength { get; private set; }
        public ShaderProperty<Vector2> ContentNoiseOffset { get; private set; }
        public ShaderProperty<bool> PulsateWhenLow { get; private set; }
        public ShaderProperty<float> PulseSpeed { get; private set; }
        public ShaderProperty<float> PulseActivationThreshold { get; private set; }
        public ShaderKeyword OverlayTextureEnabled { get; private set; }
        public ShaderProperty<Texture2D> OverlayTexture { get; private set; }
        public ShaderProperty<float> OverlayTextureOpacity { get; private set; }
        public ShaderProperty<Vector2> OverlayTextureTiling { get; private set; }
        public ShaderProperty<Vector2> OverlayTextureOffset { get; private set; }
        public ShaderKeyword InnerTextureEnabled { get; private set; }
        public ShaderProperty<Texture2D> InnerTexture { get; private set; }
        public ShaderProperty<bool> AlignInnerTexture { get; private set; }
        public ShaderProperty<bool> InnerTextureScaleWithSegments { get; private set; }
        public ShaderProperty<float> InnerTextureOpacity { get; private set; }
        public ShaderProperty<Vector2> InnerTextureTiling { get; private set; }
        public ShaderProperty<Vector2> InnerTextureOffset { get; private set; }
        public ShaderKeyword BorderTextureEnabled { get; private set; }
        public ShaderProperty<Texture2D> BorderTexture { get; private set; }
        public ShaderProperty<bool> AlignBorderTexture { get; private set; }
        public ShaderProperty<bool> BorderTextureScaleWithSegments { get; private set; }
        public ShaderProperty<float> BorderTextureOpacity { get; private set; }
        public ShaderProperty<Vector2> BorderTextureTiling { get; private set; }
        public ShaderProperty<Vector2> BorderTextureOffset { get; private set; }
        public ShaderKeyword EmptyTextureEnabled { get; private set; }
        public ShaderProperty<Texture2D> EmptyTexture { get; private set; }
        public ShaderProperty<bool> AlignEmptyTexture { get; private set; }
        public ShaderProperty<bool> EmptyTextureScaleWithSegments { get; private set; }
        public ShaderProperty<float> EmptyTextureOpacity { get; private set; }
        public ShaderProperty<Vector2> EmptyTextureTiling { get; private set; }
        public ShaderProperty<Vector2> EmptyTextureOffset { get; private set; }
        public ShaderKeyword SpaceTextureEnabled { get; private set; }
        public ShaderProperty<Texture2D> SpaceTexture { get; private set; }
        public ShaderProperty<bool> AlignSpaceTexture { get; private set; }
        public ShaderProperty<float> SpaceTextureOpacity { get; private set; }
        public ShaderProperty<Vector2> SpaceTextureTiling { get; private set; }
        public ShaderProperty<Vector2> SpaceTextureOffset { get; private set; }
        public ShaderProperty<Gradient> InnerGradient { get; private set; }
        public ShaderProperty<bool> InnerGradientEnabled { get; private set; }
        public ShaderProperty<bool> ValueAsGradientTimeInner { get; private set; }
        public ShaderProperty<Gradient> EmptyGradient { get; private set; }
        public ShaderProperty<bool> EmptyGradientEnabled { get; private set; }
        public ShaderProperty<bool> ValueAsGradientTimeEmpty { get; private set; }

        #endregion

        #region Serialized Properties

        [SerializeField] [ColorUsage(true, true)]
        private Color _overlayColor = Color.white;

        [SerializeField] [ColorUsage(true, true)]
        private Color _innerColor = Color.red;

        [SerializeField] [ColorUsage(true, true)]
        private Color _borderColor = Color.white;

        [SerializeField] [ColorUsage(true, true)]
        private Color _emptyColor = Color.gray;

        [SerializeField] [ColorUsage(true, true)]
        private Color _spaceColor = Color.clear;

        [SerializeField] private float _segmentCount = 5;
        [SerializeField] private float _removedSegments = 1;
        [SerializeField] private float _spacing = 0.02f;
        [SerializeField] private float _radius = .35f;
        [SerializeField] private float _lineWidth = .06f;
        [SerializeField] private float _rotation = 0;
        [SerializeField] private float _borderWidth = 0.01f;
        [SerializeField] private float _borderSpacing = 0.01f;
        [SerializeField] private float _removeBorder = 1.0f;
        [SerializeField] private bool _overlayNoiseEnabled = false;
        [SerializeField] private float _overlayNoiseScale = 100;
        [SerializeField] private float _overlayNoiseStrength = 0.5f;
        [SerializeField] private Vector2 _overlayNoiseOffset = new Vector2();
        [SerializeField] private bool _emptyNoiseEnabled = false;
        [SerializeField] private float _emptyNoiseScale = 100;
        [SerializeField] private float _emptyNoiseStrength = 0.5f;
        [SerializeField] private Vector2 _emptyNoiseOffset = new Vector2();
        [SerializeField] private bool _contentNoiseEnabled = false;
        [SerializeField] private float _contentNoiseScale = 100;
        [SerializeField] private float _contentNoiseStrength = 0.5f;
        [SerializeField] private Vector2 _contentNoiseOffset = new Vector2();
        [SerializeField] private bool _pulsateWhenLow = true;
        [SerializeField] private float _pulseSpeed = 10;
        [SerializeField] private float _pulseActivationThreshold = 0.25f;
        [SerializeField] private bool _overlayTextureEnabled = false;
        [SerializeField] private Texture2D _overlayTexture;
        [SerializeField] private float _overlayTextureOpacity = 1;
        [SerializeField] private Vector2 _overlayTextureTiling = new Vector2(1, 1);
        [SerializeField] private Vector2 _overlayTextureOffset = new Vector2();
        [SerializeField] private bool _innerTextureEnabled = false;
        [SerializeField] private Texture2D _innerTexture;
        [SerializeField] private bool _alignInnerTexture = true;
        [SerializeField] private bool _innerTextureScaleWithSegments = true;
        [SerializeField] private float _innerTextureOpacity = 1;
        [SerializeField] private Vector2 _innerTextureTiling = new Vector2(1, 1);
        [SerializeField] private Vector2 _innerTextureOffset = new Vector2();
        [SerializeField] private bool _borderTextureEnabled = false;
        [SerializeField] private Texture2D _borderTexture;
        [SerializeField] private bool _alignBorderTexture = true;
        [SerializeField] private bool _borderTextureScaleWithSegments = true;
        [SerializeField] private float _borderTextureOpacity = 1;
        [SerializeField] private Vector2 _borderTextureTiling = new Vector2(1, 1);
        [SerializeField] private Vector2 _borderTextureOffset = new Vector2();
        [SerializeField] private bool _emptyTextureEnabled = false;
        [SerializeField] private Texture2D _emptyTexture;
        [SerializeField] private bool _alignEmptyTexture = true;
        [SerializeField] private bool _emptyTextureScaleWithSegments = true;
        [SerializeField] private float _emptyTextureOpacity = 1;
        [SerializeField] private Vector2 _emptyTextureTiling = new Vector2(1, 1);
        [SerializeField] private Vector2 _emptyTextureOffset = new Vector2();
        [SerializeField] private bool _spaceTextureEnabled = false;
        [SerializeField] private Texture2D _spaceTexture;
        [SerializeField] private bool _alignSpaceTexture = true;
        [SerializeField] private float _spaceTextureOpacity = 1;
        [SerializeField] private Vector2 _spaceTextureTiling = new Vector2(1, 1);
        [SerializeField] private Vector2 _spaceTextureOffset = new Vector2();
        [SerializeField] private Gradient _innerGradient = new Gradient();
        [SerializeField] private bool _innerGradientEnabled = true;
        [SerializeField] private bool _valueAsGradientTimeInner = false;
        [SerializeField] private Gradient _emptyGradient = new Gradient();
        [SerializeField] private bool _emptyGradientEnabled = true;
        [SerializeField] private bool _valueAsGradientTimeEmpty = false;

        #endregion

        private Material currentMaterial;
        private SpriteRenderer spriteRenderer;
        private Image image;

        private bool materialAssigned = false;
        
        private static string BaseMaterialName {
            get {
                if (GraphicsSettings.renderPipelineAsset)
                    return "RadialSegmentedHealthBarMaterial";

                return "RadialSegmentedHealthBarMaterialBuiltIn";
            }
        }

        private const string MaterialName = "radialSegmentedHealthBarInstance";
        private const string PlaceholderSpriteName = "rshb_placeholderSprite";

        private SortedDictionary<string, IShaderProperty> properties = new SortedDictionary<string, IShaderProperty>();

        private void Awake() {
            if (Application.isPlaying)
                StatusBarsManager.AddHealthBar(this);

            InitProperties();

            GenerateRequiredComponents(true);
        }

        private void Update() {
            if (usingSpriteRenderer) {
                if (!spriteRenderer || spriteRenderer && (!spriteRenderer.sprite || !spriteRenderer.sharedMaterial || spriteRenderer.sharedMaterial.name != MaterialName)) {
                    GenerateRequiredComponents(false);
                }
            }
            else if (!image || image && (!image.material || image.material.name != MaterialName)) {
                GenerateRequiredComponents(false);
            }

            if (materialAssigned) {
#if UNITY_EDITOR
                if (oldParentName != parentName || oldHbName != hbName) {
                    StatusBarsManager.RemoveHealthBar(this, oldParentName, oldHbName, false);
                    StatusBarsManager.AddHealthBar(this);
                    oldParentName = parentName;
                    oldHbName = hbName;
                }
#endif
                ResetMaterialWithSerializedFields(false);
            }
        }

        private void GenerateRequiredComponents(bool useExisting) {
            spriteRenderer = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();
            Canvas parentCanvas = GetComponentInParent<Canvas>();
            //set up image
            if (useExisting && image || useExisting && parentCanvas || !useExisting && !usingSpriteRenderer) {
                usingSpriteRenderer = false;
                if (spriteRenderer) DestroyImmediate(spriteRenderer);
                GameObject go;
                if (!parentCanvas) {
                    go = new GameObject("Canvas", typeof(Canvas));
                    transform.parent = go.transform;
                }

                if (!image) {
                    gameObject.AddComponent(typeof(Image));
                    image = GetComponent<Image>();
                    //img.hideFlags = HideFlags.HideInInspector;
                }

                image.sprite = Resources.Load<Sprite>(PlaceholderSpriteName);
                AssignMaterial(image);
            }
            //set up sprite renderer
            else {
                usingSpriteRenderer = true;
                if (image) {
                    DestroyImmediate(image);
                    DestroyImmediate(GetComponent<CanvasRenderer>());
                }

                if (spriteRenderer == null) {
                    gameObject.AddComponent(typeof(SpriteRenderer));
                    spriteRenderer = GetComponent<SpriteRenderer>();
                    //sr.hideFlags = HideFlags.HideInInspector;
                }

                spriteRenderer.sprite = Resources.Load<Sprite>(PlaceholderSpriteName);
                AssignMaterial(spriteRenderer);
            }
        }

        private void InitProperties() {
            properties = new SortedDictionary<string, IShaderProperty>{
                ["OverlayColor"] = OverlayColor = new ShaderProperty<Color>("_OverlayColor", ColorPropertyFunc, _overlayColor),
                ["InnerColor"] = InnerColor = new ShaderProperty<Color>("_InnerColor", ColorPropertyFunc, _innerColor),
                ["BorderColor"] = BorderColor = new ShaderProperty<Color>("_BorderColor", ColorPropertyFunc, _borderColor),
                ["EmptyColor"] = EmptyColor = new ShaderProperty<Color>("_EmptyColor", ColorPropertyFunc, _emptyColor),
                ["SpaceColor"] = SpaceColor = new ShaderProperty<Color>("_SpaceColor", ColorPropertyFunc, _spaceColor),
                ["SegmentCount"] = SegmentCount = new ShaderProperty<float>("_SegmentCount", FloatPropertyFunc, _segmentCount),
                ["RemovedSegments"] = RemovedSegments = new ShaderProperty<float>("_RemoveSegments", FloatPropertyFunc, _removedSegments),
                ["Spacing"] = Spacing = new ShaderProperty<float>("_SegmentSpacing", FloatPropertyFunc, _spacing),
                ["Radius"] = Radius = new ShaderProperty<float>("_Radius", FloatPropertyFunc, _radius),
                ["LineWidth"] = LineWidth = new ShaderProperty<float>("_LineWidth", FloatPropertyFunc, _lineWidth),
                ["Rotation"] = Rotation = new ShaderProperty<float>("_Rotation", FloatPropertyFunc, _rotation),
                ["BorderWidth"] = BorderWidth = new ShaderProperty<float>("_BorderWidth", FloatPropertyFunc, _borderWidth),
                ["BorderSpacing"] = BorderSpacing = new ShaderProperty<float>("_BorderSpacing", FloatPropertyFunc, _borderSpacing),
                ["RemoveBorder"] = RemoveBorder = new ShaderProperty<float>("_RemoveBorder", FloatPropertyFunc, _removeBorder),
                ["OverlayNoiseEnabled"] = OverlayNoiseEnabled = new ShaderProperty<bool>("_OverlayNoiseEnabled", BoolPropertyFunc, _overlayNoiseEnabled),
                ["OverlayNoiseScale"] = OverlayNoiseScale = new ShaderProperty<float>("_OverlayNoiseScale", FloatPropertyFunc, _overlayNoiseScale),
                ["OverlayNoiseStrength"] = OverlayNoiseStrength = new ShaderProperty<float>("_OverlayNoiseStrength", FloatPropertyFunc, _overlayNoiseStrength),
                ["OverlayNoiseOffset"] = OverlayNoiseOffset = new ShaderProperty<Vector2>("_OverlayNoiseOffset", VectorPropertyFunc, _overlayNoiseOffset),
                ["EmptyNoiseEnabled"] = EmptyNoiseEnabled = new ShaderProperty<bool>("_EmptyNoiseEnabled", BoolPropertyFunc, _emptyNoiseEnabled),
                ["EmptyNoiseScale"] = EmptyNoiseScale = new ShaderProperty<float>("_EmptyNoiseScale", FloatPropertyFunc, _emptyNoiseScale),
                ["EmptyNoiseStrength"] = EmptyNoiseStrength = new ShaderProperty<float>("_EmptyNoiseStrength", FloatPropertyFunc, _emptyNoiseStrength),
                ["EmptyNoiseOffset"] = EmptyNoiseOffset = new ShaderProperty<Vector2>("_EmptyNoiseOffset", VectorPropertyFunc, _emptyNoiseOffset),
                ["ContentNoiseEnabled"] = ContentNoiseEnabled = new ShaderProperty<bool>("_ContentNoiseEnabled", BoolPropertyFunc, _contentNoiseEnabled),
                ["ContentNoiseScale"] = ContentNoiseScale = new ShaderProperty<float>("_ContentNoiseScale", FloatPropertyFunc, _contentNoiseScale),
                ["ContentNoiseStrength"] = ContentNoiseStrength = new ShaderProperty<float>("_ContentNoiseStrength", FloatPropertyFunc, _contentNoiseStrength),
                ["ContentNoiseOffset"] = ContentNoiseOffset = new ShaderProperty<Vector2>("_ContentNoiseOffset", VectorPropertyFunc, _contentNoiseOffset),
                ["PulsateWhenLow"] = PulsateWhenLow = new ShaderProperty<bool>("_PulsateWhenLow", BoolPropertyFunc, _pulsateWhenLow),
                ["PulseSpeed"] = PulseSpeed = new ShaderProperty<float>("_PulseSpeed", FloatPropertyFunc, _pulseSpeed),
                ["PulseActivationThreshold"] = PulseActivationThreshold = new ShaderProperty<float>("_PulseActivationThreshold", FloatPropertyFunc, _pulseActivationThreshold),
                ["OverlayTextureEnabled"] = OverlayTextureEnabled = new ShaderKeyword("OVERLAY_TEXTURE_ON", KeywordFunc, _overlayTextureEnabled),
                ["OverlayTexture"] = OverlayTexture = new ShaderProperty<Texture2D>("_OverlayTexture", TexturePropertyFunc, _overlayTexture),
                ["OverlayTextureOpacity"] = OverlayTextureOpacity = new ShaderProperty<float>("_OverlayTextureOpacity", FloatPropertyFunc, _overlayTextureOpacity),
                ["OverlayTextureTiling"] = OverlayTextureTiling = new ShaderProperty<Vector2>("_OverlayTextureTiling", VectorPropertyFunc, _overlayTextureTiling),
                ["OverlayTextureOffset"] = OverlayTextureOffset = new ShaderProperty<Vector2>("_OverlayTextureOffset", VectorPropertyFunc, _overlayTextureOffset),
                ["InnerTextureEnabled"] = InnerTextureEnabled = new ShaderKeyword("INNER_TEXTURE_ON", KeywordFunc, _innerTextureEnabled),
                ["InnerTexture"] = InnerTexture = new ShaderProperty<Texture2D>("_InnerTexture", TexturePropertyFunc, _innerTexture),
                ["AlignInnerTexture"] = AlignInnerTexture = new ShaderProperty<bool>("_AlignInnerTexture", BoolPropertyFunc, _alignInnerTexture),
                ["InnerTextureScaleWithSegments"] = InnerTextureScaleWithSegments = new ShaderProperty<bool>("_InnerTextureScaleWithSegments", BoolPropertyFunc, _innerTextureScaleWithSegments),
                ["InnerTextureOpacity"] = InnerTextureOpacity = new ShaderProperty<float>("_InnerTextureOpacity", FloatPropertyFunc, _innerTextureOpacity),
                ["InnerTextureTiling"] = InnerTextureTiling = new ShaderProperty<Vector2>("_InnerTextureTiling", VectorPropertyFunc, _innerTextureTiling),
                ["InnerTextureOffset"] = InnerTextureOffset = new ShaderProperty<Vector2>("_InnerTextureOffset", VectorPropertyFunc, _innerTextureOffset),
                ["BorderTextureEnabled"] = BorderTextureEnabled = new ShaderKeyword("BORDER_TEXTURE_ON", KeywordFunc, _borderTextureEnabled),
                ["BorderTexture"] = BorderTexture = new ShaderProperty<Texture2D>("_BorderTexture", TexturePropertyFunc, _borderTexture),
                ["AlignBorderTexture"] = AlignBorderTexture = new ShaderProperty<bool>("_AlignBorderTexture", BoolPropertyFunc, _alignBorderTexture),
                ["BorderTextureScaleWithSegments"] = BorderTextureScaleWithSegments = new ShaderProperty<bool>("_BorderTextureScaleWithSegments", BoolPropertyFunc, _borderTextureScaleWithSegments),
                ["BorderTextureOpacity"] = BorderTextureOpacity = new ShaderProperty<float>("_BorderTextureOpacity", FloatPropertyFunc, _borderTextureOpacity),
                ["BorderTextureTiling"] = BorderTextureTiling = new ShaderProperty<Vector2>("_BorderTextureTiling", VectorPropertyFunc, _borderTextureTiling),
                ["BorderTextureOffset"] = BorderTextureOffset = new ShaderProperty<Vector2>("_BorderTextureOffset", VectorPropertyFunc, _borderTextureOffset),
                ["EmptyTextureEnabled"] = EmptyTextureEnabled = new ShaderKeyword("EMPTY_TEXTURE_ON", KeywordFunc, _emptyTextureEnabled),
                ["EmptyTexture"] = EmptyTexture = new ShaderProperty<Texture2D>("_EmptyTexture", TexturePropertyFunc, _emptyTexture),
                ["AlignEmptyTexture"] = AlignEmptyTexture = new ShaderProperty<bool>("_AlignEmptyTexture", BoolPropertyFunc, _alignEmptyTexture),
                ["EmptyTextureScaleWithSegments"] = EmptyTextureScaleWithSegments = new ShaderProperty<bool>("_EmptyTextureScaleWithSegments", BoolPropertyFunc, _emptyTextureScaleWithSegments),
                ["EmptyTextureOpacity"] = EmptyTextureOpacity = new ShaderProperty<float>("_EmptyTextureOpacity", FloatPropertyFunc, _emptyTextureOpacity),
                ["EmptyTextureTiling"] = EmptyTextureTiling = new ShaderProperty<Vector2>("_EmptyTextureTiling", VectorPropertyFunc, _emptyTextureTiling),
                ["EmptyTextureOffset"] = EmptyTextureOffset = new ShaderProperty<Vector2>("_EmptyTextureOffset", VectorPropertyFunc, _emptyTextureOffset),
                ["SpaceTextureEnabled"] = SpaceTextureEnabled = new ShaderKeyword("SPACE_TEXTURE_ON", KeywordFunc, _spaceTextureEnabled),
                ["SpaceTexture"] = SpaceTexture = new ShaderProperty<Texture2D>("_SpaceTexture", TexturePropertyFunc, _spaceTexture),
                ["AlignSpaceTexture"] = AlignSpaceTexture = new ShaderProperty<bool>("_AlignSpaceTexture", BoolPropertyFunc, _alignSpaceTexture),
                ["SpaceTextureOpacity"] = SpaceTextureOpacity = new ShaderProperty<float>("_SpaceTextureOpacity", FloatPropertyFunc, _spaceTextureOpacity),
                ["SpaceTextureTiling"] = SpaceTextureTiling = new ShaderProperty<Vector2>("_SpaceTextureTiling", VectorPropertyFunc, _spaceTextureTiling),
                ["SpaceTextureOffset"] = SpaceTextureOffset = new ShaderProperty<Vector2>("_SpaceTextureOffset", VectorPropertyFunc, _spaceTextureOffset),
                ["InnerGradient"] = InnerGradient = new ShaderProperty<Gradient>("_InnerGradient", GradientPropertyFunc, _innerGradient),
                ["InnerGradientEnabled"] = InnerGradientEnabled = new ShaderProperty<bool>("_InnerGradientEnabled", BoolPropertyFunc, _innerGradientEnabled),
                ["ValueAsGradientTimeInner"] = ValueAsGradientTimeInner = new ShaderProperty<bool>("_ValueAsGradientTimeInner", BoolPropertyFunc, _valueAsGradientTimeInner),
                ["EmptyGradient"] = EmptyGradient = new ShaderProperty<Gradient>("_EmptyGradient", GradientPropertyFunc, _emptyGradient),
                ["EmptyGradientEnabled"] = EmptyGradientEnabled = new ShaderProperty<bool>("_EmptyGradientEnabled", BoolPropertyFunc, _emptyGradientEnabled),
                ["ValueAsGradientTimeEmpty"] = ValueAsGradientTimeEmpty = new ShaderProperty<bool>("_ValueAsGradientTimeEmpty", BoolPropertyFunc, _valueAsGradientTimeEmpty),
            };
        }

        public void ResetSerializedProperties() {
#if UNITY_EDITOR
            _overlayColor = Color.white;
            _innerColor = Color.red;
            _borderColor = Color.white;
            _emptyColor = Color.gray;
            _spaceColor = Color.clear;
            _segmentCount = 5;
            _removedSegments = 1;
            _spacing = 0.02f;
            _radius = .35f;
            _lineWidth = .06f;
            _rotation = 0;
            _borderWidth = 0.01f;
            _borderSpacing = 0.01f;
            _removeBorder = 1.0f;
            _overlayNoiseEnabled = false;
            _overlayNoiseScale = 100;
            _overlayNoiseStrength = 0.5f;
            _overlayNoiseOffset = new Vector2();
            _emptyNoiseEnabled = false;
            _emptyNoiseScale = 100;
            _emptyNoiseStrength = 0.5f;
            _emptyNoiseOffset = new Vector2();
            _contentNoiseEnabled = false;
            _contentNoiseScale = 100;
            _contentNoiseStrength = 0.5f;
            _contentNoiseOffset = new Vector2();
            _pulsateWhenLow = true;
            _pulseSpeed = 10;
            _pulseActivationThreshold = 0.25f;
            _overlayTextureEnabled = false;

            _overlayTextureOpacity = 1;
            _overlayTextureTiling = new Vector2(1, 1);
            _overlayTextureOffset = new Vector2();
            _innerTextureEnabled = false;

            _alignInnerTexture = true;
            _innerTextureScaleWithSegments = true;
            _innerTextureOpacity = 1;
            _innerTextureTiling = new Vector2(1, 1);
            _innerTextureOffset = new Vector2();
            _borderTextureEnabled = false;

            _alignBorderTexture = true;
            _borderTextureScaleWithSegments = true;
            _borderTextureOpacity = 1;
            _borderTextureTiling = new Vector2(1, 1);
            _borderTextureOffset = new Vector2();
            _emptyTextureEnabled = false;

            _alignEmptyTexture = true;
            _emptyTextureScaleWithSegments = true;
            _emptyTextureOpacity = 1;
            _emptyTextureTiling = new Vector2(1, 1);
            _emptyTextureOffset = new Vector2();
            _spaceTextureEnabled = false;

            _alignSpaceTexture = true;
            _spaceTextureOpacity = 1;
            _spaceTextureTiling = new Vector2(1, 1);
            _spaceTextureOffset = new Vector2();

            _innerGradientEnabled = true;
            _valueAsGradientTimeInner = false;
            _emptyGradientEnabled = true;
            _valueAsGradientTimeEmpty = false;

            ResetMaterialWithSerializedFields(false);
#endif
        }

        public bool GetShaderProperty<T>(string propertyName, out ShaderProperty<T> shaderProperty) {
            if (properties[propertyName] is ShaderProperty<T> p) {
                shaderProperty = p;
                return true;
            }

            shaderProperty = null;
            return false;
        }

        public bool GetShaderKeyword(string propertyName, out ShaderKeyword shaderKeyword) {
            if (properties[propertyName] is ShaderKeyword p) {
                shaderKeyword = p;
                return true;
            }

            shaderKeyword = null;
            return false;
        }

        public bool GetShaderPropertyValue<T>(string propertyName, out T value) {
            if (properties[propertyName] is ShaderProperty<T> p) {
                value = p.Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool SetShaderPropertyValue<T>(string propertyName, T value) {
            if (properties[propertyName] is ShaderProperty<T> p) {
                p.Value = value;
                return true;
            }

            return false;
        }

        public bool GetShaderKeywordValue(string propertyName, out bool value) {
            if (properties[propertyName] is ShaderKeyword p) {
                value = p.Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool SetShaderKeywordValue(string propertyName, bool value) {
            if (properties[propertyName] is ShaderKeyword p) {
                p.Value = value;
                return true;
            }

            return false;
        }

        //property functions
        private bool BoolPropertyFunc(int id, bool setInShader, bool value) {
            if (materialAssigned && !setInShader)
                return Convert.ToBoolean(currentMaterial.GetFloat(id));
            if (materialAssigned && setInShader) {
                currentMaterial.SetFloat(id, value ? 1 : 0);
            }

            return default;
        }

        private float FloatPropertyFunc(int id, bool setInShader, float value) {
            if (materialAssigned && !setInShader)
                return currentMaterial.GetFloat(id);
            if (materialAssigned && setInShader) {
                currentMaterial.SetFloat(id, value);
            }

            return default;
        }

        private Color ColorPropertyFunc(int id, bool setInShader, Color value) {
            if (materialAssigned && !setInShader)
                return currentMaterial.GetColor(id);
            if (materialAssigned && setInShader) {
                currentMaterial.SetColor(id, value);
            }

            return default;
        }

        private Vector2 VectorPropertyFunc(int id, bool setInShader, Vector2 value) {
            if (materialAssigned && !setInShader)
                return currentMaterial.GetVector(id);
            if (materialAssigned && setInShader) {
                currentMaterial.SetVector(id, value);
            }

            return default;
        }

        private Texture2D TexturePropertyFunc(int id, bool setInShader, Texture2D value) {
            if (materialAssigned && !setInShader)
                return (Texture2D) currentMaterial.GetTexture(id);
            if (materialAssigned && setInShader) {
                currentMaterial.SetTexture(id, value);
            }

            return default;
        }
        
        private Gradient GradientPropertyFunc(int id, bool setInShader, Gradient value) {
            if (materialAssigned && !setInShader)
                return value;
            if (materialAssigned && setInShader) {
                currentMaterial.SetTexture(id, value.ToTexture2D());
            }

            return default;
        }

        private bool KeywordFunc(string id, bool setInShader, bool value) {
            if (materialAssigned && !setInShader) return currentMaterial.IsKeywordEnabled(id);
            if (materialAssigned && setInShader && value) currentMaterial.EnableKeyword(id);
            else if (materialAssigned && setInShader && !value) currentMaterial.DisableKeyword(id);
            return false;
        }

        void AssignMaterial(Image r) {
            //get material
            Material mat = Resources.Load<Material>(BaseMaterialName);

            if (mat != null && r != null) {
                //generate and apply the material
                currentMaterial = new Material(mat);
                currentMaterial.name = MaterialName;
                r.material = currentMaterial;
                materialAssigned = true;
                ResetMaterial(true);
#if UNITY_EDITOR
                //the scene needs to be saved
                if (!Application.isPlaying)
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
#endif
            }
            else {
                //something went wrong, remove the component
                Debug.LogError("RadialSegmentedHealthBar: Something went wrong.");
                DestroyImmediate(this);
            }
        }

        void AssignMaterial(SpriteRenderer r) {
            //get resources
            Material mat = Resources.Load<Material>(BaseMaterialName);
            Sprite sprite = Resources.Load<Sprite>(PlaceholderSpriteName);

            if (mat != null && r != null) {
                //make sure the sprite will render the shader correctly
                if (r.sprite == null && sprite != null) {
                    r.sprite = sprite;
                }

                r.drawMode = SpriteDrawMode.Simple;

                //generate and apply the material
                currentMaterial = new Material(mat);
                currentMaterial.name = MaterialName;
                r.sharedMaterial = currentMaterial;
                materialAssigned = true;
                ResetMaterial(true);
#if UNITY_EDITOR
                //the scene needs to be saved
                if (!Application.isPlaying)
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
#endif
            }
            else {
                //something went wrong, remove the component
                Debug.LogError("RadialSegmentedHealthBar: Something went wrong.");
                DestroyImmediate(this);
            }
        }

        void ResetMaterial(bool ignoreDirty) {
            foreach (var property in properties) {
                property.Value.ApplyToShader(ignoreDirty);
            }
        }

        void ResetMaterialWithSerializedFields(bool ignoreDirty) {
            if (OverlayColor == null) {
                InitProperties();
            }

            OverlayColor.ApplyValueToShader(ignoreDirty, ref _overlayColor);
            InnerColor.ApplyValueToShader(ignoreDirty, ref _innerColor);
            BorderColor.ApplyValueToShader(ignoreDirty, ref _borderColor);
            EmptyColor.ApplyValueToShader(ignoreDirty, ref _emptyColor);
            SpaceColor.ApplyValueToShader(ignoreDirty, ref _spaceColor);
            SegmentCount.ApplyValueToShader(ignoreDirty, ref _segmentCount);
            RemovedSegments.ApplyValueToShader(ignoreDirty, ref _removedSegments);
            Spacing.ApplyValueToShader(ignoreDirty, ref _spacing);
            Radius.ApplyValueToShader(ignoreDirty, ref _radius);
            LineWidth.ApplyValueToShader(ignoreDirty, ref _lineWidth);
            Rotation.ApplyValueToShader(ignoreDirty, ref _rotation);
            BorderWidth.ApplyValueToShader(ignoreDirty, ref _borderWidth);
            BorderSpacing.ApplyValueToShader(ignoreDirty, ref _borderSpacing);
            RemoveBorder.ApplyValueToShader(ignoreDirty, ref _removeBorder);
            OverlayNoiseEnabled.ApplyValueToShader(ignoreDirty, ref _overlayNoiseEnabled);
            OverlayNoiseScale.ApplyValueToShader(ignoreDirty, ref _overlayNoiseScale);
            OverlayNoiseStrength.ApplyValueToShader(ignoreDirty, ref _overlayNoiseStrength);
            OverlayNoiseOffset.ApplyValueToShader(ignoreDirty, ref _overlayNoiseOffset);
            EmptyNoiseEnabled.ApplyValueToShader(ignoreDirty, ref _emptyNoiseEnabled);
            EmptyNoiseScale.ApplyValueToShader(ignoreDirty, ref _emptyNoiseScale);
            EmptyNoiseStrength.ApplyValueToShader(ignoreDirty, ref _emptyNoiseStrength);
            EmptyNoiseOffset.ApplyValueToShader(ignoreDirty, ref _emptyNoiseOffset);
            ContentNoiseEnabled.ApplyValueToShader(ignoreDirty, ref _contentNoiseEnabled);
            ContentNoiseScale.ApplyValueToShader(ignoreDirty, ref _contentNoiseScale);
            ContentNoiseStrength.ApplyValueToShader(ignoreDirty, ref _contentNoiseStrength);
            ContentNoiseOffset.ApplyValueToShader(ignoreDirty, ref _contentNoiseOffset);
            PulsateWhenLow.ApplyValueToShader(ignoreDirty, ref _pulsateWhenLow);
            PulseSpeed.ApplyValueToShader(ignoreDirty, ref _pulseSpeed);
            PulseActivationThreshold.ApplyValueToShader(ignoreDirty, ref _pulseActivationThreshold);
            OverlayTextureEnabled.ApplyValueToShader(ignoreDirty, ref _overlayTextureEnabled);
            OverlayTexture.ApplyValueToShader(ignoreDirty, ref _overlayTexture);
            OverlayTextureOpacity.ApplyValueToShader(ignoreDirty, ref _overlayTextureOpacity);
            OverlayTextureTiling.ApplyValueToShader(ignoreDirty, ref _overlayTextureTiling);
            OverlayTextureOffset.ApplyValueToShader(ignoreDirty, ref _overlayTextureOffset);
            InnerTextureEnabled.ApplyValueToShader(ignoreDirty, ref _innerTextureEnabled);
            InnerTexture.ApplyValueToShader(ignoreDirty, ref _innerTexture);
            AlignInnerTexture.ApplyValueToShader(ignoreDirty, ref _alignInnerTexture);
            InnerTextureScaleWithSegments.ApplyValueToShader(ignoreDirty, ref _innerTextureScaleWithSegments);
            InnerTextureOpacity.ApplyValueToShader(ignoreDirty, ref _innerTextureOpacity);
            InnerTextureTiling.ApplyValueToShader(ignoreDirty, ref _innerTextureTiling);
            InnerTextureOffset.ApplyValueToShader(ignoreDirty, ref _innerTextureOffset);
            BorderTextureEnabled.ApplyValueToShader(ignoreDirty, ref _borderTextureEnabled);
            BorderTexture.ApplyValueToShader(ignoreDirty, ref _borderTexture);
            AlignBorderTexture.ApplyValueToShader(ignoreDirty, ref _alignBorderTexture);
            BorderTextureScaleWithSegments.ApplyValueToShader(ignoreDirty, ref _borderTextureScaleWithSegments);
            BorderTextureOpacity.ApplyValueToShader(ignoreDirty, ref _borderTextureOpacity);
            BorderTextureTiling.ApplyValueToShader(ignoreDirty, ref _borderTextureTiling);
            BorderTextureOffset.ApplyValueToShader(ignoreDirty, ref _borderTextureOffset);
            EmptyTextureEnabled.ApplyValueToShader(ignoreDirty, ref _emptyTextureEnabled);
            EmptyTexture.ApplyValueToShader(ignoreDirty, ref _emptyTexture);
            AlignEmptyTexture.ApplyValueToShader(ignoreDirty, ref _alignEmptyTexture);
            EmptyTextureScaleWithSegments.ApplyValueToShader(ignoreDirty, ref _emptyTextureScaleWithSegments);
            EmptyTextureOpacity.ApplyValueToShader(ignoreDirty, ref _emptyTextureOpacity);
            EmptyTextureTiling.ApplyValueToShader(ignoreDirty, ref _emptyTextureTiling);
            EmptyTextureOffset.ApplyValueToShader(ignoreDirty, ref _emptyTextureOffset);
            SpaceTextureEnabled.ApplyValueToShader(ignoreDirty, ref _spaceTextureEnabled);
            SpaceTexture.ApplyValueToShader(ignoreDirty, ref _spaceTexture);
            AlignSpaceTexture.ApplyValueToShader(ignoreDirty, ref _alignSpaceTexture);
            SpaceTextureOpacity.ApplyValueToShader(ignoreDirty, ref _spaceTextureOpacity);
            SpaceTextureTiling.ApplyValueToShader(ignoreDirty, ref _spaceTextureTiling);
            SpaceTextureOffset.ApplyValueToShader(ignoreDirty, ref _spaceTextureOffset);
            InnerGradient.ApplyValueToShader(ignoreDirty, ref _innerGradient);
            InnerGradientEnabled.ApplyValueToShader(ignoreDirty, ref _innerGradientEnabled);
            ValueAsGradientTimeInner.ApplyValueToShader(ignoreDirty, ref _valueAsGradientTimeInner);
            EmptyGradient.ApplyValueToShader(ignoreDirty, ref _emptyGradient);
            EmptyGradientEnabled.ApplyValueToShader(ignoreDirty, ref _emptyGradientEnabled);
            ValueAsGradientTimeEmpty.ApplyValueToShader(ignoreDirty, ref _valueAsGradientTimeEmpty);
        }

        void ResetPublicFields() {
            foreach (var property in properties) {
                property.Value.Reset();
            }
        }

        public void SetSegmentCount(float value) {
            SegmentCount.Value = Mathf.Max(0, value);
        }

        public void SetRemovedSegments(float value) {
            RemovedSegments.Value = Mathf.Clamp(value, 0, SegmentCount.Value);
        }

        public void SetPercent(float value) {
            float cVal = Mathf.Clamp(value, 0, 1);
            RemovedSegments.Value = (1 - cVal) * SegmentCount.Value;
        }

        public void AddRemoveSegments(float value) {
            RemovedSegments.Value += value;
            RemovedSegments.Value = Mathf.Clamp(RemovedSegments.Value, 0, SegmentCount.Value);
        }

        public void AddRemovePercent(float value) {
            RemovedSegments.Value += value * SegmentCount.Value;
            RemovedSegments.Value = Mathf.Clamp(RemovedSegments.Value, 0, SegmentCount.Value);
        }

        public string GetParentName() {
            return parentName;
        }

        public string GetName() {
            return hbName;
        }
    }
}