using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoverNavigation
{
    //Araca sağa veya sola 90 derece dön komutu uygulandığında aracın yeni yönü tanımlanır
    public class DirectionState
    {
        public char Current { get; set; } //Aracın mevcut yönü (Kuzey, Güney, Doğu, Batı)
        public char Letter { get; set; } //Komut (Sağa veya sola 90 derece dön)
        public char New { get; set; } //Aracın yeni yönü (Kuzey, Güney, Doğu, Batı)
    }
}
