
using System;
using System.IO;
using System.Windows.Forms;
namespace SacdIsoConverter;
public class MainForm:Form
{
 TextBox txtIn=new(); TextBox txtOut=new(); ProgressBar bar=new(); Button btn=new();
 public MainForm()
 {
   Text="SACD ISO Converter"; Width=700; Height=220;
   var l1=new Label(){Text="Input ISO",Left=10,Top=20,Width=80};
   txtIn.SetBounds(100,20,450,24);
   var b1=new Button(){Text="Browse",Left=560,Top=18};
   b1.Click+=(s,e)=>{using var d=new OpenFileDialog(){Filter="ISO Files|*.iso|All Files|*.*"}; if(d.ShowDialog()==DialogResult.OK){txtIn.Text=d.FileName; txtOut.Text=Path.Combine(Path.GetDirectoryName(d.FileName)!,Path.GetFileNameWithoutExtension(d.FileName)+"_2048.iso");}};
   var l2=new Label(){Text="Output",Left=10,Top=60,Width=80};
   txtOut.SetBounds(100,60,450,24);
   var b2=new Button(){Text="Browse",Left=560,Top=58};
   b2.Click+=(s,e)=>{using var d=new SaveFileDialog(){Filter="ISO Files|*.iso"}; if(d.ShowDialog()==DialogResult.OK) txtOut.Text=d.FileName;};
   bar.SetBounds(100,100,530,24);
   btn.Text="Convert"; btn.SetBounds(280,135,100,30);
   btn.Click+=async (s,e)=>{
      if(!File.Exists(txtIn.Text)){MessageBox.Show("Select a valid input ISO."); return;}
      btn.Enabled=false;
      try{
       var p=new Progress<int>(v=>bar.Value=Math.Min(100,Math.Max(0,v)));
       await IsoConverter.ConvertAsync(txtIn.Text,txtOut.Text,p);
       MessageBox.Show("Conversion completed.");
      }catch(Exception ex){MessageBox.Show(ex.Message);} finally{btn.Enabled=true;}
   };
   Controls.AddRange(new Control[]{l1,txtIn,b1,l2,txtOut,b2,bar,btn});
 }
}
