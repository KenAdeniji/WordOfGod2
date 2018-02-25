using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Text;

namespace WordEngineering
{

 /// <summary>
 /// A delegate used in error reporting.
 /// </summary>
 public delegate void ErrorReporter(string message);

 /// <summary>
 /// Used to control parsing of command line arguments.
 /// </summary>
 [Flags]    
 public enum CommandLineArgumentType
 {
  /// <summary>
  /// Indicates that this fieldInfo is required. An error will be displayed
  /// if it is not present when parsing arguments.
  /// </summary>
  Required    = 0x01,

  /// <summary>
  /// Only valid in conjunction with Multiple.
  /// Duplicate values will result in an error.
  /// </summary>
  Unique      = 0x02,
  
  /// <summary>
  /// Inidicates that the argument may be specified more than once.
  /// Only valid if the argument is a collection
  /// </summary>
  Multiple    = 0x04,

  /// <summary>
  /// The default type for non-collection arguments.
  /// The argument is not required, but an error will be reported if it is specified more than once.
  /// </summary>
  AtMostOnce  = 0x00,
        
  /// <summary>
  /// For non-collection arguments, when the argument is specified more than
  /// once no error is reported and the value of the argument is the last
  /// value which occurs in the argument list.
  /// </summary>
  LastOccurenceWins = Multiple,

  /// <summary>
  /// The default type for collection arguments.
  /// The argument is permitted to occur multiple times, but duplicate 
  /// values will cause an error to be reported.
  /// </summary>
  MultipleUnique  = Multiple | Unique,
  
 }//public enum CommandLineArgumentType

 /// <summary>WCArgument</summary>
 public class WCArgument
 {
  ///<summary>lines</summary>
  public bool lines;

  ///<summary>words</summary>  
  public bool words;
  
  ///<summary>chars</summary>  
  public bool chars;
  
  ///<summary>files</summary>
  [DefaultCommandLineArgument(CommandLineArgumentType.MultipleUnique)]
  public string[] files;
 }//public class WCArgument

 /// <summary>UtilityCommandLineArgument</summary>
 public class UtilityCommandLineArgument
 {
  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">A list of command line arguments</param>
  public static void Main
  (
   String[] argv
  )
  {
   Boolean     booleanParseCommandLineArguments  =  false;
   WCArgument  wCArgument                        =  null;
   
   wCArgument = new WCArgument();
   
   booleanParseCommandLineArguments =  UtilityParseCommandLineArgument.ParseCommandLineArguments
   ( 
    argv, 
    wCArgument
   );
   
   if ( booleanParseCommandLineArguments  == false )
   {
    // error encountered in arguments. Display usage message
    System.Console.Write
    (
     UtilityParseCommandLineArgument.CommandLineArgumentsUsage( typeof ( WCArgument ) )
    );
    return;
   }//if ( booleanParseCommandLineArguments  == false )
   
  }//static void Main( String[] argv ) 
  
 }//public class UtilityCommandLineArgument

 /// <summary>allow /? or /help to automatically generate the usage text </summary>
 public class HelpArgument 
 { 
  ///<summary>help</summary>
  [CommandLineArgumentAttribute(CommandLineArgumentType.AtMostOnce, ShortName="?")] 
  public bool help = false; 
 }//public class HelpArgument 
    
 /// <summary>
 /// Allows control of command line parsing.
 /// Attach this attribute to instance fields of types used
 /// as the destination of command line argument parsing.
 /// </summary>
 [AttributeUsage(AttributeTargets.Field)]
 public class CommandLineArgumentAttribute : Attribute
 {
  /// <summary>
  /// Allows control of command line parsing.
  /// </summary>
  /// <param name="type"> Specifies the error checking to be done on the argument. </param>
  public CommandLineArgumentAttribute(CommandLineArgumentType type)
  {
   this.type = type;
  }
        
  /// <summary>
  /// The error checking to be done on the argument.
  /// </summary>
  public CommandLineArgumentType Type
  {
   get { return this.type; }
  }
  
