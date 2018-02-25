using System;
using System.IO;
using System.Reflection;
using SpeechTypeLib;

namespace WordEngineering
{

 /// <summary>UtilitySpeechArgument</summary>
 public class UtilitySpeechArgument
 {
  ///<summary>xml</summary>  
  public bool     xml           =  false;

  ///<summary>pathSource</summary>  
  public string[] pathSource    =  null;

  ///<summary>pathAudio</summary>  
  public string   pathAudio     =  null;

  ///<summary>text</summary>  
  public string[] text          =  null;

  ///<summary>voice</summary>  
  public string[] voice         =  null;

  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public String[] files;

  /// <summary>Constructor Overloading</summary>
  public UtilitySpeechArgument()
  :this
  (
   false, //xml
   null,  //pathSource
   null,  //pathAudio
   null,  //text
   null   //voice
  ) 
  {
  }//public UtilitySpeechArgument()

  /// <summary>Constructor Overloading</summary>
  public UtilitySpeechArgument
  (
   bool     xml,
   string[] pathSource,
   string   pathAudio,
   string[] text,
   string[] voice
  )
  {
   this.xml         =  xml;
   this.pathSource  =  pathSource;
   this.pathAudio   =  pathAudio;
   this.text        =  text;
   this.voice       =  voice;
  }//public UtilitySpeechArgument()
 }//public class UtilitySpeechArgument

 ///<summary>UtilitySpeech</summary>
 ///<remarks>
 /// Speech Application Language Tags (SALT)
 /// EggHeadCafe.com/articles/20050813.asp                          Peter A. Bromberg, Ph.D. Build a SAPI Text to Wav Converter Library
 /// SamsPublishing.com/articles/article.asp?p=27219&amp;seqNum=1   Adam Nathan The Essentials for Using COM in Managed Code
 /// Microsoft.com/indonesia/msdn/hip_aspnet.asp                    An ASP.NET Framework for Human Interactive Proofs
 /// Blogs.msdn.com/speechlead                                      Philipp Schmid
 ///</remarks>
 public class UtilitySpeech
 {
  ///<summary>SpeechVoiceSpeakFlagsDefault</summary>
  public static SpeechVoiceSpeakFlags  SpeechVoiceSpeakFlagsDefault   =  SpeechVoiceSpeakFlags.SVSFDefault;
  
  ///<summary>SpeechVoiceSpeakFlagsFilename</summary>
  public static SpeechVoiceSpeakFlags  SpeechVoiceSpeakFlagsFilename  =  SpeechVoiceSpeakFlags.SVSFIsFilename;

  ///<summary>SpeechVoiceSpeakFlagsXML</summary>
  public static SpeechVoiceSpeakFlags  SpeechVoiceSpeakFlagsXML       =  SpeechVoiceSpeakFlags.SVSFIsXML;

  ///<summary>ISpeechObjectTokensVoices</summary>
  public static ISpeechObjectTokens    ISpeechObjectTokensVoices      =  null;

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of arguments</param>
  public static void Main( string[] argv )
  {
   Boolean                  booleanParseCommandLineArguments  =  false;
   string                   exceptionMessage                  =  null;
   UtilitySpeechArgument    utilitySpeechArgument             =  null;
   
   utilitySpeechArgument         =  new UtilitySpeechArgument();
   
   booleanParseCommandLineArguments  =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    utilitySpeechArgument
   );

   if ( booleanParseCommandLineArguments == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( UtilitySpeechArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )

   SpVoiceSpeak
   (
    ref utilitySpeechArgument,
    ref exceptionMessage
   );
   
  }//public static void Main()

  ///<summary>SpeechVoiceSpeakFlagsEnum</summary>
  public static SpeechVoiceSpeakFlags SpeechVoiceSpeakFlagsEnum
  (
   bool filename,
   bool xml
  )
  {
   SpeechVoiceSpeakFlags  speechVoiceSpeakFlags  =  SpeechVoiceSpeakFlagsDefault;
   if ( filename == true ) { speechVoiceSpeakFlags += (int) SpeechVoiceSpeakFlags.SVSFIsFilename; }
   if ( xml == true ) { speechVoiceSpeakFlags += (int) SpeechVoiceSpeakFlags.SVSFIsXML; }
   #if (DEBUG)
    System.Console.WriteLine( "SpeechVoiceSpeakFlags: {0}", speechVoiceSpeakFlags );   
   #endif
   return ( speechVoiceSpeakFlags );
  }  	
  
