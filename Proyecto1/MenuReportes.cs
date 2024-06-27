using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1.Proceso
{
    public class MenuReportes
    {
        public static void Menu(MenuPrincipal menuPrincipal)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("**                 SUBMENÚ DE REPORTES                **");
                Console.WriteLine("********************************************************");
                Console.WriteLine("1. Ver todos los Pagos");
                Console.WriteLine("2. Ver Pagos por tipo de Servicio");
                Console.WriteLine("3. Ver Pagos por código de caja");
                Console.WriteLine("4. Ver Dinero Comisionado por servicios");
                Console.WriteLine("5. Regresar Menú Principal");
                Console.WriteLine("Seleccione una opción...");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerTodosLosPagos(menuPrincipal);
                        break;
                    case "2":
                        VerPagosPorTipoDeServicio(menuPrincipal);
                        break;
                    case "3":
                        VerPagosPorCodigoDeCaja(menuPrincipal);
                        break;
                    case "4":
                        VerDineroComisionadoPorServicios(menuPrincipal);
                        break;
                    case "5":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida, por favor inténtelo de nuevo.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
        #region Métodos del menú
        private static void VerTodosLosPagos(MenuPrincipal menuPrincipal)
        {
            decimal totalPagos = 0;
            int cuenta = 0;

            Console.Clear();
            Console.WriteLine("*******************************************************************************************************");
            Console.WriteLine("**                              SUBMENÚ DE REPORTES: VER TODOS LOS PAGOS                             **");
            Console.WriteLine("*******************************************************************************************************");
            Console.WriteLine($"{"#Pago",-9}{"Fecha",-11}{"Hora",-8}{"Cédula",-13}{"Nombre",-20}{"1° Apellido",-15}{"2° Apellido",-15}{"Monto Recibo",-10}");
            Console.WriteLine("=======================================================================================================");

            for (int i = 0; i < menuPrincipal.numerosDePago.Length; i++)
            {
                if (menuPrincipal.numerosDePago[i] != 0)
                {
                    Console.WriteLine($"{menuPrincipal.numerosDePago[i],-9}" +
                                        $"{menuPrincipal.fechas[i].ToString("dd/MM/yyyy"),-11}" +
                                        $"{menuPrincipal.horas[i].ToString(@"hh\:mm"),-8}" +
                                        $"{menuPrincipal.cedulas[i],-13}" +
                                        $"{menuPrincipal.nombres[i],-20}" +
                                        $"{menuPrincipal.apellidos1[i],-15}" +
                                        $"{menuPrincipal.apellidos2[i],-15}" +
                                        $"{menuPrincipal.montosPagados[i],-10}");

                    totalPagos += menuPrincipal.montosPagados[i];
                    cuenta ++;
                }
            }

            Console.WriteLine("=======================================================================================================");
            Console.WriteLine($"Total de pagos registrados: {menuPrincipal.numerosDePago.Count(x => x != 0)}");
            Console.WriteLine($"Total de montos de pagos: {totalPagos}");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("                             << Presione cualquier tecla para continuar >>                             ");
            Console.ReadKey();
        }

        private static void VerPagosPorTipoDeServicio(MenuPrincipal menuPrincipal)
        {
            decimal totalPagos = 0;
            int cuenta = 0;

            Console.Clear();
            Console.WriteLine("*******************************************************************************************************");
            Console.WriteLine("**                          SUBMENÚ DE REPORTES: VER PAGOS POR TIPO DE SERVICIO                      **");
            Console.WriteLine("*******************************************************************************************************");
            int tipoServicio = LeerTipoServicio();
            Console.WriteLine();
            Console.WriteLine($"{"#Pago",-9}{"Fecha",-11}{"Hora",-8}{"Cédula",-13}{"Nombre",-20}{"1° Apellido",-15}{"2° Apellido",-15}{"Monto Recibo",-10}");
            Console.WriteLine("=======================================================================================================");

            for (int i = 0; i < menuPrincipal.numerosDePago.Length; i++)
            {
                if (menuPrincipal.numerosDePago[i] != 0 && menuPrincipal.tiposServicio[i] == tipoServicio)
                {
                    Console.WriteLine($"{menuPrincipal.numerosDePago[i],-9}" +
                                        $"{menuPrincipal.fechas[i].ToString("dd/MM/yyyy"),-11}" +
                                        $"{menuPrincipal.horas[i].ToString(@"hh\:mm"),-8}" +
                                        $"{menuPrincipal.cedulas[i],-13}" +
                                        $"{menuPrincipal.nombres[i],-20}" +
                                        $"{menuPrincipal.apellidos1[i],-15}" +
                                        $"{menuPrincipal.apellidos2[i],-15}" +
                                        $"{menuPrincipal.montosPagados[i],-10}");

                    totalPagos += menuPrincipal.montosPagados[i];
                    cuenta++;
                }
            }

            Console.WriteLine("=======================================================================================================");
            Console.WriteLine($"Total de registros: {cuenta}");
            Console.WriteLine($"Total de montos de pagados: {totalPagos}");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("                             << Presione cualquier tecla para continuar >>                             ");
            Console.ReadKey();
        }

        private static void VerPagosPorCodigoDeCaja(MenuPrincipal menuPrincipal)
        {
            decimal totalPagos = 0;
            int cuenta = 0;

            Console.Clear();
            Console.WriteLine("*******************************************************************************************************");
            Console.WriteLine("**                          SUBMENÚ DE REPORTES: VER PAGOS POR CÓDIGO DE CAJA                        **");
            Console.WriteLine("*******************************************************************************************************");
            Console.Write("Ingrese el número de caja: ");
            int numeroCaja = LeerEntero();
            Console.WriteLine();
            Console.WriteLine($"{"#Pago",-9}{"Fecha",-11}{"Hora",-8}{"Cédula",-13}{"Nombre",-20}{"1° Apellido",-15}{"2° Apellido",-15}{"Monto Recibo",-10}");
            Console.WriteLine("=======================================================================================================");

            for (int i = 0; i < menuPrincipal.numerosDePago.Length; i++)
            {
                if (menuPrincipal.numerosDePago[i] != 0 && menuPrincipal.numerosCaja[i] == numeroCaja)
                {
                    Console.WriteLine($"{menuPrincipal.numerosDePago[i],-7}" +
                                        $"{menuPrincipal.fechas[i].ToString("dd/MM/yyyy"),-11}" +
                                        $"{menuPrincipal.horas[i].ToString(@"hh\:mm"),-6}" +
                                        $"{menuPrincipal.cedulas[i],-11}" +
                                        $"{menuPrincipal.nombres[i],-20}" +
                                        $"{menuPrincipal.apellidos1[i],-20}" +
                                        $"{menuPrincipal.apellidos2[i],-20}" +
                                        $"{menuPrincipal.montosPagados[i],-10}");

                    totalPagos += menuPrincipal.montosPagados[i];
                    cuenta++;
                }
            }

            Console.WriteLine("=======================================================================================================");
            Console.WriteLine($"Total de registros: {cuenta}");
            Console.WriteLine($"Total de montos pagados: {totalPagos}");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("                             << Presione cualquier tecla para continuar >>                             ");
            Console.ReadKey();
        }

        private static void VerDineroComisionadoPorServicios(MenuPrincipal menuPrincipal)
        {
            int cuenta1 = 0;
            int cuenta2 = 0;
            int cuenta3 = 0;
            int cuentaTotal = 0;
            decimal total1 = 0;
            decimal total2 = 0;
            decimal total3 = 0;
            decimal totalPagos = 0;

            for (int i = 0; i < menuPrincipal.numerosDePago.Length; i++)
            {
                if (menuPrincipal.numerosDePago[i] != 0)
                {
                    if (menuPrincipal.tiposServicio[i] == 1)
                    {
                        cuenta1++;
                        total1 += menuPrincipal.montosComision[i];
                        cuentaTotal++;
                    }
                    else if (menuPrincipal.tiposServicio[i] == 2)
                    {
                        cuenta2++;
                        total2 += menuPrincipal.montosComision[i];
                        cuentaTotal++;
                    }
                    else if (menuPrincipal.tiposServicio[i] == 3)
                    {
                        cuenta3++;
                        total3 += menuPrincipal.montosComision[i];
                        cuentaTotal++;

                    }
                }
                totalPagos += menuPrincipal.montosComision[i];
            }

            Console.Clear();
            Console.WriteLine("**************************************************************");
            Console.WriteLine("**              VER PAGOS POR TIPO DE SERVICIO              **");
            Console.WriteLine("**************************************************************");
            Console.WriteLine($"{"ITEM",-20}{"Cant. Transacciones",-20}{"Total Comisionado",-20}");
            Console.WriteLine("==============================================================");
            Console.WriteLine($"{"1- Electricidad",-20}{"         " + cuenta1,-20}{"   " + total1, -20}");
            Console.WriteLine($"{"2- Teléfono",-20}{"         " + cuenta2,-20}{"   " + total2, -20}");
            Console.WriteLine($"{"3- Agua",-20}{"         " + cuenta3,-20}{"   " + total3, -20}");
            Console.WriteLine("==============================================================");
            Console.WriteLine($"Total de registros: {cuentaTotal}");
            Console.WriteLine($"Total de montos de pagados: {totalPagos}"); 
            Console.WriteLine("==============================================================");
            Console.WriteLine("         << Presione cualquier tecla para continuar >>        ");
            Console.ReadKey();
        }
        #endregion


        #region Métodos de Validación
        private static int LeerTipoServicio()
        {
            string dato;
            int tipo;
            while (true)
            {
                Console.Write("Tipo de Servicio (1=Recibo de Luz, 2=Recibo Teléfono, 3=Recibo de Agua): ");
                dato = Console.ReadLine();

                if (int.TryParse(dato, out tipo))
                {
                    if (tipo == 1 || tipo == 2 || tipo == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Debe ingresar un tipo de servicio válido. Inténtelo nuevamente:");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Debe ingresar un número entero válido. Inténtelo nuevamente:");
                    Console.ResetColor();
                }
            }
            return tipo;
        }
        private static int LeerEntero()
        {
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Debe ingresar un número entero válido. Inténtelo nuevamente:");
                Console.ResetColor();
            }
            return numero;
        }
        #endregion

    }
}
