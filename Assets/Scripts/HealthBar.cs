using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Target (Character)")]
    [SerializeField] private Character target;

    [Header("UI")]
    [SerializeField] private Image fillImage;     
    [SerializeField] private Image backgroundImage;

    [Header("Options")]
    [SerializeField] private bool worldSpaceFollowCamera = true;  
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 1.2f, 0);
    [SerializeField] private Gradient fillColorByPercent;        

    private Camera mainCam;

    private void Awake()
    {
        if (target == null)
            target = GetComponentInParent<Character>(); 

        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        if (target != null)
        {
            target.OnHealthChanged += HandleHealthChanged;
            
            HandleHealthChanged(target.Health, target.MaxHealth);
        }
    }

    private void OnDisable()
    {
        if (target != null)
            target.OnHealthChanged -= HandleHealthChanged;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        if (transform.parent != null && transform.parent == target.transform)
        {
            transform.position = target.transform.position + worldOffset;

            if (worldSpaceFollowCamera && mainCam != null)
            {
                Vector3 camFwd = mainCam.transform.forward;
                camFwd.z = 0; 
                if (camFwd.sqrMagnitude > 0.0001f)
                    transform.up = camFwd; 
            }
        }
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (fillImage == null || max <= 0) return;

        float t = Mathf.Clamp01((float)current / max);
        fillImage.fillAmount = t;

        if (fillColorByPercent != null)
            fillImage.color = fillColorByPercent.Evaluate(t);
    }

   
    public void SetTarget(Character newTarget)
    {
        if (target == newTarget) return;

        if (target != null)
            target.OnHealthChanged -= HandleHealthChanged;

        target = newTarget;

        if (target != null)
        {
            target.OnHealthChanged += HandleHealthChanged;
            HandleHealthChanged(target.Health, target.MaxHealth);
        }
    }
}
