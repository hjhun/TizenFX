//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.9
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Tizen.NUI
{

    public class Vector2 : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal Vector2(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Vector2 obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~Vector2()
        {
            DisposeQueue.Instance.Add(this);
        }

        public virtual void Dispose()
        {
            if (!Stage.IsInstalled())
            {
                DisposeQueue.Instance.Add(this);
                return;
            }

            lock (this)
            {
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        NDalicPINVOKE.delete_Vector2(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }


        public static Vector2 operator +(Vector2 arg1, Vector2 arg2)
        {
            return arg1.Add(arg2);
        }

        public static Vector2 operator -(Vector2 arg1, Vector2 arg2)
        {
            return arg1.Subtract(arg2);
        }

        public static Vector2 operator -(Vector2 arg1)
        {
            return arg1.Subtract();
        }

        public static Vector2 operator *(Vector2 arg1, Vector2 arg2)
        {
            return arg1.Multiply(arg2);
        }

        public static Vector2 operator *(Vector2 arg1, float arg2)
        {
            return arg1.Multiply(arg2);
        }

        public static Vector2 operator /(Vector2 arg1, Vector2 arg2)
        {
            return arg1.Divide(arg2);
        }

        public static Vector2 operator /(Vector2 arg1, float arg2)
        {
            return arg1.Divide(arg2);
        }

        public float this[uint index]
        {
            get
            {
                return ValueOfIndex(index);
            }
        }

        internal static Vector2 GetVector2FromPtr(global::System.IntPtr cPtr)
        {
            Vector2 ret = new Vector2(cPtr, false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }


        public Vector2() : this(NDalicPINVOKE.new_Vector2__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public Vector2(float x, float y) : this(NDalicPINVOKE.new_Vector2__SWIG_1(x, y), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public Vector2(float[] array) : this(NDalicPINVOKE.new_Vector2__SWIG_2(array), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public Vector2(Vector3 vec3) : this(NDalicPINVOKE.new_Vector2__SWIG_3(Vector3.getCPtr(vec3)), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public Vector2(Vector4 vec4) : this(NDalicPINVOKE.new_Vector2__SWIG_4(Vector4.getCPtr(vec4)), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public static Vector2 One
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_ONE_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public static Vector2 XAxis
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_XAXIS_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public static Vector2 YAxis
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_YAXIS_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public static Vector2 NegativeXAxis
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_NEGATIVE_XAXIS_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public static Vector2 NegativeYAxis
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_NEGATIVE_YAXIS_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public static Vector2 Zero
        {
            get
            {
                global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_ZERO_get();
                Vector2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Vector2(cPtr, false);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        internal Vector2 Assign(float[] array)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Assign__SWIG_0(swigCPtr, array), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Assign(Vector3 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Assign__SWIG_1(swigCPtr, Vector3.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Assign(Vector4 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Assign__SWIG_2(swigCPtr, Vector4.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Add(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Add(swigCPtr, Vector2.getCPtr(rhs)), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 AddAssign(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_AddAssign(swigCPtr, Vector2.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Subtract(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Subtract__SWIG_0(swigCPtr, Vector2.getCPtr(rhs)), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 SubtractAssign(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_SubtractAssign(swigCPtr, Vector2.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Multiply(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Multiply__SWIG_0(swigCPtr, Vector2.getCPtr(rhs)), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Multiply(float rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Multiply__SWIG_1(swigCPtr, rhs), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 MultiplyAssign(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_MultiplyAssign__SWIG_0(swigCPtr, Vector2.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 MultiplyAssign(float rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_MultiplyAssign__SWIG_1(swigCPtr, rhs), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Divide(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Divide__SWIG_0(swigCPtr, Vector2.getCPtr(rhs)), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Divide(float rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Divide__SWIG_1(swigCPtr, rhs), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 DivideAssign(Vector2 rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_DivideAssign__SWIG_0(swigCPtr, Vector2.getCPtr(rhs)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 DivideAssign(float rhs)
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_DivideAssign__SWIG_1(swigCPtr, rhs), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal Vector2 Subtract()
        {
            Vector2 ret = new Vector2(NDalicPINVOKE.Vector2_Subtract__SWIG_1(swigCPtr), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool EqualTo(Vector2 rhs)
        {
            bool ret = NDalicPINVOKE.Vector2_EqualTo(swigCPtr, Vector2.getCPtr(rhs));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool NotEqualTo(Vector2 rhs)
        {
            bool ret = NDalicPINVOKE.Vector2_NotEqualTo(swigCPtr, Vector2.getCPtr(rhs));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal float ValueOfIndex(uint index)
        {
            float ret = NDalicPINVOKE.Vector2_ValueOfIndex__SWIG_0(swigCPtr, index);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public float Length()
        {
            float ret = NDalicPINVOKE.Vector2_Length(swigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public float LengthSquared()
        {
            float ret = NDalicPINVOKE.Vector2_LengthSquared(swigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public void Normalize()
        {
            NDalicPINVOKE.Vector2_Normalize(swigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        public void Clamp(Vector2 min, Vector2 max)
        {
            NDalicPINVOKE.Vector2_Clamp(swigCPtr, Vector2.getCPtr(min), Vector2.getCPtr(max));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal SWIGTYPE_p_float AsFloat()
        {
            global::System.IntPtr cPtr = NDalicPINVOKE.Vector2_AsFloat__SWIG_0(swigCPtr);
            SWIGTYPE_p_float ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_float(cPtr, false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public float X
        {
            set
            {
                NDalicPINVOKE.Vector2_X_set(swigCPtr, value);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = NDalicPINVOKE.Vector2_X_get(swigCPtr);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Width
        {
            set
            {
                NDalicPINVOKE.Vector2_Width_set(swigCPtr, value);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = NDalicPINVOKE.Vector2_Width_get(swigCPtr);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Y
        {
            set
            {
                NDalicPINVOKE.Vector2_Y_set(swigCPtr, value);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = NDalicPINVOKE.Vector2_Y_get(swigCPtr);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Height
        {
            set
            {
                NDalicPINVOKE.Vector2_Height_set(swigCPtr, value);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = NDalicPINVOKE.Vector2_Height_get(swigCPtr);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

    }

}
