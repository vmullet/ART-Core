package com.unity.art.plugin.android;
import android.app.Activity;
import android.os.BatteryManager;
import android.widget.Toast;
import com.unity3d.player.UnityPlayer;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

import static android.content.Context.BATTERY_SERVICE;

/**
 * Created by Valentin on 24/02/2018.
 */

public class ArtSystem {

    private Activity unityActivity;

    public ArtSystem() {
        unityActivity = UnityPlayer.currentActivity;
    }

    public String getDate(String format) {
        Date date = Calendar.getInstance().getTime();
        SimpleDateFormat fmt = new SimpleDateFormat(format,Locale.FRANCE);
        return fmt.format(date);
    }

    public void showMessage(String message) {
        Toast.makeText(unityActivity, message, Toast.LENGTH_SHORT).show();
    }

}
