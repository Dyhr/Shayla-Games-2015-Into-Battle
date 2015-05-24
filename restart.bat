"C:\Development\Android SDK\platform-tools\adb.exe" shell am force-stop com.tinyprogress.vr
cd "C:\Development\Android SDK\platform-tools"
adb shell am start -S -a android.intent.action.MAIN -n com.tinyprogress.vr/com.unity3d.player.UnityPlayerNativeActivity