using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    
    public Sprite IconStyle1;
    public Sprite IconStyle2;
    public Sprite IconStyle3;
    public Sprite IconStyle4;
    public Sprite IconStyle5;
    public Sprite IconStyle6;
    public Sprite IconStyle7;
    public Sprite IconStyle8;
    
    private Image objectImage;

    void Start()
    {
        
        objectImage = GetComponent<Image>();


        if (objectImage != null)
        {
            
            switch (GameInit.IconSelected)
            {
                case 1:
                    objectImage.sprite = IconStyle1;
                    break;

                case 2:
                    objectImage.sprite = IconStyle2;
                    break;

                case 3:
                    objectImage.sprite = IconStyle3;
                    break;

                case 4:
                    objectImage.sprite = IconStyle4;
                    break;

                case 5:
                    objectImage.sprite = IconStyle5;
                    break;

                case 6:
                    objectImage.sprite = IconStyle6;
                    break;

                case 7:
                    objectImage.sprite = IconStyle7;
                    break;

                case 8:
                    objectImage.sprite = IconStyle8;
                    break;

                default:
                    objectImage.sprite = IconStyle1;
                    break;
            }
        }
        else
        {
            Debug.LogError("No Image component found on this GameObject.");
        }
    }
}
