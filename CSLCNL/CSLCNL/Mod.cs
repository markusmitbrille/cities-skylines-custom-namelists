using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;
using UnityEngine;

namespace Makaki.CustomNameLists
{
    public class Mod : IUserMod
    {
        public string Name => "Custom Name Lists";
        public string Description => "Provides a framework to add namelists for cities, districts, streets, buildings and much more.";
    }
}
