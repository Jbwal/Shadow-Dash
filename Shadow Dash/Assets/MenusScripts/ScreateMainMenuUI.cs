using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class CreateMainMenuUI : EditorWindow
{
    [MenuItem("Tools/Create Main Menu UI")]
    public static void CreateUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) { Debug.LogError("No Canvas found!"); return; }

        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
        if (scaler == null) scaler = canvas.gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 720);
        scaler.matchWidthOrHeight = 0.5f;

        // ── Background plein écran ──
        GameObject bg = CreatePanel("Background", canvas.transform,
            Vector2.zero, Vector2.one,
            Vector2.zero, Vector2.zero,
            new Color(0.031f, 0.023f, 0.031f, 1f));

        // ── Vignette overlay ──
        CreatePanel("Vignette", bg.transform,
            Vector2.zero, Vector2.one,
            Vector2.zero, Vector2.zero,
            new Color(0f, 0f, 0f, 0.45f));

        // ── LightRay — trait doré fin en haut au centre ──
        GameObject ray = CreatePanel("LightRay", bg.transform,
            new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
            Vector2.zero, Vector2.zero,
            new Color(0.78f, 0.59f, 0.047f, 0.25f));
        RectTransform rayRt = ray.GetComponent<RectTransform>();
        rayRt.anchoredPosition = new Vector2(0f, 0f);
        rayRt.sizeDelta = new Vector2(2f, 200f);
        rayRt.pivot = new Vector2(0.5f, 1f);

        // ── Panel central ──
        GameObject panel = new GameObject("MenuPanel", typeof(RectTransform));
        panel.transform.SetParent(bg.transform, false);
        RectTransform prt = panel.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0.5f, 0.5f);
        prt.anchorMax = new Vector2(0.5f, 0.5f);
        prt.anchoredPosition = Vector2.zero;
        prt.sizeDelta = new Vector2(360f, 420f);

        // Ornement haut
        CreateTMPAuto("OrnamentTop", panel.transform,
            new Vector2(0f, 0.92f), new Vector2(1f, 0.98f),
            "- * -",
            new Color(0.23f, 0.125f, 0.016f), FontStyles.Normal, 9, 13);

        // ── TitleBlock ──
        GameObject titleBlock = CreatePanel("TitleBlock", panel.transform,
            new Vector2(0f, 0.70f), new Vector2(1f, 0.90f),
            Vector2.zero, Vector2.zero,
            new Color(0f, 0f, 0f, 0f));

        // Bordure haut du titre
        CreatePanel("BorderTop", titleBlock.transform,
            new Vector2(0f, 1f), new Vector2(1f, 1f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.165f, 0.094f, 0.016f, 1f));

        // Bordure bas du titre
        CreatePanel("BorderBottom", titleBlock.transform,
            new Vector2(0f, 0f), new Vector2(1f, 0f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.165f, 0.094f, 0.016f, 1f));

        // Coins décoratifs
        AddCorner("CornerTL", titleBlock.transform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, -8f), true, true);
        AddCorner("CornerTR", titleBlock.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-8f, -8f), false, true);
        AddCorner("CornerBL", titleBlock.transform, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), true, false);
        AddCorner("CornerBR", titleBlock.transform, new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-8f, 0f), false, false);

        // Titre
        CreateTMPAuto("GameTitle", titleBlock.transform,
            new Vector2(0.05f, 0.52f), new Vector2(0.95f, 0.95f),
            "SHADOW DASH",
            new Color(0.78f, 0.565f, 0.125f), FontStyles.Bold, 20, 32);

        // Sous-titre
        CreateTMPAuto("Subtitle", titleBlock.transform,
            new Vector2(0.05f, 0.05f), new Vector2(0.95f, 0.48f),
            "The light fades. The darkness remains.",
            new Color(0.29f, 0.18f, 0.063f), FontStyles.Italic, 9, 14);

        // ── Divider haut ──
        CreatePanel("DividerTop", panel.transform,
            new Vector2(0.05f, 0.675f), new Vector2(0.95f, 0.675f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.165f, 0.094f, 0.016f, 1f));

        // ── Boutons ──
        CreateMenuButton("BtnNewGame", panel.transform, 0.555f, 0.465f, "New Game", new Color(0.78f, 0.565f, 0.125f), false);
        CreateMenuButton("BtnContinue", panel.transform, 0.445f, 0.355f, "Continue", new Color(0.545f, 0.345f, 0.125f), false);
        CreateMenuButton("BtnQuit", panel.transform, 0.295f, 0.205f, "Quit", new Color(0.416f, 0.125f, 0.063f), true);

        // ── Divider bas ──
        CreatePanel("DividerBottom", panel.transform,
            new Vector2(0.05f, 0.185f), new Vector2(0.95f, 0.185f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.165f, 0.094f, 0.016f, 1f));

        // ── Ornement bas ──
        CreateTMPAuto("OrnamentBottom", panel.transform,
            new Vector2(0f, 0.09f), new Vector2(1f, 0.165f),
            "* * *",
            new Color(0.165f, 0.094f, 0.016f), FontStyles.Normal, 9, 13);

        // ── Version ──
        CreateTMPAuto("Version", panel.transform,
            new Vector2(0f, 0.01f), new Vector2(1f, 0.08f),
            "v0.1  —  Early Build",
            new Color(0.165f, 0.094f, 0.016f), FontStyles.Italic, 8, 12);

        Debug.Log("Main Menu UI created! Attach MainMenu.cs to a GameObject and connect the buttons.");
        Selection.activeGameObject = bg;
    }

    // ─────────── Menu Button ───────────
    static void CreateMenuButton(string name, Transform parent,
        float anchorYMax, float anchorYMin,
        string label, Color accentColor, bool isDanger)
    {
        // Bordure gauche colorée via double panel
        GameObject btnBorder = CreatePanel(name + "_LeftBorder", parent,
            new Vector2(0f, anchorYMin), new Vector2(1f, anchorYMax),
            new Vector2(0f, 2f), new Vector2(0f, -2f),
            new Color(0f, 0f, 0f, 0f));

        GameObject lb = new GameObject("LeftAccent", typeof(RectTransform), typeof(Image));
        lb.transform.SetParent(btnBorder.transform, false);
        RectTransform lbrt = lb.GetComponent<RectTransform>();
        lbrt.anchorMin = new Vector2(0f, 0f);
        lbrt.anchorMax = new Vector2(0f, 1f);
        lbrt.pivot = new Vector2(0f, 0.5f);
        lbrt.anchoredPosition = Vector2.zero;
        lbrt.sizeDelta = new Vector2(3f, 0f);
        lb.GetComponent<Image>().color = accentColor;

        // Fond du bouton
        GameObject btn = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        btn.transform.SetParent(btnBorder.transform, false);
        RectTransform brt = btn.GetComponent<RectTransform>();
        brt.anchorMin = Vector2.zero;
        brt.anchorMax = Vector2.one;
        brt.offsetMin = new Vector2(3f, 0f);
        brt.offsetMax = new Vector2(0f, 0f);
        btn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.02f);

        // Bordure du bouton
        Outline outline = btn.AddComponent<Outline>();
        outline.effectColor = new Color(0.165f, 0.094f, 0.016f, 1f);
        outline.effectDistance = new Vector2(1f, -1f);

        // Texte du bouton
        GameObject txtGo = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
        txtGo.transform.SetParent(btn.transform, false);
        RectTransform trt = txtGo.GetComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.offsetMin = Vector2.zero;
        trt.offsetMax = Vector2.zero;
        TextMeshProUGUI tmp = txtGo.GetComponent<TextMeshProUGUI>();
        tmp.text = label.ToUpper();
        tmp.color = accentColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;
        tmp.textWrappingMode = TextWrappingModes.NoWrap;
        tmp.enableAutoSizing = true;
        tmp.fontSizeMin = 9f;
        tmp.fontSizeMax = 15f;
        tmp.characterSpacing = 3f;
    }

    // ─────────── Corner decoration ───────────
    static void AddCorner(string name, Transform parent,
        Vector2 anchorMin, Vector2 anchorMax,
        Vector2 pos, bool leftSide, bool topSide)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = new Vector2(leftSide ? 0f : 1f, topSide ? 1f : 0f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(8f, 8f);
        go.GetComponent<Image>().color = new Color(0.353f, 0.188f, 0.031f, 1f);
    }

    // ─────────── Helpers ───────────
    static GameObject CreatePanel(string name, Transform parent,
        Vector2 anchorMin, Vector2 anchorMax,
        Vector2 offsetMin, Vector2 offsetMax, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = offsetMin;
        rt.offsetMax = offsetMax;
        go.GetComponent<Image>().color = color;
        return go;
    }

    static void CreateTMPAuto(string name, Transform parent,
        Vector2 anchorMin, Vector2 anchorMax,
        string text, Color color, FontStyles style,
        float minSize, float maxSize)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.color = color;
        tmp.fontStyle = style;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.textWrappingMode = TextWrappingModes.Normal;
        tmp.enableAutoSizing = true;
        tmp.fontSizeMin = minSize;
        tmp.fontSizeMax = maxSize;
    }
}