package com.unity.art.plugin.android;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.RemoteException;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.vuzix.sdk.speechrecognitionservice.VuzixSpeechClient;

/**
 * Created by Valentin on 25/02/2018.
 */

public class VoiceCommandReceiver extends BroadcastReceiver {

    static final String GAMEOBJECT_VOICE_CONTROLER = "CommandControler";
    static final String FUNC_EXECUTE_COMMAND = "ExecuteVoiceCommand";

    private VuzixSpeechClient vuzixSpeechClient;
    private ArtActivity mainActivity;

    public VoiceCommandReceiver(ArtActivity artActivity)
    {
        mainActivity = artActivity;
        mainActivity.registerReceiver(this, new IntentFilter(VuzixSpeechClient.ACTION_VOICE_COMMAND));
        try {
            vuzixSpeechClient = new VuzixSpeechClient(artActivity);
            vuzixSpeechClient.deletePhrase("*");
        } catch (RemoteException re) {
            Toast.makeText(artActivity, "Erreur reconnaissance vocale", Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public void onReceive(Context context, Intent intent) {
        String action = intent.getAction();
        if (action != null && action.equals(VuzixSpeechClient.ACTION_VOICE_COMMAND)) {
            Bundle extras = intent.getExtras();
            if (extras != null && extras.containsKey(VuzixSpeechClient.PHRASE_STRING_EXTRA)) {
                String phrase = intent.getStringExtra(VuzixSpeechClient.PHRASE_STRING_EXTRA).replaceAll(" ","");
                UnityPlayer.UnitySendMessage(GAMEOBJECT_VOICE_CONTROLER,FUNC_EXECUTE_COMMAND, phrase);
            }
        }
    }

    public void enableReceiver(boolean enable) {
        VuzixSpeechClient.EnableRecognizer(mainActivity, enable);
    }

    public void addSpeech(String voice, String command) {
        vuzixSpeechClient.insertPhrase(voice,command);
    }

    public void triggerVoice(boolean trigger) {
        VuzixSpeechClient.TriggerVoiceAudio(mainActivity, trigger);
    }

    public void unregister() {
        mainActivity.unregisterReceiver(this);
        mainActivity = null;
    }

}
