namespace Asdf.Application.Api.Devices
{
    public class CreateDeviceResponse
    {
        public long? Id { get; set; }

        public CreateDeviceResponse(long? id)
        {
            this.Id = id;
        }
    }
}