  /// <summary>
  /// Returns true if the argument did not have an explicit short name specified.
  /// </summary>
  public bool DefaultShortName    { get { return null == this.shortName; } }
        
  /// <summary>
  /// The short name of the argument.
  /// </summary>
  public string ShortName
  {
   get { return this.shortName; }
   set { this.shortName = value; }
  }

  /// <summary>
  /// Returns true if the argument did not have an explicit long name specified.
  /// </summary>
  public bool DefaultLongName     { get { return null == this.longName; } }
        
  /// <summary>
  /// The long name of the argument.
  /// </summary>
  public string LongName
  {
   get { Debug.Assert(!this.DefaultLongName); return this.longName; }
   set { this.longName = value; }
  }
        
  private string shortName;
  private string longName;
  private CommandLineArgumentType type;
  
 }//public class CommandLineArgumentAttribute : Attribute

 /// <summary>
 /// Indicates that this argument is the default argument.
 /// '/' or '-' prefix only the argument value is specified.
 /// </summary>
 [AttributeUsage(AttributeTargets.Field)]
 public class DefaultCommandLineArgumentAttribute : CommandLineArgumentAttribute
 {
  /// <summary>
  /// Indicates that this argument is the default argument.
  /// </summary>
  /// <param name="type"> Specifies the error checking to be done on the argument. </param>
  public DefaultCommandLineArgumentAttribute(CommandLineArgumentType type)
         : base (type)
  {
  }//public DefaultCommandLineArgumentAttribute(CommandLineArgumentType type)
 }//public class DefaultCommandLineArgumentAttribute : CommandLineArgumentAttribute

 /// <summary>
 /// Parser for command line arguments.
 ///
 /// The parser specification is infered from the instance fields of the object
 /// specified as the destination of the parse.
 /// Valid argument types are: int, uint, string, bool, enums
 /// Also argument types of Array of the above types are also valid.
 /// 
 /// Error checking options can be controlled by adding a CommandLineArgumentAttribute
 /// to the instance fields of the destination object.
 ///
 /// At most one fieldInfo may be marked with the DefaultCommandLineArgumentAttribute
 /// indicating that arguments without a '-' or '/' prefix will be parsed as that argument.
 ///
 /// If not specified then the parser will infer default options for parsing each
 /// instance fieldInfo. The default long name of the argument is the fieldInfo name. The
 /// default short name is the first character of the long name. Long names and explicitly
 /// specified short names must be unique. Default short names will be used provided that
 /// the default short name does not conflict with a long name or an explicitly
 /// specified short name.
 ///
 /// Arguments which are array types are collection arguments. Collection
 /// arguments can be specified multiple times.
 /// </summary>
 public class CommandLineArgumentParser
 {
  /// <summary>
  /// Creates a new command line argument parser.
  /// </summary>
  /// <param name="argumentSpecification"> The type of object to  parse. </param>
  /// <param name="errorReporter"> The destination for parse errors. </param>
  public CommandLineArgumentParser
  (
   Type           argumentSpecification, 
   ErrorReporter  errorReporter
  )
  {
   this.errorReporter  =  errorReporter;
   this.arguments      =  new ArrayList();
   this.argumentMap    =  new Hashtable();
            
   foreach (FieldInfo fieldInfo in argumentSpecification.GetFields())
   {
    if (!fieldInfo.IsStatic && !fieldInfo.IsInitOnly && !fieldInfo.IsLiteral)
    {
     CommandLineArgumentAttribute attribute = GetAttribute(fieldInfo);
     
     if (attribute is DefaultCommandLineArgumentAttribute)
     {
      Debug.Assert(this.defaultArgument == null);
      this.defaultArgument = new Argument(attribute, fieldInfo, errorReporter );
     }//if (attribute is DefaultCommandLineArgumentAttribute)
     else
     {
      this.arguments.Add(new Argument(attribute, fieldInfo, errorReporter ));
     }//else (attribute is DefaultCommandLineArgumentAttribute)
    }//if (!fieldInfo.IsStatic && !fieldInfo.IsInitOnly && !fieldInfo.IsLiteral)
   }//foreach (FieldInfo fieldInfo in argumentSpecification.GetFields())
            
   // add explicit names to map
   foreach (Argument argument in this.arguments)
   {
    Debug.Assert(!argumentMap.ContainsKey(argument.LongName));
    this.argumentMap[argument.LongName] = argument;
    if (argument.ExplicitShortName && argument.ShortName != null && argument.ShortName.Length > 0)
    {
     Debug.Assert(!argumentMap.ContainsKey(argument.ShortName));
     this.argumentMap[argument.ShortName] = argument;
    }//if (argument.ExplicitShortName && argument.ShortName != null && argument.ShortName.Length > 0)
   }//foreach (Argument argument in this.arguments)
            
   // add implicit names which don't collide to map
   foreach (Argument argument in this.arguments)
   {
    if (!argument.ExplicitShortName && argument.ShortName != null && argument.ShortName.Length > 0)
    {
     if (!argumentMap.ContainsKey(argument.ShortName))
     {     
      this.argumentMap[argument.ShortName] = argument;
     }//if (!argumentMap.ContainsKey(argument.ShortName)) 
    }//if (!argument.ExplicitShortName && argument.ShortName != null && argument.ShortName.Length > 0)
   }//foreach (Argument argument in this.arguments)
  }//public CommandLineArgumentParser(Type argumentSpecification, ErrorReporter reporter)

