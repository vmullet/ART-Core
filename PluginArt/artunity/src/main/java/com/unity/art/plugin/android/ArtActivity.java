package com.unity.art.plugin.android;

import android.view.KeyEvent;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

/**
 * Created by Valentin on 28/02/2018.
 */

public class ArtActivity extends UnityPlayerActivity {

    public static final String GAMEOBJECT_COMMAND_CONTROLER = "CommandControler";
    public static final String FUNC_EXECUTE_COMMAND = "ExecuteButtonCommand";

    @Override
    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        switch(keyEvent.getKeyCode()) {
            case KeyEvent.KEYCODE_ENTER:
                triggerButton(ArtButton.REAR_BUTTON_SHORT);
                return true;
            case KeyEvent.KEYCODE_BACK:
                triggerButton(ArtButton.REAR_BUTTON_LONG);
                triggerButton(ArtButton.BACK_BUTTON);
                return true;
            case KeyEvent.KEYCODE_VOLUME_UP:
                triggerButton(ArtButton.VOLUME_UP_BUTTON);
                return true;
            case KeyEvent.KEYCODE_VOLUME_DOWN:
                triggerButton(ArtButton.VOLUME_DOWN_BUTTON);
                return true;
            case KeyEvent.KEYCODE_MENU:
                triggerButton(ArtButton.FRONT_BUTTON_LONG);
                return true;
            case KeyEvent.KEYCODE_DPAD_LEFT:
                triggerButton(ArtButton.MIDDLE_BUTTON_SHORT);
                return true;
            case KeyEvent.KEYCODE_DPAD_RIGHT:
                triggerButton(ArtButton.FRONT_BUTTON_SHORT);
                return true;
        }
        return super.onKeyUp(i, keyEvent);
    }

    @Override
    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        switch(keyEvent.getKeyCode()) {
            case KeyEvent.KEYCODE_VOLUME_UP:
                return true;
            case KeyEvent.KEYCODE_VOLUME_DOWN:
                return true;
        }
        return super.onKeyDown(i, keyEvent);
    }

    public void triggerButton(String artButton) {
        UnityPlayer.UnitySendMessage(GAMEOBJECT_COMMAND_CONTROLER,FUNC_EXECUTE_COMMAND,artButton);
    }
}
