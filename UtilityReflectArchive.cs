using System;
using System.Reflection;

namespace WordEngineering
{
 /// <summary>UtilityReflect</summary>
 /// <remarks>CoreCSharp.net Core C# and .NET</remarks>
 public class UtilityReflect: I1, I2
 {
  public UtilityReflect() {}
  public UtilityReflect( int parameter ) { parameter = -1 * parameter; }

  /// <summary>The entry point for the application.</summary>
  /// <param name="argv">A list of command line arguments</param>
  public static void Main(String[] argv)
  {

   ConstructorStub(typeof(UtilityReflect));

   InterfaceStub(typeof(UtilityReflect));

   MemberInfoStub(typeof(UtilityReflect));

   MethodInfoStub(typeof(UtilityReflect));
   MethodInfoStub(Type.GetType("WordEngineering.UtilityReflect"));
   MethodInfoStub( (new UtilityReflect()).GetType() );
   
  }

  ///<summary>ConstructorStub</summary>
  ///<remarks>
  /// ConstructorStub(typeof(UtilityReflect));
  ///</remarks>
  public static void ConstructorStub(Type type)
  {
   foreach 
   (
    ConstructorInfo constructorInfo in type.GetConstructors
                                       (
                                        BindingFlags.Public | 
                                        BindingFlags.NonPublic |
                                        BindingFlags.Static | 
                                        BindingFlags.Instance
                                       )
   )
   {
    System.Console.WriteLine("Constructor name: {0}", constructorInfo.Name);
    foreach(ParameterInfo parameterInfo in constructorInfo.GetParameters())
    {
     System.Console.WriteLine
     (
      " {0} {1} ", 
      parameterInfo.ParameterType, 
      parameterInfo.Name
     );
    }
   }
  }

  ///<summary>InterfaceStub</summary>
  ///<remarks>
  /// InterfaceStub(typeof(UtilityReflect));
  ///</remarks>
  public static void InterfaceStub(Type type)
  {
   foreach (Type typeInterface in type.GetInterfaces())
   {
    System.Console.WriteLine("Interface name: {0}", typeInterface.FullName);
   }
  }

  ///<summary>MemberInfoStub</summary>
  ///<remarks>
  /// MemberInfoStub(typeof(UtilityReflect));
  ///</remarks>
  public static void MemberInfoStub(Type type)
  {
   foreach (MemberInfo memberInfo in type.GetMembers())
   {
    System.Console.WriteLine("Member name: {0}", memberInfo.Name);
   }
  }

  ///<summary>MethodInfoStub</summary>
  ///<remarks>
  /// MethodInfoStub(typeof(System.Object));
  /// MethodInfoStub(Type.GetType("System.Object"));
  /// MethodInfoStub( (new UtilityReflect()).GetType() );
  ///</remarks>
  public static void MethodInfoStub(Type type)
  {
   foreach (MethodInfo methodInfo in type.GetMethods())
   {
    System.Console.WriteLine
    (
     "Method name: {0} | ReturnType: {1}",
     methodInfo.Name,
     methodInfo.ReturnType
    );
   }
  }

  ///<summary>P</summary>
  public void P() { }

  ///<summary>Q</summary>
  public void Q() { }
  
  static UtilityReflect()
  {
   
  }

 }

 interface I1
 {
  void P();
 }

 interface I2 : I1
 {
  void Q();
 }

}