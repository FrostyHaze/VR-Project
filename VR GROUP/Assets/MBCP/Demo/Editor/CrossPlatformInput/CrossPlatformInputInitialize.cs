using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput.Inspector
{
    [InitializeOnLoad]
    public class CrossPlatformInitialize
    {
        static CrossPlatformInitialize()
        {
            var defines = GetDefinesList(BuildTargetGroup.Standalone);
            if (!defines.Contains("CROSS_PLATFORM_INPUT"))
            {
                SetEnabled("CROSS_PLATFORM_INPUT", true, false);
                SetEnabled("MOBILE_INPUT", true, true);
            }
        }

        [MenuItem("Mobile Input/Enable")]
        private static void Enable()
        {
            SetEnabled("MOBILE_INPUT", true, true);
            if (IsMobilePlatform(EditorUserBuildSettings.activeBuildTarget))
            {
                EditorUtility.DisplayDialog("Mobile Input",
                    "You have enabled Mobile Input. Use the Unity Remote app on a connected device to control your game in the Editor.",
                    "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Mobile Input",
                    "You have enabled Mobile Input, but your current build target is not a mobile platform. Switch the build target to Android or iOS to enable mobile controls.",
                    "OK");
            }
        }

        [MenuItem("Mobile Input/Enable", true)]
        private static bool EnableValidate()
        {
            var defines = GetDefinesList(BuildTargetGroup.Android);
            return !defines.Contains("MOBILE_INPUT");
        }

        [MenuItem("Mobile Input/Disable")]
        private static void Disable()
        {
            SetEnabled("MOBILE_INPUT", false, true);
            if (IsMobilePlatform(EditorUserBuildSettings.activeBuildTarget))
            {
                EditorUtility.DisplayDialog("Mobile Input",
                    "You have disabled Mobile Input. Mobile control rigs won't be visible, and the Cross Platform Input functions will return standalone controls.",
                    "OK");
            }
        }

        [MenuItem("Mobile Input/Disable", true)]
        private static bool DisableValidate()
        {
            var defines = GetDefinesList(BuildTargetGroup.Android);
            return defines.Contains("MOBILE_INPUT");
        }

        private static bool IsMobilePlatform(BuildTarget target)
        {
            return target == BuildTarget.Android || target == BuildTarget.iOS;
        }

        private static BuildTargetGroup[] buildTargetGroups = new BuildTargetGroup[]
        {
            BuildTargetGroup.Standalone,
#if UNITY_2019_4_OR_NEWER
            BuildTargetGroup.Android,
            BuildTargetGroup.iOS
#endif
        };

        private static BuildTargetGroup[] mobileBuildTargetGroups = new BuildTargetGroup[]
        {
            BuildTargetGroup.Android,
            BuildTargetGroup.iOS
        };

        private static void SetEnabled(string defineName, bool enable, bool mobile)
        {
            foreach (var group in mobile ? mobileBuildTargetGroups : buildTargetGroups)
            {
                var defines = GetDefinesList(group);
                if (enable)
                {
                    if (defines.Contains(defineName))
                        return;
                    defines.Add(defineName);
                }
                else
                {
                    if (!defines.Contains(defineName))
                        return;
                    defines.RemoveAll(d => d == defineName);
                }
                string definesString = string.Join(";", defines.ToArray());
                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, definesString);
            }
        }

        private static List<string> GetDefinesList(BuildTargetGroup group)
        {
            return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));
        }
    }
}
