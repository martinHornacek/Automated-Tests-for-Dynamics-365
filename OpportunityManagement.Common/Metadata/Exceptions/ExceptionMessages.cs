using Microsoft.Xrm.Sdk;
using OpportunityManagement.Common.Metadata.Plugin;

namespace OpportunityManagement.Common.Metadata.Exceptions
{
    public static class ExceptionMessages
    {
        public static PluginException X300001 => new PluginException
        {
            ErrorCode = ParseErrorCode(nameof(X300001)),
            Message = "Invalid plug-in registration. Plug-in should be registered for entity '{0}', not '{1}'.",
            Status = OperationStatus.Failed
        };

        public static PluginException X300002 => new PluginException
        {
            ErrorCode = ParseErrorCode(nameof(X300002)),
            Message = "IPluginExecutionContext.InputParameters is missing 'Target' entry.",
            Status = OperationStatus.Failed
        };

        public static PluginException X300003 => new PluginException
        {
            ErrorCode = ParseErrorCode(nameof(X300003)),
            Message = "Attribute 'parentaccountid' on opportunity cannot be null.",
            Status = OperationStatus.Failed
        };

        public static PluginException X300004 => new PluginException
        {
            ErrorCode = ParseErrorCode(nameof(X300004)),
            Message = "User with domain name '{0}' does not exist in target CRM Organization.",
            Status = OperationStatus.Failed
        };

        private static int ParseErrorCode(string errorCode)
        {
            return int.Parse(errorCode.Remove(0, 1));
        }
    }
}