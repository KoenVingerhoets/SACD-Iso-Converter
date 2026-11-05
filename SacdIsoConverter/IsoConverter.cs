
using System;
using System.IO;
using System.Threading.Tasks;

namespace SacdIsoConverter;
public static class IsoConverter
{
    const int SectorSize = 2064;
    const int PayloadOffset = 12;
    const int PayloadSize = 2048;
    public static async Task ConvertAsync(string input,string output,IProgress<int>? progress=null)
    {
        using FileStream source=new(input,FileMode.Open,FileAccess.Read);
        using FileStream dest=new(output,FileMode.Create,FileAccess.Write);
        byte[] sector=new byte[SectorSize];
        long total=source.Length;
        long processed=0;
        while(await source.ReadAsync(sector,0,SectorSize)==SectorSize)
        {
            await dest.WriteAsync(sector,PayloadOffset,PayloadSize);
            processed+=SectorSize;
            progress?.Report((int)((processed*100)/total));
        }
    }
}
