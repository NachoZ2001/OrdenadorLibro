using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace OrdenadorLibroIVA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Establecer el estilo del borde y deshabilitar el cambio de tamaño
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Establecer el tamaño mínimo y máximo para evitar el cambio de tamaño
            this.MinimumSize = this.MaximumSize = this.Size;
        }
        private void SeleccionarArchivoExcel(System.Windows.Forms.TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";
                openFileDialog.Title = "Seleccionar el archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    textBox.Text = openFileDialog.FileName;
                }
            }
        }
        private void buttonProcesar_Click(object sender, EventArgs e)
        {
            int filaEncabezadoHolistor = 7;

            // Modificamos los comprobantes de Holistor
            ModificarComprobantesHolistor(textBoxRutaExcelHolistor.Text, filaEncabezadoHolistor);

            // Armamos el diccionario de los PDF'S
            var diccionario = ArmarDiccionarioPDF(textBoxRutaCarpeta.Text);

            // Comparamos y renombramos
            CompararRenombrar(diccionario, textBoxRutaExcelHolistor.Text, textBoxRutaCarpeta.Text);

            MessageBox.Show("Proceso completado");
        }

        static void CompararRenombrar(Dictionary<string, List<(string emisor, string rutaArchivo)>> diccionario, string rutaExcel, string carpetaArchivos)
        {
            using (var workbook = new XLWorkbook(rutaExcel))
            {
                var worksheet = workbook.Worksheet(1); // Abrimos la primera hoja del Excel
                int contadorIteracion = 1;

                int indiceColumnaComprobante = ObtenerIndiceColumna(worksheet, "Comprobante");
                int indiceColumnaFecha = ObtenerIndiceColumna(worksheet, "Fecha");
                int indiceColumnaDenominacionEmisor = ObtenerIndiceColumna(worksheet, "Proveedor");

                if (indiceColumnaComprobante == -1 || indiceColumnaFecha == -1 || indiceColumnaDenominacionEmisor == -1)
                {
                    MessageBox.Show("Error al encontrar alguna de las columnas requeridas.");
                    return;
                }

                foreach (var row in worksheet.RowsUsed())
                {
                    string fecha = worksheet.Cell(row.RowNumber(), indiceColumnaFecha).Value.ToString().Trim();
                    if (string.IsNullOrEmpty(fecha) || row.RowNumber() == 7)
                    {
                        continue;
                    }

                    string comprobante = worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Value.ToString().Trim();
                    string denominacionEmisor = worksheet.Cell(row.RowNumber(), indiceColumnaDenominacionEmisor).Value.ToString().Trim();

                    if (diccionario.ContainsKey(comprobante))
                    {
                        var registrosExcel = diccionario[comprobante];

                        if (registrosExcel.Count > 0)
                        {
                            // Buscar la mejor coincidencia si tengo mas de un valor
                            var mejorCoincidencia = registrosExcel
                                .OrderByDescending(val => ObtenerSimilitud(val.emisor, denominacionEmisor))
                                .FirstOrDefault();

                            if (!string.IsNullOrEmpty(mejorCoincidencia.emisor))
                            {
                                // Eliminar la coincidencia usada del diccionario
                                registrosExcel.Remove(mejorCoincidencia);
                                if (registrosExcel.Count == 0)
                                {
                                    diccionario.Remove(comprobante);
                                }

                                // Renombrar archivo usando la ruta original
                                try
                                {
                                    string rutaOriginal = mejorCoincidencia.rutaArchivo;
                                    string nuevoNombre = System.IO.Path.Combine(carpetaArchivos, $"{contadorIteracion.ToString("D3")}-{System.IO.Path.GetFileName(rutaOriginal)}");
                                    if (!File.Exists(nuevoNombre)) // Verificamos si el archivo ya existe
                                    {
                                        File.Move(rutaOriginal, nuevoNombre);
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Ya existe un archivo con el nombre {nuevoNombre}. No se puede renombrar.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error al renombrar archivo para el comprobante {comprobante}: {ex.Message}");
                                }

                                // Marcar la fila del Excel como procesada (en verde)
                                worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 204, 255, 204);

                                contadorIteracion++;
                            }
                        }
                        else
                        {
                            // Tengo un solo valor, renombro directamente
                            // Renombrar archivo usando la ruta original
                            try
                            {
                                string rutaOriginal = diccionario[comprobante].Single().rutaArchivo;
                                string nuevoNombre = System.IO.Path.Combine(carpetaArchivos, $"{contadorIteracion.ToString("D3")}-{System.IO.Path.GetFileName(rutaOriginal)}");
                                if (!File.Exists(nuevoNombre)) // Verificamos si el archivo ya existe
                                {
                                    File.Move(rutaOriginal, nuevoNombre);
                                }
                                else
                                {
                                    MessageBox.Show($"Ya existe un archivo con el nombre {nuevoNombre}. No se puede renombrar.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al renombrar archivo para el comprobante {comprobante}: {ex.Message}");
                            }

                            // Eliminar la coincidencia usada del diccionario
                            diccionario.Remove(comprobante);

                            // Marcar la fila del Excel como procesada (en verde)
                            worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 204, 255, 204);

                            contadorIteracion++;
                        }
                    }
                    else
                    {
                        // Marco la fila del Excel en rojo (no encontré ningún comprobante)
                        worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 255, 204, 204);

                        contadorIteracion++;
                    }
                }

                workbook.SaveAs(rutaExcel);
            }
        }


        // Función auxiliar para medir la similitud entre dos cadenas (puede ser ajustada a tu necesidad)
        static int ObtenerSimilitud(string original, string comparacion)
        {
            original = original.ToLower();
            comparacion = comparacion.ToLower();

            // Podrías implementar una lógica más avanzada como distancia de Levenshtein, por ahora usamos una básica
            int similitud = original.Intersect(comparacion).Count();
            return similitud;
        }

        static Dictionary<string, List<(string DenominacionEmisor, string RutaOriginal)>> ArmarDiccionarioPDF(string rutaCarpetaPDF)
        {
            DirectoryInfo directorio = new DirectoryInfo(rutaCarpetaPDF);

            var diccionario = new Dictionary<string, List<(string DenominacionEmisor, string RutaOriginal)>>();

            foreach (var archivo in directorio.GetFiles())
            {
                // Obtener el nombre del archivo y dividirlo en partes
                string nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(archivo.FullName);
                string[] partesNombre = nombreArchivo.Split('-', StringSplitOptions.RemoveEmptyEntries);

                string puntoVenta = partesNombre[0];
                string numeroComprobante = partesNombre[1];
                string denominacionEmisor = partesNombre[2];

                // Procesar las partes
                puntoVenta = EliminarCerosNoSignificativos(puntoVenta);
                numeroComprobante = EliminarCerosNoSignificativos(numeroComprobante);
                string comprobante = puntoVenta + numeroComprobante;

                // Armar diccionario con denominación y ruta original
                if (!diccionario.ContainsKey(comprobante))
                {
                    diccionario[comprobante] = new List<(string DenominacionEmisor, string RutaOriginal)>();
                }

                diccionario[comprobante].Add((denominacionEmisor, archivo.FullName)); // Guardar denominación y ruta original
            }

            return diccionario;
        }


        static void ModificarComprobantesHolistor(string rutaExcel, int filaEncabezadoHolistor)
        {
            using (var workbook = new XLWorkbook(rutaExcel))
            {
                var worksheet = workbook.Worksheet(1); // Abrimos la primera hoja del Excel

                int indiceColumnaComprobante = ObtenerIndiceColumna(worksheet, "Comprobante");
                int indiceColumnaFecha = ObtenerIndiceColumna(worksheet, "Fecha");
                if (indiceColumnaComprobante == -1)
                {
                    MessageBox.Show("Erro al encontrar la fila Comprobante");
                }
                else if (indiceColumnaFecha == -1)
                {
                    MessageBox.Show("Erro al encontrar la fila Fecha");
                }
                else
                {
                    foreach (var row in worksheet.RowsUsed())
                    {
                        string fecha = worksheet.Cell(row.RowNumber(), indiceColumnaFecha).Value.ToString().Trim();
                        if (fecha == "" || row.RowNumber() == 7)
                        {
                            continue;
                        }
                        else
                        {
                            string comprobante = worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Value.ToString();
                            comprobante = ProcesarNumerosHolistor(comprobante);
                            worksheet.Cell(row.RowNumber(), indiceColumnaComprobante).Value = comprobante;
                        }
                    }
                }

                workbook.SaveAs(rutaExcel);
            }

        }

        // Función para obtener el indíce donde esta una determinada columna en un Excel
        static int ObtenerIndiceColumna(IXLWorksheet worksheet, string nombreColumna)
        {
            int indiceColumna = -1;

            for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
            {
                string valor = worksheet.Cell(7, col).GetString();

                if (valor.Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                {
                    indiceColumna = col;
                    break;
                }
            }

            return indiceColumna;
        }

        // Función para procesar correctamente los números de comprobante
        static string ProcesarNumerosHolistor(string input)
        {
            // Eliminar caracteres que no sean números
            string numeros = Regex.Replace(input, @"\D", "");

            // Insertar guion en la posición 5, para separar punto de venta de comprobante
            numeros = numeros.Insert(4, "-");

            // Separar punto de venta y número de comprobante con un guion
            string[] partes = numeros.Split('-');
            string puntoDeVenta = partes[0];
            string numeroComprobante = partes[1];

            // Eliminar los ceros no significativos antes del primer número distinto de 0
            puntoDeVenta = EliminarCerosNoSignificativos(puntoDeVenta);
            numeroComprobante = EliminarCerosNoSignificativos(numeroComprobante);

            // Unir punto de venta y número de comprobante sin el guion
            return puntoDeVenta + numeroComprobante;
        }

        // Función para eliminar los ceros de los string que no aportan nada
        static string EliminarCerosNoSignificativos(string input)
        {
            // Encuentra el índice del primer dígito distinto de cero
            int indice = 0;
            while (indice < input.Length && input[indice] == '0')
            {
                indice++;
            }

            // Elimina los ceros no significativos antes del primer dígito distinto de cero
            return input.Substring(indice);
        }

        private void buttonSeleccionarExcelLibro_Click(object sender, EventArgs e)
        {
            SeleccionarArchivoExcel(textBoxRutaExcelHolistor);
        }

        private void buttonSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxRutaCarpeta.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
