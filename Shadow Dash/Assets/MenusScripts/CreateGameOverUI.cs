using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class CreateGameOverUI : EditorWindow
{
    [MenuItem("Tools/Create Game Over UI")]
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
        GameObject bg = CreatePanel("GameOverUI", canvas.transform,
            Vector2.zero, Vector2.one,
            Vector2.zero, Vector2.zero,
            new Color(0.02f, 0.01f, 0.02f, 1f));

        // ── Vignette ──
        CreatePanel("Vignette", bg.transform,
            Vector2.zero, Vector2.one,
            Vector2.zero, Vector2.zero,
            new Color(0f, 0f, 0f, 0.55f));

        // ── Trait rouge vertical ── ambiance mort
        GameObject ray = CreatePanel("BloodRay", bg.transform,
            new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
            Vector2.zero, Vector2.zero,
            new Color(0.5f, 0.04f, 0.04f, 0.3f));
        RectTransform rayRt = ray.GetComponent<RectTransform>();
        rayRt.anchoredPosition = Vector2.zero;
        rayRt.sizeDelta = new Vector2(2f, 260f);
        rayRt.pivot = new Vector2(0.5f, 1f);

        // ── Panel central ──
        GameObject panel = new GameObject("Panel", typeof(RectTransform));
        panel.transform.SetParent(bg.transform, false);
        RectTransform prt = panel.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0.5f, 0.5f);
        prt.anchorMax = new Vector2(0.5f, 0.5f);
        prt.anchoredPosition = Vector2.zero;
        prt.sizeDelta = new Vector2(360f, 400f);

        // Ornement haut
        CreateTMPAuto("OrnamentTop", panel.transform,
            new Vector2(0f, 0.92f), new Vector2(1f, 0.98f),
            "- * -",
            new Color(0.35f, 0.063f, 0.063f), FontStyles.Normal, 9, 13);

        // ── TitleBlock ──
        GameObject titleBlock = CreatePanel("TitleBlock", panel.transform,
            new Vector2(0f, 0.70f), new Vector2(1f, 0.90f),
            Vector2.zero, Vector2.zero,
            new Color(0f, 0f, 0f, 0f));

        CreatePanel("BorderTop", titleBlock.transform,
            new Vector2(0f, 1f), new Vector2(1f, 1f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.35f, 0.063f, 0.063f, 1f));

        CreatePanel("BorderBottom", titleBlock.transform,
            new Vector2(0f, 0f), new Vector2(1f, 0f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.35f, 0.063f, 0.063f, 1f));

        // Titre
        CreateTMPAuto("GameOverTitle", titleBlock.transform,
            new Vector2(0.05f, 0.52f), new Vector2(0.95f, 0.95f),
            "YOU DIED",
            new Color(0.72f, 0.094f, 0.094f), FontStyles.Bold, 22, 38);

        // Sous-titre
        CreateTMPAuto("Subtitle", titleBlock.transform,
            new Vector2(0.05f, 0.05f), new Vector2(0.95f, 0.48f),
            "The darkness has claimed your soul...",
            new Color(0.29f, 0.094f, 0.094f), FontStyles.Italic, 9, 14);

        // ── Divider haut ──
        CreatePanel("DividerTop", panel.transform,
            new Vector2(0.05f, 0.675f), new Vector2(0.95f, 0.675f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.25f, 0.047f, 0.047f, 1f));

        // ── Boutons ──
        CreateMenuButton("BtnRetry", panel.transform, 0.555f, 0.455f, "Try Again", new Color(0.72f, 0.094f, 0.094f));
        CreateMenuButton("BtnMainMenu", panel.transform, 0.435f, 0.335f, "Main Menu", new Color(0.545f, 0.188f, 0.063f));
        CreateMenuButton("BtnQuit", panel.transform, 0.275f, 0.175f, "Quit", new Color(0.29f, 0.094f, 0.094f));

        // ── Divider bas ──
        CreatePanel("DividerBottom", panel.transform,
            new Vector2(0.05f, 0.155f), new Vector2(0.95f, 0.155f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.25f, 0.047f, 0.047f, 1f));

        // ── Ornement bas ──
        CreateTMPAuto("OrnamentBottom", panel.transform,
            new Vector2(0f, 0.07f), new Vector2(1f, 0.135f),
            "* * *",
            new Color(0.25f, 0.047f, 0.047f), FontStyles.Normal, 9, 13);

        Debug.Log("Game Over UI created! Attach GameOver.cs and connect the buttons.");
        Selection.activeGameObject = bg;
    }

    // ─────────── Button ───────────
    static void CreateMenuButton(string name, Transform parent,
        float anchorYMax, float anchorYMin,
        string label, Color accentColor)
    {
        GameObject container = CreatePanel(name + "_Container", parent,
            new Vector2(0f, anchorYMin), new Vector2(1f, anchorYMax),
            new Vector2(0f, 2f), new Vector2(0f, -2f),
            new Color(0f, 0f, 0f, 0f));

        // Bande gauche colorée
        GameObject lb = new GameObject("LeftAccent", typeof(RectTransform), typeof(Image));
        lb.transform.SetParent(container.transform, false);
        RectTransform lbrt = lb.GetComponent<RectTransform>();
        lbrt.anchorMin = new Vector2(0f, 0f);
        lbrt.anchorMax = new Vector2(0f, 1f);
        lbrt.pivot = new Vector2(0f, 0.5f);
        lbrt.anchoredPosition = Vector2.zero;
        lbrt.sizeDelta = new Vector2(3f, 0f);
        lb.GetComponent<Image>().color = accentColor;

        // Fond bouton
        GameObject btn = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        btn.transform.SetParent(container.transform, false);
        RectTransform brt = btn.GetComponent<RectTransform>();
        brt.anchorMin = Vector2.zero;
        brt.anchorMax = Vector2.one;
        brt.offsetMin = new Vector2(3f, 0f);
        brt.offsetMax = Vector2.zero;
        btn.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.02f);

        Outline outline = btn.AddComponent<Outline>();
        outline.effectColor = new Color(0.25f, 0.047f, 0.047f, 1f);
        outline.effectDistance = new Vector2(1f, -1f);

        // Texte
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
        tmp.enableWordWrapping = false;
        tmp.enableAutoSizing = true;
        tmp.fontSizeMin = 9f;
        tmp.fontSizeMax = 15f;
        tmp.characterSpacing = 3f;
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
        tmp.enableWordWrapping = true;
        tmp.enableAutoSizing = true;
        tmp.fontSizeMin = minSize;
        tmp.fontSizeMax = maxSize;
    }
}