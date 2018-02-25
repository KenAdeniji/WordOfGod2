using System;
using System.IO;
using System.Messaging;

namespace WordEngineering
{
 ///<summary>UtilityMessageQueue</summary>
 ///<remarks>
 /// http://msdn2.microsoft.com/en-us/library/system.messaging.messagequeue.path
 ///</remarks>
 public static class UtilityMessageQueue
 {
  ///<summary>MessageQueueType</summary>
  public enum MessageQueueType
  {
   Publicqueue,
   Privatequeue,
   Journalqueue,
   Machinejournalqueue,
   Machinedeadletterqueue,
   Machinetransactionaldeadletterqueue
  }

  ///<summary>MessageMessageQueuePathFormat</summary>
  public static readonly string[] MessageMessageQueuePathFormat = new string[] 
                                       {
                                        @"{0}\{1}", //Public queue
                                        @"{0}\Private$\{1}", //Private queue
                                        @"{0}\{1}\Journal$", //Journal queue
                                        @"{0}\Journal$", //Machine journal queue
                                        @"{0}\Deadletter$", //Machine dead-letter queue
                                        @"{0}\XactDeadletter$" //Machine transactional dead-letter queue
                                       };

  ///<summary>The entry point for the application.</summary>
  ///<param name="argv">Command-line parameters.</param>
  public static void Main(string[] argv)
  {
   Stub();
  }

  ///<summary>Stub</summary>
  public static void Stub()
  {
  }

  ///<summary>MessageQueueCreate</summary>
  ///<remarks>
  /// MessageQueueCreate(".", "QueueName", true, MessageQueueType.Publicqueue, ref exceptionMessage);
  ///</remarks>
  public static MessageQueue MessageQueueCreate
  (
       string computer,
       string queueName,
       bool   transactional,
       MessageQueueType MessageQueueType,
   ref string exceptionMessage
  )
  {
   string path = MessageQueuePath( computer, queueName, MessageQueueType );
   MessageQueue messageQueue = null;
   messageQueue = MessageQueueCreate( path, transactional, ref exceptionMessage );
   return ( messageQueue );
  }

  ///<summary>MessageQueueCreate</summary>
  public static MessageQueue MessageQueueCreate
  (
       string path,
       bool   transactional,
   ref string exceptionMessage
  )
  {
   MessageQueue messageQueue = null;
   try
   {
    if ( MessageQueue.Exists( path ) ) { messageQueue = new MessageQueue(path); }
    else {messageQueue = MessageQueue.Create(path, transactional);}
   }
   catch (Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   return( messageQueue );
  }

  ///<summary>MessageQueuePath</summary>
  public static string MessageQueuePath
  (
   string computer,
   string queueName,
   MessageQueueType MessageQueueType
  )
  {
   string path = null;
   if ( MessageQueueType <= MessageQueueType.Privatequeue ) { path = String.Format(MessageMessageQueuePathFormat[(int)MessageQueueType], computer, queueName); }
   else { path = String.Format(MessageMessageQueuePathFormat[(int)MessageQueueType], computer); }
   return (path);
  }

  ///<summary>MessageQueuePublicQuery</summary>
  public static MessageQueue[] MessageQueuePublicQuery()
  {
   MessageQueueCriteria messageQueueCriteria = new MessageQueueCriteria();
   MessageQueue[] messageQueue = MessageQueue.GetPublicQueues(messageQueueCriteria);

   /*
   #if (DEBUG)
    foreach( MessageQueue messageQueueCurrent in messageQueue )
    {
     System.Console.WriteLine
     ( 
      "Path: {0} | QueueName: {1} | MachineName: {2}",
      messageQueueCurrent.Path,
      messageQueueCurrent.QueueName,
      messageQueueCurrent.MachineName 
     );
   }
   #endif
   */

   return( messageQueue );
  }

  ///<summary>MessageQueueReceive</summary>
  public static void MessageQueueReceive
  (
       string path,
   out Message message,
   ref string exceptionMessage
  )
  {
   MessageQueue messageQueue;
   message = null;
   try
   {
    messageQueue = new MessageQueue(path);
    messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) } );
    message = messageQueue.Receive();
   }
   catch (Exception ex)
   {
    exceptionMessage = ex.Message;
   }
   /*
   #if (DEBUG)
    System.Console.WriteLine("Label: {0}", message.Label);
    System.Console.WriteLine("Body: {0}", message.Body);
   #endif
   */
  }

  ///<summary>MessageQueueSend</summary>
  public static void MessageQueueSend
  (
       string path,
       object obj,
   ref string exceptionMessage,
       string label
  )
  {
   Message message;
   MessageQueue messageQueue;
   try
   {
    messageQueue = new MessageQueue(path);
    message = new Message(obj);
    if (label != null) {message.Label = label;}
    messageQueue.Send(message);
   }
   catch (Exception ex)
   {
    exceptionMessage = ex.Message;
   }
  }

  ///<summary>MessageQueueSendFile</summary>
  public static string MessageQueueSendFile
  (
   string path,
   string filename
  )
  {
   string content;
   string exceptionMessage = null;
   FileStream fileStream = null;
   StreamReader streamReader = null;
   try
   {
    if ( System.IO.File.Exists(filename) ) 
    { 
     fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read );
     streamReader = new StreamReader(fileStream);
     content = streamReader.ReadToEnd();
     MessageQueueSend(path, content, ref exceptionMessage, filename);
    }
   }
   catch(Exception ex)
   {
    exceptionMessage = ex.Message;    
   }
   finally
   {
    if (streamReader != null) { streamReader.Close(); streamReader=null; }
    if (fileStream != null) { fileStream.Close(); fileStream=null;}
   }
   return(exceptionMessage);
  }

 }
}