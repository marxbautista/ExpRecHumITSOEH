using System;

namespace EntidadesRH
{
    public class ENivelAcceso
    {
        private Int16 idNivel;

        public Int16 IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }
        
        private String descNivel;

        public String DescNivel
        {
            get { return descNivel; }
            set { descNivel = value; }
        }
    }
}
