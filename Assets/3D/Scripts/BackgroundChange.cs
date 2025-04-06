using UnityEngine;


public class ChangeTexture : MonoBehaviour
{
    public Texture BGStyle1;
    public Texture BGStyle2;
    public Texture BGStyle3;
    public Texture BGStyle4;
    public Texture BGStyle5;
    public Texture BGStyle6;
    public Texture BGStyle7;
    public Texture BGStyle8;
    public Texture BGStyle9;
    
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        switch (GameInit.BackgroundSelected)
        {
            case 1:
                objectRenderer.material.mainTexture = BGStyle1;
                break;

            case 2:
                objectRenderer.material.mainTexture = BGStyle2;
                break;

            case 3:
                objectRenderer.material.mainTexture = BGStyle3;
                break;

            case 4:
                objectRenderer.material.mainTexture = BGStyle4;
                break;

            case 5:
                objectRenderer.material.mainTexture = BGStyle5;
                break;

            case 6:
                objectRenderer.material.mainTexture = BGStyle6;
                break;
            
            case 7:
                objectRenderer.material.mainTexture = BGStyle7;
                break;

            case 8:
                objectRenderer.material.mainTexture = BGStyle8;
                break;
                
            case 9:
                objectRenderer.material.mainTexture = BGStyle9;
                break;

            default:
                objectRenderer.material.mainTexture = BGStyle1;
                break;
        }
    }
}
