﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hamilton_Kane_GOL
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[5, 5];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            //timer.Enabled = true; // start timer running
        }

        // Calculate the next generation of cells
        // Iterate through current universe
        // Count the neighbors
        // Get the neighbor count for each cell, can't do much without this
        // Apply the rules of GOL
        // Turn cells on and off in the second array
        // When done, swap the two arrays
        private void NextGeneration()
        {


            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                float cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float y = e.Y / cellHeight;

                // Toggle the cell's state
                int x1 = (int)(x);
                int y1 = (int)(y);
                universe[x1, y1] = !universe[x1, y1];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            generations = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Start timer
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Pause timer
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // Next timer
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NextGeneration();
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        //Color - Back Color
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cdlg = new ColorDialog();
            cdlg.Color = graphicsPanel1.BackColor;
            if (DialogResult.OK == cdlg.ShowDialog())
            {
                graphicsPanel1.BackColor = cdlg.Color;
                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        //Color - Cell Color
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cdlg = new ColorDialog();
            cdlg.Color = cellColor;
            if (DialogResult.OK == cdlg.ShowDialog())
            {
                cellColor = cdlg.Color;
                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        //Color - Grid Color
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cdlg = new ColorDialog();
            cdlg.Color = gridColor;
            if (DialogResult.OK == cdlg.ShowDialog())
            {
                gridColor = cdlg.Color;
                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        //Color - Grid x10 Color
        private void gridX10ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Randomize - From Seed
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomizeBox rb = new RandomizeBox();

            if (DialogResult.OK == rb.ShowDialog())
            {

            }
        }
    }
}