  private static CommandLineArgumentAttribute GetAttribute
  (
   FieldInfo fieldInfo
  )
  {
   object[] attribute = null;
   
   attribute = fieldInfo.GetCustomAttributes(typeof(CommandLineArgumentAttribute), false);
   
   if (attribute.Length == 1)
   {
    return (CommandLineArgumentAttribute) attribute[0];
   }//if (attribute.Length == 1) 

   Debug.Assert(attribute.Length == 0);
   return null;
  }//private static CommandLineArgumentAttribute GetAttribute(FieldInfo fieldInfo) 
  
  private void ReportUnrecognizedArgument
  (
   String argument
  )
  {
   this.errorReporter(string.Format("Unrecognized command line argument '{0}'", argument));
  }
        
  /// <summary>Parses an argument list into an object</summary>
  /// <param name="argv"></param>
  /// <param name="destination"></param>
  /// <returns> true if an error occurred </returns>
  private bool ParseArgumentList
  (
   String[] argv, 
   object   destination
  )
  {
   bool hadError = false;
   
   if ( argv != null )
   {
    foreach ( String argument in argv )
    {
     if ( argument.Length > 0 )
     {
      switch ( argument[0] )
      {
       case '-':
       case '/':
        int    endIndex = argument.IndexOfAny(new char[] {':', '+', '-'}, 1);
        string option = argument.Substring(1, endIndex == -1 ? argument.Length - 1 : endIndex - 1);
        string optionArgument;
        
        if (option.Length + 1 == argument.Length)
        {
         optionArgument = null;
        }
        else if (argument.Length > 1 + option.Length && argument[1 + option.Length] == ':')
        {
         optionArgument = argument.Substring(option.Length + 2);
        }
        else
        {
         optionArgument = argument.Substring(option.Length + 1);
        }
                                
        Argument arg = (Argument) this.argumentMap[option];
        if ( arg == null )
        {
         ReportUnrecognizedArgument(argument);
         hadError = true;
        }
        else
        {
         hadError |= !arg.SetValue(optionArgument, destination);
        }
        break;
        
       case '@':
        string[] nestedArguments;
        hadError |= LexFileArguments(argument.Substring(1), out nestedArguments);
        hadError |= ParseArgumentList(nestedArguments, destination);
        break;
        
       default:
        if (this.defaultArgument != null)
        {
         hadError |= !this.defaultArgument.SetValue(argument, destination);
        }
        else
        {
         ReportUnrecognizedArgument(argument);
         hadError = true;
        }
       break;
      }//switch ( argument[0] )
     }//if ( argument.Length > 0 )
    }//foreach ( String argument in argv )
   }//if ( args != null )
           
   return hadError;
   
  }//private bool ParseArgumentList
        
