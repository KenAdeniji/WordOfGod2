using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

//using	MSXML4;

namespace WordEngineering
{
 /// <summary>Utilty Serialize.</summary>
 public class UtiltySerialize
 {

  ///<summary>Serialize an object in an XML format.</summary>
  ///<returns>void.</returns>
  ///<code>UtilitySerialize.SerializeXml(instance);</code>
  public static void SerializeXml
  ( 
       object objectCurrent,
   ref string exceptionMessage
  ) 
  {
   string        className          = UtilityClass.SimpleName( objectCurrent );
   XmlSerializer xmlSerializer      = null;
   TextWriter    textWriter         = null;	
   Type          typeObjectCurrent  = objectCurrent.GetType();
   
   exceptionMessage = null;
   
   try
   {
    //Create a new XmlSerializer.
    xmlSerializer = new XmlSerializer( typeObjectCurrent );
       
    //Writing the file requires a StreamWriter.
    textWriter = new StreamWriter( className + ".xml" );

    // Serialize the class, write it to disk, and close the TextWriter. 
    xmlSerializer.Serialize( textWriter, objectCurrent );
    textWriter.Close();
   }//try   
   catch (SecurityException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SecurityException: {0}", exception.Message );
   }
   catch (XmlException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "XmlException: {0}", exception.Message );
   }
   catch (SystemException exception)
   {
    exceptionMessage = exception.Message;
    System.Console.WriteLine( "SystemException: {0}", exception.Message );
   }
   catch (Exception exception)
   {
    exceptionMessage = exception.Message;   
    System.Console.WriteLine( "Exception: {0}", exception.Message );
   }
   finally
   {
   }//finally
  }

  /*
  public void GetObjectData
  (
   SerializationInfo  serializationInfo,
   StreamingContext   streamingContext
  )
  {
   Add value to the Serialization information.
   serializationInfo.AddValue("arrayListCollection", arrayListCollection);
  }
  */
    
  /*
  public static void SerializeBinary()
  { SerializeBinary( strFilenameSerializeBinary ); }

  ///<summary>
  ///Serialize the bible book in Binary format.
  ///</summary>
  ///<returns>void.</returns>
  /// <param name="filenameXml">The filename to store the serialization.</param>
  ///<code>
  ///BibleBook.SerializeBinary(@"Serialize\BibleBook.bin");
  ///</code>
  public static void SerializeBinary( String strFilenameSerialize )
  {
   Stream           objStream          = null;
   BinaryFormatter  objBinaryFormatter = null;
   objStream          = File.Create(strFilenameSerialize);
   objBinaryFormatter = new BinaryFormatter();
   objBinaryFormatter.Serialize(objStream, new BibleBook() );
   objStream.Close();
  }

  ///<summary>
  ///Serialize the bible book in SOAP format.
  ///</summary>
  ///<returns>void.</returns>
  ///<code>
  ///BibleBook.SerializeSoap();
  ///</code>
  public static void SerializeSoap()
  { SerializeSoap(strFilenameSerializeSoap); }

  ///<summary>
  ///Serialize the bible book in SOAP format.
  ///</summary>
  ///<returns>void.</returns>
  ///<param name="strFilenameSerialize">The filename to store the serialization.</param>
  ///<code>
  ///BibleBook.SerializeSoap(@"Serialize\BibleBook.xml");
  ///</code>
  public static void SerializeSoap( String strFilenameSerialize )
  {
   Stream         objStream        = null;
   SoapFormatter  objSoapFormatter = null;
   objStream        = File.Create(strFilenameSerialize);
   objSoapFormatter = new SoapFormatter();
   objSoapFormatter.Serialize(objStream, new BibleBook());
   objStream.Close();
  }

  ///<summary>
  ///Serialize the bible book in XML format.
  ///</summary>
  ///<returns>void.</returns>
  /// <param name="strFilenameSerialize">The filename to store the XML file.</param>
  ///<code>
  ///BibleBook.SerializeXml(@"Serialize\BibleBook.xml");
  ///</code>
  public static void SerializeXml( String strFilenameSerialize )
  {
   XmlSerializer objXmlSerializer = null;
   TextWriter    objTextWriter    = null;	
   BibleBook[]   arrayBibleBook   = null;
   //Convert the natural storage, ArrayList, to Array
   arrayBibleBook = (BibleBook[]) arrayListCollection.ToArray( Type.GetType("WordEngineering.BibleBook") );
   //Create a new XmlSerializer.
   objXmlSerializer = new XmlSerializer( typeof( BibleBook[] ) );
   //Writing the file requires a StreamWriter.
   objTextWriter = new StreamWriter( strFilenameSerialize );
   // Serialize the class, write it to disk, and close the TextWriter. 
   objXmlSerializer.Serialize( objTextWriter, arrayBibleBook );
   objTextWriter.Close();
  }

  public static void XMLDeserialize()
  { XMLDeserialize( strFilenameSerializeXml ); }

  ///<summary>
  ///Deserialize the bible book in XML format.
  ///</summary>
  ///<returns>void.</returns>
  ///<param name="filenameXml">The filename to store the XML file.</param>
  ///<code>
  ///BibleBook.XMLDeserialize( @"Serialize\BibleBook.xml" );
  ///</code>
  public static void XMLDeserialize( String strFilenameSerializeXml )
  {
   FileStream     objFileStream     = null;
   XmlSerializer  objXmlSerializer  = null;

   //Declare an object variable of the type to be deserialized.
   BibleBook[]    arrayBibleBook    = null;
   //Create a new XmlSerializer.
   objXmlSerializer = new XmlSerializer( typeof( BibleBook[] ) );
   objXmlSerializer.UnknownNode	+= new XmlNodeEventHandler(serializer_UnknownNode);
   objXmlSerializer.UnknownAttribute	+= new XmlAttributeEventHandler(serializer_UnknownAttribute);
   // A FileStream is needed to read the XML document.
   objFileStream=new FileStream( strFilenameSerializeXml, FileMode.Open);
   //Use the Deserialize method to restore the object's state with
   //Data from the XML document. 
   arrayBibleBook = (BibleBook[]) objXmlSerializer.Deserialize(objFileStream);
   foreach ( BibleBook bibleBook in arrayBibleBook )
   { System.Console.WriteLine( bibleBook ); }
  }
  
  protected static void serializer_UnknownNode
  ( object sender, XmlNodeEventArgs e )
  { System.Console.WriteLine("Unknown Node:" +   e.Name + "\t" + e.Text); }
  protected static void serializer_UnknownAttribute( object sender, XmlAttributeEventArgs e )
  { System.Xml.XmlAttribute attr = e.Attr;
    Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
  }
  */
  
 }//UtilitySerialize
}//WordEbgineering  
