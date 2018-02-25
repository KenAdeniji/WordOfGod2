using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.WebControls;

namespace WordEngineering
{
 /// <summary>UtilityWebControl</summary>
 /// <remarks>
 /// </remarks>
 public class UtilityWebControl
 {  

  /// <summary>SelectedItem</summary>
  public static void SelectedItem
  ( 
       System.Web.UI.WebControls.ListControl  listControl,
   ref ArrayList                              selectedItem
  )
  {
   
   if ( listControl.SelectedIndex < 0 )
   {
    return;
   }
   
   selectedItem = new ArrayList();
   
   foreach ( ListItem listItem in  listControl.Items )
   { 
    if ( listItem.Selected )
    {
     selectedItem.Add( listItem.Text );
    } 
   }//foreach ( ListItem listItem in  listControl.Items )
  }//public static void SelectedItem

  /// <summary>SelectedItem</summary>
  public static void SelectedItem
  ( 
       System.Web.UI.WebControls.ListControl  listControl,
   ref String[]                               selectedItem
  )
  {
   ArrayList  arrayListSelectedItem  =  null;
   
   SelectedItem
   (
        listControl,
    ref arrayListSelectedItem
   );
   
   if ( arrayListSelectedItem == null )
   {
   	return;
   }	
   selectedItem = ( string[] ) arrayListSelectedItem.ToArray( typeof( string ) );
  }//public static void SelectedItem

  /// <summary>SelectItem</summary>
  public static void SelectItem
  ( 
   System.Web.UI.WebControls.ListControl  listControl,
   string                                 selectItem
  )
  {
   string[]  selectItemArray  =  new string[] { selectItem };
   SelectItem
   (
    listControl,
    selectItemArray
   );
   selectItem = selectItemArray[0]; 
  }//public static void SelectItem

  /// <summary>SelectItem</summary>
  public static void SelectItem
  ( 
   System.Web.UI.WebControls.ListControl  listControl,
   string[]                               selectItem
  )
  {
   ListItem  listItem;
   
   foreach ( string selectItemCurrent in selectItem )
   { 
    listItem = listControl.Items.FindByText( selectItemCurrent );
    
    if ( listItem != null )
    {
     listItem.Selected = true;
    }     	
   }//foreach ( string selectItemCurrent in selectItem )
  }//public static void SelectItem

  /// <summary>SelectItem</summary>
  public static void SelectItem  
  ( 
   System.Web.UI.WebControls.ListControl  listControl,
   ArrayList                              arrayListSelectItem
  )
  {
   string[]  selectItem;
   
   if ( arrayListSelectItem == null )
   {
   	return;
   }
   
   selectItem = ( string[] ) arrayListSelectItem.ToArray( typeof( string ) );
   
   SelectItem
   (
    listControl,
    selectItem
   );
  }//public static void SelectItem
  
 }//public class UtilityWebControl
}//namespace WordEngineering