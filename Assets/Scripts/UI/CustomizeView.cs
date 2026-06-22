using UnityEngine;
using UnityEngine.UI;

public class CustomizeView : MonoBehaviour
{
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Button nextButton = null;

    [SerializeField] private RawImage skinImage = null;
    [SerializeField] private Texture[] skins = null;
    private int currentSkinIndex = 0;

    private void Awake()
    {
        InitView();
        BindAction();
    }

    private void OnEnable()
    {
        InitView();
        BindAction();
    }

    private void OnDisable()
    {
        UnBindAction();
    }

    private void OnDestroy()
    {
        UnBindAction();
    }

    private void UnBindAction()
    {
        previousButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();
    }

    private void BindAction()
    {
        previousButton.onClick.AddListener( HandlePreviousButtonClick );
        nextButton.onClick.AddListener( HandleNextButtonClicked );
    }

    private void InitView()
    {
        currentSkinIndex = MainManager.instance.skinIndex;
        skinImage.texture = skins[currentSkinIndex];
    }

    private void HandleNextButtonClicked()
    {
        currentSkinIndex++;
        if( currentSkinIndex >= skins.Length )
        {
            currentSkinIndex = 0;
        }

        UpdateTexture();
        UpdateSkinIndexSave();
    }

    private void HandlePreviousButtonClick()
    {
        currentSkinIndex--;
        if( currentSkinIndex < 0 )
        {
            currentSkinIndex = skins.Length - 1;
        }

        UpdateTexture();
        UpdateSkinIndexSave();
    }

    private void UpdateTexture()
    {
        LeanTween.cancel( skinImage.gameObject );

        skinImage.texture = skins[currentSkinIndex];
        skinImage.transform.localScale = new Vector3( 0, 0, 0 );
        skinImage.gameObject.LeanScale( Vector3.one, 1f ).setEaseOutQuart();
    }

    private void UpdateSkinIndexSave()
    {
        MainManager.instance.skinIndex = currentSkinIndex;
        MainManager.instance.Save();
    }
}
