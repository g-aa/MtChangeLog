﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views
{
    public class ProjectRevisionShortView
    {
        public Guid Id { get; set; }
        public string Module { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
    }
}