  /// <summary>Parses an argument list.</summary>
  /// <param name="argv"> The arguments to parse. </param>
  /// <param name="destination"> The destination of the parsed arguments. </param>
  /// <returns> true if no parse errors were encountered. </returns>
  public bool Parse
  (
   String[] argv, 
   object   destination
  )
  {
   bool hadError = false;
   
   hadError = ParseArgumentList(argv, destination);

   // check for missing required arguments
   foreach (Argument arg in this.arguments)
   {
    hadError |= arg.Finish(destination);
   }
   
   if ( this.defaultArgument != null )
   {
    hadError |= this.defaultArgument.Finish(destination);
   }
            
   return ( !hadError );
  }//public bool Parse
        
        
  /// <summary>A user firendly usage string describing the command line argument syntax.</summary>
  public string Usage
  {
   get
   {
    bool           first      = true;
    int            oldLength;   	
    StringBuilder  sb         = null;
    Type           typeValue;
    
    sb = new StringBuilder();
                
    foreach ( Argument arg in this.arguments )
    {

     oldLength = sb.Length;
     sb.Append("    /");
     sb.Append(arg.LongName);
     
     typeValue = arg.ValueType;
     if ( typeValue == typeof(int) )
     {
      sb.Append(":<int>");
     }
     else if ( typeValue == typeof(uint) )
     {
      sb.Append(":<uint>");
     }
     else if (typeValue == typeof(bool))
     {
      sb.Append("[+|-]");
     }
     else if (typeValue == typeof(string))
     {
      sb.Append(":<string>");
     }
     else
     {
      Debug.Assert(typeValue.IsEnum);
                        
      sb.Append(":{");
      first = true;
      foreach ( FieldInfo fieldInfo in typeValue.GetFields() )
      {
       if ( fieldInfo.IsStatic )
       {
        if ( first )
        {        
         first = false;
        }//if ( first )
        else
        { 
         sb.Append('|');
         sb.Append(fieldInfo.Name);
        }//else ( first )
       }//if ( fieldInfo.IsStatic )
       sb.Append('}');
      }//foreach ( FieldInfo fieldInfo in typeValue.GetFields() )
     }//else if ( typeValue == typeof(int) )
     
     if ( arg.ShortName != arg.LongName && this.argumentMap[arg.ShortName] == arg )
     {
      sb.Append(' ', IndentLength(sb.Length - oldLength));
      sb.Append("short form /");
      sb.Append(arg.ShortName);
     }//if ( arg.ShortName != arg.LongName && this.argumentMap[arg.ShortName] == arg )
                    
     sb.Append(UtilityParseCommandLineArgument.NewLine);
      
    }//foreach ( Argument arg in this.arguments )
                
    oldLength = sb.Length;
    sb.Append("    @<file>");
    sb.Append(' ', IndentLength(sb.Length - oldLength));
    sb.Append("Read response file for more options");
    sb.Append(UtilityParseCommandLineArgument.NewLine);
                
    if ( this.defaultArgument != null )
    {
     oldLength = sb.Length;
     sb.Append("    <");
     sb.Append( this.defaultArgument.LongName );
     sb.Append(">");
     sb.Append(UtilityParseCommandLineArgument.NewLine);
    }//if ( this.defaultArgument != null )
                
    return sb.ToString();
    
   }//get
  }//public string Usage
            
  private static int IndentLength
  (
   int lineLength
  )
  {
   return Math.Max(4, 40 - lineLength);
  }
        
  private bool LexFileArguments
  (
       String fileName, 
   out String[] arguments
  )
  {

   bool           hadError    =  false;
   bool           inQuotes    =  false;                       
   
   int            cSlashes    =  -1;
   int            index       =  -1;

   String         args        =  null;
         
   ArrayList      argArray    =  null;
   StringBuilder  currentArg  =  null;

   try
   {
    using ( FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read) )
    {
     args = (new StreamReader(file)).ReadToEnd();
    }
   }
   catch (Exception e)
   {
    this.errorReporter(string.Format("Error: Can't open command line argument file '{0}' : '{1}'", fileName, e.Message));
    arguments = null;
    return false;
   }

