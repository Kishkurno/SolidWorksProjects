using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidLab5
{
    public partial class Form1 : Form
    {
        SldWorks app;
        IModelDoc2 doc;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            double k = 1000.0;
            double[] l = new double[] { 18 / k, 44 / k, 36 / k, 74 / k, 85 / k, 100 / k, 45 / k, 150 / k, 85 / k, 20 / k, 30 / k };


            // создаём объект приложения солидворкс
            app = new SldWorks();
            // полный экран
            app.FrameState = (int)swWindowState_e.swWindowMaximized;

            app.Visible = true;


            // создаём файл детали 
            doc = (ModelDoc2)app.INewPart();

            doc.SetUnits((short)swLengthUnit_e.swMM, (short)swFractionDisplay_e.swDECIMAL, 0, 0, false);
            // открываем файл детали
            doc = app.IActiveDoc2;


            var sm = doc.SketchManager;
            var fm = doc.FeatureManager;

            string front = "Front Plane";
            string top = "Top Plane";
            double size_x = 0.01;
            double size_y = 0.01;
            double size_z = 0.01;

            int pref_toggle = (int)swUserPreferenceToggle_e.swInputDimValOnCreate;

            app.SetUserPreferenceToggle(pref_toggle, false);


            selectPlane(top);

            sm.InsertSketch(false);

            var rect = sm.CreateCenterRectangle(0, 0, 0, l[4] / 2, l[7] / 2, 0);
            //очистить буфер выбранных элементов
            doc.ClearSelection();

            //выбрать линию с прямоугольника
            var el = rect[0] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0, -size_y, 0.2 + l[4] / 2 + size_y);
            el.DeSelect();

            el = rect[1] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0 + l[7] / 2 + size_x, -size_y, 0);
            el.DeSelect();

            //вытянуть бобышку
            var feature = featureExtrusion(l[0]);
            doc.ClearSelection();

            //получить массив грани бобышки
            var faces = feature.GetFaces();
            //выбрать низ бобышки
            var ent = faces[0] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);



            rect = sm.CreateCenterRectangle(-l[4] / 2 + l[10] / 2, 0, 0, -l[4] / 2 + l[10], l[7] / 2, 0);

            el = rect[0] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0, -size_y, 0.2 + l[4] / 2 + size_y);
            el.DeSelect();

            el = rect[1] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0 + l[7] / 2 + size_x, -size_y, 0);
            el.DeSelect();

            var feature1 = featureExtrusion(l[5], true);
            doc.ClearSelection();

            //получить массив грани бобышки
            faces = feature.GetFaces();
            //выбрать вторую (вверх бобышки)
            ent = faces[1] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);

            rect = sm.CreateCenterRectangle((l[10] / 2), 0, 0, l[4] / 2, l[3] / 2, 0);

            el = rect[0] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0, -size_y, 0.2 + l[4] / 2 + size_y);
            el.DeSelect();

            el = rect[1] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0 + l[7] / 2 + size_x, -size_y, 0);
            el.DeSelect();
            //вытянуть бобышку
            var feature2 = featureExtrusion(l[6]);
            doc.ClearSelection();




            //получить массив грани бобышки
            faces = feature2.GetFaces();
            //выбрать вторую (вверх бобышки)
            ent = faces[1] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);


            sm.CreateLine(-l[4] / 2 + l[10], l[0] + l[6], 0, l[4] / 2, l[0], 0);
            doc.ClearSelection();
            sm.CreateLine(l[4] / 2, l[0] + l[6], 0, l[4] / 2, l[0], 0);
            doc.ClearSelection();

            sm.CreateLine(-l[4] / 2 + l[10], l[0] + l[6], 0, l[4] / 2, l[0] + l[6], 0);
            doc.ClearSelection();

            featureCut(l[3], false, swEndConditions_e.swEndCondThroughAll);
            doc.ClearSelection();

            //получить массив грани бобышки
            faces = feature.GetFaces();
            //выбрать вторую (вверх бобышки)
            ent = faces[0] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);


            //получить массив грани бобышки
            faces = feature.GetFaces();
            //выбрать низ бобышки
            ent = faces[2] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);

            rect = sm.CreateCenterRectangle(l[10] / 2, 0, 0, l[4] / 2, l[2] / 2, 0);

            el = rect[0] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0, -size_y, 0.2 + l[4] / 2 + size_y);
            el.DeSelect();

            el = rect[1] as SketchSegment;
            el.Select(true);
            doc.IAddDimension2(0 + l[7] / 2 + size_x, -size_y, 0);
            el.DeSelect();
            //вытянуть бобышку
            var feature4 = featureExtrusion(l[1],true);
            doc.ClearSelection();


            //получить массив грани бобышки
            faces = feature1.GetFaces();
            //выбрать вторую (вверх бобышки)
            ent = faces[0] as Entity;
            //выбрать верхнюю грань
            ent.Select(true);
            //добавить на нее эскиз
            sm.InsertSketch(false);

            sm.CreateLine(l[3]/2 , l[5], 0, l[7] / 2 , l[5], 0);
            doc.ClearSelection();
            sm.CreateLine(l[7] / 2, l[0] + l[6], 0, l[7] / 2, l[5], 0);
            doc.ClearSelection();

            sm.CreateLine(l[3] / 2, l[5], 0, l[7] / 2, l[0] + l[6], 0);
            doc.ClearSelection();

           

            sm.CreateLine(-l[3] / 2, l[5], 0, -l[7] / 2, l[5], 0);
            doc.ClearSelection();
            sm.CreateLine(-l[7] / 2, l[0] + l[6], 0,- l[7] / 2, l[5], 0);
            doc.ClearSelection();

            sm.CreateLine(-l[3] / 2, l[5], 0, -l[7] / 2, l[0] + l[6], 0);
            doc.ClearSelection();

            featureCut(l[10] - l[9], false, swEndConditions_e.swEndCondBlind);
            doc.ClearSelection();
        }



        //выбрать плоскость 
        private void selectPlane(string name, string obj = "PLANE")
        {
            doc.Extension.SelectByID2(name, obj, 0, 0, 0, false, 0, null, 0);
        }

        private Feature featureExtrusion(double deepth, bool dir = false)
        {
            return doc.FeatureManager.FeatureExtrusion2(true, false, dir,
                (int)swEndConditions_e.swEndCondBlind, (int)swEndConditions_e.swEndCondBlind,
                deepth, 0, false, false, false, false, 0, 0, false, false, false, false, true,
                true, true, 0, 0, false);
        }

        private Feature featureCut(double deepth, bool flip = false, swEndConditions_e mode = swEndConditions_e.swEndCondBlind)
        {
            return doc.FeatureManager.FeatureCut2(true, flip, false, (int)mode, (int)mode,
                deepth, 0, false, false, false, false, 0, 0, false, false, false, false, false,
                false, false, false, false, false);
        }
        private Feature featureExtrusionFromSerface(double deepth, bool dir = false)
        {
            return doc.FeatureManager.FeatureExtrusion2(true, false, dir,
                (int)swEndConditions_e.swEndCondOffsetFromSurface, (int)swEndConditions_e.swEndCondOffsetFromSurface,
                deepth, 0, false, false, false, false, 0, 0, false, false, false, false, true,
                true, true, 0, 0, false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

