using System;
using System.Net;
using System.Text;

namespace WordEngineering
{
 ///<summary>UtilityICMP.</summary>
 ///<remarks>UtilityICMP.</remarks>
 public class UtilityICMP
 {
  ///<summary>type.</summary>
  public byte   type;
  
  ///<summary>codee.</summary>
  
  public byte   code;

  ///<summary>checksum.</summary>
  public UInt16 checksum;
  
  ///<summary>messageSize.</summary>
  public int    messageSize;
  
  ///<summary>message.</summary>  
  public byte[] message     = new byte[1024];

  ///<summary>The class constructor.</summary>
  public UtilityICMP()
  {
  }//public UtilityICMP()

  ///<summary>The class constructor.</summary>
  ///<param name="data">Data</param>
  ///<param name="size">Size</param>  
  public UtilityICMP
  (
   byte[] data, 
   int size
  )
  {
   type = data[20];
   code = data[21];
   checksum = BitConverter.ToUInt16(data, 22);
   messageSize = size - 24;
   Buffer.BlockCopy(data, 24, message, 0, messageSize);
  }//public UtilityICMP(byte[] data, int size)

  /// <summary>Code.</summary>
  public byte Code
  {
   get
   {
    return ( code );
   } 
   set
   {
    code = value;
   }
  }//public byte Type

  /// <summary>Checksum.</summary>
  public UInt16 Checksum
  {
   get
   {
    return ( checksum );
   } 
   set
   {
    checksum = value;
   }
  }//public byte Checksum

  /// <summary>Message.</summary>
  public byte[] Message
  {
   get
   {
    return ( message );
   } 
   set
   {
    message = value;
   }
  }//public int Message
  
  /// <summary>MessageSize.</summary>
  public int MessageSize
  {
   get
   {
    return ( messageSize );
   } 
   set
   {
    messageSize = value;
   }
  }//public int MessageSize

  /// <summary>Type.</summary>
  public byte Type
  {
   get
   {
    return ( type );
   } 
   set
   {
    type = value;
   }
  }//public byte Type

  ///<summary>GetBytes</summary>
  public byte[] GetBytes()
  {
   byte[] data = new byte[messageSize + 9];
   Buffer.BlockCopy(BitConverter.GetBytes(type), 0, data, 0, 1);
   Buffer.BlockCopy(BitConverter.GetBytes(code), 0, data, 1, 1);
   Buffer.BlockCopy(BitConverter.GetBytes(checksum), 0, data, 2, 2);
   Buffer.BlockCopy(message, 0, data, 4, messageSize);
   return data;
  }//public byte[] GetBytes()

  ///<summary>GetChecksum().</summary>
  public UInt16 GetChecksum()
  {
   UInt32 chcksm = 0;
   byte[] data = GetBytes();
   int packetsize = messageSize + 8;
   int index = 0;

   while ( index < packetsize)
   {
    chcksm += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
    index += 2;
   }
   chcksm = (chcksm >> 16) + (chcksm & 0xffff);
   chcksm += (chcksm >> 16);
   return (UInt16)(~chcksm);
  }//public UInt16 GetChecksum()
 }//class UtilityICMP
}//namespace WordEngineering 