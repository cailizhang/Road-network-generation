using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
namespace LinkIdentification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string shapeFileFullName = string.Empty;
        IFeature f1 = null;
        IPoint centerpoint1 = null;

        IFeature f11 = null;
        IPoint centerpoint11 = null;

        List<IFeature> label = new List<IFeature>();
        List<IFeature> labe2 = new List<IFeature>();
        List<IFeature> labe3 = new List<IFeature>();

        private void 加载shpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFdlg = new OpenFileDialog();
            OpenFdlg.Title = "加载shp文件";
            OpenFdlg.Filter = "Shape格式文件(*.shp)|*.shp";
            OpenFdlg.ShowDialog();
            string strFileName = OpenFdlg.FileName;
            if (strFileName == string.Empty)
                return;
            string pathName = System.IO.Path.GetDirectoryName(strFileName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(strFileName);
            axMapControl1.AddShapeFile(pathName, filename);





        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            axTOCControl1.SetBuddyControl(axMapControl1);


        }

        #region 创建面shp文件
        private IFeatureLayer Createpolygonshp(string outfileNamePath, List<IFeature> gc)
        {
            string folder = System.IO.Path.GetDirectoryName(outfileNamePath);
            string shapeFileFullName = System.IO.Path.GetFileName(outfileNamePath);


            string name = System.IO.Path.GetFileNameWithoutExtension(outfileNamePath);

            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IWorkspace pWS = pWSF.OpenFromFile(folder, 0);
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            if (File.Exists(outfileNamePath))
            {
                IFeatureClass featureClass = pFWS.OpenFeatureClass(shapeFileFullName);
                IDataset pDataset = (IDataset)featureClass;
                pDataset.Delete();


            }

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);




            IFeatureClass pFeatureClass;
            pFeatureClass = pFWS.CreateFeatureClass(shapeFileFullName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            for (int i = 0; i < gc.Count; i++)
            {

                IFeature pFeature = pFeatureClass.CreateFeature();

                pFeature.Shape = gc[i].Shape;


                pFeature.Store();
            }
            IFeatureLayer pFeaturelayer = new FeatureLayerClass();
            pFeaturelayer.FeatureClass = pFeatureClass;
            pFeaturelayer.Name = name;
            return pFeaturelayer;


        }

        #endregion
       
        #region 创建线shp文件
        private IFeatureLayer Createpolylineshp2(string outfileNamePath, List<IFeature> gc)
        {
            string folder = System.IO.Path.GetDirectoryName(outfileNamePath);
            string shapeFileFullName = System.IO.Path.GetFileName(outfileNamePath);


            string name = System.IO.Path.GetFileNameWithoutExtension(outfileNamePath);

            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IWorkspace pWS = pWSF.OpenFromFile(folder, 0);
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            if (File.Exists(outfileNamePath))
            {
                IFeatureClass featureClass = pFWS.OpenFeatureClass(shapeFileFullName);
                IDataset pDataset = (IDataset)featureClass;
                pDataset.Delete();


            }

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);




            IFeatureClass pFeatureClass;
            pFeatureClass = pFWS.CreateFeatureClass(shapeFileFullName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            for (int i = 0; i < gc.Count; i++)
            {

                IFeature pFeature = pFeatureClass.CreateFeature();

                pFeature.Shape = gc[i].Shape;


                pFeature.Store();
            }
            IFeatureLayer pFeaturelayer = new FeatureLayerClass();
            pFeaturelayer.FeatureClass = pFeatureClass;
            pFeaturelayer.Name = name;
            return pFeaturelayer;


        }

        #endregion


        









        #region 创建线shp文件

        private IFeatureLayer Createpolylineshp(string outfileNamePath, List<ISegmentCollection> gc)
        {
            string folder = System.IO.Path.GetDirectoryName(outfileNamePath);
            string shapeFileFullName = System.IO.Path.GetFileName(outfileNamePath);


            string name = System.IO.Path.GetFileNameWithoutExtension(outfileNamePath);

            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IWorkspace pWS = pWSF.OpenFromFile(folder, 0);
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            if (File.Exists(outfileNamePath))
            {
                IFeatureClass featureClass = pFWS.OpenFeatureClass(shapeFileFullName);
                IDataset pDataset = (IDataset)featureClass;
                pDataset.Delete();


            }

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);




            IFeatureClass pFeatureClass;
            pFeatureClass = pFWS.CreateFeatureClass(shapeFileFullName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");





            for (int i = 0; i < gc.Count; i++)
            {
                object oo = Type.Missing;
                IGeometryCollection g = new PolylineClass();
                g.AddGeometry(gc[i] as IGeometry, ref oo, ref oo);

                IFeature pFeature = pFeatureClass.CreateFeature();

                pFeature.Shape = g as IPolyline;



                pFeature.Store();
            }

            IFeatureLayer pFeaturelayer = new FeatureLayerClass();
            pFeaturelayer.FeatureClass = pFeatureClass;
            pFeaturelayer.Name = name;
            return pFeaturelayer;


        }


        #endregion



        private IFeatureLayer Createpolylineshp1(string outfileNamePath, List<IPolyline> gc)
        {
            string folder = System.IO.Path.GetDirectoryName(outfileNamePath);
            string shapeFileFullName = System.IO.Path.GetFileName(outfileNamePath);


            string name = System.IO.Path.GetFileNameWithoutExtension(outfileNamePath);

            IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
            IWorkspace pWS = pWSF.OpenFromFile(folder, 0);
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            if (File.Exists(outfileNamePath))
            {
                IFeatureClass featureClass = pFWS.OpenFeatureClass(shapeFileFullName);
                IDataset pDataset = (IDataset)featureClass;
                pDataset.Delete();


            }

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            pFieldEdit.GeometryDef_2 = pGeometryDef;
            pFieldsEdit.AddField(pField);




            IFeatureClass pFeatureClass;
            pFeatureClass = pFWS.CreateFeatureClass(shapeFileFullName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");





            for (int i = 0; i < gc.Count; i++)
            {/*
                object oo = Type.Missing;
                IGeometryCollection g = new PolylineClass();
                g.AddGeometry(gc[i] as IGeometry, ref oo, ref oo);
             
                IFeature pFeature = pFeatureClass.CreateFeature();
               
                pFeature.Shape = g as IPolyline ;
                */



                IFeature pFeature = pFeatureClass.CreateFeature();

                pFeature.Shape = gc[i] as IPolyline;


                pFeature.Store();
            }

            IFeatureLayer pFeaturelayer = new FeatureLayerClass();
            pFeaturelayer.FeatureClass = pFeatureClass;
            pFeaturelayer.Name = name;
            return pFeaturelayer;


        }







       



        #region 求邻近三角形及中点
        private Tuple<IFeature, IPoint> approximately(IFeature pf, IFeature f1, IFeatureClass fc)
        {
            IFeature feature = null;
            IPoint centerpoint = new PointClass();
            Tuple<IFeature, IPoint> tup = null;

            ITopologicalOperator top = f1.Shape as ITopologicalOperator;
            for (int i = 0; i < fc.FeatureCount(null); i++)
            {
                IFeature feature1 = fc.GetFeature(i);
                if (pf == null)
                {
                    if ((feature1.get_Value(2).ToString()) != (f1.get_Value(2).ToString()))
                    {
                        IPolyline result = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension) as IPolyline;
                        if (result != null)
                        {



                            if (result.IsEmpty)
                            {
                                continue;
                            }

                            else
                            {

                                feature = feature1;
                                IPoint p1 = result.FromPoint;
                                IPoint p2 = result.ToPoint;

                                centerpoint.X = (p1.X + p2.X) / 2;
                                centerpoint.Y = (p1.Y + p2.Y) / 2;
                            }
                        }

                    }

                }
                else
                {
                    if (((feature1.get_Value(2).ToString()) != (f1.get_Value(2).ToString())) && (feature1.get_Value(2).ToString() != pf.get_Value(2).ToString()))
                    {
                        IPolyline result = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension) as IPolyline;
                        if (result != null)
                        {



                            if (result.IsEmpty)
                            {
                                continue;
                            }

                            else
                            {

                                feature = feature1;
                                IPoint p1 = result.FromPoint;
                                IPoint p2 = result.ToPoint;

                                centerpoint.X = (p1.X + p2.X) / 2;
                                centerpoint.Y = (p1.Y + p2.Y) / 2;
                            }
                        }

                    }


                }

            }



            return tup = new Tuple<IFeature, IPoint>(feature, centerpoint);



        }


        #endregion



        #region 求第三类三角形的所有邻近三角形
        private List<IFeature> Allapproximately(IFeature f1, IFeatureClass fc)
        {

            List<IFeature> all = new List<IFeature>();



            ITopologicalOperator top = f1.Shape as ITopologicalOperator;
            for (int i = 0; i < fc.FeatureCount(null); i++)
            {
                IFeature feature1 = fc.GetFeature(i);

                if ((feature1.get_Value(2).ToString()) != (f1.get_Value(2).ToString()))
                {
                    IPolyline result = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension) as IPolyline;
                    if (result != null)
                    {



                        if (result.IsEmpty)
                        {
                            continue;
                        }

                        else
                        {

                            all.Add(feature1);

                        }
                    }

                }

            }

            return all;

        }






        #endregion


        #region 求中点

        private IPoint getcenter(ISegment result)
        {

            IPoint p1 = result.FromPoint;
            IPoint p2 = result.ToPoint;
            IPoint centerpoint = new PointClass();
            centerpoint.X = (p1.X + p2.X) / 2;
            centerpoint.Y = (p1.Y + p2.Y) / 2;

            return centerpoint;
        }

        #endregion

        #region 判断feature是否存在某一List
        private bool exist(IFeature f, List<IFeature> gc)
        {

            bool result = false;
            int count = 0;
            for (int i = 0; i < gc.Count; i++)
            {
                if (f.get_Value(2).ToString() == gc[i].get_Value(2).ToString())
                    count++;


            }
            if (count > 0)
                result = true;

            return result;



        }


        #endregion


        #region 判断point是否存在某一List
        private bool existpoint(IPoint f, IFeatureClass gc)
        {

            bool result = false;
            int count = 0;
          
            for (int i = 0; i < gc.FeatureCount(null); i++)
            {

                IFeature feature = gc.GetFeature(i);
                IPoint p = feature.Shape as IPoint;
                if (dis(p, f) ==0)
                    count++;


            }
            if (count > 0)
                result = true;

            return result;



        }


        #endregion



        #region 判断线段是否存在某个list
        private bool existpolyline(List<IPoint> p1, List<ISegmentCollection> pPolyline)
        {
            bool result = false;
            int count = 0;

            for (int ii = 0; ii < pPolyline.Count; ii++)
            {
                IPath g = pPolyline[ii] as IPath;

                IPoint fp = g.FromPoint;
                IPoint tp = g.ToPoint;

                if ((p1[0].X == tp.X) && (p1[0].Y == tp.Y) && (p1[1].X == fp.X) && (p1[1].Y == fp.Y))
                {
                    count++;
                    break;
                }
            }

            if (count == 1)
                result = true;

            return result;


        }

        #endregion

        #region 判断线段是否存在某个list
        private bool existpoly(IPolyline p1, IList<IPolyline> pPolyline)
        {
            bool result = false;
            int count = 0;

            for (int ii = 0; ii < pPolyline.Count; ii++)
            {
                IPolyline g = pPolyline[ii];




                IPoint fp = g.FromPoint;
                IPoint tp = g.ToPoint;

                if ((p1.FromPoint.X == fp.X) && (p1.ToPoint.Y == tp.Y) && (p1.FromPoint.Y == fp.Y) && (p1.ToPoint.X == tp.X))
                {
                    count++;
                    break;
                }

                if ((p1.FromPoint.X == tp.X) && (p1.ToPoint.Y == fp.Y) && (p1.FromPoint.Y == tp.Y) && (p1.ToPoint.X == fp.X))
                {
                    count++;
                    break;
                }

            }

            if (count == 1)
                result = true;

            return result;


        }

        #endregion


        # region 求球面距离

        private   double dis(IPoint point1, IPoint point2)
        {
            double ER = 6378137;
            double distacne;
            double tmp00 = 0.5 * (point1.Y - point2.Y) * Math.PI / 180;
            double tmp01 = 0.5 * (point1.X - point2.X) * Math.PI / 180;


            double tmp1 = Math.Sin(Math.Pow(tmp00, 2));
            double tmp2 = Math.Sin(Math.Pow(tmp01, 2));
            double tmp3 = Math.Cos(point1.Y * Math.PI / 180) * Math.Cos(point2.Y * Math.PI / 180);
            distacne = 2.0 * ER * Math.Asin(Math.Sqrt(tmp1 + tmp2 * tmp3));

            return distacne;



        }


        # endregion

       

   

        #region 求方位角
        private double angle(IPoint p1, IPoint p2)
        {
            
            double dy=(p2.X - p1.X);
            double dx = (p2.Y - p1.Y);
            /*
            if  (dx >0 && dy==0)
                Angle=0;
            
            if  (dx ==0 && dy>0)
                Angle=90;
            if  (dx <0 && dy==0)
                Angle=180;


            if (dx == 0 && dy < 0)
                Angle = 270;
           */

                double a = dy / dx;
                double b = Math.Atan(a) * 180 / Math.PI;
                double Angle = b;
                if ((dx > 0|| dx ==0) && (dy > 0||dy ==0))
                {
                    Angle = b;
                }
                if (dx < 0 && dy > 0)
                {
                    Angle = 180 + b;

                }
                if (dx < 0 && (dy < 0|| dy ==0))
                {
                    Angle = 180 + b;

                }
                if ((dx > 0 || dx ==0)&& (dy < 0||dy ==0))
                {

                    Angle = 360 + b;
                }

            

            return Angle;



        }


        #endregion



        #region
        private bool with(IPoint p1, IPoint p2, double radius)
        {
            bool result = false;

            double distance=dis (p1 ,p2 );

            if (distance < radius)
            {
                result = true;
            }
            return result;
        
        }

        #endregion

