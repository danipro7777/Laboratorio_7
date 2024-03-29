﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_7
{
    public partial class Form1 : Form
    {
        List<Propiedad> propiedades = new List<Propiedad>();
        List<Propietario> propietarios = new List<Propietario>();
        List<Resumen> resumen = new List<Resumen>();
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarPropiedades()
        {
            FileStream stream = new FileStream("Propiedades.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Propiedad propiedad = new Propiedad();
                propiedad.numeroCasa = reader.ReadLine();
                propiedad.dpi = reader.ReadLine();
                propiedad.cuota = Convert.ToDecimal(reader.ReadLine());
                propiedades.Add(propiedad);

            }
            reader.Close();
        }

        private void CargarPropietarios()
        {
            FileStream stream = new FileStream("Propietarios.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Propietario propietario = new Propietario();
                propietario.dpi = reader.ReadLine();
                propietario.nombre = reader.ReadLine();
                propietario.apellido = reader.ReadLine();


                propietarios.Add(propietario);
            }
            reader.Close();
        }

        private void CargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = resumen;
            dataGridView1.Refresh();
        }

        private void buttonResumen_Click(object sender, EventArgs e)
        {
            CargarPropiedades();
            CargarPropietarios();

            for (int i = 0; i < propiedades.Count; i++)
            {
                for (int j = 0; j < propietarios.Count; j++)
                {
                    if (propiedades[i].dpi == propietarios[j].dpi)
                    {
                        Resumen datoresumen = new Resumen();
                        datoresumen.nombre = propietarios[j].nombre;
                        datoresumen.apellido = propietarios[j].apellido;
                        datoresumen.numeroCasa = propiedades[i].numeroCasa;
                        datoresumen.cuota = propiedades[i].cuota;

                        resumen.Add(datoresumen);
                    }
                }
            }
            CargarGrid();
            buttonOrdenar.Enabled = true;
        }

        private void buttonOrdenar_Click(object sender, EventArgs e)
        {
            resumen = resumen.OrderBy(c => c.cuota).ToList();
            CargarGrid();

            buttonMayormenor.Enabled = true;
        }

        private void buttonMayormenor_Click(object sender, EventArgs e)
        {
            labelMenor.Text = resumen[0].cuota.ToString();
            int cuantos = resumen.Count();
            labelMayor.Text = resumen[cuantos - 1].cuota.ToString();

            labelNombre.Text = resumen[cuantos - 1].nombre+", "+resumen[cuantos-1].apellido;

        }

        private void labelMenor_Click(object sender, EventArgs e)
        {

        }
    }
}