   inQuotes    =  false;
   hadError    =  false;                       

   index       =  0;

   argArray    =  new ArrayList();
   currentArg  =  new StringBuilder();
            
   // while (index < args.Length)
   try
   {
    while ( true )
    {
     // skip whitespace
     while ( char.IsWhiteSpace(args[index]) )
     {
      index += 1;
     }
                    
     // # - comment to end of line
     if (args[index] == '#')
     {
      index += 1;
      while (args[index] != '\n')
      {
       index += 1;
      }
      continue;
     }
                    
     // do one argument
     do
     {
      if ( args[index] == '\\' )
      {
       cSlashes = 1;
       index += 1;
       
       while (index == args.Length && args[index] == '\\')
       {
        cSlashes += 1;
       }

       if ( index == args.Length || args[index] != '"' )
       {
        currentArg.Append('\\', cSlashes);
       }//if ( index == args.Length || args[index] != '"' )
       else
       {
        currentArg.Append('\\', (cSlashes >> 1));
        
        if ( 0 != (cSlashes & 1) )
        {
         currentArg.Append('"');
        }//if ( 0 != (cSlashes & 1) )
        else
        {
         inQuotes = !inQuotes;
        }//else ( 0 != (cSlashes & 1) )
       }//else ( index == args.Length || args[index] != '"' )
      }//if ( args[index] == '\\' )
      else if (args[index] == '"')
      {
       inQuotes = !inQuotes;
       index += 1;
      }//else if (args[index] == '"')
      else
      {
       currentArg.Append(args[index]);
       index += 1;
      }//else if (args[index] == '\\')
      
     } while (!char.IsWhiteSpace(args[index]) || inQuotes);
                    
     argArray.Add(currentArg.ToString());
     currentArg.Length = 0;
    }
   }
   catch ( System.IndexOutOfRangeException indexOutOfRangeException )
   {
    // got EOF 
    if ( inQuotes )
    {
     this.errorReporter
     (
      String.Format
      (
       "Error: Unbalanced '\"' in command line argument file '{0}' | IndexOutOfRangeException: {1}",
       fileName, 
       indexOutOfRangeException.Message
      )
     );
     hadError = true;
    }//if ( inQuotes )
    else if ( currentArg.Length > 0 )
    {
     // valid argument can be terminated by EOF
     argArray.Add(currentArg.ToString());
    }//else if ( currentArg.Length > 0 )
   }//catch ( System.IndexOutOfRangeException indexOutOfRangeException )
            
   arguments = (string[]) argArray.ToArray(typeof (string));
   return hadError;
   
  }//private bool LexFileArguments( String fileName, out String[] arguments )
  
  private static String LongName
  (
   CommandLineArgumentAttribute commandLineArgumentAttribute, 
   FieldInfo                    fieldInfo
  )
  {
   return (commandLineArgumentAttribute == null || commandLineArgumentAttribute.DefaultLongName) ? fieldInfo.Name : commandLineArgumentAttribute.LongName;
  }//private static String LongName
        
  private static string ShortName
  (
   CommandLineArgumentAttribute  commandLineArgumentAttribute, 
   FieldInfo                     fieldInfo
  )
  {
   return !ExplicitShortName(commandLineArgumentAttribute) ? LongName(commandLineArgumentAttribute, fieldInfo).Substring(0,1) : commandLineArgumentAttribute.ShortName;
  }//private static string ShortName
        
  private static bool ExplicitShortName
  (
   CommandLineArgumentAttribute commandLineArgumentAttribute
  )
  {
   return (commandLineArgumentAttribute != null && !commandLineArgumentAttribute.DefaultShortName);
  }//private static bool ExplicitShortName
        
  private static Type ElementType
  (
   FieldInfo fieldInfo
  )
  {
   if ( IsCollectionType ( fieldInfo.FieldType ) )
   {
    return fieldInfo.FieldType.GetElementType();
   }//if ( IsCollectionType ( fieldInfo.FieldType ) ) 
   else
   {
    return null;
   }//else ( IsCollectionType ( fieldInfo.FieldType ) ) 
  }//private static Type ElementType ( FieldInfo fieldInfo )

