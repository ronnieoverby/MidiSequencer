﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinDrums {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WinDrums.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;drums&gt;
        ///  &lt;drum note=&quot;35&quot; name=&quot;Acoustic Bass Drum&quot; /&gt;
        ///  &lt;drum note=&quot;36&quot; name=&quot;Bass Drum 1&quot; /&gt;
        ///  &lt;drum note=&quot;37&quot; name=&quot;Side Stick&quot; /&gt;
        ///  &lt;drum note=&quot;38&quot; name=&quot;Acoustic Snare&quot; /&gt;
        ///  &lt;drum note=&quot;39&quot; name=&quot;Hand Clap&quot; /&gt;
        ///  &lt;drum note=&quot;40&quot; name=&quot;Electric Snare&quot; /&gt;
        ///  &lt;drum note=&quot;41&quot; name=&quot;Low Floor Tom&quot; /&gt;
        ///  &lt;drum note=&quot;42&quot; name=&quot;Closed Hi Hat&quot; /&gt;
        ///  &lt;drum note=&quot;43&quot; name=&quot;High Floor Tom&quot; /&gt;
        ///  &lt;drum note=&quot;44&quot; name=&quot;Pedal HiHat&quot; /&gt;
        ///  &lt;drum note=&quot;45&quot; name=&quot;Low Tom&quot; /&gt;
        ///  &lt;drum note=&quot;46&quot; name=&quot;Open HiHat&quot; /&gt;
        /// </summary>
        internal static string drums {
            get {
                return ResourceManager.GetString("drums", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;pattern frameLength=&quot;260&quot;&gt;
        ///  36|*   | Bass Drum 1
        ///  38|  * | Acoustic Snare
        ///  46| * | Open HiHat
        ///  42|- -| Closed Hi Hat
        ///  45| * * * * * 989   | Low Tom
        ///  43|           989767| High Floor Tom
        ///  41|              878| Low Floor Tom
        ///  56| *   | Cowbell
        ///&lt;/pattern&gt;.
        /// </summary>
        internal static string Fonkay {
            get {
                return ResourceManager.GetString("Fonkay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;pattern frameLength=&quot;200&quot;&gt;
        ///42|8484848484848484    848484848484| Closed Hi Hat
        ///46|                7 7             | Open HiHat
        ///36|9999 9     9    9 9        9 9  | Bass Drum 1
        ///38|    9       9       9       9   | Acoustic Snare
        ///&lt;/pattern&gt;.
        /// </summary>
        internal static string ShoveIt {
            get {
                return ResourceManager.GetString("ShoveIt", resourceCulture);
            }
        }
    }
}