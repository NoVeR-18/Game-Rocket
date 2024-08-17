using UnityEngine;

public class PointConteiner : MonoBehaviour
{
    [SerializeField]
    private RectTransform _completeImage;
    private void Awake()
    {
        ResetUpgrade();
    }
    public void CompleteUpgrade()
    {
        _completeImage?.gameObject.SetActive(true);
    }
    public void ResetUpgrade()
    {
        _completeImage?.gameObject.SetActive(false);
    }
}
