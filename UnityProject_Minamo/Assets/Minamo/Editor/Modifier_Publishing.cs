using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System.Linq;

namespace Assets.Minamo.Editor {
    class Modifier_Publishing : IModifier {
        // android
        AssignableType<bool> android_useApkExpansion;

        string[] uwp_capability;

        // ps4
        AssignableType<bool> ps4_attribExclusiveVR;
        AssignableType<bool> ps4_attribShareSupport;
        AssignableType<bool> ps4_attribMoveSupport;
        AssignableType<PlayerSettings.PS4.PS4AppCategory> ps4_category;
        AssignableType<string> ps4_masterVersion;
        AssignableType<string> ps4_contentID;
        AssignableType<int> ps4_applicationParameter1;
        AssignableType<int> ps4_applicationParameter2;
        AssignableType<int> ps4_applicationParameter3;
        AssignableType<int> ps4_applicationParameter4;
        AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment> ps4_enterButtonAssignment;


        public void Apply() {
            if (android_useApkExpansion.Flag) {
                PlayerSettings.Android.useAPKExpansionFiles = android_useApkExpansion;
            }

            var capabilityList = uwp_capability.ToList();
            var table = StringEnumConverter.Get<PlayerSettings.WSACapability>();
            foreach(var kv in table) {
                var flag = capabilityList.Contains(kv.Key);
                PlayerSettings.WSA.SetCapability(kv.Value, flag);
            }

            if (ps4_attribExclusiveVR.Flag) {
                PlayerSettings.PS4.attribExclusiveVR = ps4_attribExclusiveVR;
            }
            if (ps4_attribShareSupport.Flag) {
                PlayerSettings.PS4.attribShareSupport = ps4_attribShareSupport;
            }
            if (ps4_attribMoveSupport.Flag) {
                PlayerSettings.PS4.attribMoveSupport = ps4_attribMoveSupport;
            }
            if (ps4_category.Flag) {
                PlayerSettings.PS4.category = ps4_category;
            }
            if (ps4_masterVersion.Flag) {
                PlayerSettings.PS4.masterVersion = ps4_masterVersion;
            }
            if (ps4_contentID.Flag) {
                PlayerSettings.PS4.contentID = ps4_contentID;
            }
            if (ps4_applicationParameter1.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter1;
            }
            if (ps4_applicationParameter2.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter2;
            }
            if (ps4_applicationParameter3.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter3;
            }
            if (ps4_applicationParameter4.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter4;
            }
            if (ps4_enterButtonAssignment.Flag) {
                PlayerSettings.PS4.enterButtonAssignment = ps4_enterButtonAssignment;
            }
        }

        public void Reload(AnyDictionary dict) {
            android_useApkExpansion = AssignableType<bool>.FromDict(dict, "android_useApkExpansion");

            var l = dict.GetList("uwpCapability");
            var list = new List<string>();
            for(int i = 0; l != null && i < l.Count; i++) {
                var v = l[i] as string;
                if(v != null) {
                    list.Add(v);
                }
            }
            this.uwp_capability = list.ToArray();

            ps4_attribExclusiveVR = AssignableType<bool>.FromDict(dict, "ps4_attribExclusiveVR");
            ps4_attribShareSupport = AssignableType<bool>.FromDict(dict, "ps4_attribShareSupport");
            ps4_attribMoveSupport = AssignableType<bool>.FromDict(dict, "ps4_attribMoveSupport");
            ps4_category = AssignableType<PlayerSettings.PS4.PS4AppCategory>.FromEnumDict(dict, "ps4_category");
            ps4_masterVersion = AssignableType<string>.FromDict(dict, "ps4_masterVersion");
            ps4_contentID = AssignableType<string>.FromDict(dict, "ps4_contentID");
            ps4_applicationParameter1 = AssignableType<int>.FromDict(dict, "ps4_applicationParameter1");
            ps4_applicationParameter2 = AssignableType<int>.FromDict(dict, "ps4_applicationParameter2");
            ps4_applicationParameter3 = AssignableType<int>.FromDict(dict, "ps4_applicationParameter3");
            ps4_applicationParameter4 = AssignableType<int>.FromDict(dict, "ps4_applicationParameter4");
            ps4_enterButtonAssignment = AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment>.FromEnumDict(dict, "ps4_enterButtonAssignment");
        }

        internal static Modifier_Publishing Current() {
            var capabilityList = new List<string>();
            var table = StringEnumConverter.Get<PlayerSettings.WSACapability>();
            foreach(var kv in table) {
                var f = PlayerSettings.WSA.GetCapability(kv.Value);
                if(f) {
                    capabilityList.Add(kv.Key);
                }
            }

            return new Modifier_Publishing()
            {
                android_useApkExpansion = AssignableType<bool>.Create(PlayerSettings.Android.useAPKExpansionFiles),

                uwp_capability = capabilityList.ToArray(),

                ps4_attribExclusiveVR = AssignableType<bool>.Create(PlayerSettings.PS4.attribExclusiveVR),
                ps4_attribShareSupport = AssignableType<bool>.Create(PlayerSettings.PS4.attribShareSupport),
                ps4_attribMoveSupport = AssignableType<bool>.Create(PlayerSettings.PS4.attribMoveSupport),
                ps4_category = AssignableType<PlayerSettings.PS4.PS4AppCategory>.Create(PlayerSettings.PS4.category),
                ps4_masterVersion = AssignableType<string>.Create(PlayerSettings.PS4.masterVersion),
                ps4_contentID = AssignableType<string>.Create(PlayerSettings.PS4.contentID),
                ps4_applicationParameter1 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter1),
                ps4_applicationParameter2 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter2),
                ps4_applicationParameter3 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter3),
                ps4_applicationParameter4 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter4),
                ps4_enterButtonAssignment = AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment>.Create(PlayerSettings.PS4.enterButtonAssignment),
            };
        }

        public string GetConfigText() {
            var cb = new ConfigTextBuilder();

            // android
            cb.Append("android_useApkExpansion", android_useApkExpansion);

            cb.Append("uwp_capability", string.Join("|", uwp_capability));

            // ps4 attributes
            cb.Append("ps4_attribExclusiveVR", ps4_attribExclusiveVR);
            cb.Append("ps4_attribShareSupport", ps4_attribShareSupport);
            cb.Append("ps4_attribMoveSupport", ps4_attribMoveSupport);
            cb.Append("ps4_category", ps4_category);
            cb.Append("ps4_masterVersion", ps4_masterVersion);
            cb.Append("ps4_contentID", ps4_contentID);
            cb.Append("ps4_applicationParameter1", ps4_applicationParameter1);
            cb.Append("ps4_applicationParameter2", ps4_applicationParameter2);
            cb.Append("ps4_applicationParameter3", ps4_applicationParameter3);
            cb.Append("ps4_applicationParameter4", ps4_applicationParameter4);
            cb.Append("ps4_enterButtonAssignment", ps4_enterButtonAssignment);

            return cb.ToString();
        }
    }
}
