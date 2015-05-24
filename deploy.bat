"C:\Development\Android SDK\platform-tools\adb.exe" install -r ".\build.apk"
cd "C:\Development\Android SDK\platform-tools"
adb shell am start -S -a android.intent.action.MAIN -n com.tinyprogress.vr/com.unity3d.player.UnityPlayerNativeActivity