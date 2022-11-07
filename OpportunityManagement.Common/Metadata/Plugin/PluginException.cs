using Microsoft.Xrm.Sdk;
using System;

namespace OpportunityManagement.Common.Metadata.Plugin
{
    public class PluginException
    {
        public int ErrorCode { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public OperationStatus Status { get; set; }
    }
}