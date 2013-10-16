﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class SlottedContainerSlotData
{
    public string slotNameID = "";
    public ItemData slotData = null;
    public List<int> validTypes = new List<int>();
    public int slotMaxCountLimit = 1;
    public int priority = 0;
}

