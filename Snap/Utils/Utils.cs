using System;
using System.ComponentModel;
using System.Reflection;


namespace Snap {
  public static class Utils {

    /// <summary>
    /// Retrieves the enum item value description
    /// </summary>
    /// <remarks>
    /// Falls back to the name if there is no description
    /// </remarks>
    /// <param name="value"> An enum item value </param>
    /// <returns>what was set in the description attribute for the given enum item value</returns>
    public static string GetEnumDescription( this Enum value ) {
      Type type = value.GetType();
      string name = Enum.GetName( type, value );
      if ( string.IsNullOrEmpty( name ) ) {
        return null;
      }

      FieldInfo field = type.GetField( name );
      if ( field == null ) {
        return null;
      }

      DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute( field, typeof( DescriptionAttribute ) );
      return attr == null ? name : attr.Description;
    }

    public static int GetNumOfAllCards(int numOfPlayers)
    {
      return 52 - ( 52 % numOfPlayers );
    }
  }
}