#region
        private double tometer(double x)
        {
      
             double det = 0.0001;
            IPoint refPt=new PointClass ();
//芝加哥，[-87.6,41.8] 武汉， [114.3,30.5]
            refPt.X =114.3;
                refPt.Y=30.5;

            IPoint otherPt=new PointClass ();
            otherPt.X = 114.3+ det;
                otherPt.Y=30.5;

        
   
    
   double  dist = dis(refPt, otherPt);
   double  degPerMet = det/dist;
   return x / degPerMet;
        
        }
#endregion

  


        private IList<IFeature> extractpoint(int start, int end, IFeatureClass featureclass0, IList<IFeature> featurelist) 
        {
            IPoint intersection1 = featureclass0.GetFeature(start).Shape as IPoint;
            IPoint intersection2 = featureclass0.GetFeature(end).Shape as IPoint;

            IPoint center = new PointClass();
            center.X = (intersection1.X + intersection2.X) / 2;
            center.Y = (intersection1.Y + intersection2.Y) / 2;
            double Cdis = dis(intersection1, intersection2) / 2;


            IList<IFeature> result = new List<IFeature>();

            int m = 0;

            for (int nn = 0; nn < featurelist.Count; nn++)
            {
                IList<int> lab1 = new List<int>();
                IList<int> lab2 = new List<int>();

                if (nn != m)
                {
                    continue;
                }

                center.X = (intersection1.X + intersection2.X) / 2;
                center.Y = (intersection1.Y + intersection2.Y) / 2;

                IPoint P = featurelist[nn].Shape as IPoint;
                //交叉口范围
                string dis1 = featureclass0.GetFeature(start).get_Value(6).ToString();
                double distance = Convert.ToDouble(dis1);
                double azimuth = angle(intersection1, intersection2);



                string dis2 = featureclass0.GetFeature(end).get_Value(6).ToString();
                double distance2 = Convert.ToDouble(dis2);
                double a1 = Convert.ToDouble(featurelist[nn].get_Value(7).ToString());

                double dif = 0;
                if (Math.Abs(a1 - azimuth) < (360 - Math.Abs(a1 - azimuth)))
                {
                    dif = Math.Abs(a1 - azimuth);
                }
                else
                {
                    dif = 360 - Math.Abs(a1 - azimuth);

                }

                if (with(intersection1, P, tometer(distance)) == true && with(center, P, Cdis) == true && dif < 60)
                {
                    lab1.Add(nn);


                }


                for (int nnn = nn + 1; nnn < featurelist.Count; nnn++)
                {
                    if (featurelist[nnn].get_Value(2).ToString() == featurelist[nnn - 1].get_Value(2).ToString())
                    {
                        IPoint P2 = featurelist[nnn].Shape as IPoint;
                        double aa1 = Convert.ToDouble(featurelist[nnn].get_Value(7).ToString());
                        double different = 0;
                        if (Math.Abs(aa1 - azimuth) < (360 - Math.Abs(aa1 - azimuth)))
                        {
                            different = Math.Abs(aa1 - azimuth);
                        }
                        else
                        {
                            different = 360 - Math.Abs(aa1 - azimuth);

                        }
                        if (with(center, P2, Cdis) == true && with(intersection1, P2, tometer(distance)) == true &&different < 60)
                        {

                            lab1.Add(nnn);

                        }


                        if (with(center, P2, Cdis) == true && with(intersection2, P2, tometer(distance2)) == true && different < 60)
                        {

                            lab2.Add(nnn);

                        }



                    }
                    else
                    {

                        if (lab1.Count != 0 && lab2.Count != 0)
                        {
                            double wholedistance = 0;
                            double average = Convert.ToDouble(featurelist[lab1[0]].get_Value(6).ToString()); ;
                            double count=1;
                            double t1 = Convert.ToDouble(featurelist[lab1[0]].get_Value(3).ToString());
                            double angle1 = Convert.ToDouble(featurelist[lab1[0]].get_Value(7).ToString());
                            double t2 = Convert.ToDouble(featurelist[lab2[lab2.Count - 1]].get_Value(3).ToString());
                            double angle2 = Convert.ToDouble(featurelist[lab2[lab2.Count - 1]].get_Value(7).ToString());
                            IPoint point0 = featurelist[lab1[0]].Shape as IPoint;
                            double wholedistance0 = dis(intersection1, point0);
                            for (int k = lab1[0]; k < lab2[lab2.Count - 1]; k++)
                            {
                                average = average +Convert .ToDouble ( featurelist[k + 1].get_Value(6).ToString ());
                                IPoint point1 = featurelist[k].Shape as IPoint;
                                IPoint point2 = featurelist[k + 1].Shape as IPoint;
                                double adis = dis(point1, point2);
                                wholedistance = wholedistance + adis;
                                count =count +1;

                            }
                            IPoint pointn = featurelist[lab2[lab2.Count - 1]].Shape as IPoint;
                            double wholedistancen = dis(intersection2, pointn);
                            double Wholedistance = wholedistance + wholedistance0 + wholedistancen;
                            average = average / count;
                            double d = average * (t2 - t1);
                            double different = 0;
                            if (Math.Abs(angle1 - angle2) < (360 - Math.Abs(angle1 - angle2)))
                            {
                                different = Math.Abs(angle1 - angle2);
                            }
                            else
                            {
                                different = 360 - Math.Abs(angle1 - angle2);

                            }

                            if ((Wholedistance < 2 * 1.2 * Cdis) & (d < 2 * 1.2 * Cdis) & (different < 60))
                            {

                                for (int k = lab1[0]; k < lab2[lab2.Count - 1] + 1; k++)
                                {

                                    result.Add(featurelist[k]);

                                }


                            }


                        }

                        m = nnn;
                        break;
                    }


                    if (nnn == featurelist.Count - 1)
                    {

                        if (lab1.Count != 0 && lab2.Count != 0)
                        {
                            double wholedistance = 0;
                            double average = Convert.ToDouble(featurelist[lab1[0]].get_Value(6).ToString());
                            double count = 1;
                            double t1 = Convert.ToDouble(featurelist[lab1[0]].get_Value(3).ToString());
                            double angle1 = Convert.ToDouble(featurelist[lab1[0]].get_Value(7).ToString());
                            double t2 = Convert.ToDouble(featurelist[lab2[lab2.Count - 1]].get_Value(3).ToString());
                            double angle2 = Convert.ToDouble(featurelist[lab2[lab2.Count - 1]].get_Value(7).ToString());
                            IPoint point0 = featurelist[lab1[0]].Shape as IPoint;
                           double  wholedistance0 = dis(intersection1, point0);
                            for (int k = lab1[0]; k < lab2[lab2.Count - 1]; k++)
                            {
                                average = average + Convert.ToDouble(featurelist[k+1].get_Value(6).ToString());
                                IPoint point1 = featurelist[k].Shape as IPoint;
                                IPoint point2 = featurelist[k + 1].Shape as IPoint;
                                double adis = dis(point1, point2);
                                wholedistance = wholedistance + adis;
                                count = count + 1;

                            }
                            IPoint pointn = featurelist[lab2[lab2.Count - 1]].Shape as IPoint;
                           double  wholedistancen = dis(intersection2, pointn) ;
                           double Wholedistance = wholedistance + wholedistance0 + wholedistancen;
                            average = average / count;
                            double d = average * (t2 - t1);
                            double different = 0;
                            if (Math.Abs(angle1 - angle2) < (360 - Math.Abs(angle1 - angle2)))
                            {
                                different = Math.Abs(angle1 - angle2);
                            }
                            else
                            {
                                different = 360 - Math.Abs(angle1 - angle2);

                            }

                            if ((Wholedistance < 2 * 1.2 * Cdis) & (d < 2 * 1.2 * Cdis) & (different < 60))
                            {

                                for (int k = lab1[0]; k < lab2[lab2.Count - 1] + 1; k++)
                                {

                                    result.Add(featurelist[k]);

                                }


                            }


                        }



                    }








                }



            }

            return result;
        }



        private void 提取子轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);
            ILayer player3 = pmap.get_Layer(3);

            IFeatureClass featureclass0 = null;//候选link
            IFeatureClass featureclass1 = null;//直线link
            IFeatureLayer featurelayer0 = null;
            IFeatureLayer featurelayer1 = null;
            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//交叉点
            IFeatureLayer featurelayer3 = player3 as IFeatureLayer;
            IFeatureClass featureclass3 = featurelayer3.FeatureClass;//轨迹点

            if (player0.Name == "候选link")
            {
                //候选link
                featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }


            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                IPolyline  se = featureclass0.GetFeature(i).Shape as IPolyline ;

                IPoint Spoint = se.FromPoint;
                IPoint Epoint = se.ToPoint;
                int start=0;
                int end=0;
                for (int j = 0; j < featureclass2.FeatureCount(null); j++)
                {
                    IPoint p = featureclass2.GetFeature(j).Shape  as IPoint;
                    if (Spoint.X == p.X && Spoint .Y  == p.Y )
                    {
                        start = j;
                    }

                    if (Epoint .Y  == p.Y  && Epoint.X == p.X)
                    {
                        end  = j;
                    }
                }

                int lab = 0;
                

                for (int m = 0; m < featureclass1.FeatureCount(null); m++)
                {

                    int s1 =Convert .ToInt32 ( featureclass1.GetFeature(m).get_Value(7).ToString ());
                    int e1 =Convert .ToInt32 ( featureclass1.GetFeature(m).get_Value(8).ToString ());

                    if (start == s1 && end == e1)
                    {

                        if (featureclass1.GetFeature(m).get_Value(11).ToString() == "双")
                        {
                            lab = 1;
                            break;
                        
                        }

                        if (featureclass1.GetFeature(m).get_Value(11).ToString() == "单")
                        {
                            lab = 2;//se
                            break;

                        }
                    }

                    else if (start == e1 && end == s1)
                    {

                        if (featureclass1.GetFeature(m).get_Value(11).ToString() == "双")
                        {
                            lab = 1;
                            break;

                        }

                        if (featureclass1.GetFeature(m).get_Value(11).ToString() == "单")
                        {
                            lab = 3;//es
                            break;

                        }
                    }

                    else
                    {
                        lab = 4;//无
                    
                    
                    }
                
                }

                if (lab == 1)
                {
                    continue;
                
                }

               else  if (lab == 2)
                { 
                //判断es

                    IPoint intersection1 = featureclass2.GetFeature(end).Shape as IPoint;
                    IPoint intersection2 = featureclass2.GetFeature(start).Shape as IPoint;

                    IPoint center = new PointClass();
                    center.X = (intersection1.X + intersection2.X) / 2;
                    center.Y = (intersection1.Y + intersection2.Y) / 2;
                    double Cdis = dis(intersection1, intersection2) / 2;
                    IList<IFeature> featurelist = new List<IFeature>();

                   

                    for (int n = 0; n < featureclass3.FeatureCount(null); n++)
                    {
                        IPoint P = featureclass3.GetFeature(n).Shape as IPoint;

                        if (with(center, P, Cdis) == true)
                        {
                            featurelist.Add(featureclass3.GetFeature(n));

                        }



                    }
                    IList<IFeature> result = new List<IFeature>();
                    result  = extractpoint(end, start, featureclass2, featurelist);

                    if (result.Count > 0)
                    {
                        string ID = end  + "-" + start ;





                        string xjFileFullPath1 = "D:\\data\\提取结果1\\武汉\\trip4\\" + end  + "-" + start  + ".txt";

                        System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                        StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                        for (int iii = 0; iii < result.Count; iii++)
                        {
                            IPoint pp = result[iii].Shape as IPoint;

                            string pointline = result[iii].get_Value(2).ToString() + "," + pp.X.ToString() + "," + pp.Y.ToString() + "," + result[iii].get_Value(7).ToString();



                            xjExportStreamWriter1.WriteLine(pointline);
                        }

                        xjExportStreamWriter1.Flush();//清空缓冲区

                        xjExportStreamWriter1.Close();//关闭流

                        xjExportFileStream1.Close();









                    }

                
                
                }
                else if (lab == 3)
                { 

                    //判断se

                    IPoint intersection1 = featureclass2.GetFeature(start ).Shape as IPoint;
                    IPoint intersection2 = featureclass2.GetFeature(end).Shape as IPoint;

                    IPoint center = new PointClass();
                    center.X = (intersection1.X + intersection2.X) / 2;
                    center.Y = (intersection1.Y + intersection2.Y) / 2;
                    double Cdis = dis(intersection1, intersection2) / 2;
                    IList<IFeature> featurelist = new List<IFeature>();



                    for (int n = 0; n < featureclass3.FeatureCount(null); n++)
                    {
                        IPoint P = featureclass3.GetFeature(n).Shape as IPoint;

                        if (with(center, P, Cdis) == true)
                        {
                            featurelist.Add(featureclass3.GetFeature(n));

                        }



                    }
                    IList<IFeature> result = new List<IFeature>();
                    result = extractpoint(start , end , featureclass2, featurelist);

                    if (result.Count > 0)
                    {
                        string ID = start  + "-" + end ;





                        string xjFileFullPath1 = "D:\\data\\提取结果1\\武汉\\trip4\\" + start  + "-" + end  + ".txt";

                        System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                        StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                        for (int iii = 0; iii < result.Count; iii++)
                        {
                            IPoint pp = result[iii].Shape as IPoint;

                            string pointline = result[iii].get_Value(2).ToString() + "," + pp.X.ToString() + "," + pp.Y.ToString() + "," + result[iii].get_Value(7).ToString();



                            xjExportStreamWriter1.WriteLine(pointline);
                        }

                        xjExportStreamWriter1.Flush();//清空缓冲区

                        xjExportStreamWriter1.Close();//关闭流

                        xjExportFileStream1.Close();









                    }









                
                }
                else if (lab == 4)
                { 
                //判断se,es


                    IPoint intersection1 = featureclass2.GetFeature(start).Shape as IPoint;
                    IPoint intersection2 = featureclass2.GetFeature(end).Shape as IPoint;

                    IPoint center = new PointClass();
                    center.X = (intersection1.X + intersection2.X) / 2;
                    center.Y = (intersection1.Y + intersection2.Y) / 2;
                    double Cdis = dis(intersection1, intersection2) / 2;
                    IList<IFeature> featurelist = new List<IFeature>();



                    for (int n = 0; n < featureclass3.FeatureCount(null); n++)
                    {
                        IPoint P = featureclass3.GetFeature(n).Shape as IPoint;

                        if (with(center, P, Cdis) == true)
                        {
                            featurelist.Add(featureclass3.GetFeature(n));

                        }



                    }
                    IList<IFeature> result = new List<IFeature>();
                    result = extractpoint(start, end, featureclass2, featurelist);

                    IList<IFeature> result1 = new List<IFeature>();
                    result1 = extractpoint(end , start , featureclass2, featurelist);


                    if (result.Count > 0)
                    {
                        string ID = start + "-" + end;





                        string xjFileFullPath1 = "D:\\data\\提取结果1\\武汉\\trip4\\" + start + "-" + end + ".txt";

                        System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                        StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                        for (int iii = 0; iii < result.Count; iii++)
                        {
                            IPoint pp = result[iii].Shape as IPoint;

                            string pointline = result[iii].get_Value(2).ToString() + "," + pp.X.ToString() + "," + pp.Y.ToString() + "," + result[iii].get_Value(7).ToString();



                            xjExportStreamWriter1.WriteLine(pointline);
                        }

                        xjExportStreamWriter1.Flush();//清空缓冲区

                        xjExportStreamWriter1.Close();//关闭流

                        xjExportFileStream1.Close();









                    }




                    if (result1.Count > 0)
                    {
                        string ID = end + "-" + start ;





                        string xjFileFullPath1 = "D:\\data\\提取结果1\\武汉\\trip4\\" + end  + "-" + start  + ".txt";

                        System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                        StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                        for (int iii = 0; iii < result1.Count; iii++)
                        {
                            IPoint pp = result1[iii].Shape as IPoint;

                            string pointline = result1[iii].get_Value(2).ToString() + "," + pp.X.ToString() + "," + pp.Y.ToString() + "," + result1[iii].get_Value(7).ToString();



                            xjExportStreamWriter1.WriteLine(pointline);
                        }

                        xjExportStreamWriter1.Flush();//清空缓冲区

                        xjExportStreamWriter1.Close();//关闭流

                        xjExportFileStream1.Close();

                    }
                
                
                }
            
            }

            

        }

        private void 探测候选linkToolStripMenuItem1_Click(object sender, EventArgs e)
        {  
            List<IFeature > point = new List<IFeature >();
          
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);//初步剔除伪link后link集
            ILayer player1 = pmap.get_Layer(1);//轨迹数据
          
            IFeatureLayer featurelayer0 =  player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;

            IFeatureLayer featurelayer1 =  player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;
            

           

            List<IPolyline> polyline = new List<IPolyline>();
            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
               
                IFeature feature=featureclass0 .GetFeature (i);

                IGeometry ige = feature.Shape;
                IPolyline poly = new PolylineClass();
                if (ige.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    poly = ige as IPolyline;
                
                }

                IPoint intersection1 = poly.FromPoint;
                IPoint intersection2 = poly.ToPoint;
                IPoint center = new PointClass();
                double ang = angle(intersection1, intersection2);
                double ang1 = angle(intersection2, intersection1);

                center.X = (intersection1.X + intersection2.X) / 2;
                center.Y = (intersection1.Y + intersection2.Y) / 2;

                IPoint center1 = new PointClass();
                center1.X = (intersection1.X + center.X) / 2;
                center1.Y = (intersection1.Y + center.Y) / 2;

                IPoint center2 = new PointClass();
                center2.X = (intersection2.X + center.X) / 2;
                center2.Y = (intersection2.Y + center.Y) / 2;

                double count1=0;
                double count=0;
                double count2 = 0;
                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    IPoint P = featureclass1.GetFeature(j ).Shape as IPoint;
                    string a1 = featureclass1.GetFeature(j).get_Value(7).ToString();
                    double a = Convert.ToDouble(a1);
                    double diff1 =Math.Abs(a- ang);
                    double diff2 = Math.Abs(a - ang1);
                    if (with(P,center1,20)&((diff1<30)|(diff2<30)))
                    { 
                        count1=count1+1;
                    
                    }
                     if (with(P ,center,20)&((diff1<30)|(diff2<30)))
                    { 
                        count=count+1;
                    
                    }
                     if (with(P ,center2,20)&((diff1<30)|(diff2<30)))
                    { 
                        count2=count2+1;
                    
                    }            
                
                
                
                }

                IPolyline feature1 = featureclass0.GetFeature(i).Shape as IPolyline;
                if ((count>5)&(count1>5)&(count2>5))

                {
                    polyline.Add(feature1);
                
                }              
            }


            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);
            #endregion

            




        }

        private bool within(ISegment se, List<ISegment> polyline)
        {

            bool result = false;
            int count = 0;
            for (int m = 0; m < polyline.Count; m++)
            {
                ISegment s1 = polyline[m];
                if ((dis(s1.FromPoint, se.FromPoint) < 5 && dis(s1.ToPoint, se.ToPoint) < 5) || (dis(s1.FromPoint, se.ToPoint) < 5 && dis(s1.ToPoint, se.FromPoint) < 5))
                {
                    count = count + 1;

                }

            }

            if (count > 0)
            {
                result = true;

            }
            return result;
        }


        private bool within1(IPolyline se, List<ISegment> polyline)
        {

            bool result = false;
            int count = 0;
            for (int m = 0; m < polyline.Count; m++)
            {
                ISegment s1 = polyline[m];
                if ((dis(s1.FromPoint, se.FromPoint) < 5 && dis(s1.ToPoint, se.ToPoint) < 5) || (dis(s1.FromPoint, se.ToPoint) < 5 && dis(s1.ToPoint, se.FromPoint) < 5))
                {
                    count = count + 1;

                }

            }

            if (count > 0)
            {
                result = true;

            }
            return result;
        }





        private void 探测候选linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFeature> pPolyline = new List<IFeature>();

            List<IFeature> pPolyline1 = new List<IFeature>();


            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//true link

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//三角形

            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//待确定link


            for (int i = 0; i < featureclass2.FeatureCount(null); i++)
            {
                IFeature feature = featureclass2.GetFeature(i);
                ITopologicalOperator top = feature.Shape as ITopologicalOperator;
                List<int> lablist = new List<int>();//标记相邻三角形
                bool result = false;//待确定link标记是否为伪


                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    IFeature feature1 = featureclass1.GetFeature(j);
                    IGeometry intersection = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension);

                    if (intersection.IsEmpty == false)
                    {
                        lablist.Add(j);

                    }


                }

                for (int m = 0; m < lablist.Count; m++)
                {
                    ISegmentCollection pPc = featureclass1.GetFeature(lablist[m]).Shape as ISegmentCollection;
                    int count = 0;

                    for (int ii = 0; ii < pPc.SegmentCount; ii++)
                    {

                        ISegment sg1 = pPc.get_Segment(ii);

                        IGeometryCollection sg = new PolylineClass();



                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        sg.AddGeometry(pPath1 as IGeometry, ref oo, ref oo);



                        ITopologicalOperator top1 = sg as ITopologicalOperator;

                        for (int jj = 0; jj < featureclass0.FeatureCount(null); jj++)
                        {
                            IFeature line = featureclass0.GetFeature(jj);
                            IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                            if (intersection1.IsEmpty == false)
                            {
                                count++;
                            }


                        }



                    }

                    if (count == 2)
                    {

                        result = true;

                    }

                }


                if (result == false)
                {

                    pPolyline.Add(feature);




                }

                else
                {

                    pPolyline1.Add(feature);//伪link
                
                }





            }
            

            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp2(shapeFileFullName, pPolyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult1 = saveFileDialog1.ShowDialog();
            if (dialogResult1 == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog1.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer1 = Createpolylineshp2(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer1);



            #endregion


        }

        private void 计算linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;//link
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;
            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//轨迹点           
            
            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//交叉点
          

            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                

                IPolyline se = featureclass0.GetFeature(i).Shape as IPolyline;

                IPoint Spoint = se.FromPoint;
                IPoint Epoint = se.ToPoint;
                int start = 0;
                int end = 0;

                for (int j = 0; j < featureclass2.FeatureCount(null); j++)
                {
                    IPoint p = featureclass2.GetFeature(j).Shape as IPoint;
                    if (dis(Spoint, p) < 5)
                    {
                        start = j;
                    }

                    if (dis(Epoint, p) < 5)
                    {
                        end = j;
                    }
                }



                //判断se



                IPoint intersection1 = featureclass2.GetFeature(end).Shape as IPoint;
                IPoint intersection2 = featureclass2.GetFeature(start).Shape as IPoint;

                IPoint center = new PointClass();
                center.X = (intersection1.X + intersection2.X) / 2;
                center.Y = (intersection1.Y + intersection2.Y) / 2;
                double Cdis = dis(intersection1, intersection2) / 2;
                IList<IFeature> featurelist = new List<IFeature>();



                for (int n = 0; n < featureclass1.FeatureCount(null); n++)
                {
                    IPoint P = featureclass1.GetFeature(n).Shape as IPoint;


                    if (with(center, P, Cdis) == true)
                    {
                        featurelist.Add(featureclass1.GetFeature(n));

                    }



                }

                IList<IFeature> result = new List<IFeature>();
                result = extractpoint(start, end, featureclass2, featurelist);

                int idcount = 0;
                int m = 0;
                for (int a = 0; a < result.Count ; a++)
                {
                    if (a  != m )
                    {
                        continue;
                    }
                   idcount = idcount + 1;
                    int id0 =Convert .ToInt32 ( result[a].get_Value(2).ToString());
                    
                    for (int b = a +1; b < result.Count; b++)
                    {  int id1 = Convert.ToInt32(result[b].get_Value(2).ToString());

                        if (id1 == id0)
                        {
                            continue;

                        }
                        else
                        {
                            m = b;
                           
                            break;
                        }
                    }
                
                
                }



                    if (result.Count > 0& idcount >0)
                    {
                        string ID = start + "-" + end;

                        string xjFileFullPath1 = "D:\\2020年\\第二篇论文\\汉口\\data\\strip\\" + start + "-" + end + ".txt";

                        System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                        StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                        for (int iii = 0; iii < result.Count; iii++)
                        {
                            IPoint pp = result[iii].Shape as IPoint;

                            string pointline =  pp.X.ToString() + "," + pp.Y.ToString();



                            xjExportStreamWriter1.WriteLine(pointline);
                        }


                        xjExportStreamWriter1.Flush();//清空缓冲区

                        xjExportStreamWriter1.Close();//关闭流

                        xjExportFileStream1.Close();

                    }





                //判断es
                IList<IFeature> result1 = new List<IFeature>();
                result1 = extractpoint(end, start, featureclass2, featurelist);
                int idcount1 = 0;
                int m1 = 0;
                for (int a = 0; a < result1.Count; a++)
                {
                    if (a != m1)
                    {
                        continue;
                    }
                    idcount1 = idcount1 + 1;
                    int id0 = Convert.ToInt32(result1[a].get_Value(2).ToString());

                    for (int b = a + 1; b < result1.Count; b++)
                    {
                        int id1 = Convert.ToInt32(result1[b].get_Value(2).ToString());

                        if (id1 == id0)
                        {
                            continue;

                        }
                        else
                        {
                            m1 = b;

                            break;
                        }
                    }


                }

                if (result1.Count > 0 & idcount1>0)
                {
                    string ID = end + "-" + start;

                    string xjFileFullPath1 = "D:\\2020年\\第二篇论文\\汉口\\data\\strip\\" + end + "-" + start + ".txt";

                    System.IO.FileStream xjExportFileStream1 = new System.IO.FileStream(xjFileFullPath1, FileMode.Create);
                    StreamWriter xjExportStreamWriter1 = new StreamWriter(xjExportFileStream1);

                    for (int iii = 0; iii < result1.Count; iii++)
                    {
                        IPoint pp = result1[iii].Shape as IPoint;

                        string pointline =pp.X.ToString() + "," + pp.Y.ToString();



                        xjExportStreamWriter1.WriteLine(pointline);
                    }

                    xjExportStreamWriter1.Flush();//清空缓冲区

                    xjExportStreamWriter1.Close();//关闭流

                    xjExportFileStream1.Close();


                }






             



            }
            

            MessageBox.Show("成功！！！");









        }

    

        private void 提取斜边ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);


            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//三角形

            List<ISegment> pPolyline = new List<ISegment>();
            List<ISegment> dPolyline = new List<ISegment>();
            List<ISegmentCollection> pPolyline1 = new List<ISegmentCollection>();

            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                ISegmentCollection pPc = featureclass0.GetFeature(i).Shape as ISegmentCollection;
                ISegment sg0 = pPc.get_Segment(0);
                double a = sg0.Length;
                double max = a;
                ISegment lab = sg0;
                int a1 = 0;
                for (int ii = 1; ii < pPc.SegmentCount; ii++)
                {

                    ISegment sg1 = pPc.get_Segment(ii);

                    double b = sg1.Length;
                    if (b > max)
                    {
                        max = b;
                        lab = sg1;
                        a1 = ii;
                    }



                }



                pPolyline.Add(lab);
            }
            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                ISegmentCollection pPc = featureclass0.GetFeature(i).Shape as ISegmentCollection;
                for (int j = 0; j < pPc.SegmentCount; j++)
                {

                    ISegment sg1 = pPc.get_Segment(j);


                    if (within(sg1, pPolyline) == true || within(sg1, dPolyline) == true)
                    {
                        continue;
                    }
                    else
                    {
                        dPolyline.Add(sg1);

                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        pPolyline1.Add(pPath1);

                    }



                }


            }














            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion


          

        }

        private void allLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);


            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//三角形

            List<ISegment> pPolyline = new List<ISegment>();
            List<ISegment> dPolyline = new List<ISegment>();
            List<ISegmentCollection> pPolyline1 = new List<ISegmentCollection>();

           




            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                ISegmentCollection pPc = featureclass0.GetFeature(i).Shape as ISegmentCollection;
                for (int j = 0; j < pPc.SegmentCount; j++)
                {

                    ISegment sg1 = pPc.get_Segment(j);


                    if (within(sg1, pPolyline) == true || within(sg1, dPolyline) == true)
                    {
                        continue;
                    }
                    else
                    {
                        dPolyline.Add(sg1);

                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        pPolyline1.Add(pPath1);

                    }



                }


            }














            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion





















        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            List<ISegment> pPolyline = new List<ISegment>();
            List<ISegment> dPolyline = new List<ISegment>();
            List<ISegmentCollection> pPolyline1 = new List<ISegmentCollection>();




            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass = featurelayer0.FeatureClass;//三角形


            for (int i = 0; i < featureclass.FeatureCount(null); i++)
            {

                ISegmentCollection pPc = featureclass.GetFeature(i).Shape as ISegmentCollection;
                double max1 = pPc.get_Segment(0).Length;
                int lab = 0;
                for (int m = 1; m < pPc.SegmentCount; m++)
                {
                    ISegment seg = pPc.get_Segment(m);
                    if (max1 < seg.Length)
                    {
                        max1 = seg.Length;
                        lab = m;
                    }



                }

                IFeature feature = featureclass.GetFeature(i);

                ITopologicalOperator top = feature.Shape as ITopologicalOperator;

                for (int j = 0; j < featureclass.FeatureCount(null); j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    IFeature feature1 = featureclass.GetFeature(j);
                    IGeometry result = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension);





                    if (result.IsEmpty)
                    {
                        continue;
                    }

                    else
                    {



                        ISegmentCollection pPc1 = featureclass.GetFeature(j).Shape as ISegmentCollection;
                        double max2 = pPc1.get_Segment(0).Length;
                        for (int m = 1; m < pPc1.SegmentCount; m++)
                        {
                            ISegment seg = pPc1.get_Segment(m);

                            if (max2 < seg.Length)
                            {
                                max2 = seg.Length;
                            }


                        }

                        if (max1 == max2)
                        {
                            ISegment seg = pPc.get_Segment(lab);
                            if (within(seg, pPolyline) == true)
                            {
                                continue;
                            }

                            pPolyline.Add(seg);


                        }






                    }
                }
            }



            for (int ii = 0; ii < featureclass.FeatureCount(null); ii++)
            {
                ISegmentCollection pPc2 = featureclass.GetFeature(ii).Shape as ISegmentCollection;
                for (int j = 0; j < pPc2.SegmentCount; j++)
                {

                    ISegment sg1 = pPc2.get_Segment(j);


                    if (within(sg1, pPolyline) == true || within(sg1, dPolyline) == true)
                    {
                        continue;
                    }
                    else
                    {
                        dPolyline.Add(sg1);

                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        pPolyline1.Add(pPath1);

                    }



                }


            }























            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion


           










        }

        private void 提取长边ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);


            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//三角形

            
            List<ISegmentCollection> pPolyline1 = new List<ISegmentCollection>();

            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                ISegmentCollection pPc = featureclass0.GetFeature(i).Shape as ISegmentCollection;
                ISegment sg0 = pPc.get_Segment(0);
                double a = sg0.Length;
                double max = a;
                ISegment lab = sg0;
                int a1 = 0;
                for (int ii = 1; ii < pPc.SegmentCount; ii++)
                {

                    ISegment sg1 = pPc.get_Segment(ii);

                    double b = sg1.Length;
                    if (b > max)
                    {
                        max = b;
                        lab = sg1;
                        a1 = ii;
                    }



                }




                ISegmentCollection pPath1 = new PathClass();
                object oo = Type.Missing;
                pPath1.AddSegment(lab, ref oo, ref oo);
                pPolyline1.Add(pPath1);






            }




            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion








        }

        private void 剔除伪link2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFeature> pPolyline = new List<IFeature>();//待定

            List<IFeature> pPolyline1 = new List<IFeature>();//伪

            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);
            ILayer player3 = pmap.get_Layer(3);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//true link

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//三角形

            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//待定 link
            IFeatureLayer featurelayer3 = player3 as IFeatureLayer;
            IFeatureClass featureclass3 = featurelayer3.FeatureClass;//false link


           


            for (int i = 0; i < featureclass2.FeatureCount(null); i++)
            {
                IFeature feature = featureclass2.GetFeature(i);
                ITopologicalOperator top = feature.Shape as ITopologicalOperator;
                List<int> lablist = new List<int>();//标记相邻三角形
                bool result = false;//待确定link标记是否为伪


                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    IFeature feature1 = featureclass1.GetFeature(j);
                    IGeometry intersection = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension);

                    if (intersection.IsEmpty == false)
                    {
                        lablist.Add(j);

                    }




                }


                for (int m = 0; m < lablist.Count; m++)
                {
                    ISegmentCollection pPc = featureclass1.GetFeature(lablist[m]).Shape as ISegmentCollection;
                    int count = 0;

                    for (int ii = 0; ii < pPc.SegmentCount; ii++)
                    {

                        ISegment sg1 = pPc.get_Segment(ii);

                        IGeometryCollection sg = new PolylineClass();



                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        sg.AddGeometry(pPath1 as IGeometry, ref oo, ref oo);






                        ITopologicalOperator top1 = sg as ITopologicalOperator;

                        for (int jj = 0; jj < featureclass0.FeatureCount(null); jj++)
                        {
                            IFeature line = featureclass0.GetFeature(jj);
                            IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                            if (intersection1.IsEmpty == false)
                            {
                                count++;
                            }


                        }










                    }

                    if (count == 2)
                    {

                        result = true;

                    }

                }


                if (result == false)
                {

                    pPolyline.Add(feature);




                }

                else
                {

                    pPolyline1.Add(feature);//伪link

                }





            }






            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp2(shapeFileFullName, pPolyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult1 = saveFileDialog1.ShowDialog();
            if (dialogResult1 == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog1.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer1 = Createpolylineshp2(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer1);



            #endregion































        }

        private void 得到伪link规则2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> polyline = new List<IPolyline>();

            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);//初步剔除伪link后link集
            ILayer player1 = pmap.get_Layer(1);//剔除后的center点

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;


            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                IPolyline feature = featureclass0.GetFeature(i).Shape as IPolyline;
                int id = Convert.ToInt32(featureclass0.GetFeature(i).get_Value(0).ToString());
                int count = 0;
                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    int ID = Convert.ToInt32(featureclass1.GetFeature(j).get_Value(2).ToString());
                    if (id == ID)
                    {
                        count = count + 1;

                    }


                }


                if (count == 0)
                {
                    polyline.Add(feature);


                }


            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion


        }

        private void 删除狭长边ToolStripMenuItem_Click(object sender, EventArgs e)
        {



           

            IMap pmap = axMapControl1.Map;
            ILayer player = pmap.get_Layer(0);//三角形
            IFeatureLayer featurelayer = player as IFeatureLayer;
            IFeatureClass featureclass = featurelayer.FeatureClass;

             List<ISegment> pPolyline = new List<ISegment>();
             List<IFeature> featurelist = new List<IFeature>();

             #region 剔除狭长边
             for (int i = 0; i < featureclass.FeatureCount(null); i++)
            {
                featurelist.Add(featureclass.GetFeature(i));

                ISegmentCollection pPc = featureclass.GetFeature(i).Shape as ISegmentCollection;
                ISegment sg0 = pPc.get_Segment(0);
                double a = sg0.Length;
                double max = a;
                ISegment lab = sg0;
                int a1 = 0;
                for (int ii = 1; ii < pPc.SegmentCount; ii++)
                {

                    ISegment sg1 = pPc.get_Segment(ii);

                    double b = sg1.Length;
                    if (b > max)
                    {
                        max = b;
                        lab = sg1;
                        a1 = ii;
                    }



                }
                pPolyline.Add(lab);

            }

            List<IFeature> featurelistcopy = featurelist ;
            
            int n = 0;
            while (featurelist.Count!=n)
            {
                n = featurelist.Count;
                List<int> gc1 = new List<int>();
                for (int i = 0; i < featurelist.Count; i++)
                {
                
                    
                   
                    int Count = 0;
                    IFeature feature = featurelist[i];
                    ITopologicalOperator top = feature.Shape   as ITopologicalOperator;
                   

                    for (int j = 0; j < featurelist.Count; j++)
                    {


                        IGeometry result1 = top.Intersect(featurelist[j].Shape, esriGeometryDimension.esriGeometry0Dimension);
                        IGeometry result2 = top.Intersect(featurelist[j].Shape, esriGeometryDimension.esriGeometry1Dimension);
                        IGeometry result3 = top.Intersect(featurelist[j].Shape, esriGeometryDimension.esriGeometry2Dimension);


                        if ((result2 .IsEmpty ==false ))
                        {

                            Count = Count + 1;


                        }

                    }
                    if (Count == 3)
                    {
                        object missing = Type.Missing;

                        ISegmentCollection pPc = feature.Shape as ISegmentCollection;
                        List<int> lab = new List<int>();

                        int count = 0;
                        for (int m = 0; m < pPc.SegmentCount; m++)
                        {
                            ISegment s = pPc.get_Segment(m);
                            if (within(s, pPolyline))
                            {
                                count = count + 1;
                                lab.Add(m);

                            }

                        }

                        if (count == 1)
                        {
                            List<int> l = new List<int>();
                            l.Add(0);
                            l.Add(1);
                            l.Add(2);
                            int a = 0;
                            double max = pPc.get_Segment(0).Length;
                            for (int ii = 1; ii < pPc.SegmentCount; ii++)
                            {

                                ISegment sg1 = pPc.get_Segment(ii);

                                double b = sg1.Length;
                                if (b > max )
                                {
                                    max  = b;
                                    a = ii;

                                }



                            }
                            l.Remove(a);


                            IPoint start = pPc.get_Segment(l[0]).FromPoint;
                            IPoint end = pPc.get_Segment(l[0]).ToPoint;
                            IPoint start1 = pPc.get_Segment(l[1]).FromPoint;
                            IPoint end1 = pPc.get_Segment(l[1]).ToPoint;
                            double angle1 = 0;
                            double angle2 = 0;
                            if ((start.X != start1.X) | (start.Y != start1.Y))
                            {
                                angle1 = angle(end, start);
                                angle2 = angle(start1, end1);

                            }
                            else if ((start.X == start1.X) & (start.Y == start1.Y))
                            {

                                angle1 = angle(start , end );
                                angle2 = angle(start1, end1);
                            
                            }

                            double diff = 180 - Math.Abs(Math.Abs(angle1 - angle2) - 180);
                            if (diff > 135)
                            {

                                gc1.Add(Convert.ToInt32(featurelist[i].get_Value(4)));

                            }
                        
                        
                        }




                        if (count == 2)
                        {
                           

                            List<int> l = new List<int>();
                            l.Add(0);
                            l.Add(1);
                            l.Add(2);
                            int a = 0;
                            double max = pPc.get_Segment(0).Length;
                            for (int ii = 1; ii < pPc.SegmentCount; ii++)
                            {

                                ISegment sg1 = pPc.get_Segment(ii);

                                double b = sg1.Length;
                                if (b > max)
                                {
                                    max = b;
                                    a = ii;

                                }



                            }
                            l.Remove(a);


                            IPoint start = pPc.get_Segment(l[0]).FromPoint;
                            IPoint end = pPc.get_Segment(l[0]).ToPoint;
                            IPoint start1 = pPc.get_Segment(l[1]).FromPoint;
                            IPoint end1 = pPc.get_Segment(l[1]).ToPoint;
                            double angle1 = 0;
                            double angle2 = 0;
                            if ((start.X != start1.X) | (start.Y != start1.Y))
                            {
                                angle1 = angle(end, start);
                                angle2 = angle(start1, end1);

                            }
                            else if ((start.X == start1.X) & (start.Y == start1.Y))
                            {

                                angle1 = angle(start, end);
                                angle2 = angle(start1, end1);

                            }

                            double diff = 180 - Math.Abs(Math.Abs(angle1 - angle2) - 180);
                            if (diff > 135)
                            {

                                gc1.Add(Convert.ToInt32(featurelist[i].get_Value(4)));

                            }
                        
                        }



                        if (count ==3)
                        {
                            
                            List<int> l = new List<int>();
                            l.Add(0);
                            l.Add(1);
                            l.Add(2);
                            int a = 0;
                            double max = pPc.get_Segment(0).Length;
                            for (int ii = 1; ii < pPc.SegmentCount; ii++)
                            {

                                ISegment sg1 = pPc.get_Segment(ii);

                                double b = sg1.Length;
                                if (b >max  )
                                {
                                   max = b;
                                    a = ii;
                                   
                                }



                            }
                            l.Remove(a);


                            IPoint start = pPc.get_Segment(l[0]).FromPoint;
                            IPoint end = pPc.get_Segment(l[0]).ToPoint;
                            IPoint start1 = pPc.get_Segment(l[1]).FromPoint;
                            IPoint end1 = pPc.get_Segment(l[1]).ToPoint;
                            double angle1 = 0;
                            double angle2 = 0;
                            if ((start.X != start1.X) | (start.Y != start1.Y))
                            {
                                angle1 = angle(end, start);
                                angle2 = angle(start1, end1);

                            }
                            else if ((start.X == start1.X) & (start.Y == start1.Y))
                            {

                                angle1 = angle(start, end);
                                angle2 = angle(start1, end1);

                            }

                            double diff = 180 - Math.Abs(Math.Abs(angle1 - angle2) - 180);
                            if (diff > 135)
                            {

                                gc1.Add(Convert.ToInt32(featurelist[i].get_Value(4)));

                            }
                        }


                    }
                }

                for (int mm = 0; mm < gc1.Count; mm++)


                {

                    for (int k = 0; k < featurelistcopy.Count; k++)
                    {
                        if (Convert.ToInt32(featurelist[k].get_Value(4)) == gc1[mm])
                        {
                            featurelist.Remove(featurelist[k ]);
                        }
                        
                    
                    }
                       
                
                }
                featurelistcopy = featurelist;



            }

             #endregion
            #region 提取All link
            List<ISegment> pPolyline0 = new List<ISegment>();
            List<ISegment> dPolyline = new List<ISegment>();
            List<ISegmentCollection> pPolyline1 = new List<ISegmentCollection>();

            for (int i = 0; i < featurelist.Count; i++)
            {
                ISegmentCollection pPc = featurelist[i].Shape as ISegmentCollection; 
               
                for (int j = 0; j < pPc.SegmentCount; j++)
                {

                    ISegment sg1 = pPc.get_Segment(j);


                    if (within(sg1, pPolyline0) == true || within(sg1, dPolyline) == true)
                    {
                        continue;
                    }
                    else
                    {
                        dPolyline.Add(sg1);

                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        pPolyline1.Add(pPath1);

                    }



                }


            }
            #endregion

            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp(shapeFileFullName, pPolyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion











           
           



        }

     
        
        private void 探测候选伪link规则1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> polyline = new List<IPolyline>();

            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);//初步剔除伪link后link集
            ILayer player1 = pmap.get_Layer(1);//剔除后的center点

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;


            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                IPolyline feature = featureclass0.GetFeature(i).Shape as IPolyline;
                int id = Convert.ToInt32(featureclass0.GetFeature(i).get_Value(0).ToString());
                int count = 0;
                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    int ID = Convert.ToInt32(featureclass1.GetFeature(j).get_Value(2).ToString())/3;
                    if (id == ID)
                    {
                        count = count + 1;

                    }


                }


                if (count ==0)
                {
                    polyline.Add(feature);


                }


            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion



        }

        private void 生成路网ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> polyline = new List<IPolyline>();

           


            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//truelink

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//交叉点

            

            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {

                List<IFeature> point = new List<IFeature>();
                
                IPoint start=featureclass1.GetFeature(i).Shape as IPoint ;
                //条件1，距离限制
                for (int m = 0; m < featureclass1.FeatureCount(null); m++)
                {
                    IFeature ppfeature = featureclass1.GetFeature(m) as IFeature;
                    IPoint sstart = ppfeature.Shape as IPoint;

                    if (dis(start, sstart) < 1000)
                    {
                        point.Add(ppfeature);
                    
                    }

                
                }




                ITopologicalOperator top1 = featureclass1.GetFeature(i).Shape as ITopologicalOperator;
                    IGeometry f = top1.Buffer(5e-05);
                    ITopologicalOperator top = f as ITopologicalOperator;

                    List<int> featurelist = new List<int>();

                    for (int j = 0; j < featureclass0.FeatureCount(null); j++)
                    {

                        IGeometry result = top.Intersect(featureclass0.GetFeature(j).Shape, esriGeometryDimension.esriGeometry0Dimension);

                        if ((result.IsEmpty == false))
                        {
                            featurelist.Add(j);

                        }



                    }


                    if (featurelist.Count > 0)
                    {


                        for (int ii = 0; ii < point.Count ; i++)
                        {
                            if (i == ii)
                            {
                                continue;
                            }
                            IPoint end = point[ii].Shape as IPoint;

                            int count = 0;
                            //条件1,与truelink相交大于60

                            for (int iii = 0; iii < featurelist.Count; iii++)
                            {


                                IPolyline py = featureclass0.GetFeature(featurelist[iii]).Shape as IPolyline;
                                IPoint start1 = py.FromPoint;
                                IPoint end1 = py.ToPoint;

                                double angle1 = 0;
                                double angle2 = 0;
                                if ((start.X != start1.X) | (start.Y != start1.Y))
                                {
                                    if ((end.X != end1.X) | (end.Y != end1.Y))
                                    {
                                        angle1 = angle(end, start);
                                        angle2 = angle(start1, end1);
                                    }
                                    else
                                    {
                                        angle1 = angle(end, start);
                                        angle2 = angle(end1, start1);



                                    }

                                }

                                else if ((start.X == start1.X) & (start.Y == start1.Y))
                                {

                                    angle1 = angle(start, end);
                                    angle2 = angle(start1, end1);

                                }

                                double diff = Math.Min(Math.Abs(angle1 - angle2), Math.Abs(360 - Math.Abs(angle1 - angle2)));


                                if (diff < 60)
                                {

                                    count = count + 1;

                                }



                            }


                            if (count == 0)
                            {
                                //条件2，不能与其他truelink相交
                                IPolyline py1 = new PolylineClass();
                                py1.FromPoint = start;
                                py1.ToPoint = end;

                                ITopologicalOperator topp = py1 as ITopologicalOperator;
                                int count0 = 0;
                                for (int jj = 0; jj < featureclass0.FeatureCount(null); jj++)
                                {
                                    int count1 = 0;

                                    for (int jjj = 0; jjj < featurelist.Count; jjj++)
                                    {

                                        if (jj == featurelist[jjj])
                                        {

                                            count1 = count1 + 1;
                                        }


                                    }

                                    if (count1 > 0)
                                    {
                                        continue;
                                    }

                                    else
                                    {
                                        IGeometry result1 = topp.Intersect(featureclass0.GetFeature(jj).Shape, esriGeometryDimension.esriGeometry0Dimension);
                                        if (result1.IsEmpty == false)
                                        {
                                            count0 = count0 + 1;

                                        }

                                    }



                                }

                                if (count0 == 0)
                                {
                                    polyline.Add(py1);


                                }



                            }








                        }



                    }




                
              
            
            
            }




            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);















        }

        private void 提取最三角形短边ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
           
            
            IFeatureClass featureclass0 = null;//交叉点
            IFeatureClass featureclass1 = null;//泰森多边形
            IFeatureLayer featurelayer0 = null;
            IFeatureLayer featurelayer1 = null;
           

            if (player0.Name == "过滤交叉点")
            {
                //交叉点
                featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }


            string xjFileFullPath = "D:\\data\\提取结果\\武汉\\straightline.txt";
           

            System.IO.FileStream xjExportFileStream = new System.IO.FileStream(xjFileFullPath, FileMode.Create);
            StreamWriter xjExportStreamWriter = new StreamWriter(xjExportFileStream);
            double Count = 0;
            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {





                List<int> candinatelist = new List<int>();
                IPoint intersection1 = featureclass0.GetFeature(i).Shape as IPoint;

                double ExportX = intersection1.X;
                double ExportY = intersection1.Y;


                string Pointline0 = ExportX.ToString() + "," + ExportY.ToString();

                int lab = -1;

                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    string num1 = featureclass1.GetFeature(j).get_Value(3).ToString();
                    int num = Convert.ToInt32(num1);
                    if (num == i)
                    {
                        lab = j;
                        continue;

                    }
                }

                int count = featureclass1.FeatureCount(null);
                IFeature feature2 = featureclass1.GetFeature(lab);
                IRelationalOperator rel = feature2.Shape as IRelationalOperator;


                IFeatureCursor pfeaturecursor = featurelayer1.Search(null, false);
                IFeature pfeature = pfeaturecursor.NextFeature();

                //判断候选link
                while (pfeature != null)
                {
                    ITopologicalOperator top1 = pfeature.Shape as ITopologicalOperator;
                    IGeometry feature = top1.Buffer(5e-05);

                    if (rel.Overlaps(feature))
                    {
                        string num1 = pfeature.get_Value(3).ToString();
                        int num = Convert.ToInt32(num1);
                        Pointline0 = Pointline0 + "," + num1;

                        candinatelist.Add(num);

                    }

                    pfeature = pfeaturecursor.NextFeature();
                }


                

                // 构建intersection-list,先提取最小link
                IPoint intersection0 = featureclass0.GetFeature(candinatelist[0]).Shape as IPoint;
                double min = dis(intersection1, intersection0);
                int nummin = 0;
                for (int mm = 1; mm < candinatelist.Count; mm++)
                {
                    IPoint intersection = featureclass0.GetFeature(candinatelist[mm]).Shape as IPoint;


                    double Cdis = dis(intersection1, intersection) ;

                    if( Cdis <min )
                    {
                        min = Cdis;
                        nummin = mm;
                    
                    
                    }



                }


                string ID = i + "-" + candinatelist[nummin];

                IPoint intersection2 = featureclass0.GetFeature(candinatelist[nummin]).Shape as IPoint;


                string Pointline1 = Count + "," + ID + "," + ExportX.ToString() + "," + ExportY.ToString();
                xjExportStreamWriter.WriteLine(Pointline1);


                double ExportX1 = intersection2.X;
                double ExportY1 = intersection2.Y;
                string Pointline2 = Count + "," + ID + "," + ExportX1.ToString() + "," + ExportY1.ToString();


                xjExportStreamWriter.WriteLine(Pointline2);

                Count++;




            }


            xjExportStreamWriter.Flush();//清空缓冲区

            xjExportStreamWriter.Close();//关闭流

            xjExportFileStream.Close();

            MessageBox.Show("成功！！！");











        }

        private void 剔除伪link规则3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFeature> pPolyline = new List<IFeature>();

            List<IFeature> pPolyline1 = new List<IFeature>();


            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);
            ILayer player3 = pmap.get_Layer(3);
            ILayer player4 = pmap.get_Layer(4);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//直线

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//三角形

            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//待确定link

            IFeatureLayer featurelayer3 = player3 as IFeatureLayer;
            IFeatureClass featureclass3 = featurelayer3.FeatureClass;//伪link2

            IFeatureLayer featurelayer4 = player4 as IFeatureLayer;
            IFeatureClass featureclass4 = featurelayer4.FeatureClass;//伪link1

            List<IFeature> ffeature = new List<IFeature>();

            for (int i = 0; i < featureclass3.FeatureCount(null); i++)
            {
                IFeature feature = featureclass3.GetFeature(i);
                ffeature.Add(feature);

            }
            for (int i = 0; i < featureclass4.FeatureCount(null); i++)
            {
                IFeature feature = featureclass4.GetFeature(i);
                ffeature.Add(feature);

            }



            for (int i = 0; i < featureclass2.FeatureCount(null); i++)
            {
                IFeature feature = featureclass2.GetFeature(i);
                ITopologicalOperator top = feature.Shape as ITopologicalOperator;
                List<int> lablist = new List<int>();//标记相邻三角形
                bool result = false;//待确定link标记是否为伪


                for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                {
                    IFeature feature1 = featureclass1.GetFeature(j);
                    IGeometry intersection = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension);

                    if (intersection.IsEmpty == false)
                    {
                        lablist.Add(j);

                    }




                }

                for (int m = 0; m < lablist.Count; m++)
                {
                    ISegmentCollection pPc = featureclass1.GetFeature(lablist[m]).Shape as ISegmentCollection;
                    int count = 0;
                    int count1 = 0;
                    int lab = 0;
                    int lab1 = 0;

                    for (int ii = 0; ii < pPc.SegmentCount; ii++)
                    {

                        ISegment sg1 = pPc.get_Segment(ii);

                        IGeometryCollection sg = new PolylineClass();



                        ISegmentCollection pPath1 = new PathClass();
                        object oo = Type.Missing;
                        pPath1.AddSegment(sg1, ref oo, ref oo);
                        sg.AddGeometry(pPath1 as IGeometry, ref oo, ref oo);






                        ITopologicalOperator top1 = sg as ITopologicalOperator;

                        for (int jj = 0; jj < featureclass0.FeatureCount(null); jj++)
                        {
                            IFeature line = featureclass0.GetFeature(jj);
                            IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                            if (intersection1.IsEmpty == false)
                            {
                                count++;
                                lab = ii;
                            }


                        }

                        for (int jjj = 0; jjj < ffeature .Count; jjj++)
                        {
                            IFeature line = ffeature[jjj];
                            IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                            if (intersection1.IsEmpty == false)
                            {
                                count1++;
                                lab1 = ii;
                            }


                        }



                    }

                    if ((count == 1)&(count1 == 1))
                    {
                        ISegment sg1 = pPc.get_Segment(lab );

                        double a = sg1.Length;

                        ISegment sg2 = pPc.get_Segment(lab1);

                        double b = sg2.Length;
                        double c =0;
                        for (int iii = 0; iii < pPc.SegmentCount; iii++)
                        {
                            if (iii == lab | iii == lab1)
                            {
                                continue;
                            }
                            else
                            {
                                ISegment sg3 = pPc.get_Segment(iii );

                                 c = sg3.Length;
                            
                            }

                        }

                        if (c > a & c > b)
                        {
                            result = true;
                        }

                    }







                }


                if (result == false)  //待定
                {

                    pPolyline.Add(feature);




                }

              




            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp2(shapeFileFullName, pPolyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);
           


            #endregion




           



















        }

        private void 剔除伪link规则3迭代ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFeature> featurelist = new List<IFeature>();
            List<IFeature> Ffeaturelist = new List<IFeature>();

            List<IFeature> pPolyline1 = new List<IFeature>();
            List<int> ID = new List<int>();

            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            ILayer player2 = pmap.get_Layer(2);
            ILayer player3 = pmap.get_Layer(3);
            ILayer player4 = pmap.get_Layer(4);

            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//直线

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//三角形

            IFeatureLayer featurelayer2 = player2 as IFeatureLayer;
            IFeatureClass featureclass2 = featurelayer2.FeatureClass;//待确定link

            IFeatureLayer featurelayer3 = player3 as IFeatureLayer;
            IFeatureClass featureclass3 = featurelayer3.FeatureClass;//伪link

            IFeatureLayer featurelayer4 = player4 as IFeatureLayer;
            IFeatureClass featureclass4 = featurelayer4.FeatureClass;//伪link1


            for (int i = 0; i < featureclass2.FeatureCount(null); i++)
            {
                IFeature feature = featureclass2.GetFeature(i);
                featurelist.Add(feature);
                ID.Add(i);



            }


            for (int i = 0; i < featureclass3.FeatureCount(null); i++)
            {
                IFeature feature = featureclass3.GetFeature(i);
                Ffeaturelist.Add(feature);



            }

            for (int i = 0; i < featureclass4.FeatureCount(null); i++)
            {
                IFeature feature = featureclass4.GetFeature(i);
                Ffeaturelist.Add(feature);



            }



            List<IFeature> featurelistcopy = featurelist;

            int n = 0;
           
            while (featurelist.Count != n)
            {
                n = featurelist.Count;
                List<int> gc1 = new List<int>();
                for (int i = 0; i < n ; i++)
                {
                    IFeature feature = featurelist [i];
                    ITopologicalOperator top = feature.Shape as ITopologicalOperator;
                    List<int> lablist = new List<int>();//标记相邻三角形
                    bool result = false;//待确定link标记是否为伪


                    for (int j = 0; j < featureclass1.FeatureCount(null); j++)
                    {
                        IFeature feature1 = featureclass1.GetFeature(j);
                        IGeometry intersection = top.Intersect(feature1.Shape, esriGeometryDimension.esriGeometry1Dimension);

                        if (intersection.IsEmpty == false)
                        {
                            lablist.Add(j);

                        }




                    }

                    for (int m = 0; m < lablist.Count; m++)
                    {
                        ISegmentCollection pPc = featureclass1.GetFeature(lablist[m]).Shape as ISegmentCollection;
                        int count = 0;
                        int count1 = 0;
                        int lab = 0;
                        int lab1 = 0;

                        for (int ii = 0; ii < pPc.SegmentCount; ii++)
                        {

                            ISegment sg1 = pPc.get_Segment(ii);

                            IGeometryCollection sg = new PolylineClass();



                            ISegmentCollection pPath1 = new PathClass();
                            object oo = Type.Missing;
                            pPath1.AddSegment(sg1, ref oo, ref oo);
                            sg.AddGeometry(pPath1 as IGeometry, ref oo, ref oo);






                            ITopologicalOperator top1 = sg as ITopologicalOperator;

                            for (int jj = 0; jj < featureclass0.FeatureCount(null); jj++)
                            {
                                IFeature line = featureclass0.GetFeature(jj);
                                IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                                if (intersection1.IsEmpty == false)
                                {
                                    count++;
                                    lab = ii;
                                }


                            }

                            for (int jjj = 0; jjj < Ffeaturelist.Count ; jjj++)
                            {
                                IFeature line = Ffeaturelist[jjj];
                                IGeometry intersection1 = top1.Intersect(line.Shape, esriGeometryDimension.esriGeometry1Dimension);
                                if (intersection1.IsEmpty == false)
                                {
                                    count1++;
                                    lab1 = ii;
                                }


                            }



                        }

                        if ((count == 1) & (count1 == 1))
                        {
                            ISegment sg1 = pPc.get_Segment(lab);

                            double a = sg1.Length;

                            ISegment sg2 = pPc.get_Segment(lab1);

                            double b = sg2.Length;
                            double c = 0;
                            for (int iii = 0; iii < pPc.SegmentCount; iii++)
                            {
                                if (iii == lab | iii == lab1)
                                {
                                    continue;
                                }
                                else
                                {
                                    ISegment sg3 = pPc.get_Segment(iii);

                                    c = sg3.Length;

                                }

                            }

                            if (c > a & c > b)
                            {
                                result = true;
                            }

                        }







                    }


                    if (result == true ) 
                    {

                       Ffeaturelist.Add(feature);//伪link
                       gc1.Add(ID[i]);
                        



                    }

                   





                }

                for (int mm = 0; mm < gc1.Count; mm++)
                {


                    for (int k = 0; k < featurelistcopy.Count; k++)
                    {
                        if (ID[k] == gc1[mm])
                        {
                            featurelist.Remove(featurelist[k]);
                            ID.Remove(ID[k]);
                        }


                    }

                }

                featurelistcopy = featurelist;


            }
            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp2(shapeFileFullName, featurelist);


            axMapControl1.Map.AddLayer(pFeatureLayer);
          



            #endregion

              





        }

        private void 规则4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline > polyline = new List<IPolyline >();

            List<IPolyline> polyline1 = new List<IPolyline>();


            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);


            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//truelink

            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//待定link

            for (int i=0; i < featureclass1.FeatureCount(null); i++)
            {
                
                IFeature f1 = featureclass1.GetFeature(i);
                IPolyline feature = f1.Shape  as IPolyline;
                ITopologicalOperator top = f1.Shape as ITopologicalOperator;
                IPolyline poly = f1.Shape as IPolyline;
                IPoint start = poly.FromPoint;
                IPoint end = poly.ToPoint;
                int count = 0;
                for (int j = 0; j < featureclass0.FeatureCount(null); j++)
                {


                    IGeometry result1 = top.Intersect(featureclass0.GetFeature(j).Shape, esriGeometryDimension.esriGeometry0Dimension);


                    if ((result1.IsEmpty == false ))
                    {

                        IPolyline poly1 = featureclass0.GetFeature(j ).Shape as IPolyline;

                        IPoint start1 = poly1.FromPoint;
                        IPoint end1 = poly1.ToPoint;

                        double angle1 = 0;
                        double angle2 = 0;
                        if ((start.X != start1.X) | (start.Y != start1.Y))
                        {
                            if ((end.X != end1.X) | (end.Y != end1.Y))
                            {
                                angle1 = angle(end, start);
                                angle2 = angle(start1, end1);
                            }
                            else
                            {
                                angle1 = angle(end, start);
                                angle2 = angle(end1, start1);



                            }

                        }

                        else if ((start.X == start1.X) & (start.Y == start1.Y))
                        {

                            angle1 = angle(start, end);
                            angle2 = angle(start1, end1);

                        }

                        double diff = Math.Min(Math.Abs(angle1 - angle2), Math.Abs(360 - Math.Abs(angle1 - angle2)));
                        if (diff < 60)
                        {

                            count = count + 1;

                        }


                    }



                }

                if (count == 0)
                {

                    polyline.Add(feature);


                }
                else
                {
                    polyline1.Add(feature);
                }

            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);









           SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult1 = saveFileDialog1.ShowDialog();
            if (dialogResult1 == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog1.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer1 = Createpolylineshp1(shapeFileFullName, polyline1);


            axMapControl1.Map.AddLayer(pFeatureLayer1);






            #endregion













        }

        private void 优化ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            IFeatureClass featureclass0 = null;
            IFeatureClass featureclass1 = null;
            if (player0.Name == "过滤交叉点")
            {
                //交叉点
                IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                IFeatureLayer featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }
            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                pPolyline.Add(poly);

                

            }
            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i );


                IPolyline poly = feature.Shape as IPolyline;
                IPoint frompoint = poly.FromPoint;
                IPoint topoint = poly.ToPoint;

                double dismin = 0;
                IPoint point = featureclass0.GetFeature(0).Shape as IPoint;

                double dis1 = dis(frompoint, point);
                double dis2 = dis(topoint, point);
                if (dis1 < dis2)
                {
                    dismin = dis1;

                }
                else
                {

                    dismin = dis2;
                }

                int nummin = 0;
                for (int j = 1; j < featureclass0.FeatureCount(null); j++)
                {
                    IPoint point1 = featureclass0.GetFeature(j).Shape as IPoint;

                    double dis11= dis(frompoint, point1);
                    double dis22 = dis(topoint, point1);
                    double dis00 = 0;
                    if (dis11 < dis22)
                    {
                        dis00 = dis11;

                    }
                    else
                    {
                        dis00 = dis22;
                    }

                    if (dis00 < dismin)
                    {
                        dismin = dis00;
                        nummin = j;
                    
                    }


                
                }

                IPoint p = featureclass0.GetFeature(nummin ).Shape as IPoint;

                double d1 = dis(frompoint, p);
                double d2 = dis(topoint, p);

                if (d1 < d2)
                {
                    


                    IPolyline newpoly = new PolylineClass();
                    newpoly.FromPoint = frompoint ;
                    newpoly.ToPoint = p ;

                    pPolyline.Add(newpoly );

                }
                else
                {

                    IPolyline newpoly = new PolylineClass();
                    newpoly.FromPoint =topoint  ;
                    newpoly.ToPoint = p ;

                    
                    pPolyline.Add(newpoly );
                
                }
            
            
            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, pPolyline);

            //IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, Apolyline);
            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion













        }

        private void 优化匹配交叉点新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            IFeatureClass featureclass0 = null;
            IFeatureClass featureclass1 = null;
            if (player0.Name == "交叉点")
            {
                //交叉点
                IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                IFeatureLayer featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }

            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                pPolyline.Add(poly);



            }


            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                IPoint frompoint = poly.FromPoint;
                IPoint topoint = poly.ToPoint;

                
                IPoint point = featureclass0.GetFeature(0).Shape as IPoint;

                double fdismin = dis(frompoint, point);
                double tdismin = dis(topoint, point);
                
                int fnummin = 0;
                int tnummin = 0;
                for (int j = 1; j < featureclass0.FeatureCount(null); j++)
                {
                    IPoint point1 = featureclass0.GetFeature(j).Shape as IPoint;

                    double dis11 = dis(frompoint, point1);
                    double dis22 = dis(topoint, point1);
                    

                    if (dis11 < fdismin)
                    {
                        fdismin = dis11;
                        fnummin = j;

                    }
                    if (dis22 < tdismin)
                    {
                        tdismin = dis22;
                        tnummin = j;

                    }


                }

                if (tnummin == fnummin)
                {

                    if (fdismin < tdismin)
                    {

                        IPoint p = featureclass0.GetFeature(fnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);



                    }

                    else
                    {
                        IPoint p = featureclass0.GetFeature(tnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = topoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);


                    }

                }

                else
                {

                    if (fdismin < 120)
                    {
                        IPoint p = featureclass0.GetFeature(fnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);
                    }

                    if (tdismin < 120)
                    {
                        IPoint p1 = featureclass0.GetFeature(tnummin).Shape as IPoint;
                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = p1;

                        pPolyline.Add(newpoly1);

                    }
                
                
                }


            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, pPolyline);

            //IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, Apolyline);
            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion











        }

        private void 优化匹配交叉点3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            IFeatureClass featureclass0 = null;
            IFeatureClass featureclass1 = null;
            if (player0.Name == "过滤交叉点")
            {
                //交叉点
                IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                IFeatureLayer featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }



            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                pPolyline.Add(poly);



            }


            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                IPoint frompoint = poly.FromPoint;
                IPoint topoint = poly.ToPoint;



                //frompoint
                IPointCollection pPc = poly  as IPointCollection;


                IPoint p1 = pPc.get_Point(1);
                IPoint pn = pPc.get_Point(pPc.PointCount - 2);

                IPoint point = featureclass0.GetFeature(0).Shape as IPoint;

                double fdismin = dis(frompoint, point);//距离

                double fa1 = angle(frompoint, point );
                double fa2 = angle(p1, frompoint);

                double fA = Math.Min(Math.Abs(fa1 - fa2), Math.Abs(Math.Abs(fa1 - fa2) - 360));

                double fsame = 0.5 * Math.Exp(-fdismin/3600) + 0.5 *Math.Exp( Math.Cos ( fA )-1);

                //topoint
                double tdismin = dis(topoint, point);
                double ta1 = angle(topoint, point);
                double ta2 = angle(pn, topoint );

                double tA = Math.Min(Math.Abs(ta1 - ta2), Math.Abs(Math.Abs(ta1 - ta2) - 360));

                double tsame = 0.5 * Math.Exp(-tdismin/3600) + 0.5 *Math .Exp (Math.Cos ( tA)-1 );
                

                int fnummin = 0;
                int tnummin = 0;
                for (int j = 1; j < featureclass0.FeatureCount(null); j++)
                {
                    IPoint point1 = featureclass0.GetFeature(j).Shape as IPoint;

                    double dis11 = dis(frompoint, point1);

                    double fa11 = angle(frompoint, point1);
                    double fa22 = angle(p1, frompoint);

                    double fA11 = Math.Min(Math.Abs(fa11 - fa22), Math.Abs(Math.Abs(fa11 - fa22) - 360));
                    double fsame11 = 0.5 *Math.Exp (- dis11/3600 )+ 0.5 *Math.Exp (Math.Cos ( fA11)-1 );


                    double dis22 = dis(topoint, point1);

                    double ta11 = angle(topoint, point1);
                    double ta22 = angle(pn, topoint);

                    double tA11 = Math.Min(Math.Abs(ta11 - ta22), Math.Abs(Math.Abs(ta11 - ta22) - 360));
                    double tsame11 = 0.5 *Math.Exp (- dis22/3600 )+ 0.5 *Math.Exp (Math.Cos ( tA11)-1 );




                    if (fsame11 > fsame)
                    {
                        fdismin = fsame11;
                        fnummin = j;

                    }
                    if (tsame11 > tsame)
                    {
                        tsame = tsame11;
                        tnummin = j;

                    }


                }

                if (tnummin == fnummin)
                {

                    if (fsame > tsame)
                    {

                        IPoint p = featureclass0.GetFeature(fnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);



                    }

                    else
                    {
                        IPoint p = featureclass0.GetFeature(tnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = topoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);


                    }

                }

                else
                {

                   
                        IPoint p = featureclass0.GetFeature(fnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);
                    

                    
                        IPoint pp = featureclass0.GetFeature(tnummin).Shape as IPoint;
                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = pp;

                        pPolyline.Add(newpoly1);

                    


                }


            }



            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, pPolyline);

            //IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, Apolyline);
            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion












        }

        private void 优化匹配交叉点4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> pPolyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            ILayer player1 = pmap.get_Layer(1);
            IFeatureClass featureclass0 = null;
            IFeatureClass featureclass1 = null;
            if (player0.Name == "交叉点")
            {
                //交叉点
                IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;

            }
            else
            {

                //交叉点
                IFeatureLayer featurelayer0 = player1 as IFeatureLayer;
                featureclass0 = featurelayer0.FeatureClass;

                //骨架线
                IFeatureLayer featurelayer1 = player0 as IFeatureLayer;
                featureclass1 = featurelayer1.FeatureClass;


            }

            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {
                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                pPolyline.Add(poly);



            }


            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {


                IFeature feature = featureclass1.GetFeature(i);


                IPolyline poly = feature.Shape as IPolyline;
                IPoint frompoint = poly.FromPoint;
                IPoint topoint = poly.ToPoint;

                IPointCollection pPc = poly as IPointCollection;
                IPoint p1 = pPc.get_Point(1);
                IPoint pn = pPc.get_Point(pPc.PointCount - 2);

                IPoint point = featureclass0.GetFeature(0).Shape as IPoint;

                double fdismin = dis(frompoint, point);
                double tdismin = dis(topoint, point);

                int fnummin = 0;
                int tnummin = 0;
                int a = 0;
                int b = 0;
                for (int j = 1; j < featureclass0.FeatureCount(null); j++)
                {
                    IPoint point1 = featureclass0.GetFeature(j).Shape as IPoint;

                    double dis11 = dis(frompoint, point1);
                    double dis22 = dis(topoint, point1);


                    if (dis11 < fdismin)
                    {
                        fdismin = dis11;
                        fnummin = j;

                    }
                    if (dis22 < tdismin)
                    {
                        tdismin = dis22;
                        tnummin = j;

                    }


                }

                for (int n = 0; n < featureclass1.FeatureCount(null); n++)
                {
                    if (i == n)
                        continue;

                    IPolyline py = featureclass1.GetFeature(n).Shape as IPolyline;

                    IPoint fromp = py.FromPoint;
                    IPoint tp = py.ToPoint;
                    double fdis1 = dis(frompoint, fromp);
                    double fdis11 = dis(frompoint, tp);
                    double tdis2 = dis(topoint, fromp);
                    double tdis22 = dis(topoint, tp);
                    if (fdis1 != 0)
                    {
                        if (fdis1 < fdismin)
                        {
                            fdismin = fdis1;
                            fnummin = n;
                            a = 1;

                        }
                    }
                    if (fdis11 != 0)
                    {
                        if (fdis11 < fdismin)
                        {
                            fdismin = fdis11;
                            fnummin = n;
                            a = 2;

                        }
                    }
                    if (tdis2 != 0)
                    {
                        if (tdis2 < tdismin)
                        {
                            tdismin = tdis2;
                            tnummin = n;
                            b = 1;

                        }
                    }
                    if (tdis22 != 0)
                    {

                        if (tdis22 < tdismin)
                        {
                            tdismin = tdis22;
                            tnummin = n;
                            b = 2;

                        }
                    }


                }
                #region





                if (fdismin < 50)
                {
                    if (a == 0)
                    {
                        IPoint p = featureclass0.GetFeature(fnummin).Shape as IPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);
                    }

                    if (a == 1)
                    {
                        IPolyline ppoly = featureclass1.GetFeature(fnummin).Shape as IPolyline;
                        IPoint p = ppoly.FromPoint;
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = p;

                        pPolyline.Add(newpoly);

                    }

                    if (a == 2)
                    {
                        IPolyline ppoly = featureclass1.GetFeature(fnummin).Shape as IPolyline;
                        IPoint p = ppoly.ToPoint;

                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = p;

                        pPolyline.Add(newpoly1);

                    }





                }
                else
                {


                    IPoint point1 = featureclass0.GetFeature(0).Shape as IPoint;

                    double anglemin = 60;
                    double dis1 = dis(point1, frompoint);
                    IPoint pt = null;


                    double f1 = angle(frompoint, point1);
                    double f2 = angle(p1, frompoint);

                    double fA1 = Math.Min(Math.Abs(f1 - f2), Math.Abs(Math.Abs(f1 - f2) - 360));

                    if ((fA1 < anglemin) & (dis1 < 150))
                    {
                        pt = point1;
                        anglemin = fA1;

                    }


                    for (int m = 1; m < featureclass0.FeatureCount(null); m++)
                    {

                        IPoint p = featureclass0.GetFeature(m).Shape as IPoint;

                        double dis11 = dis(frompoint, p);

                        double fa11 = angle(frompoint, p);
                        double fa22 = angle(p1, frompoint);

                        double fA11 = Math.Min(Math.Abs(fa11 - fa22), Math.Abs(Math.Abs(fa11 - fa22) - 360));

                        if ((fA11 < anglemin) & (dis11 < 150))
                        {
                            pt = p;
                            anglemin = fA11;

                        }


                    }

                 

                    if (pt != null)
                    {
                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = frompoint;
                        newpoly.ToPoint = pt;
                        if (existpoly(newpoly, pPolyline) == false)
                            pPolyline.Add(newpoly);



                    }

                }

                if (tdismin < 50)
                {
                    if (b == 0)
                    {
                        IPoint p11 = featureclass0.GetFeature(tnummin).Shape as IPoint;
                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = p11;

                        pPolyline.Add(newpoly1);
                    }
                    if (b == 1)
                    {
                        IPolyline ppoly = featureclass1.GetFeature(tnummin).Shape as IPolyline;
                        IPoint p11 = ppoly.FromPoint;

                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = p11;

                        pPolyline.Add(newpoly1);

                    }

                    if (b == 2)
                    {
                        IPolyline ppoly = featureclass1.GetFeature(tnummin).Shape as IPolyline;
                        IPoint p11 = ppoly.ToPoint;

                        IPolyline newpoly1 = new PolylineClass();
                        newpoly1.FromPoint = topoint;
                        newpoly1.ToPoint = p11;

                        pPolyline.Add(newpoly1);

                    }


                }

                else
                {
                    IPoint point1 = featureclass0.GetFeature(0).Shape as IPoint;

                    double anglemin = 60;
                    double dis1 = dis(point1, topoint);
                    IPoint pt = null;


                    double f1 = angle(topoint, point1);
                    double f2 = angle(pn, topoint);

                    double fA1 = Math.Min(Math.Abs(f1 - f2), Math.Abs(Math.Abs(f1 - f2) - 360));

                    if ((fA1 < anglemin) & (dis1 < 150))
                    {
                        pt = point1;
                        anglemin = fA1;

                    }


                    for (int m = 1; m < featureclass0.FeatureCount(null); m++)
                    {
                        IPoint p = featureclass0.GetFeature(m).Shape as IPoint;

                        double dis11 = dis(topoint, p);

                        double fa11 = angle(topoint, p);
                        double fa22 = angle(pn, topoint);

                        double fA11 = Math.Min(Math.Abs(fa11 - fa22), Math.Abs(Math.Abs(fa11 - fa22) - 360));

                        if ((fA11 < anglemin) & (dis11 < 150))
                        {

                            anglemin = fA11;
                            pt = p;




                        }


                    }
                   
                    if (pt != null)
                    {


                        IPolyline newpoly = new PolylineClass();
                        newpoly.FromPoint = topoint;
                        newpoly.ToPoint = pt;

                        if (existpoly(newpoly, pPolyline) == false)
                            pPolyline.Add(newpoly);


                    }





                }
                #endregion




            }




            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, pPolyline);

            //IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, Apolyline);
            axMapControl1.Map.AddLayer(pFeatureLayer);






            #endregion



        }

        private void 规则2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);


            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//三角形


            List<ISegment> pPolyline1 = new List<ISegment>();

            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
                ISegmentCollection pPc = featureclass0.GetFeature(i).Shape as ISegmentCollection;
                ISegment sg0 = pPc.get_Segment(0);
                double a = sg0.Length;
                double max = a;
                ISegment lab = sg0;
                //int a1 = 0;
                for (int ii = 1; ii < pPc.SegmentCount; ii++)
                {

                    ISegment sg1 = pPc.get_Segment(ii);

                    double b = sg1.Length;
                    if (b > max)
                    {
                        max = b;
                        lab = sg1;
                        //a1 = ii;
                    }



                }

                pPolyline1.Add(lab);              

            }

            


            
            List<IPolyline> polyline = new List<IPolyline>();
           
            ILayer player1 = pmap.get_Layer(1);
            IFeatureLayer featurelayer1 = player1 as IFeatureLayer;
            IFeatureClass featureclass1 = featurelayer1.FeatureClass;//候选truelink
            

            for (int i = 0; i < featureclass1.FeatureCount(null); i++)
            {

                IPolyline py = featureclass1.GetFeature(i).Shape as IPolyline;                
                ITopologicalOperator top = featureclass1.GetFeature(i).Shape as ITopologicalOperator;
                int counts = 0;
                int counte = 0;
                List<int> labs = new List<int>();
                List<int> labe = new List<int>();
                IPoint start = py.FromPoint;
                IPoint end = py.ToPoint;
                for (int ii = 0; ii < featureclass1.FeatureCount(null); ii++)
                {
                    if (ii == i)
                    {
                        continue;
                    }


                    IGeometry result1 = top.Intersect(featureclass1.GetFeature(ii).Shape, esriGeometryDimension.esriGeometry0Dimension);

                    ITopologicalOperator result0 = result1 as ITopologicalOperator;
                    IGeometry buffer = result0.Buffer(0.00005);

                    IRelationalOperator result = buffer as IRelationalOperator;
                    if ((result1.IsEmpty == false))
                    {
                        if (result.Contains(start))
                        {

                            counts = counts + 1;
                            labs.Add(ii);
                        }

                        if (result.Contains(end))
                        {

                            counte = counte + 1;
                            labe.Add(ii);
                        }


                    }



                }

                if (counts == 0 | counte == 0)
                {
                    polyline.Add(py);
                }
                else

                {
                    List<int> lab = new List<int>();

                    for (int j = 0; j < labe.Count; j++)
                    {

                       
                        ITopologicalOperator Top = featureclass1.GetFeature(labe[j]).Shape as ITopologicalOperator;

                        for (int jj = 0; jj< labs.Count; jj++)
                        {

                            IGeometry result1 = Top.Intersect(featureclass1.GetFeature(labs[jj]).Shape, esriGeometryDimension.esriGeometry0Dimension);
                            if ((result1.IsEmpty == false))
                            {
                                lab.Add(j);
                                lab.Add(jj);
                            
                            }

                        
                        }
                    
                    
                    }
                    int count = 0;
                    int labb = 0;
                    for (int m = 0; m < lab.Count; m=m +2)
                    {
                        IPolyline py1 = featureclass1.GetFeature(labe[lab[m]]).Shape as IPolyline;
                        IPolyline py2 = featureclass1.GetFeature(labs[lab[m+1]]).Shape as IPolyline;
                        if ((py.Length >py1 .Length )&(py.Length >py2.Length))
                        {
                            count++;
                            labb = m;
                        
                        }
                    
                    
                    }

                    if ((count == 1)&(lab.Count /2==1))
                    {
                        IPolyline py1 = featureclass1.GetFeature(labe[lab[labb]]).Shape as IPolyline;
                        IPolyline py2 = featureclass1.GetFeature(labs[lab[labb+1]]).Shape as IPolyline;

                        if (within1(py1, pPolyline1) == true & within1(py2, pPolyline1) == true)
                       {
                           polyline.Add(py);
                       
                       }



                    }
                    if (count==0)
                    {

                        polyline.Add(py);
                    
                    }
                
                
                }






            }

            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);




            #endregion




        }

        private void 规则3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IPolyline> polyline = new List<IPolyline>();
            IMap pmap = axMapControl1.Map;
            ILayer player0 = pmap.get_Layer(0);
            IFeatureLayer featurelayer0 = player0 as IFeatureLayer;
            IFeatureClass featureclass0 = featurelayer0.FeatureClass;//候选truelink
            
            for (int i = 0; i < featureclass0.FeatureCount(null); i++)
            {
               
                IPolyline py = featureclass0.GetFeature(i).Shape as IPolyline;
                ITopologicalOperator top = featureclass0.GetFeature(i).Shape as ITopologicalOperator;
                int counts = 0;
                int counte = 0;
                List<int> labs = new List <int>();
                List<int> labe = new List<int>();
                IPoint start = py.FromPoint;
                IPoint end = py.ToPoint;
                for (int ii = 0; ii < featureclass0.FeatureCount(null); ii++)
                {
                    if (ii == i)
                    {
                        continue;
                    }
                    

                    IGeometry result1 = top.Intersect(featureclass0.GetFeature(ii).Shape, esriGeometryDimension.esriGeometry0Dimension);

                    ITopologicalOperator result0 = result1 as ITopologicalOperator;
                    IGeometry buffer = result0.Buffer(0.00005);

                    IRelationalOperator result = buffer as IRelationalOperator;
                    if ((result1.IsEmpty == false))
                    {
                        if (result.Contains(start))
                        {

                            counts = counts + 1;
                            labs.Add(ii);
                        }

                        if (result.Contains(end))
                        {

                            counte = counte + 1;
                            labe.Add(ii);
                        }


                    }



                }
                if (counts == 0|counte ==0)
                {
                    if (counts == 0)
                    {
                        int count = 0;
                        for (int j = 0; j < labe.Count; j++)
                        {
                            IPolyline poly1 = featureclass0.GetFeature(labe[j]).Shape as IPolyline;

                            IPoint start1 = poly1.FromPoint;
                            IPoint end1 = poly1.ToPoint;

                            double angle1 = 0;
                            double angle2 = 0;
                            if ((start.X != start1.X) | (start.Y != start1.Y))
                            {
                                if ((end.X != end1.X) | (end.Y != end1.Y))
                                {
                                    angle1 = angle(end, start);
                                    angle2 = angle(start1, end1);
                                }
                                else
                                {
                                    angle1 = angle(end, start);
                                    angle2 = angle(end1, start1);



                                }

                            }

                            else if ((start.X == start1.X) & (start.Y == start1.Y))
                            {

                                angle1 = angle(start, end);
                                angle2 = angle(start1, end1);

                            }

                            double diff = Math.Min(Math.Abs(angle1 - angle2), Math.Abs(360 - Math.Abs(angle1 - angle2)));
                            if (diff < 60)
                            {

                                count = count + 1;

                            }
                            
                        
                        }

                        if (count ==0)
                           {
                                polyline.Add(py);
                            }
                    
                    }

                    if (counte == 0)
                    {
                        int count = 0;
                        for (int j = 0; j < labs.Count; j++)
                        {
                            IPolyline poly1 = featureclass0.GetFeature(labs[j]).Shape as IPolyline;

                            IPoint start1 = poly1.FromPoint;
                            IPoint end1 = poly1.ToPoint;

                            double angle1 = 0;
                            double angle2 = 0;
                            if ((start.X != start1.X) | (start.Y != start1.Y))
                            {
                                if ((end.X != end1.X) | (end.Y != end1.Y))
                                {
                                    angle1 = angle(end, start);
                                    angle2 = angle(start1, end1);
                                }
                                else
                                {
                                    angle1 = angle(end, start);
                                    angle2 = angle(end1, start1);



                                }

                            }

                            else if ((start.X == start1.X) & (start.Y == start1.Y))
                            {

                                angle1 = angle(start, end);
                                angle2 = angle(start1, end1);

                            }

                            double diff = Math.Min(Math.Abs(angle1 - angle2), Math.Abs(360 - Math.Abs(angle1 - angle2)));
                            if (diff < 60)
                            {

                                count = count + 1;

                            }


                        }

                        if (count == 0)
                        {
                            polyline.Add(py);
                        }

                    }


                }
                else
                {

                    polyline.Add(py);
                
                }

            }


            #region 生成shp文件


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shape文件(*.shp)|*.shp";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;

            }

            IFeatureLayer pFeatureLayer = Createpolylineshp1(shapeFileFullName, polyline);


            axMapControl1.Map.AddLayer(pFeatureLayer);




            #endregion







        }

     



        }
    }

