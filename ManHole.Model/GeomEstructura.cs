using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHole.Model
{
    public class GeomEstructura
    {
        // -------------------------------------------------------------------------------------------------------------------------------
        // DATOS DE ENTRADA //

        /// <summary>
        /// Diámetro interior del cilindro _ [m]
        /// </summary>
        public double D1;

        /// <summary>
        /// Diámetro exterior de la base _ [m]
        /// </summary>
        public double D2;

        /// <summary>
        /// Diámetro interior del acceso _ [m]
        /// </summary>
        public double D3;

        /// <summary>
        /// Espesor de la base _ [m]
        /// </summary>
        public double E1;

        /// <summary>
        /// Espesor de la pared del cilindro _ [m]
        /// </summary>
        public double E2;

        /// <summary>
        /// Espesor del cuello _ [m]
        /// </summary>
        public double E3;

        /// <summary>
        /// Ancho de la sección en planta del cuello _ [m]
        /// </summary>
        public double E4;

        /// <summary>
        /// Espesor de la llave en la losa de fondo _ [m]
        /// </summary>
        public double E5;

        /// <summary>
        /// Espesor de la llave en la losa de fondo _ [m]
        /// </summary>
        public double E6;

        /// <summary>
        /// Espesor de la pared del cono _ [m]
        /// </summary>
        public double Ec;

        /// <summary>
        /// Sobreancho en la base _ [m]
        /// </summary>
        public double x;

        /// <summary>
        /// Altura del cono _ [m]
        /// </summary>
        public double h;

        /// <summary>
        /// Altura útil _ [m]
        /// </summary>
        public double hu;

        /// <summary>
        /// Altura total _ [m]
        /// </summary>
        public double HT;

    }
}