  private static CommandLineArgumentType Flags
  (
   CommandLineArgumentAttribute  commandLineArgumentAttribute, 
   FieldInfo                     fieldInfo
  )
  {
   if ( commandLineArgumentAttribute != null )
   {
    return commandLineArgumentAttribute.Type;
   }//if ( commandLineArgumentAttribute != null ) 
   else if ( IsCollectionType(fieldInfo.FieldType ) )
   {
    return CommandLineArgumentType.MultipleUnique;
   }//else if ( IsCollectionType(fieldInfo.FieldType ) ) 
   else
   {
    return CommandLineArgumentType.AtMostOnce;
   }//else 
  }//private static CommandLineArgumentType Flags ( CommandLineArgumentAttribute commandLineArgumentAttribute, FieldInfo fieldInfo )

  private static bool IsCollectionType
  (
   Type type
  )
  {
   return type.IsArray;
  }//private static bool IsCollectionType
            
  private static bool IsValidElementType
  (
   Type type
  )
  {
   return type != null && 
   (
    type == typeof(int) ||
    type == typeof(uint) ||
    type == typeof(string) ||
    type == typeof(bool) ||
    type.IsEnum
   );
  }//private static bool IsValidElementType
        
  private class Argument
  {
   ///<summary>Constructor.</summary>
   public Argument
   (
    CommandLineArgumentAttribute commandLineArgumentAttribute, 
    FieldInfo                    fieldInfo, 
    ErrorReporter                errorReporter
   )
   {
    this.longName           =  CommandLineArgumentParser.LongName(commandLineArgumentAttribute, fieldInfo);
    this.explicitShortName  =  CommandLineArgumentParser.ExplicitShortName(commandLineArgumentAttribute);
    this.shortName          =  CommandLineArgumentParser.ShortName(commandLineArgumentAttribute, fieldInfo);
    this.elementType        =  ElementType(fieldInfo);
    this.flags              =  Flags(commandLineArgumentAttribute, fieldInfo);
    this.fieldInfo          =  fieldInfo;
    this.seenValue          =  false;
    this.errorReporter      =  errorReporter;
    this.isDefault          =  commandLineArgumentAttribute != null && commandLineArgumentAttribute is DefaultCommandLineArgumentAttribute;
                
    if ( IsCollection )
    {
     this.collectionValues = new ArrayList();
    }
                
    Debug.Assert(this.longName != null && this.longName.Length > 0);
    Debug.Assert(!IsCollection || AllowMultiple, "Collection arguments must have allow multiple");
    Debug.Assert(!Unique || IsCollection, "Unique only applicable to collection arguments");
    Debug.Assert(IsValidElementType(Type) || IsCollectionType(Type));
    Debug.Assert
    (
     (IsCollection && IsValidElementType(elementType)) ||
     (!IsCollection && elementType == null)
    );
   }//public Argument()
            
   public bool Finish
   (
    object destination
   )
   {
    if ( this.IsCollection )
    {
     this.fieldInfo.SetValue(destination, this.collectionValues.ToArray(this.elementType));
    }
                
    return ReportMissingRequiredArgument();
   }//public bool Finish
            
   private bool ReportMissingRequiredArgument()
   {
    if ( this.IsRequired && !this.SeenValue )
    {
     if ( this.IsDefault )
     {
      errorReporter(string.Format("Missing required argument '<{0}>'.", this.LongName));
     }//if ( this.IsDefault ) 
     else
     {
      errorReporter(string.Format("Missing required argument '/{0}'.", this.LongName));
     }//else ( this.IsDefault ) 
     return true;
    }//if ( this.IsRequired && !this.SeenValue )
    
    return false;
   }//private bool ReportMissingRequiredArgument()
            
   private void ReportDuplicateArgumentValue(string value)
   {
    this.errorReporter(string.Format("Duplicate '{0}' argument '{1}'", this.LongName, value));
   }
            
