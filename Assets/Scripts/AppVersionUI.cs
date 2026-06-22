using TMPro;
using UnityEngine;

[RequireComponent( typeof( TextMeshProUGUI ) )]
public class AppVersionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI appVersionText = null;

    private void OnValidate()
    {
        if( appVersionText == null )
        {
            appVersionText = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Awake()
    {
        if( appVersionText == null )
        {
            Debug.LogWarning( "Missing TextMeshPro Component - added manually" );
            appVersionText = GetComponent<TextMeshProUGUI>();
        }

        appVersionText.text = $"v.{Application.version}";
    }
}
