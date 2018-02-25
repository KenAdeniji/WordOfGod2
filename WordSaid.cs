using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace WordEngineering
{
 ///<summary>WordSaid.</summary>
 public class WordSaid
 {
  private int                sequenceOrderId        = -1;
  private static int         WordSaidTableNameTotal = -1;    
  private string             commentary             = null;
  private string             scriptureReference     = null;
  private static string[]    WordSaidTableName      =
                             {
                              "Advertisement",
                              "AlphabetSequence",
                              "CaseBasedReasoning",
                              "ClassAssociates",
                              "Cry",
                              "Error",
                              "Export",
                              "JehovahRophe",
                              "Keyboard",
                              "Mouse",   
                              "Prophecy",
                              "Remember",
                              "ScriptureReading",
                              "Software",
                              "UserInterface",
                              "WordSaid"
                             }; 

  private DateTime           dated;
  private Guid               uniqueId;

  System.Data.DataSet        dataSetWordSaid       = null;
  
  System.Data.DataTable[]    dataTableWordSaid;

  private int                ElementAdvertisement          = 0;
  private int                ElementAlphabetSequence       = 1;
  private int                ElementCaseBasedReasoning     = 2;
  private int                ElementClassAssociates        = 3;
  private int                ElementCry                    = 4;
  private int                ElementError                  = 5;
  private int                ElementExport                 = 6;  
  private int                ElementJehovahRophe           = 7;
  private int                ElementKeyboard               = 8;   
  private int                ElementMouse                  = 9;   
  private int                ElementProphecy               = 10;   
  private int                ElementRemember               = 11;   
  private int                ElementScriptureReading       = 12;   
  private int                ElementSoftware               = 13;     
  private int                ElementUserInterface          = 14;       
  private int                ElementWordSaid               = 15;       
  
  ///<summary>Constructor.</summary>
  public WordSaid
  (
   int      sequenceOrderId,
   string   commentary,
   string   scriptureReference,
   DateTime dated,
   Guid     uniqueId
  ) 
  {
   this.commentary         = commentary;
   this.dated              = dated;
   this.uniqueId           = uniqueId;
   this.scriptureReference = scriptureReference;
   this.sequenceOrderId    = sequenceOrderId;
  }//public WordSaid().

  ///<summary>Commentary.</summary>
  ///<value>A value tag is used to describe the property value</value>
  public string Commentary
  {
   get 
   {
    return commentary;
   }//get
   set
   {
    this.commentary = value;
   } 
  }//public string Commentary

  ///<summary>Dated.</summary>
  ///<value>A value tag is used to describe the property value</value>
  public DateTime Dated
  {
   get 
   {
    return dated;
   }//get
   set
   {
    this.dated = value;
   } 
  }//public string Commentary

  ///<summary>Scripture Reference.</summary>
  ///<value>A value tag is used to describe the property value</value>
  public string ScriptureReference
  {
   get 
   {
    return scriptureReference;
   }//get
   set
   {
    this.scriptureReference = value;
   } 
  }//public string ScriptureReference

  ///<summary>Sequence Order Id.</summary>
  ///<value>A value tag is used to describe the property value</value>
  public int SequenceOrderId
  {
   get 
   {
    return sequenceOrderId;
   }//get
   set
   {
    this.sequenceOrderId = value;
   } 
  }//public int SequenceOrderId

  ///<summary>Unique Identity.</summary>
  ///<value>A value tag is used to describe the property value</value>
  public Guid UniqueId
  {
   get 
   {
    return uniqueId;
   }//get
   set
   {
    this.uniqueId = value;
   } 
  }//public Guid UniqueId

  ///<summary>DataSetInitialize().</summary>
  public void DataSetInitialize()
  {
   int wordSaidTableNameCount = -1;
   dataSetWordSaid   = new DataSet();
   dataTableWordSaid = new DataTable[WordSaidTableNameTotal];
   for ( wordSaidTableNameCount = 0; wordSaidTableNameCount < WordSaidTableNameTotal; ++wordSaidTableNameCount )
   {
    dataTableWordSaid[wordSaidTableNameCount] = new DataTable( WordSaidTableName[wordSaidTableNameCount] );
   }//for ( wordSaidTableNameCount = 0; wordSaidTableNameCount < wordSaidTableNameTotal; ++wordSaidTableNameCount )
  }//public static DataSet DataSetInitialize()    
  
  static WordSaid()
  {
   WordSaidTableNameTotal = WordSaidTableName.Length; 
  }//static WordSaid()
    
 }//public class WordSaid.
}//namespace WordEngineering.