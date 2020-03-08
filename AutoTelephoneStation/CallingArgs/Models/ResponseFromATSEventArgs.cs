
namespace AutoTelephoneStation.CallingArgs.Models
{
    public class ResponseFromATSEventArgs : IResponseFromATSargs
    { 
        public string MessageResponse { get; private set; }

        public ResponseFromATSEventArgs(string messageResponse)
        {
            MessageResponse = messageResponse;
        }
    }
}
