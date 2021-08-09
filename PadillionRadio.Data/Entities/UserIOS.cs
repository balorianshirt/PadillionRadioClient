namespace PadillionRadio.Data.Entities
{
    public class UserIOS
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Authorization code
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// iOS device identifier
        /// </summary>
        public string DeviceIdentifier { get; set; }
    }
}