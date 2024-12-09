using System;

namespace Domain
{
    public class Alumno
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public double Promedio { get; set; }
        public string Foto { get; set; }
    }
}
