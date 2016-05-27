using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoverNavigation
{
    //Yönler
    //---------
    //N - Kuzey
    //S - Güney
    //E - Doğu
    //W - Batı

    //Komutlar
    //---------
    //L - Sola 90 derece dön
    //R - Sağa 90 derece dön
    //M - Aracın koordinatını bulunduğu yöne göre bir birim değiştir

    class Rover
    {
        //Yönlere göre uygulanan komutlar için tüm durumlar tanımlanır
        static List<DirectionState> _directionStates = new List<DirectionState>(new DirectionState[] { 
            new DirectionState() { Current = 'N', Letter = 'L', New = 'W' },
            new DirectionState() { Current = 'W', Letter = 'L', New = 'S' },
            new DirectionState() { Current = 'S', Letter = 'L', New = 'E' },
            new DirectionState() { Current = 'E', Letter = 'L', New = 'N' },
            new DirectionState() { Current = 'N', Letter = 'R', New = 'E' },
            new DirectionState() { Current = 'E', Letter = 'R', New = 'S' },
            new DirectionState() { Current = 'S', Letter = 'R', New = 'W' },
            new DirectionState() { Current = 'W', Letter = 'R', New = 'N' }
        });

        //Yönler ve koordinat sistemindeki bölgelere göre çarpanları tanımlanır
        static Dictionary<char, int> _directionFactors = new Dictionary<char, int>()
        {
            {'N', 1},
            {'E', 1},
            {'S', -1},
            {'W', -1},
        };

        //Geçici değişkenler
        int _tempX = 0;
        int _tempY = 0;
        char _tempDirection;

        //Aracın nitelikleri tanımlanır
        public int X { get; set; } //X koordinatı
        public int Y { get; set; } //Y koordinatı
        public char Direction { get; set; } //Yön (Kuzey, Güney, Doğu, Batı)
        public Grid Grid { get; set; } //Aracın kontrol edileceği sınırları belirlenen alan

        //Yeni bir araç (Rover) tanımlanırken içinde yönlendirileceği alan (Grid) da tanımlanır
        public Rover(Grid grid)
        {
            this.Grid = grid;
        }

        //Gönderilen komut dizisi geçici değişkenler ile uygulanır
        //Eğer hata yok ise geçici değişkenler gerçek değişkenlere atanır
        public bool Move(string text)
        {
            //Gerçek değişkenler geçici değişkenlere atanır
            _tempX = this.X;
            _tempY = this.Y;
            _tempDirection = this.Direction;

            //Komut dizisi sıra ile uygulanır ve değerler geçici değişkenlere atanır
            foreach (char letter in text.ToCharArray())
                Move(letter);

            //Geçici değişkenler gerçek değişkenlere atanır
            this.X = _tempX;
            this.Y = _tempY;
            this.Direction = _tempDirection;

            return true;
        }

        //Gönderilen komut geçici değişkenler ile uygulanır
        void Move(char letter)
        {
            switch (letter)
            {
                case 'L': //Sola 90 derece dön
                case 'R': //Sağa 90 derece dön
                    //Aracın yönü değişir
                    _tempDirection = _directionStates.Where(item => item.Current == _tempDirection & item.Letter == letter).First().New;
                    break;
                case 'M':
                    //Aracın koordinatı değişir
                    int factor = 0;
                    //Aracın yönüne göre koordinat çarpanı alınır
                    _directionFactors.TryGetValue(_tempDirection, out factor);
                    switch (_tempDirection)
                    {
                        case 'E': //Doğu
                        case 'W': //Batı
                            //Araç koordinatı değiştiğinde belirlenen sınırın dışına çıkar mı
                            if (_tempX + factor < 0 || _tempX + factor > this.Grid.X)
                                throw new Exception("Araç belirlenen sınırların dışına çıkamaz");
                            _tempX += factor; //Koordinat değiştir
                            break;
                        case 'N': //Kuzey
                        case 'S': //Güney
                            //Araç koordinatı değiştiğinde belirlenen sınırın dışına çıkar mı
                            if (_tempY + factor < 0 || _tempY + factor > this.Grid.Y)
                                throw new Exception("Araç belirlenen sınırların dışına çıkamaz");
                            _tempY += factor; //Koordinat değiştir
                            break;
                        default:
                            throw new Exception(string.Format("Tanımsız yön '{0}'", _tempDirection));
                    }
                    break;
                default:
                    throw new Exception(string.Format("Tanımsız komut '{0}'", letter));
            }
        }

        //Yön tanımlı mı kontrol eder
        public bool CheckDirection(char direction)
        {
            return _directionFactors.ContainsKey(direction);
        }
    }
}
