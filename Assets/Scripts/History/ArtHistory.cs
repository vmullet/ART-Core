public class ArtHistory {

    private IndexedDictionary<string,LocalOpenAlprResult> bookingHistory;
    private int currentIndex;
    private int countNotFound;

    public static int FIRST_INDEX = 0;

    public int CurrentPosition => currentIndex + 1;

    public int CountHistory => bookingHistory.Count;

    public int CountNotFound => countNotFound;

    public bool IsEmpty => bookingHistory.Count == 0;

    public bool IsFirst => bookingHistory.Count > 0 && currentIndex == FIRST_INDEX;

    public bool IsLast => bookingHistory.Count > 0 && currentIndex == (bookingHistory.Count - 1);

    public LocalOpenAlprResult CurrentElement => currentIndex >= 0 ? bookingHistory[currentIndex] : null;

    public ArtHistory()
    {
        bookingHistory = new IndexedDictionary<string, LocalOpenAlprResult>();
        currentIndex = -1;
        countNotFound = 0;
    }

    public LocalOpenAlprResult AddBooking(LocalOpenAlprResult openAlprResult)
    {
        if (!IsEmpty)
            currentIndex = bookingHistory.Count - 1;
        else
            currentIndex = -1;

        if (!bookingHistory.Contains(openAlprResult.Plate))
        {
            bookingHistory.Add(openAlprResult.Plate, openAlprResult);
            currentIndex++;
            if (string.IsNullOrEmpty(openAlprResult.Plate))
                countNotFound++;
            return openAlprResult;
        }
        
        return null;
    }

    public int IncreaseNotFound()
    {
        countNotFound++;
        return countNotFound;
    }
    
    public LocalOpenAlprResult First()
    {
        if (IsEmpty)
            return null;

        currentIndex = FIRST_INDEX;
        return bookingHistory[currentIndex];
    }

    public LocalOpenAlprResult Last()
    {
        if (IsEmpty)
            return null;

        currentIndex = bookingHistory.Count - 1;
        return bookingHistory[currentIndex];
    }

    public LocalOpenAlprResult Previous()
    {
        if (IsEmpty)
            return null;

        if (!IsFirst)
            currentIndex--;

        return bookingHistory[currentIndex];
    }

    public LocalOpenAlprResult Next()
    {
        if (IsEmpty)
            return null;

        if (!IsLast)
            currentIndex++;

        return bookingHistory[currentIndex];
    }
}
