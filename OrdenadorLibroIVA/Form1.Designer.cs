namespace OrdenadorLibroIVA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonProcesar = new Button();
            textBoxRutaExcelHolistor = new TextBox();
            buttonSeleccionarExcelLibro = new Button();
            textBoxRutaCarpeta = new TextBox();
            buttonSeleccionarCarpeta = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonProcesar
            // 
            buttonProcesar.BackColor = Color.BlueViolet;
            buttonProcesar.FlatStyle = FlatStyle.Popup;
            buttonProcesar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonProcesar.ForeColor = Color.White;
            buttonProcesar.Location = new Point(431, 40);
            buttonProcesar.Name = "buttonProcesar";
            buttonProcesar.Size = new Size(153, 90);
            buttonProcesar.TabIndex = 0;
            buttonProcesar.Text = "Procesar";
            buttonProcesar.UseVisualStyleBackColor = false;
            buttonProcesar.Click += buttonProcesar_Click;
            // 
            // textBoxRutaExcelHolistor
            // 
            textBoxRutaExcelHolistor.BackColor = Color.BlueViolet;
            textBoxRutaExcelHolistor.BorderStyle = BorderStyle.FixedSingle;
            textBoxRutaExcelHolistor.Location = new Point(24, 40);
            textBoxRutaExcelHolistor.Name = "textBoxRutaExcelHolistor";
            textBoxRutaExcelHolistor.Size = new Size(238, 23);
            textBoxRutaExcelHolistor.TabIndex = 1;
            // 
            // buttonSeleccionarExcelLibro
            // 
            buttonSeleccionarExcelLibro.BackColor = Color.BlueViolet;
            buttonSeleccionarExcelLibro.FlatStyle = FlatStyle.Popup;
            buttonSeleccionarExcelLibro.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSeleccionarExcelLibro.ForeColor = Color.White;
            buttonSeleccionarExcelLibro.Location = new Point(289, 40);
            buttonSeleccionarExcelLibro.Name = "buttonSeleccionarExcelLibro";
            buttonSeleccionarExcelLibro.Size = new Size(93, 23);
            buttonSeleccionarExcelLibro.TabIndex = 2;
            buttonSeleccionarExcelLibro.Text = "Seleccionar";
            buttonSeleccionarExcelLibro.UseVisualStyleBackColor = false;
            buttonSeleccionarExcelLibro.Click += buttonSeleccionarExcelLibro_Click;
            // 
            // textBoxRutaCarpeta
            // 
            textBoxRutaCarpeta.BackColor = Color.BlueViolet;
            textBoxRutaCarpeta.BorderStyle = BorderStyle.FixedSingle;
            textBoxRutaCarpeta.Location = new Point(24, 107);
            textBoxRutaCarpeta.Name = "textBoxRutaCarpeta";
            textBoxRutaCarpeta.Size = new Size(238, 23);
            textBoxRutaCarpeta.TabIndex = 3;
            // 
            // buttonSeleccionarCarpeta
            // 
            buttonSeleccionarCarpeta.BackColor = Color.BlueViolet;
            buttonSeleccionarCarpeta.FlatStyle = FlatStyle.Popup;
            buttonSeleccionarCarpeta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSeleccionarCarpeta.ForeColor = Color.White;
            buttonSeleccionarCarpeta.Location = new Point(289, 107);
            buttonSeleccionarCarpeta.Name = "buttonSeleccionarCarpeta";
            buttonSeleccionarCarpeta.Size = new Size(93, 23);
            buttonSeleccionarCarpeta.TabIndex = 4;
            buttonSeleccionarCarpeta.Text = "Seleccionar";
            buttonSeleccionarCarpeta.UseVisualStyleBackColor = false;
            buttonSeleccionarCarpeta.Click += buttonSeleccionarCarpeta_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Purple;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(24, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(117, 22);
            textBox1.TabIndex = 5;
            textBox1.Text = "Ruta del Excel";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Purple;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(24, 79);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(151, 22);
            textBox2.TabIndex = 6;
            textBox2.Text = "Ruta de la carpeta";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(24, 145);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(560, 165);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(610, 322);
            Controls.Add(pictureBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(buttonSeleccionarCarpeta);
            Controls.Add(textBoxRutaCarpeta);
            Controls.Add(buttonSeleccionarExcelLibro);
            Controls.Add(textBoxRutaExcelHolistor);
            Controls.Add(buttonProcesar);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonProcesar;
        private TextBox textBoxRutaExcelHolistor;
        private Button buttonSeleccionarExcelLibro;
        private TextBox textBoxRutaCarpeta;
        private Button buttonSeleccionarCarpeta;
        private TextBox textBox1;
        private TextBox textBox2;
        private PictureBox pictureBox1;
    }
}
