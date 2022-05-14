namespace EDGV.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new System.Resources.ResourceManager("РасчетКУ.Extends.EDGV.Properties.Resources", typeof(Resources).Assembly);
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap AddFilter
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("AddFilter", resourceCulture);
            }
        }

        internal static Bitmap ASC
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("ASC", resourceCulture);
            }
        }

        internal static Bitmap ASCbool
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("ASCbool", resourceCulture);
            }
        }

        internal static Bitmap ASCnum
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("ASCnum", resourceCulture);
            }
        }

        internal static Bitmap ASCtxt
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("ASCtxt", resourceCulture);
            }
        }

        internal static Bitmap DESC
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("DESC", resourceCulture);
            }
        }

        internal static Bitmap DESCbool
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("DESCbool", resourceCulture);
            }
        }

        internal static Bitmap DESCnum
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("DESCnum", resourceCulture);
            }
        }

        internal static Bitmap DESCtxt
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("DESCtxt", resourceCulture);
            }
        }

        internal static Bitmap Filter
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("Filter", resourceCulture);
            }
        }

        internal static Bitmap FilterASC
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("FilterASC", resourceCulture);
            }
        }

        internal static Bitmap FilterDESC
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("FilterDESC", resourceCulture);
            }
        }

        internal static Bitmap ResizeGrip
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("ResizeGrip", resourceCulture);
            }
        }

        internal static Bitmap SavedFilter
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject("SavedFilter", resourceCulture);
            }
        }
    }
}
