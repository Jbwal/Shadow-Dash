using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class CreateShopUI : EditorWindow
{
    [MenuItem("Tools/Create Shop UI")]
    public static void CreateUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) { Debug.LogError("No Canvas found!"); return; }

        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
        if (scaler == null) scaler = canvas.gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 720);
        scaler.matchWidthOrHeight = 0.5f;

        // ShopUI fullscreen overlay
        GameObject shopUI = CreatePanel("ShopUI", canvas.transform,
            Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero,
            new Color(0.03f, 0.02f, 0.04f, 0.93f));
        shopUI.SetActive(false);

        // ShopWindow — double panel pour une bordure qui scale
        GameObject win = WrapWithBorder("ShopWindow", shopUI.transform,
            new Vector2(0.2f, 0.05f), new Vector2(0.8f, 0.97f),
            Vector2.zero, Vector2.zero,
            new Color(0.36f, 0.23f, 0.063f, 1f),
            new Color(0.063f, 0.047f, 0.031f, 1f));

        CreatePanel("InnerBorder", win.transform,
            Vector2.zero, Vector2.one,
            new Vector2(4f, 4f), new Vector2(-4f, -4f),
            new Color(0.83f, 0.63f, 0.25f, 0.06f));

        // ── Banner (top 20%) ──
        GameObject banner = CreatePanel("Banner", win.transform,
            new Vector2(0f, 0.80f), new Vector2(1f, 1f),
            Vector2.zero, Vector2.zero,
            new Color(0.10f, 0.06f, 0.016f, 1f));

        CreatePanel("BannerLine", banner.transform,
            new Vector2(0f, 0f), new Vector2(1f, 0f),
            new Vector2(20f, 0f), new Vector2(-20f, 1f),
            new Color(0.72f, 0.53f, 0.047f, 1f));

        CreateTMPAuto("ShopName", banner.transform,
            new Vector2(0.05f, 0.52f), new Vector2(0.95f, 0.92f),
            Vector2.zero, Vector2.zero,
            "THE CURSED ANVIL",
            new Color(0.83f, 0.63f, 0.25f), FontStyles.Bold, 20, 36);

        CreateTMPAuto("Tagline", banner.transform,
            new Vector2(0.05f, 0.05f), new Vector2(0.95f, 0.45f),
            Vector2.zero, Vector2.zero,
            "Between Light and Darkness, choose your fate...",
            new Color(0.35f, 0.24f, 0.125f), FontStyles.Italic, 10, 18);

        // ── Coin row (78%) ──
        CreateTMPAuto("CoinText", win.transform,
            new Vector2(0.1f, 0.73f), new Vector2(0.9f, 0.79f),
            Vector2.zero, Vector2.zero,
            "~ 0  Gold Coins ~",
            new Color(0.78f, 0.59f, 0.047f), FontStyles.Normal, 12, 20);

        // ── Dividers ──
        CreatePanel("DividerTop", win.transform,
            new Vector2(0.05f, 0.725f), new Vector2(0.95f, 0.725f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.29f, 0.18f, 0.031f, 1f));

        CreatePanel("DividerBottom", win.transform,
            new Vector2(0.05f, 0.09f), new Vector2(0.95f, 0.09f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.29f, 0.18f, 0.031f, 1f));

        // ── 3 cards equally spaced ──
        CreateCard("CardLight", win.transform, 0.710f, 0.510f, "Celestial Grace", "Defy the heavens - jump twice", "5", new Color(0.78f, 0.59f, 0.05f), new Color(0.09f, 0.07f, 0.04f));
        CreateCard("CardDark", win.transform, 0.500f, 0.300f, "Ancient Blood", "Darkness strengthens your flesh", "3", new Color(0.56f, 0.25f, 0.87f), new Color(0.07f, 0.04f, 0.09f));
        CreateCard("CardShadow", win.transform, 0.290f, 0.100f, "Wraith's Step", "Glide between light and shadow", "3", new Color(0.78f, 0.59f, 0.05f), new Color(0.08f, 0.06f, 0.03f));

        // ── Footer (bottom 8%) ──
        GameObject footer = CreatePanel("Footer", win.transform,
            new Vector2(0f, 0f), new Vector2(1f, 0.09f),
            Vector2.zero, Vector2.zero,
            new Color(0.05f, 0.035f, 0.016f, 1f));

        CreatePanel("FooterLine", footer.transform,
            new Vector2(0f, 1f), new Vector2(1f, 1f),
            Vector2.zero, new Vector2(0f, 1f),
            new Color(0.29f, 0.18f, 0.031f, 1f));

        CreateButtonAuto("BtnClose", footer.transform,
            new Vector2(0.25f, 0.1f), new Vector2(0.75f, 0.9f),
            Vector2.zero, Vector2.zero,
            "[ E ]   TAKE YOUR LEAVE",
            new Color(0.05f, 0.035f, 0.016f),
            new Color(0.36f, 0.23f, 0.063f),
            new Color(0.45f, 0.30f, 0.08f),
            8, 16);

        Debug.Log("Shop UI created! Assign ShopUI to BlackSmith and CoinText to ShopManager.");
        Selection.activeGameObject = shopUI;
    }

    // ─────────────────────── Card ───────────────────────
    static void CreateCard(string name, Transform parent,
        float anchorYMax, float anchorYMin,
        string title, string desc, string cost,
        Color accentColor, Color bgColor)
    {
        // Double panel pour la bordure de la carte
        GameObject card = WrapWithBorder(name, parent,
            new Vector2(0.03f, anchorYMin), new Vector2(0.97f, anchorYMax),
            new Vector2(0f, 2f), new Vector2(0f, -2f),
            new Color(0.23f, 0.14f, 0.031f, 1f),
            bgColor);

        // Left accent border — stretch vertical
        GameObject lb = new GameObject("LeftBorder", typeof(RectTransform), typeof(Image));
        lb.transform.SetParent(card.transform, false);
        RectTransform lbrt = lb.GetComponent<RectTransform>();
        lbrt.anchorMin = new Vector2(0f, 0f);
        lbrt.anchorMax = new Vector2(0f, 1f);
        lbrt.pivot = new Vector2(0f, 0.5f);
        lbrt.anchoredPosition = Vector2.zero;
        lbrt.sizeDelta = new Vector2(4f, 0f);
        lb.GetComponent<Image>().color = accentColor;

        // Name
        CreateTMPAuto("CardName", card.transform,
            new Vector2(0.06f, 0.52f), new Vector2(0.68f, 0.95f),
            Vector2.zero, Vector2.zero,
            title, accentColor, FontStyles.Bold, 10, 20);

        // Desc
        CreateTMPAuto("CardDesc", card.transform,
            new Vector2(0.06f, 0.05f), new Vector2(0.68f, 0.48f),
            Vector2.zero, Vector2.zero,
            desc, new Color(0.35f, 0.22f, 0.094f), FontStyles.Italic, 8, 14);

        // Cost
        CreateTMPAuto("CostText", card.transform,
            new Vector2(0.70f, 0.52f), new Vector2(0.97f, 0.95f),
            Vector2.zero, Vector2.zero,
            cost + " gold", new Color(0.78f, 0.59f, 0.047f), FontStyles.Bold, 8, 16);

        // Buy button
        CreateButtonAuto("BuyButton", card.transform,
            new Vector2(0.70f, 0.05f), new Vector2(0.97f, 0.48f),
            Vector2.zero, Vector2.zero,
            "ACQUIRE",
            new Color(0.10f, 0.055f, 0.008f),
            accentColor, accentColor, 7, 13);
    }

    // ─────────────────────── Helpers ───────────────────────

    // Double panel — bordure qui scale parfaitement
    static GameObject WrapWithBorder(string name, Transform parent,
        Vector2 anchorMin, Vector2 anchorMax,
        Vector2 offsetMin, Vector2 offsetMax,
        Color borderColor, Color bgColor)
    {
        GameObject border = new GameObject(name + "_Border", typeof(RectTransform), typeof(Image));
        border.transform.SetParent(parent, false);
        RectTransform brt = border.GetComponent<RectTransform>();
        brt.anchorMin = anchorMin;
        brt.anchorMax = anchorMax;
        brt.offsetMin = offsetMin;
        brt.offsetMax = offsetMax;
        border.GetComponent<Image>().color = borderColor;

        GameObject inner = new GameObject(name, typeof(RectTransform), typeof(Image));
        inner.transform.SetParent(border.transform, false);
        RectTransform irt = inner.GetComponent<RectTransform>();
        irt.anchorMin = Vector2.zero;
        irt.anchorMax = Vector2.one;
        irt.offsetMin = new Vector2(1f, 1f);
        irt.offsetMax = new Vector2(-1f, -1f);
        inner.GetComponent<Image>().color = bgColor;
        return inner;
    }

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
        Vector2 anchoredPos, Vector2 size,
        string text, Color color, FontStyles style,
        float minSize, float maxSize)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        if (size == Vector2.zero)
        { rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero; }
        else
        { rt.anchoredPosition = anchoredPos; rt.sizeDelta = size; }
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

    static void CreateButtonAuto(string name, Transform parent,
        Vector2 anchorMin, Vector2 anchorMax,
        Vector2 anchoredPos, Vector2 size,
        string label, Color bgColor, Color borderColor, Color textColor,
        float minSize, float maxSize)
    {
        // Bordure du bouton via double panel aussi
        GameObject btnBorder = new GameObject(name + "_Border", typeof(RectTransform), typeof(Image));
        btnBorder.transform.SetParent(parent, false);
        RectTransform brt = btnBorder.GetComponent<RectTransform>();
        brt.anchorMin = anchorMin;
        brt.anchorMax = anchorMax;
        if (size == Vector2.zero)
        { brt.offsetMin = new Vector2(2f, 2f); brt.offsetMax = new Vector2(-2f, -2f); }
        else
        { brt.anchoredPosition = anchoredPos; brt.sizeDelta = size; }
        btnBorder.GetComponent<Image>().color = borderColor;

        GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(btnBorder.transform, false);
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(1f, 1f);
        rt.offsetMax = new Vector2(-1f, -1f);
        go.GetComponent<Image>().color = bgColor;

        GameObject txtGo = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
        txtGo.transform.SetParent(go.transform, false);
        RectTransform trt = txtGo.GetComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.offsetMin = Vector2.zero;
        trt.offsetMax = Vector2.zero;
        TextMeshProUGUI tmp = txtGo.GetComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.color = textColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;
        tmp.enableWordWrapping = false;
        tmp.enableAutoSizing = true;
        tmp.fontSizeMin = minSize;
        tmp.fontSizeMax = maxSize;
    }

    static void AddOutline(GameObject go, Color color)
    {
        Outline o = go.AddComponent<Outline>();
        o.effectColor = color;
        o.effectDistance = new Vector2(1f, -1f);
    }
}