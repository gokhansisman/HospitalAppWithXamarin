namespace App2
{
    public class Doktor
    {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Alanı { get; set; }

        public int HastaneId { get; set; }

        

        public override string ToString()
        {
            return $"{Adi}";
        }


    }
}
