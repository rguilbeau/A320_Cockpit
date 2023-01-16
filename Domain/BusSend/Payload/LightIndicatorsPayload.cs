namespace A320_Cockpit.Domain.BusSend.Payload
{
    /// <summary>
    /// Contenu de la frame des informations des lumières du cockpit
    /// </summary>
    public class LightIndicatorsPayload
    {
        private byte fcuDisplayBrightness;
        private bool testIndicatorsLight;

        /// <summary>
        /// La valeur du rétroéclairage des afficheurs du FCU
        /// </summary>
        public byte FcuDisplayBrightness { get => fcuDisplayBrightness; set => fcuDisplayBrightness = value; }
        /// <summary>
        /// Le test des indicateurs est sur ON (affiche toutes les limères du cockpit pour checker l'état)
        /// </summary>
        public bool TestIndicatorsLight { get => testIndicatorsLight; set => testIndicatorsLight = value; }
    }
}
