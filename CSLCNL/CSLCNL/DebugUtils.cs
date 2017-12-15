using UnityEngine;
using ColossalFramework.Plugins;
using System.Diagnostics;
using System;

namespace Makaki.CustomNameLists
{
    static class DebugUtils
    {
        public static string MessagePrefix => $"[{Mod.ModName}]";

        [Conditional("DEBUG")]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log($"{MessagePrefix}[{DateTime.Now}] {message}");
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, $"{MessagePrefix} {message}");
        }

        [Conditional("DEBUG")]
        public static void Warning(string message)
        {
            UnityEngine.Debug.LogWarning($"{MessagePrefix}[{DateTime.Now}] {message}");
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, $"{MessagePrefix} {message}");
        }

        [Conditional("DEBUG")]
        public static void Error(string message)
        {
            UnityEngine.Debug.LogError($"{MessagePrefix}[{DateTime.Now}] {message}");
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, $"{MessagePrefix} {message}");
        }
    }
}
