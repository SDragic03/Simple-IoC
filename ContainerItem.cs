using System;

namespace IoCWebApp.Classes
{
    public class ContainerItem
    {

        #region Properties

        public Type AbstractionType { get; set; }
        public Type ConcreteType { get; set; }
        public Lifecycle Lifecycle { get; set; }

        #endregion

    }
}