   public bool SetValue
   (
    String  value, 
    object  destination
   )
   {

    object  newValue;
    
    if ( SeenValue && !AllowMultiple )
    {
     this.errorReporter(string.Format("Duplicate '{0}' argument", this.LongName));
     return false;
    }
    
    this.seenValue = true;
                
    if ( !ParseValue( this.ValueType, value, out newValue ) )
    {
     return false;
    }
     
    if ( this.IsCollection )
    {
     if ( this.Unique && this.collectionValues.Contains( newValue ) )
     {
      ReportDuplicateArgumentValue( value );
      return false;
     }//if ( this.Unique && this.collectionValues.Contains( newValue ) )
     else
     {
      this.collectionValues.Add( newValue );
     }//else ( this.Unique && this.collectionValues.Contains( newValue ) )
    }//if ( this.IsCollection )
    else
    {
     this.fieldInfo.SetValue( destination, newValue );
    }//else ( this.IsCollection )
                
    return true;
   }//public bool SetValue( String value, object  destination )

   public Type ValueType
   {
    get { return this.IsCollection ? this.elementType : this.Type; }
   }
            
   private void ReportBadArgumentValue
   (
    String value
   )
   {
    this.errorReporter(string.Format("'{0}' is not a valid value for the '{1}' command line option", value, this.LongName));
   }
            
   private bool ParseValue
   (
         Type    type, 
         String  stringData, 
    out  object  value
   )
   {
    // null is only valid for bool variables
    // empty string is never valid
    if ( ( stringData != null || type == typeof( bool )) && ( stringData == null || stringData.Length > 0 ) )
    {
     try
     {
      if ( type == typeof( String ) )
      {
       value = stringData;
       return true;
      }//if ( type == typeof( String ) )
      else if ( type == typeof( bool ) )
      {
       if ( stringData == null || stringData == "+" )
       {
        value = true;
        return true;
       }//if ( stringData == null || stringData == "+" )
       else if (stringData == "-")
       {
        value = false;
        return true;
       }//else if (stringData == "-")
      }//else if ( type == typeof( bool ) )
      else if (type == typeof(int))
      {
       value = int.Parse(stringData);
       return true;
      }//else if (type == typeof(int))
      else if ( type == typeof(uint) )
      {
       value = int.Parse(stringData);
       return true;
      }//else if ( type == typeof(uint) )
      else
      {
       Debug.Assert(type.IsEnum);
       value = Enum.Parse(type, stringData, true);
       return true;
      }//else
     }//try
     catch ( Exception exception )
     {
      // catch parse errors
      System.Console.WriteLine( "Exception: {0}", exception.Message );
     }//catch ( Exception exception )
    }//if ( ( stringData != null || type == typeof( bool )) && ( stringData == null || stringData.Length > 0 ) )
                                
    ReportBadArgumentValue(stringData);
    value = null;
    return false;
   
   }//private bool ParseValue( Type type, String stringData, out object value )

   public string LongName
   {
    get { return this.longName; }
   }//public string LongName

   public bool ExplicitShortName
   {
    get { return this.explicitShortName; }
   }//public bool ExplicitShortName
            
   public string ShortName
   {
    get { return this.shortName; }
   }//public string ShortName

   public bool IsRequired
   {
    get { return 0 != (this.flags & CommandLineArgumentType.Required); }
   }//public bool IsRequired
            
   public bool SeenValue
   {
    get { return this.seenValue; }
   }//public bool SeenValue
            
   public bool AllowMultiple
   {
    get { return 0 != (this.flags & CommandLineArgumentType.Multiple); }
   }//public bool AllowMultiple
            
   public bool Unique
   {
    get { return 0 != (this.flags & CommandLineArgumentType.Unique); }
   }//public bool Unique
            
   public Type Type
   {
    get { return fieldInfo.FieldType; }
   }//public Type Type
            
   public bool IsCollection
   {
    get { return IsCollectionType(Type); }
   }//public bool IsCollection
            
   public bool IsDefault
   {
    get { return this.isDefault; }
   }//public bool IsDefault
            
