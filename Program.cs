using System;
using System.Collections.Generic;
using System.Linq;

namespace GasSupportApp
{
    // Enumerare pentru starile posibile ale unui tichet
    public enum TicketStatus { Open, Assigned, Diagnosed, Paid, Closed }

    // Interfata pentru abstractizarea unui tichet
    public interface ITicket
    {
        int Id { get; }
        string Description { get; }
        TicketStatus Status { get; }
    }

    class Program
    {
        //  - Meniul Principal
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEM SUPORT APP GAZ ===");
                Console.WriteLine("1. Consola CLIENT (Adauga tichete)");
                Console.WriteLine("2. Consola OPERATOR (Gestioneaza tickets)");
                Console.WriteLine("3. Exit");
                Console.Write("Selectati optiunea: ");

                string choice = Console.ReadLine();
                if (choice == "1") RunClientConsole();
                else if (choice == "2") RunOperatorConsole();
                else if (choice == "3") break;
            }
        }

        // Logica  pentru utilizator
        static void RunClientConsole()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- CONSOLA CLIENT ---");
                Console.WriteLine("1. Adauga tichet nou");
                Console.WriteLine("2. Plateste Servicii (pentru tichete diagnosticate)");
                Console.WriteLine("3. Inapoi la Meniul Principal");

                string opt = Console.ReadLine();
                if (opt == "1")
                {
                    Console.Write("Descrie problema: ");
                    string desc = Console.ReadLine();
                    int id = TicketRepository.AllTickets.Count + 1;
                    TicketRepository.AllTickets.Add(new SupportTicket(id, desc));
                    Console.WriteLine("Tichet creat cu succes!");
                    Console.ReadKey();
                }
                else if (opt == "2")
                {
                    // Filtrare tichete 
                    var diagnosed = TicketRepository.AllTickets.Where(t => t.Status == TicketStatus.Diagnosed).ToList();
                    if (diagnosed.Count == 0) Console.WriteLine("Nu ai tichete de platit.");
                    foreach (var t in diagnosed)
                    {
                        Console.WriteLine($"ID: {t.Id} | Problema: {t.Description} | Diagnoza: {t.Diagnostics}");
                        Console.Write("Confirmati plata? (da/nu): ");
                        if (Console.ReadLine().ToLower() == "da") t.SetStatus(TicketStatus.Paid);
                    }
                    Console.ReadKey();
                }
                else if (opt == "3") break;
            }
        }

        // Logica pentru operator
        static void RunOperatorConsole()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- CONSOLA OPERATOR ---");
                Console.WriteLine("1. Vezi tichete noi si Preia (Assign)");
                Console.WriteLine("2. Ofera Diagnoza");
                Console.WriteLine("3. Inchide Tichete Platite");
                Console.WriteLine("4. Inapoi la Meniul Principal");

                string opt = Console.ReadLine();
                if (opt == "1")
                {
                    var open = TicketRepository.AllTickets.Where(t => t.Status == TicketStatus.Open).ToList();
                    foreach (var t in open)
                    {
                        Console.WriteLine($"Tichet ID: {t.Id} - {t.Description}");
                        t.SetStatus(TicketStatus.Assigned);
                        Console.WriteLine("Tichetul a fost preluat.");
                    }
                    Console.ReadKey();
                }
                else if (opt == "2")
                {
                    var assigned = TicketRepository.AllTickets.Where(t => t.Status == TicketStatus.Assigned).ToList();
                    foreach (var t in assigned)
                    {
                        Console.WriteLine($"Tichet ID: {t.Id} - {t.Description}");
                        Console.Write("Introdu raport diagnoza: ");
                        t.Diagnostics = Console.ReadLine();
                        t.SetStatus(TicketStatus.Diagnosed);
                    }
                    Console.ReadKey();
                }
                else if (opt == "3")
                {
                    var paid = TicketRepository.AllTickets.Where(t => t.Status == TicketStatus.Paid).ToList();
                    foreach (var t in paid)
                    {
                        Console.WriteLine($"Inchidem tichetul {t.Id}...");
                        t.SetStatus(TicketStatus.Closed);
                    }
                    Console.ReadKey();
                }
                else if (opt == "4") break;
            }
        }
    }
}
