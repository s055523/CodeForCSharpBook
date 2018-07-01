
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.6.1055.0



// Classes defined in this module:
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Class Program                        (auto) (ansi) 
// Class A                              (public) (auto) (ansi) 
// Class B                              (public) (auto) (ansi) 
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly TryCallUsingIL
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- 下列自定义特性会自动添加，不要取消注释 -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 0E 54 72 79 43 61 6C 6C 55 73 69 6E 67 49   // ...TryCallUsingI
                                                                                              4C 00 00 )                                        // L..
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 05 63 68 69 6E 61 00 00 )                   // ...china..
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 0E 54 72 79 43 61 6C 6C 55 73 69 6E 67 49   // ...TryCallUsingI
                                                                                                4C 00 00 )                                        // L..
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 17 43 6F 70 79 72 69 67 68 74 20 C2 A9 20   // ...Copyright .. 
                                                                                                  63 68 69 6E 61 20 32 30 31 38 00 00 )             // china 2018..
  .custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = ( 01 00 24 62 63 36 66 61 36 61 36 2D 65 64 61 66   // ..$bc6fa6a6-edaf
                                                                                                  2D 34 39 39 39 2D 39 65 35 65 2D 31 35 66 39 62   // -4999-9e5e-15f9b
                                                                                                  35 64 61 32 63 37 35 00 00 )                      // 5da2c75..
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..
  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 1C 2E 4E 45 54 46 72 61 6D 65 77 6F 72 6B   // ....NETFramework
                                                                                                        2C 56 65 72 73 69 6F 6E 3D 76 34 2E 36 2E 31 01   // ,Version=v4.6.1.
                                                                                                        00 54 0E 14 46 72 61 6D 65 77 6F 72 6B 44 69 73   // .T..FrameworkDis
                                                                                                        70 6C 61 79 4E 61 6D 65 14 2E 4E 45 54 20 46 72   // playName..NET Fr
                                                                                                        61 6D 65 77 6F 72 6B 20 34 2E 36 2E 31 )          // amework 4.6.1
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module TryCallUsingIL.exe
// MVID: {9233C8EA-53D5-4BF8-B8FC-FB516D9843B5}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00020003    //  ILONLY 32BITPREFERRED
// Image base: 0x003C0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit TryCallUsingIL.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // 代码大小       45 (0x2d)
    .maxstack  1
    .locals init ([0] class TryCallUsingIL.A a,
             [1] class TryCallUsingIL.B a2)
    nop
    newobj     instance void TryCallUsingIL.B::.ctor()
    stloc.0
    ldloc.0
    callvirt   instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
    call       void [mscorlib]System.Console::WriteLine(object)
    nop
    newobj     instance void TryCallUsingIL.B::.ctor()
    stloc.1
    ldloc.1
    callvirt   instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
    call       void [mscorlib]System.Console::WriteLine(object)
    nop
    ldloc.0
    callvirt   instance void TryCallUsingIL.B::MethodB()
	ldloc.1
    callvirt   instance void TryCallUsingIL.B::MethodB()
    nop
    ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // 代码大小       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method Program::.ctor

} // end of class TryCallUsingIL.Program

.class public auto ansi beforefieldinit TryCallUsingIL.A
       extends [mscorlib]System.Object
{
  .field public int32 a
  .method public hidebysig instance void 
          Method() cil managed
  {
    // 代码大小       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      bytearray (41 00 84 76 B9 65 D5 6C )                         // A..v.e.l
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method A::Method

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // 代码大小       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method A::.ctor

} // end of class TryCallUsingIL.A

.class public auto ansi beforefieldinit TryCallUsingIL.B
       extends TryCallUsingIL.A
{
  .field public int32 b
  .method public hidebysig instance void 
          MethodB() cil managed
  {
    // 代码大小       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      bytearray (42 00 84 76 B9 65 D5 6C )                         // B..v.e.l
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method B::MethodB

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // 代码大小       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void TryCallUsingIL.A::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method B::.ctor

} // end of class TryCallUsingIL.B


// =============================================================

// *********** 反汇编完成 ***********************
// 警告: 创建了 Win32 资源文件 C:\Users\Administrator\Desktop\C#书\第三次交稿\第3章 C#类型基础（下）\TryCallUsingIL\TryCallUsingIL\bin\Debug\tryCallUsingIL.res
