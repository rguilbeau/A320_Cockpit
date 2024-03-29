﻿using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using A320_Cockpit.Domain.Repository.Cockpit;

namespace A320_Cockpit.Domain.UseCase.ListenEvent
{
    /// <summary>
    /// Use case pour démarrer l'écoute des évenements envoyés par le cockpit
    /// </summary>
    public class ListenEventUseCase
    {
        public event EventHandler<ListenEventArgs> ?EventReceived;
        private readonly ICockpitRepository cockpitRepository;

        /// <summary>
        /// Création du use case
        /// </summary>
        /// <param name="cockpitRepository"></param>
        /// <param name="presenter"></param>
        public ListenEventUseCase(ICockpitRepository cockpitRepository) 
        { 
            this.cockpitRepository = cockpitRepository;
        }

        /// <summary>
        /// Evenement recu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="frame"></param>
        private void CockpitRepository_FrameReceived(object? sender, Frame frame)
        {
            if (frame.Id == (int)FrameId.EVENT && frame.Size == 7)
            {
                int idEvent = (frame.Data[0] << 8) | (frame.Data[1]);

                if (System.Enum.IsDefined(typeof(CockpitEvent), idEvent))
                {
                    byte[] bytes = new byte[] { frame.Data[2], frame.Data[3], frame.Data[4], frame.Data[5] };

                    if (!BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(bytes);
                    }

                    float data = BitConverter.ToSingle(bytes, 0);
                    bool isPing = frame.Data[6] == 0x01;

                    CockpitEvent e = (CockpitEvent)System.Enum.Parse(typeof(CockpitEvent), idEvent.ToString());
                    EventReceived?.Invoke(this, new ListenEventArgs(e, data, isPing));
                }
            }
        }

        /// <summary>
        /// Demarre l'écoute des évenements
        /// </summary>
        public void Listen()
        {
            cockpitRepository.FrameReceived += CockpitRepository_FrameReceived;
        }

        /// <summary>
        /// Arrête l'écoute des évenements
        /// </summary>
        public void Stop()
        {
            cockpitRepository.FrameReceived -= CockpitRepository_FrameReceived;
        }
    }
}
