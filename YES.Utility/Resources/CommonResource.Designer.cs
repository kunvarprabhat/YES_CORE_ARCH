﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YES.Utility.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CommonResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YES.Utility.Resources.CommonResource", typeof(CommonResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to add data..
        /// </summary>
        public static string AddFailed {
            get {
                return ResourceManager.GetString("AddFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data added successfully..
        /// </summary>
        public static string AddSuccess {
            get {
                return ResourceManager.GetString("AddSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to delete data..
        /// </summary>
        public static string DeleteFailed {
            get {
                return ResourceManager.GetString("DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data deleted successfully..
        /// </summary>
        public static string DeleteSuccess {
            get {
                return ResourceManager.GetString("DeleteSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This email is already exist please enter other email id.
        /// </summary>
        public static string EmailExist {
            get {
                return ResourceManager.GetString("EmailExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to fetch records..
        /// </summary>
        public static string FetchFailed {
            get {
                return ResourceManager.GetString("FetchFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successfully fetch records..
        /// </summary>
        public static string FetchSuccess {
            get {
                return ResourceManager.GetString("FetchSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This mobile number is already exist please enter other mobile.
        /// </summary>
        public static string MobileExist {
            get {
                return ResourceManager.GetString("MobileExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to update data..
        /// </summary>
        public static string UpdateFailed {
            get {
                return ResourceManager.GetString("UpdateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data updated successfully..
        /// </summary>
        public static string UpdateSuccess {
            get {
                return ResourceManager.GetString("UpdateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This user {0} do not exist please enter vaild user Id/user Name .
        /// </summary>
        public static string UserNotExist {
            get {
                return ResourceManager.GetString("UserNotExist", resourceCulture);
            }
        }
    }
}
