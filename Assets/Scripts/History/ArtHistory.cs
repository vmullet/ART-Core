﻿public class ArtHistory {

    private IndexedDictionary<string,OpenAlprResult> bookingHistory;
    private int currentIndex;
    private int countNotFound;

    public static int FIRST_INDEX = 0;

    public int CurrentPosition => currentIndex + 1;

    public int CountHistory => bookingHistory.Count;

    public int CountNotFound => countNotFound;

    public bool IsEmpty => bookingHistory.Count == 0;

    public bool IsFirst => bookingHistory.Count > 0 && currentIndex == FIRST_INDEX;

    public bool IsLast => bookingHistory.Count > 0 && currentIndex == (bookingHistory.Count - 1);

    public OpenAlprResult CurrentElement => currentIndex >= 0 ? bookingHistory[currentIndex] : null;

    public ArtHistory()
    {
        bookingHistory = new IndexedDictionary<string, OpenAlprResult>();
        currentIndex = -1;
        countNotFound = 0;
    }

    public OpenAlprResult AddBooking(OpenAlprResult openAlprResult)
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
    
    public OpenAlprResult First()
    {
        if (IsEmpty)
            return null;

        currentIndex = FIRST_INDEX;
        return bookingHistory[currentIndex];
    }

    public OpenAlprResult Last()
    {
        if (IsEmpty)
            return null;

        currentIndex = bookingHistory.Count - 1;
        return bookingHistory[currentIndex];
    }

    public OpenAlprResult Previous()
    {
        if (IsEmpty)
            return null;

        if (!IsFirst)
            currentIndex--;

        return bookingHistory[currentIndex];
    }

    public OpenAlprResult Next()
    {
        if (IsEmpty)
            return null;

        if (!IsLast)
            currentIndex++;

        return bookingHistory[currentIndex];
    }
}
