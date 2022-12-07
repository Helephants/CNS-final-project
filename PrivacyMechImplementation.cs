
using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using XPlot.Plotly; 
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using Microsoft.Data.Analysis;




class Program
{
     static void MyMethod() 
{
  Console.WriteLine("I just got executed!");
}


    static void Main(string[] args)
    {
         
        if (args.Length!=0){
          if(args[0]=="csv"){
               
               using(var reader = new StreamReader(@"./S0002.csv"))
    {
        List<string> Xpos = new List<string>();
        List<string> Ypos = new List<string>();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
          //   Console.WriteLine(line);

            Xpos.Add(values[18]);
            Ypos.Add(values[19]);
        }
        var mech="";
        Console.WriteLine("Enter the privacy mechanism: Press G for Gaussian Noise, T for temporal down-sampling and S for Spatial down-sampling");
        mech = Console.ReadLine();
                  List<double> XposNew = new List<double>();
                  List<double> YposNew = new List<double>();
                  if (mech=="T"){
                    var k ="";
                    int k_new=3;
                    Console.WriteLine("Enter value of k for which the variables need to be downsampled, default is set to 3");
                    k = Console.ReadLine();
                    Int32.TryParse(k, out k_new);
                    int i =0;
                    foreach(var x in Xpos){
                         if ((i+1)%k_new!=0){
                              double x_new;
                              double y_new;
                              Double.TryParse(Xpos[i], out x_new);
                              Double.TryParse(Ypos[i], out y_new);
                              XposNew.Add(x_new);
                              YposNew.Add(y_new);
                              
                         }
                    i++;
                   

               }
          if (!File.Exists("./OutputFile.csv")){
                  using (StreamWriter sw = File.CreateText("./OuputFile.csv"))
            {  sw.WriteLine("x_var,y_var");
               var j=0;
               foreach(var xout in XposNew){
                    if (j>0){
                    sw.WriteLine(xout+","+YposNew[j]);
                  

                    }
                    j++;
                
            }

          }
                         
}
                    
                    
                    
}
                  if (mech=="G"){
                  var index=0;
                  foreach(var s in Xpos){
                         var mean = 0;
                         var stdDev =5;
                         // Console.WriteLine(s);
                         Random rand = new Random(); 
                         double u1 = 1.0-rand.NextDouble(); 
                         double u2 = 1.0-rand.NextDouble();
                         double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                              Math.Sin(2.0 * Math.PI * u2); 
                         double randNormal =
                              mean + stdDev * randStdNormal;
                         using (StreamWriter sw = File.AppendText("./normaldist.csv"))
                              {
                                   sw.WriteLine(randNormal);
                              }
                         Console.WriteLine("random normal:");
                         Console.WriteLine(randNormal);
                         double x_new;
                         double y_new;
                         Double.TryParse(Ypos[index], out y_new);
                         Double.TryParse(s, out x_new);
                         // double x_new = Convert.ToDouble(s);
                         x_new=x_new+randNormal;
                         y_new=y_new+randNormal;
                         XposNew.Add(x_new);
                         YposNew.Add(y_new);
                         index++;   
                         

        }

          if (!File.Exists("./OutputFile.csv")){
                  using (StreamWriter sw = File.CreateText("./OuputFile.csv"))
            {  sw.WriteLine("x_var,y_var");
               var j=0;
               foreach(var xout in XposNew){
                    if (j>0){
                    sw.WriteLine(xout+","+YposNew[j]);
                  

                    }
                    j++;
                   

               }
                
            }

          }
       
        
        

        }
        if (mech=="S"){
          Console.WriteLine("The default values for M and N are set to M=1080 and N=1920");
          Console.WriteLine("enter downsampling factor L");
          var L = Console.ReadLine();
          int l = 0;
          Int32.TryParse(L, out l);
          Console.WriteLine(l);
          int M = 1080;
          int N = 1920;
          if (l!=0){
          M=2160/l;
          N=3840/l; 

          }
 
          Console.WriteLine("M = "+M);
          Console.WriteLine("N = "+N);
          double delta_x=(double)360/(double)N;
          double delta_y=(double)180/(double)M;
          Console.WriteLine("delta x is "+delta_x);
          var i = 0;
          foreach (var s in Xpos){
               
                double x_new;
                double y_new;
                Double.TryParse(Xpos[i], out x_new);
                Double.TryParse(Ypos[i], out y_new);
                var mulx=Math.Floor(x_new/delta_x);
                var muly=Math.Floor(y_new/delta_y);
            
                x_new=mulx*delta_x;
                y_new=muly*delta_y;
                XposNew.Add((x_new));
                YposNew.Add((y_new));

                
                i++;

          }
             if (!File.Exists("./OutputFile.csv")){
                  using (StreamWriter sw = File.CreateText("./OuputFile.csv"))
            {  sw.WriteLine("x_var,y_var");
               var j=0;
               foreach(var xout in XposNew){
                    if (j>0){
                    sw.WriteLine(xout+","+YposNew[j]);
                  

                    }
                    j++;
                   

               }
                
            }

          }
          



          
        }
    



        }

        var path = ".";
        var fullPath = Path.GetFullPath(path);
        Console.WriteLine(fullPath);
        using var watcher = new FileSystemWatcher(@"/Users/harsheeshah/Desktop/ConsoleProj");
        watcher.Changed += OnChanged;
        
        MyMethod();
     //    Console.WriteLine(SpecialFunctions.Erf(0.5));

        var m = Matrix<double>.Build.Random(500, 500);
        var v = Vector<double>.Build.Random(500);
        var y = m.Solve(v);
     //    Console.WriteLine(y);

     //    var mean = 0;
     //    var stdDev =0.2;
     //    Random rand = new Random(); 
     //    double u1 = 1.0-rand.NextDouble(); 
     //    double u2 = 1.0-rand.NextDouble();
     //    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
     //         Math.Sin(2.0 * Math.PI * u2); 
     //    double randNormal =
     //         mean + stdDev * randStdNormal;
     //    Console.WriteLine(randNormal);
        double mean2 = 0;
        double stdDev2 = 0.2;

        MathNet.Numerics.Distributions.Normal normalDist = new MathNet.Numerics.Distributions.Normal(mean2, stdDev2);
        double randomGaussianValue = normalDist.Sample();
        var data=DataFrame.LoadCsv("./AOIs.csv", separator:';');
     //    Console.WriteLine(data);
     


        
        
        // double[] dataX = new double[] { 1, 2, 3, 4, 5 };
        // double[] dataY = new double[] { 1, 4, 9, 16, 25 };
        // var plt = new ScottPlot.Plot(400, 300);
        // plt.AddScatter(dataX, dataY);
        // new ScottPlot.FormsPlotViewer(plt).ShowDialog();
      
}
        static void OnChanged(object sender, FileSystemEventArgs e)
        {
          //   if (e.ChangeType != WatcherChangeTypes.Changed)
          //   {
          //       return;
          //   }
           if (e.ChangeType == WatcherChangeTypes.Changed)
    {
        var info = new FileInfo(e.FullPath);
        var theSize = info.Length;
        Console.WriteLine("file has changed");

    }
        }





}}
}