   private string                    longName;
   private string                    shortName;
   private bool                      explicitShortName;
   private bool                      seenValue;
   private FieldInfo                 fieldInfo;
   private Type                      elementType;
   private CommandLineArgumentType   flags;
   private ArrayList                 collectionValues;
   private ErrorReporter             errorReporter;
   private bool                      isDefault;
  
  }//private class Argument
        
  private ArrayList arguments;
  private Hashtable argumentMap;
  private Argument defaultArgument;
  private ErrorReporter errorReporter;
 }//public class CommandLineArgumentParser

 /// <summary>
 /// Useful Stuff.
 /// </summary>
 public sealed class UtilityParseCommandLineArgument
 {
  /// <summary>
  /// The System Defined new line string.
  /// </summary>
  public const string NewLine = "\r\n";
        
  /// <summary>
  /// Don't ever call this.
  /// </summary>
  private UtilityParseCommandLineArgument() {}

  private static void NullErrorReporter( string sMessage ) 
  { 
  } 
        
  /// <summary>
  /// Parses Command Line Arguments. 
  /// Errors are output on Console.Error.
  /// Use CommandLineArgumentAttributes to control parsing behaviour.
  /// </summary>
  /// <param name="arguments"> The actual arguments. </param>
  /// <param name="destination"> The resulting parsed arguments. </param>
  /// <returns> true if no errors were detected. </returns>
  public static bool ParseCommandLineArguments(string [] arguments, object destination)
  {
   return ParseCommandLineArguments(arguments, destination, new ErrorReporter(Console.Error.WriteLine));
  }//public static bool ParseCommandLineArguments()
        
  /// <summary>
  /// Parses Command Line Arguments. 
  /// Use CommandLineArgumentAttributes to control parsing behaviour.
  /// </summary>
  /// <param name="arguments"> The actual arguments. </param>
  /// <param name="destination"> The resulting parsed arguments. </param>
  /// <param name="errorReporterArgument"> The destination for parse errors. </param>
  /// <returns> true if no errors were detected. </returns>
  public static bool ParseCommandLineArguments
  (
   string[]       arguments, 
   object         destination, 
   ErrorReporter  errorReporterArgument
  )
  {
   /*
   CommandLineArgumentParser parser = new CommandLineArgumentParser(destination.GetType(), errorReporterArgument);
   return parser.Parse(arguments, destination);
   */

   bool                        boolCommandLineArgumentParser  =  false;
   CommandLineArgumentParser   commandLineArgumentParser      =  null; 
   ErrorReporter               errorReporterLocal             =  null;
   HelpArgument                helpArgument                   =  null;

   errorReporterLocal          =  new ErrorReporter( NullErrorReporter );     

   if( errorReporterArgument == null )
   { 
    errorReporterArgument = errorReporterLocal; 
   }//if( errorReporterArgument == null )

   helpArgument              =  new HelpArgument(); 

   commandLineArgumentParser = new CommandLineArgumentParser( helpArgument.GetType(), errorReporterLocal ); 
   commandLineArgumentParser.Parse( arguments, helpArgument ); 

   if( helpArgument.help ) 
   { 
    return( false ); 
   }//if( helpArgument.help )  

   commandLineArgumentParser = new CommandLineArgumentParser(destination.GetType(), errorReporterArgument ); 

   boolCommandLineArgumentParser = commandLineArgumentParser.Parse(arguments, destination); 
          
   if ( !boolCommandLineArgumentParser )
   { 
    errorReporterArgument( "Use /? or /help for detailed command syntax" ); 
   }//if ( !boolCommandLineArgumentParser ) 

   return( boolCommandLineArgumentParser ) ; 

  }//public static bool ParseCommandLineArguments()
  
  /// <summary>
  /// Returns a Usage string for command line argument parsing.
  /// Use CommandLineArgumentAttributes to control parsing behaviour.
  /// </summary>
  /// <param name="argumentType"> The type of the arguments to display usage for. </param>
  /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
  public static string CommandLineArgumentsUsage(Type argumentType)
  {
   return (new CommandLineArgumentParser(argumentType, null)).Usage;
  }
 
 }//public sealed class UtilityParseCommandLineArgument
 
}//namespace WordEngineering