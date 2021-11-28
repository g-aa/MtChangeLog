namespace MtChangeLog.DataObjects.Enumerations
{
    internal static class StringEnumConverter
    {
        public static string ToStrnig(Status status) 
        {
            return status switch
            {
                Status.Actual => "Actual",
                Status.Deprecated => "Deprecated",
                Status.Technological => "Technological",
                Status.Test => "Test",
                _ => "Node",
            };
        }
    }
}