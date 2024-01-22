using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class shows card in had and while being draged
/// 
/// TODO: add different displays for terra/Permanent/NonPermanent cards
/// TODO: add base and different displays for cards onboard;
/// </summary>
public class CardDisplay : MonoBehaviour
{
    /*[HideInInspector]*/ public CardInfo cardInfo;
    
    public Text nameText;
    public Text descriptionText;
    public Image artworkImage;
    public Image cardBackImage;
    public Sprite cardBackSprite;
    public bool isHidden = false;
    public Text costUI;
    public Image hexUI;
    public Image cardBackground;
    [HideInInspector] public bool isMoved;

    [SerializeField] private Canvas canvas;
    
    private Image _image;

    public void SetUp(CardInfo info)
    {
        cardInfo = info;
        nameText.text = cardInfo.name;
        descriptionText.text = cardInfo.description;
        artworkImage.sprite = cardInfo.artwork;
        cardBackImage.sprite = cardBackSprite;
        
        UpdateDisplay();
        
        cardBackground.sprite = cardInfo.cardBackground;

        if (info.cardType == ECardType.Permanent)
        {
            PermanentTemplate temCard = (PermanentTemplate) info;
            hexUI.sprite = temCard.hexUI;
        }
        if (info.cardType == ECardType.TerraHex)
        {
            TerraHexTemplate temCard = (TerraHexTemplate) info;
            hexUI.sprite = temCard.hexUI;
        }
        if (info.cardType == ECardType.Whimsy)
        {
            NonPermanentTemplate temCard = (NonPermanentTemplate) info;
            costUI.text = temCard.Cost.ToString();
            hexUI.gameObject.SetActive(false);
        }
    }

    public void UpdateDisplay()
    {
        cardBackImage.enabled = isHidden ? true : false;
    }
}
