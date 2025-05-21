using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Diagnostics;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class CameraTriggerMigration : EditorWindow
{
    [MenuItem("Tools/Migrate Camera Triggers")]
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}