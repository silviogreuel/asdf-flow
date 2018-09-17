namespace Asdf.Application.Api.Devices
{
    public class CreateDeviceRequest
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
    }
}