using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    [SerializeField]
    private RawImage picture;
    [SerializeField]
    private Text vehiclePlate;
    [SerializeField]
    private Text bookingNumber;
    [SerializeField]
    private Text clientSurname;
    [SerializeField]
    private Text outboundDate;
    [SerializeField]
    private Text petQuantity;
    [SerializeField]
    private Text bookingHistoryStatus;

    const string DISPLAY_DATE_FORMAT = "dd/MM/yy HH:mm";
    const string DEFAULT_PLATE_TEXT = "READY";
    const string PLATE_NOT_FOUND = "NOT FOUND";

    private void Start()
    {
        ResetPanel();
    }

    
    public void ShowResult(LocalOpenAlprResult bookingData)
    {
        
    }

    public void ShowPicture(string picturePath)
    {
        if (File.Exists(picturePath))
        {
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, File.ReadAllBytes(picturePath));
            picture.color = new Color(1, 1, 1, 1);
            picture.texture = texture;
        }
        else
        {
            AppManager.System.ShowMessage("The file " + picturePath + " doesn't exist");
        }
    }

    public void ShowPlate(string plate)
    {
        vehiclePlate.text = plate;
    }

    public void ShowPlateNotFound()
    {
        vehiclePlate.text = PLATE_NOT_FOUND;
    }

#region NAVIGATION METHODS

    public void ShowPreviousBooking()
    {
        if (AppManager.ArtHistory.IsEmpty)
        {
            AppManager.System.ShowMessage("Historique vide");
            return;
        }

        if (!AppManager.ArtHistory.IsFirst)
            ShowResult(AppManager.ArtHistory.Previous());
        else
            AppManager.System.ShowMessage("Aucun Booking Précédent");

        UpdateHistoryStatus();
    }

    public void ShowNextBooking()
    {
        if (AppManager.ArtHistory.IsEmpty)
        {
            AppManager.System.ShowMessage("Historique vide");
            return;
        }
            
        if (!AppManager.ArtHistory.IsLast)
            ShowResult(AppManager.ArtHistory.Next());
        else
            AppManager.System.ShowMessage("Aucun Booking Suivant");

        UpdateHistoryStatus();
    }

    public void ShowLastBooking()
    {
        if (AppManager.ArtHistory.IsEmpty)
        {
            AppManager.System.ShowMessage("Historique vide");
            return;
        }

        if (!AppManager.ArtHistory.IsLast)
            ShowResult(AppManager.ArtHistory.Last());
        else
            AppManager.System.ShowMessage("Aucun Booking Suivant");

        UpdateHistoryStatus();
    }

    public void ShowFirstBooking()
    {
        if (AppManager.ArtHistory.IsEmpty)
        {
            AppManager.System.ShowMessage("Historique vide");
            return;
        }

        if (!AppManager.ArtHistory.IsFirst)
            ShowResult(AppManager.ArtHistory.First());
        else
            AppManager.System.ShowMessage("Aucun Booking Précédent");

        UpdateHistoryStatus();
    }

#endregion

    public void ResetPanel()
    {
#if LIGHTUI
        picture.texture = null;
        picture.color = new Color(1, 1, 1, 0);
#endif
        vehiclePlate.text = DEFAULT_PLATE_TEXT;
        bookingNumber.text = string.Empty;
        clientSurname.text = string.Empty;
        outboundDate.text = string.Empty;
        petQuantity.text = string.Empty;
        bookingHistoryStatus.text = string.Empty;
    }

    private void UpdateHistoryStatus()
    {
        if (!AppManager.ArtHistory.IsEmpty)
            bookingHistoryStatus.text = AppManager.ArtHistory.CurrentPosition
                + " / "
                + AppManager.ArtHistory.CountHistory;
    }

}
