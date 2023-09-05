using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHole.Model
{
    public class Muros
    {
        // -------------------------------------------------------------------------------------------------------------------------------
        // DATOS DE ENTRADA //

        /// <summary>
        /// Ancho de la sección _ [cm]
        /// </summary>
        public double b;

        /// <summary>
        /// Espesor del elemento _ [cm]
        /// </summary>
        public double h;

        /// <summary>
        /// Recubrimiento del refuerzo _ [cm]
        /// </summary>
        public double r;

        /// <summary>
        /// Altura efectiva de la sección _ [cm]
        /// </summary>
        public double d;

        /// <summary>
        /// Cuantía mínima de refuerzo en muros
        /// </summary>
        public double pmin;
                
        /// <summary>
        /// Número de la barra a emplear en el diseño de los muros
        /// </summary>
        public int Nb;
        
        /// <summary>
        /// Lista o vector de momentos de diseño para refuerzo vertical {Muy(+), Muy(-)} _ [kN.cm]
        /// </summary>
        public double[] LMuy;

        /// <summary>
        /// Lista o vector de momentos de diseño para refuerzo horizontal {Mux(+), Mux(-)} _ [kN.cm]
        /// </summary>
        public double[] LMux;

        /// <summary>
        /// Fuerzas cortantes = max(Vux, Vuy) _ [kN]
        /// </summary>
        public double Vu;


        // -------------------------------------------------------------------------------------------------------------------------------
        // METODOS //

        public string ChequeoMomentoNominal(double Mu, double Mn)
        {
            string Opcion1 = "Cumple";
            string Opcion2 = "No cumple";

            if (Mu < Mn)
            { return Opcion1; }
            else
            { return Opcion2; }
        }


        public string ChequeoCortante(double Vu, double Vc)
        {
            string Opcion1 = "Cumple";
            string Opcion2 = "No cumple";

            if (Vc > Vu)
            { return Opcion1; }
            else
            { return Opcion2; }
        }
    }
}
