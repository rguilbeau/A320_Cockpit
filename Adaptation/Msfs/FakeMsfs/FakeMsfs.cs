using A320_Cockpit.Adaptation.Msfs.Model.Event;
using A320_Cockpit.Adaptation.Msfs.Model.Variable;

namespace A320_Cockpit.Adaptation.Msfs.FakeMsfs
{
    /// <summary>
    /// Adapteur d'un faux MSFS, il ne fait rien, et la connexion est toujours ouverte
    /// à utiliser à des fins de debug avec le FakeA320
    /// </summary>
    public class FakeMsfs : IMsfs
    {
        /// <summary>
        /// La connexion est toujours ouverte
        /// </summary>
        public bool IsOpen => true;

        /// <summary>
        /// Inop
        /// </summary>
        public void Close() {}

        /// <summary>
        /// Inop
        /// </summary>
        public void Open() {}

        /// <summary>
        /// Inop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(Lvar<T> variable) {}

        /// <summary>
        /// Inop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Write<T>(Lvar<T> variable){}

        /// <summary>
        /// Inop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        public void Read<T>(SimVar<T> variable) {}

        /// <summary>
        /// Inop
        /// </summary>
        public void ResumeRead() {}

        /// <summary>
        /// Inop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kEvent"></param>
        public void Send<T>(KEvent<T> kEvent) {}

        /// <summary>
        /// Inop
        /// </summary>
        /// <param name="hEvent"></param>
        public void Send(HEvent hEvent) {}

        /// <summary>
        /// Inop
        /// </summary>
        public void StartTransaction() {}

        /// <summary>
        /// Inop
        /// </summary>
        public void StopRead() {}

        /// <summary>
        /// Inop
        /// </summary>
        public void StopTransaction() {}
    }
}
