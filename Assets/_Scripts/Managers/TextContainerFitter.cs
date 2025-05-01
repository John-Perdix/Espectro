using UnityEngine;

namespace Messy
{
    [AddComponentMenu("Layout/TMP Container Size Fitter")]
    public class TextContainerFitter : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI m_TMProUGUI;
        [SerializeField] private float MaxWidth = 100f;

        public TMPro.TextMeshProUGUI TextMeshPro
        {
            get
            {
                if (m_TMProUGUI == null && transform.GetComponentInChildren<TMPro.TextMeshProUGUI>())
                {
                    m_TMProUGUI = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                    m_TMPRectTransform = m_TMProUGUI.rectTransform;
                }
                return m_TMProUGUI;
            }
        }

        protected RectTransform m_RectTransform;
        public RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                    m_RectTransform = GetComponent<RectTransform>();
                return m_RectTransform;
            }
        }

        protected RectTransform m_TMPRectTransform;
        public RectTransform TMPRectTransform { get { return m_TMPRectTransform; } }

        protected float m_PreferredHeight;
        public float PreferredHeight { get { return m_PreferredHeight; } }

        protected float m_PreferredWidth;
        public float PreferredWidth { get { return m_PreferredWidth; } }

        protected virtual void SetSize()
        {
            if (TextMeshPro == null)
                return;

            m_PreferredWidth = TextMeshPro.preferredWidth;
            m_PreferredHeight = TextMeshPro.preferredHeight;

            float width = Mathf.Min(m_PreferredWidth, MaxWidth);
            float height = width < MaxWidth ? rectTransform.sizeDelta.y : m_PreferredHeight;

            rectTransform.sizeDelta = new Vector2(width, height);

            SetDirty();
        }

        protected virtual void OnEnable()
        {
            SetSize();
        }

        protected virtual void Start()
        {
            SetSize();
        }

        protected virtual void Update()
        {
            if (TextMeshPro == null)
                return;

            if (TextMeshPro.preferredWidth != m_PreferredWidth ||
                (TextMeshPro.preferredWidth >= MaxWidth && TextMeshPro.preferredHeight != m_PreferredHeight))
            {
                SetSize();
            }
        }

        #region MarkLayoutForRebuild
        public virtual bool IsActive()
        {
            return isActiveAndEnabled;
        }

        protected void SetDirty()
        {
            if (!IsActive())
                return;

            UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

        protected virtual void OnRectTransformDimensionsChange()
        {
            SetDirty();
        }

        #if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            SetDirty();
        }
        #endif

        #endregion
    }
}
