using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCD_LinqDersi
{
    class Program
    {
        static void Main(string[] args)
        {
            dataSource ds = new dataSource();
            List<Musteri> musteriListe = ds.musteriListesi();

            // Liste içerisinde bulunan isim değeri a ile başlayan kayıt sayısı
            int bulunanToplam = 0;
            for (int i = 0; i < musteriListe.Count; i++)
            {
                if (musteriListe[i].isim[0] == 'A')
                {
                    bulunanToplam++;
                    Console.WriteLine("İsim: " + musteriListe[i].isim);
                }
            }
            Console.WriteLine("Liste içerisinde bulunan isim değeri A ile başlayan kayıt sayısı {0} ", bulunanToplam);
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            //Linq ile yapılışı
            bulunanToplam = 0;
            bulunanToplam = musteriListe.Where(i => i.isim.StartsWith("A")).Count();
            Console.WriteLine("Liste içerisinde bulunan isim değeri A ile başlayan kayıt sayısı {0} ", bulunanToplam);

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //1.YOL : Linq method ile sorgulama
            int toplamMusteriAdet1 = musteriListe.Where(i => i.isim.StartsWith("A") && i.soyisim.StartsWith("B")).Count();
            Console.WriteLine("Liste içerisinde bulunan isim değeri A ile başlayan kayıt sayısı {0}", toplamMusteriAdet1);

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //2.YOL : Linq to Query ile sorgulama
            var toplamMusteriBulunan = (from I in musteriListe
                                        where I.isim.StartsWith("A")
                                        select I).Count();

            Console.WriteLine("Liste içerisinde bulunan isim değeri A ile başlayan kayıt sayısı {0}", toplamMusteriBulunan);

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //1 - Musteriler icerisinde ulke degeri A ile başlayan musterileri Linq to metot kullanarak bulalım

            IEnumerable<Musteri> musteriAlistirma1 = musteriListe.Where(i => i.ulke.StartsWith("A"));
            Console.WriteLine(musteriAlistirma1.Count());

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //2 - Musteriler icerisinde ulke degeri A ile başlayan musterileri List kullanarak bulalım
            List<Musteri> musteriAlistirma2 = musteriListe.Where(i => i.ulke.StartsWith("A")).ToList();
            Console.WriteLine(musteriAlistirma2.Count());

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //3 - Müşteriler listesi içerisindeki kayıtlardan isminin içerisinde b harfi geçen ülke değeri içinde A harfi geçen müşterilerin listesini getiriniz.
            var musteriAlistirma3 = musteriListe.Where(i => i.isim.Contains("b") && i.ulke.Contains("a"));

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            //4 - Musteriler listesi içerisindeki kayıtlardan doğum yılı 1990 dan büyük olan veya isminin içerisinde a harfi geçen müşterileri Linq to Query ile getir.

            var musteriAlistirma4 = from I in musteriListe
                                    where I.isim.StartsWith("A") || I.dogumTarih.Year > 1990
                                    select I;

            Console.WriteLine("Musteriler listesi içerisindeki kayıtlardan doğum yılı 1990 dan büyük olan veya isminin içerisinde a harfi geçen müşterilerin sayısı : {0}", musteriAlistirma4.Count());

            Console.WriteLine("-----------------------------------------------------------------------------------------");

            #region Delegate Kullanımı
            //var DelegateKullanim1 = musteriListe.Where(i => i.isim.StartsWith("A"));
            //Func<Musteri, bool> funcDelegate1 = new Func<Musteri, bool>(funcDelegateKullanimi);

            //bool deger = false;
            //foreach (var item in musteriListe)
            //{
            //    deger = funcDelegate1(item);
            //    Console.WriteLine(deger);
            //}

            //var delegateKullanim2 = musteriListe.Where(funcDelegateKullanimi).Count();
            //var delegateKullanim3 = musteriListe.Where(funcDelegate1).Count();

            //Console.WriteLine(" {0} - {1} ", delegateKullanim2, delegateKullanim3);
            ////Farklı Kullanım Şekilleri
            //var delegateKullanim4 = musteriListe.Where(new Func<Musteri, bool>(funcDelegateKullanimi));
            //var delegateKullanim5 = musteriListe.Where(delegate (Musteri m) { return m.isim[0] == 'A' ? true : false; });
            //var delegateKullanim6 = musteriListe.Where((Musteri m) => { return m.isim[0] == 'A' ? true : false; });
            //var delegateKullanim7 = musteriListe.Where((m) => { return m.isim[0] == 'A' ? true : false; });
            //var delegateKullanim8 = musteriListe.Where(m => m.isim[0] == 'A');

            #endregion


            #region Predicate Kullanımı
            Predicate<Musteri> predicate = new Predicate<Musteri>(predicateDelegateMetot);
            var delegateKullanimPredicate1 = musteriListe.FindAll(predicate);

            var delegateKullanimPredicate2 = musteriListe.FindAll(new Predicate<Musteri>(predicateDelegateMetot)); //2.kullanım şekli
            var delegateKullanimPredicate3 = musteriListe.FindAll(delegate (Musteri m) { return m.dogumTarih.Year > 1990; }); //3.kullanım şekli
            var delegateKullanimPredicate4 = musteriListe.FindAll((Musteri m) => { return m.dogumTarih.Year > 1990; });
            var delegateKullanimPredicate5 = musteriListe.FindAll(m => m.dogumTarih.Year > 1990);

            #endregion

            #region Action Delegate Kullanımı
            //1.Yöntem
            Action<Musteri> actionMusteri = new Action<Musteri>(musteriListele);
            musteriListe.ForEach(actionMusteri);

            //2.Yöntem
            musteriListe.ForEach(new Action<Musteri>(musteriListele));

            //3.Yöntem
            musteriListe.ForEach(delegate (Musteri m) { Console.WriteLine(m.isim + " " + m.soyisim); });

            //4.Yöntem
            musteriListe.ForEach((Musteri m) => { Console.WriteLine(m.isim + " " + m.soyisim); });

            //5.Yöntem
            musteriListe.ForEach(m => { Console.WriteLine(m.isim + " " + m.soyisim); });

            #endregion



            Console.ReadLine();

        }

        static bool funcDelegateKullanimi(Musteri m)
        {
            if (m.isim[0] == 'A')
                return true;
            else
                return false;
        }

        static bool predicateDelegateMetot(Musteri m)
        {
            if (m.dogumTarih.Year > 1990)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void musteriListele(Musteri m)
        {
            Console.WriteLine(m.isim + " " + m.soyisim);
        }
    }
}
