//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.9
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Tizen.NUI {

    internal class PagePanSignal : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PagePanSignal(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PagePanSignal obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~PagePanSignal() {
    DisposeQueue.Instance.Add(this);
  }

  public virtual void Dispose() {
    if (!Stage.IsInstalled()) {
      DisposeQueue.Instance.Add(this);
      return;
    }

    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NDalicPINVOKE.delete_PagePanSignal(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }


  public bool Empty() {
    bool ret = NDalicPINVOKE.PagePanSignal_Empty(swigCPtr);
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetConnectionCount() {
    uint ret = NDalicPINVOKE.PagePanSignal_GetConnectionCount(swigCPtr);
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Connect(System.Delegate func) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(func); 
    {
      NDalicPINVOKE.PagePanSignal_Connect(swigCPtr, new System.Runtime.InteropServices.HandleRef(this, ip));
      if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    }
  }

  public void Disconnect(System.Delegate func) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(func); 
    {
      NDalicPINVOKE.PagePanSignal_Disconnect(swigCPtr, new System.Runtime.InteropServices.HandleRef(this, ip));
      if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    }
  }

  public void Emit(PageTurnView arg) {
    NDalicPINVOKE.PagePanSignal_Emit(swigCPtr, PageTurnView.getCPtr(arg));
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
  }

  public PagePanSignal() : this(NDalicPINVOKE.new_PagePanSignal(), true) {
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
