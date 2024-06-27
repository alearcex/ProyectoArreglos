using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1.Proceso
{
    public class MenuPrincipal
    {
        public int[] numerosDePago = new int[10];
        public DateTime[] fechas = new DateTime[10];
        public TimeSpan[] horas = new TimeSpan[10];
        public int[] cedulas = new int[10];
        public string[] nombres = new string[10];
        public string[] apellidos1 = new string[10];
        public string[] apellidos2 = new string[10];
        public int[] numerosCaja = new int[10];
        public int[] tiposServicio = new int[10];
        public int[] numerosFactura = new int[10];
        public decimal[] montosPagados = new decimal[10];
        public decimal[] montosComision = new decimal[10];
        public decimal[] montosDeducidos = new decimal[10];
        public decimal[] montosPagaCliente = new decimal[10];
        public decimal[] vuelto = new decimal[10];

        public MenuPrincipal()
        {
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("**            SISTEMA DE PAGO DE SERVICIOS            **");
                Console.WriteLine("********************************************************");
                Console.WriteLine("Seleccione una opción: ");
                Console.WriteLine("1- Inicializar Vectores");
                Console.WriteLine("2- Realizar Pagos");
                Console.WriteLine("3- Consultar Pagos");
                Console.WriteLine("4- Modificar Pagos");
                Console.WriteLine("5- Eliminar Pagos");
                Console.WriteLine("6- Submenú de reportes");
                Console.WriteLine("7- Salir");
                Console.WriteLine("Seleccione una opción...");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        InicializarVectores();
                        break;
                    case "2":
                        RealizarPago();
                        break;
                    case "3":
                        ConsultarPago();
                        break;
                    case "4":
                        ModificarPago();
                        break;
                    case "5":
                        EliminarPago();
                        break;
                    case "6":
                        int cuenta = 0;
                        for (int i = 0; i < numerosDePago.Length; i++)
                        {
                            if (numerosDePago[i] != 0)
                            {
                                cuenta++;
                            }
                        }
                        if (cuenta > 0)
                        {
                            //El this se refiere al objeto MenuReportes
                            MenuReportes.Menu(this);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("No hay registros con los cuáles generar reportes.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;
                    case "7":
                        Console.WriteLine("Saliendo...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida, por favor inténtelo de nuevo.");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        #region Opciones del Menú

        private void InicializarVectores()
        {
            Array.Clear(numerosDePago, 0, numerosDePago.Length);
            Array.Clear(fechas, 0, fechas.Length);
            Array.Clear(horas, 0, horas.Length);
            Array.Clear(cedulas, 0, cedulas.Length);
            Array.Clear(nombres, 0, nombres.Length);
            Array.Clear(apellidos1, 0, apellidos1.Length);
            Array.Clear(apellidos2, 0, apellidos2.Length);
            Array.Clear(numerosCaja, 0, numerosCaja.Length);
            Array.Clear(tiposServicio, 0, tiposServicio.Length);
            Array.Clear(numerosFactura, 0, numerosFactura.Length);
            Array.Clear(montosPagados, 0, montosPagados.Length);
            Array.Clear(montosComision, 0, montosComision.Length);
            Array.Clear(montosDeducidos, 0, montosDeducidos.Length);
            Array.Clear(montosPagaCliente, 0, montosPagaCliente.Length);
            Array.Clear(vuelto, 0, vuelto.Length);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vectores inicializados correctamente.");
            Console.ResetColor();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void RealizarPago()
        {
            for (int i = 0; i < numerosDePago.Length; i++)
            {
                if (numerosDePago[i] != 0)
                    continue; // Salta los índices ya ocupados

                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("**                    REALIZAR PAGO                   **");
                Console.WriteLine("********************************************************");
                Console.WriteLine($"Ingrese los datos para el registro {i + 1}:");

                numerosDePago[i] = i + 1;

                fechas[i] = LeerFecha();

                horas[i] = LeerHora();

                Console.Write("Cédula: ");
                cedulas[i] = LeerEntero();

                nombres[i] = LeerString("Nombre: ");

                apellidos1[i] = LeerString("Primer Apellido: ");

                apellidos2[i] = LeerString("Segundo Apellido: ");

                Console.Write("Número de Caja: ");
                numerosCaja[i] = LeerEntero();

                tiposServicio[i] = LeerTipoServicio();

                Console.Write("Número de Factura: ");
                numerosFactura[i] = LeerEntero();

                Console.Write("Monto a Pagar: ");
                montosPagados[i] = LeerDecimal();

                while (true)
                {
                    Console.Write("Monto Paga Cliente: ");
                    montosPagaCliente[i] = LeerDecimal();

                    if (montosPagaCliente[i] < montosPagados[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("El monto con el que paga el cliente no puede ser menor que el monto a pagar. Inténtelo nuevamente.");
                        Console.ResetColor();
                    }
                    else
                    {
                        break;
                    }
                }
                vuelto[i] = montosPagaCliente[i] - montosPagados[i];
                Console.WriteLine($"Vuelto: {vuelto[i]}");

                // Calcula el monto de comisión y deducido basado en el tipo de servicio
                decimal porcentajeComision = 0;
                switch (tiposServicio[i])
                {
                    case 1:
                        porcentajeComision = 0.04m; // 4% por recibo electricidad
                        break;
                    case 2:
                        porcentajeComision = 0.055m; // 5.5% por recibo telefónico
                        break;
                    case 3:
                        porcentajeComision = 0.065m; // 6.5% por recibo de agua
                        break;
                }
                decimal montoComision = Math.Round(montosPagados[i] * porcentajeComision, 2);
                montosComision[i] = montoComision;
                montosDeducidos[i] = montosPagados[i] - montoComision;

                Console.WriteLine($"Monto Comisión ({porcentajeComision * 100}%): {montoComision}");
                Console.WriteLine($"Monto Deducido: {montosDeducidos[i]}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registro guardado correctamente.");
                Console.ResetColor();

                Console.Write("¿Desea ingresar otro pago? (S/N): ");
                if (Console.ReadLine().ToUpper() != "S")
                    break;
            }

            // Verifica si los vectores están llenos
            if (numerosDePago[numerosDePago.Length - 1] != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vectores Llenos.");
                Console.ResetColor();
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ConsultarPago()
        {
            Console.Clear();
            Console.WriteLine("********************************************************");
            Console.WriteLine("**                 CONSULTAR PAGO                     **");
            Console.WriteLine("********************************************************");
            Console.WriteLine("Ingrese el número de pago que desea consultar:");

            int numeroPagoConsultar = LeerEntero();

            int posicion = Array.IndexOf(numerosDePago, numeroPagoConsultar);

            if (posicion != -1)
            {
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("**                    DATOS DEL PAGO                  **");
                Console.WriteLine("********************************************************");
                Console.WriteLine($"Número de pago: {numerosDePago[posicion]}");
                Console.WriteLine($"Fecha: {fechas[posicion]:dd/MM/yyyy}");
                Console.WriteLine($"Hora: {horas[posicion]:hh\\:mm}");
                Console.WriteLine($"Cédula: {cedulas[posicion]}");
                Console.WriteLine($"Nombre: {nombres[posicion]}");
                Console.WriteLine($"Primer Apellido: {apellidos1[posicion]}");
                Console.WriteLine($"Segundo Apellido: {apellidos2[posicion]}");
                Console.WriteLine($"Número de Caja: {numerosCaja[posicion]}");
                Console.WriteLine($"Tipo de Servicio: {tiposServicio[posicion]}");
                Console.WriteLine($"Número de Factura: {numerosFactura[posicion]}");
                Console.WriteLine($"Monto a Pagar: {montosPagados[posicion]}");
                Console.WriteLine($"Monto Comisión: {montosComision[posicion]}");
                Console.WriteLine($"Monto Deducido: {montosDeducidos[posicion]}");
                Console.WriteLine($"Monto Paga Cliente: {montosPagaCliente[posicion]}");
                Console.WriteLine($"Vuelto: {vuelto[posicion]}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"El número de pago {numeroPagoConsultar} no existe.");
                Console.ResetColor();
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ModificarPago()
        {
            Console.Clear();
            Console.WriteLine("********************************************************");
            Console.WriteLine("**                 MODIFICAR PAGO                     **");
            Console.WriteLine("********************************************************");
            Console.WriteLine("Ingrese el número de pago que desea modificar:");
            int numeroPagoModificar = LeerEntero();

            int posicion = Array.IndexOf(numerosDePago, numeroPagoModificar);

            if (posicion != -1)
            {
                Console.WriteLine($"Datos encontrados para el número de pago {numeroPagoModificar}:");
                Console.WriteLine($"Número de pago: {numerosDePago[posicion]}");
                Console.WriteLine($"Monto: {montosPagados[posicion]}");

                while (true)
                {
                    Console.WriteLine("\nSeleccione el dato que desea modificar:");
                    Console.WriteLine("1- Fecha");
                    Console.WriteLine("2- Hora");
                    Console.WriteLine("3- Cédula");
                    Console.WriteLine("4- Nombre");
                    Console.WriteLine("5- Apellido1");
                    Console.WriteLine("6- Apellido2");
                    Console.WriteLine("7- Tipo de Servicio");
                    Console.WriteLine("8- Número de factura");
                    Console.WriteLine("9- Monto a Pagar");
                    Console.WriteLine("10- Monto comisión");
                    Console.WriteLine("11- Monto Paga Cliente");
                    Console.WriteLine("12- Vuelto");
                    Console.WriteLine("0- Finalizar modificación");

                    var opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            fechas[posicion] = LeerFecha();
                            break;
                        case "2":
                            horas[posicion] = LeerHora();
                            break;
                        case "3":
                            Console.Write("Ingrese la nueva cédula: ");
                            cedulas[posicion] = LeerEntero();
                            break;
                        case "4":
                            nombres[posicion] = LeerString("Ingrese el nombre: ");
                            break;
                        case "5":
                            apellidos1[posicion] = LeerString("Ingrese el primer apellido: ");
                            break;
                        case "6":
                            apellidos2[posicion] = LeerString("Ingrese el primer apellido: ");
                            break;
                        case "7":
                            tiposServicio[posicion] = LeerTipoServicio();
                            break;
                        case "8":
                            Console.Write("Ingrese el nuevo número de factura: ");
                            numerosFactura[posicion] = LeerEntero();
                            break;
                        case "9":
                            Console.Write("Ingrese el nuevo monto a pagar: ");
                            montosPagados[posicion] = LeerDecimal();
                            break;
                        case "10":
                            decimal montoComision = 0;
                            switch (tiposServicio[posicion])
                            {
                                case 1:
                                    montoComision = montosPagados[posicion] * 0.04m; // 4% por recibo electricidad
                                    break;
                                case 2:
                                    montoComision = montosPagados[posicion] * 0.055m; // 5.5% por recibo telefónico
                                    break;
                                case 3:
                                    montoComision = montosPagados[posicion] * 0.065m; // 6.5% por recibo de agua
                                    break;
                            }
                            montosComision[posicion] = montoComision;
                            montosDeducidos[posicion] = montosPagados[posicion] - montoComision;
                            break;
                        case "11":
                            Console.Write("Ingrese el nuevo monto que paga el cliente: ");
                            montosPagaCliente[posicion] = LeerDecimal();
                            break;
                        case "12":
                            Console.Write("Ingrese el nuevo vuelto: ");
                            vuelto[posicion] = LeerDecimal();
                            break;
                        case "0":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Modificación finalizada.");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción no válida, por favor inténtelo de nuevo.");
                            Console.ResetColor();
                            break;
                    }

                    if (opcion == "0")
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"El número de pago {numeroPagoModificar} no existe.");
                Console.ResetColor();
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarPago()
        {
            Console.Clear();
            Console.WriteLine("********************************************************");
            Console.WriteLine("**                  ELIMINAR PAGO                     **");
            Console.WriteLine("********************************************************");
            Console.WriteLine("Ingrese el número de pago que desea eliminar:");
            int numeroPagoEliminar = LeerEntero();

            int posicion = Array.IndexOf(numerosDePago, numeroPagoEliminar);

            if (posicion != -1)
            {
                Console.WriteLine($"Datos encontrados para el número de pago {numeroPagoEliminar}:");
                Console.WriteLine($"Número de pago: {numerosDePago[posicion]}");
                Console.WriteLine($"Monto: {montosPagados[posicion]}");

                Console.Write("¿Está seguro de eliminar el dato? (S/N): ");
                string confirmacion = Console.ReadLine().ToUpper();

                if (confirmacion == "S")
                {
                    numerosDePago[posicion] = 0;
                    fechas[posicion] = DateTime.MinValue;
                    horas[posicion] = TimeSpan.Zero;
                    cedulas[posicion] = 0;
                    nombres[posicion] = null;
                    apellidos1[posicion] = null;
                    apellidos2[posicion] = null;
                    numerosCaja[posicion] = 0;
                    tiposServicio[posicion] = 0;
                    numerosFactura[posicion] = 0;
                    montosPagados[posicion] = 0;
                    montosComision[posicion] = 0;
                    montosDeducidos[posicion] = 0;
                    montosPagaCliente[posicion] = 0;
                    vuelto[posicion] = 0;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("El pago fue eliminado correctamente.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Eliminación cancelada.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"El número de pago {numeroPagoEliminar} no existe.");
                Console.ResetColor();
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        #endregion

        #region Métodos de validación
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

        private static decimal LeerDecimal()
        {
            decimal numero;
            while (!decimal.TryParse(Console.ReadLine(), out numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Debe ingresar un número decimal válido. Inténtelo nuevamente:");
                Console.ResetColor();
            }
            return numero;
        }

        private static DateTime LeerFecha()
        {
            DateTime fecha;
            while (true)
            {
                Console.Write("Ingrese la fecha (DD/MM/AAAA): ");
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fecha no válida. Inténtelo nuevamente.");
                    Console.ResetColor();
                }
            }
            return fecha;
        }

        private static TimeSpan LeerHora()
        {
            TimeSpan hora;
            while (true)
            {
                Console.Write("Ingrese la hora (HH:MM): ");
                string input = Console.ReadLine();
                if (TimeSpan.TryParse(input, out hora))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hora no válida. Inténtelo nuevamente.");
                    Console.ResetColor();
                }
            }
            return hora;
        }

        private static string LeerString(string mensaje)
        {
            string dato;
            while (true)
            {
                Console.Write($"{mensaje}");
                dato = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(dato) && EsSoloLetras(dato))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Solo se acepta el ingreso de letras. Inténtelo nuevamente.");
                    Console.ResetColor();
                }
            }
            return dato;
        }

        private static bool EsSoloLetras(string cadena)
        {
            foreach (char c in cadena)
            {
                if (!char.IsLetter(c) && c != 'ñ' && c != 'Ñ' && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

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

        #endregion
    }
}
