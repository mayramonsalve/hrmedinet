﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViewRes.Models {
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
    public class Category {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Category() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MedinetClassLibrary.Resources.Models.Category", typeof(Category).Assembly);
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
        ///   Looks up a localized string similar to Description.
        /// </summary>
        public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Description field is required..
        /// </summary>
        public static string DescriptionRequired {
            get {
                return ResourceManager.GetString("DescriptionRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Grouping category.
        /// </summary>
        public static string GroupingCategory {
            get {
                return ResourceManager.GetString("GroupingCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Questionnaire.
        /// </summary>
        public static string Questionnaire {
            get {
                return ResourceManager.GetString("Questionnaire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Questionnaire field is required..
        /// </summary>
        public static string QuestionnaireRequired {
            get {
                return ResourceManager.GetString("QuestionnaireRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Weighing.
        /// </summary>
        public static string Weighing {
            get {
                return ResourceManager.GetString("Weighing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Weighing field must be between 0.01 and 100..
        /// </summary>
        public static string WeighingRangeInvalid {
            get {
                return ResourceManager.GetString("WeighingRangeInvalid", resourceCulture);
            }
        }
    }
}