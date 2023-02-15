using UnityEditor;
using FlagCapturing.Controllers;
[CustomEditor(typeof(Joystick))]
public class JoystickInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
