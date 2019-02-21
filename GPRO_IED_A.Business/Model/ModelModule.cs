﻿using System;
using System.Collections.Generic;
using System.Linq;
using GPRO_IED_A.Business.Model;
using GPRO_IED_A.Data; 

namespace GPRO_IED_A.Business.Model
{
    public class ModelModule  
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string ModuleName { get; set; }
        public bool IsSystem { get; set; }
        public int OrderIndex { get; set; }
        public string Description { get; set; }
        public string ModuleUrl { get; set; }
        public bool IsShow { get; set; }
        public List<ModelPermission> Permissions { get; set; }
    }
}
