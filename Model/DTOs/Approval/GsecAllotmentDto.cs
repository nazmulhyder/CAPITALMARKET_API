﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class GsecAllotmentApprovalDto
    {      
        public string? OMIBuyIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }   
    }
}
