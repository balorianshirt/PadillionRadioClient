namespace PadillionRadio.Business.Models
{
    public class UserModel
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
        /// Device identifier
        /// </summary>
        public string DeviceIdentifier { get; set; }  
    }
}