  ///<summary>SpVoiceSpeak</summary>
  public static void SpVoiceSpeak
  (
   ref UtilitySpeechArgument  utilitySpeechArgument,
   ref string                 exceptionMessage
  )
  {
   object                 voice                  =  null;
   object[]               voiceArgv              =  null;                  
   SpeechVoiceSpeakFlags  speechVoiceSpeakFlags  =  SpeechVoiceSpeakFlagsDefault;
   SpAudioFormatClass     spAudioFormatClass     =  null;
   SpFileStream           spFileStream           =  null;
   SpVoice                spVoice                =  null;
   Type                   typeSAPISpVoice        =  null;
   try
   {
    spVoice                =  new SpVoice();
    typeSAPISpVoice        =  Type.GetTypeFromProgID("SAPI.SpVoice");
    voice                  =  Activator.CreateInstance( typeSAPISpVoice );
    voiceArgv              =  new object[2];
    voiceArgv[1]           =  0;
    speechVoiceSpeakFlags  =  SpeechVoiceSpeakFlagsEnum( false, utilitySpeechArgument.xml );
    if ( string.IsNullOrEmpty( utilitySpeechArgument.pathAudio ) == false )
    {
     spAudioFormatClass         =  new SpAudioFormatClass();
     spAudioFormatClass.Type    =  SpeechAudioFormatType.SAFTGSM610_11kHzMono; //Heavily compressed
     spFileStream               =  new SpFileStream();
     spFileStream.Format        =  spAudioFormatClass;
     spFileStream.Open( utilitySpeechArgument.pathAudio, SpeechStreamFileMode.SSFMCreateForWrite, false );
     spVoice.AudioOutputStream  =  spFileStream;
     spVoice.Rate = -5; //Ranges from -10 to 10 
    }
    foreach( string text in utilitySpeechArgument.text )
    {
     /*
     spVoice.Speak( text, speechVoiceSpeakFlags );
     */
     voiceArgv[0]  =  text;
     typeSAPISpVoice.InvokeMember("Speak", BindingFlags.InvokeMethod, null, voice, voiceArgv);
    }
    speechVoiceSpeakFlags = SpeechVoiceSpeakFlagsEnum( true, utilitySpeechArgument.xml );
    foreach( string pathSource in utilitySpeechArgument.pathSource )
    {
     if ( string.IsNullOrEmpty( pathSource ) ) { continue; }
     if ( File.Exists( pathSource ) == false ) { continue; }     
     spVoice.Speak( pathSource, speechVoiceSpeakFlags );
    }//foreach( string pathSource in utilitySpeechArgument.pathSource )
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
   if ( spFileStream != null ) { spFileStream.Close(); }
  }//public static void SpVoiceSpeak()
  
  ///<summary>SpeechObjectTokensVoices</summary>
  public static void SpeechObjectTokensVoices
  (
   ref ISpeechObjectTokens  speechObjectTokens,
   ref string               exceptionMessage
  )
  {
   SpVoice              spVoice             =  null;
   try
   {
    spVoice = new SpVoice();
    speechObjectTokens  =  spVoice.GetVoices("", "");
    #if (DEBUG)
     foreach ( ISpeechObjectToken  speechObjectToken in speechObjectTokens )
     {
      System.Console.WriteLine("Voice: {0}", speechObjectToken.GetDescription(1033) );
     }
    #endif
   }//try
   catch ( Exception exception ) { UtilityException.ExceptionLog( exception, exception.GetType().Name, ref exceptionMessage ); }
  }//public static ISpeechObjectTokens SpeechObjectTokensVoices()

  static UtilitySpeech()
  {
   string  exceptionMessage  =  null;
   SpeechObjectTokensVoices( ref ISpeechObjectTokensVoices, ref exceptionMessage );
  }//static UtilitySpeech()

 }//public class UtilitySpeech
}//namespace WordEngineering