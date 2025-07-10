using DotNetCore.CAP;

using IDP.Api.Application.Auth.Command;

namespace IDP.Api.Application.Helper
{
    public class CustomerAddedEventSubscriber : ICapSubscribe
    {
        [CapSubscribe("otpevent")]
        public void Consumer(AuthCommand authCommand)

        {
            Console.WriteLine(authCommand.MobileNumber);
        }
    }
}
