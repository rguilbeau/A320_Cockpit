using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Entity.Payload;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;
using A320_Cockpit.Domain.Repository.Payload;

namespace A320_Cockpit.Domain.UseCase.SendPayload
{
    /// <summary>
    /// UseCase pour l'envoi d'une frame
    /// </summary>
    public class SendPayloadUseCase
    {
        private readonly ICockpitRepository cockpitRepository;
        private readonly IPayloadRepository payloadRepository;
        private static readonly History history = new(5000);
        
        /// <summary>
        /// Création du UseCase
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        public SendPayloadUseCase(ICockpitRepository cockpitRepository, IPayloadRepository payloadRepository)
        {
            this.cockpitRepository = cockpitRepository;
            this.payloadRepository = payloadRepository;
        }

        /// <summary>
        /// Execute le UseCase
        /// </summary>
        public void Exec(CockpitEvent e)
        {
            PayloadEntity payload = payloadRepository.Find(e);
            Frame frame = payload.Frame;

            if(history.IsExpired(frame))
            {
                try
                {
                    cockpitRepository.Send(frame);
                    history.Update(frame);
                }
                catch (Exception ex)
                {
                    history.Expire(frame);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
