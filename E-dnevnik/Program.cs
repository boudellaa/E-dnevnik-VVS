﻿using E_dnevnik;

public class Program
{
    public static void Main()
    {
        Ucenik ucenik1 = new Ucenik("Kenan", "Dizdarević");
        Ucenik ucenik2 = new Ucenik("Nedim", "Krupalija");

        Nastavnik nastavnik1 = new Nastavnik("Berin", "Karahodžić");
        Nastavnik nastavnik2 = new Nastavnik("Nedim", "Hošić");

        Predmet predmet1 = new Predmet("Matematika", nastavnik1);
        Predmet predmet2 = new Predmet("Fizika", nastavnik2);

        predmet1.DodajUcenika(ucenik1);
        predmet1.DodajUcenika(ucenik2);
        predmet2.DodajUcenika(ucenik2);

        Razred razred1 = new Razred("II-4");
        Razred razred2 = new Razred("III-2");

        razred1.DodajUcenika(ucenik1);
        razred2.DodajUcenika(ucenik2);

        Skola skola = new Skola("Treća gimnazija Sarajevo");

        skola.DodajRazred(razred1);
        skola.DodajRazred(razred2);

        skola.PrikaziRazrede();
        string korisnickoIme="";
        string sifra = "";
        do {
            Console.WriteLine("Unesite korisničko ime: ");
            korisnickoIme = Console.ReadLine();
        } while (korisnickoIme.Length == 0);
        do
        {
            Console.WriteLine("Unesite korisničko ime: ");
            sifra = Console.ReadLine();
        } while (sifra.Length == 0);


        Console.WriteLine("Unesite lozinku: ");
        string lozinka = Console.ReadLine();
    }
}