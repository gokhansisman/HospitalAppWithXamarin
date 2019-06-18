using System;

namespace App2
{

    public class Sorular
    { 

    public int Id { get; set; }

    public string Adi { get; set; }

    public string Cevap { get; set; }

    public bool CevaplandiMi { get; set; }

    public int DoktorId { get; set; }

    public int HemsireId { get; set; }

    public int NesneId { get; set; }

    public int OdaId { get; set; }

    public int KoridorId { get; set; }

    public string ZamanPeridoyu { get; set; }

        public DateTime CevaplanmaZamani { get; set; }


        public override string ToString()
    {
        return $"({Id}) {Adi},{Cevap},{CevaplandiMi},{DoktorId},{HemsireId},{OdaId},{KoridorId},{ZamanPeridoyu},{CevaplanmaZamani}";
    }

    }
}
