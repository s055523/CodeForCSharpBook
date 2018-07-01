
.assembly extern mscorlib
{
    .ver 4:0:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}


	.class public auto ansi beforefieldinit TryCallUsingIL.A
		extends [mscorlib]System.Object
	{
		// Fields
		.field public int32 a

		// Methods
		.method public hidebysig 
			instance void Method () cil managed 
		{
			// Method begins at RVA 0x2092
			// Code size 13 (0xd)
			.maxstack 8

			nop
			ldstr "A Method"
			call void [mscorlib]System.Console::WriteLine(string)
			nop
			ret
		} // end of method A::Method

		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Method begins at RVA 0x2089
			// Code size 8 (0x8)
			.maxstack 8

			ldarg.0
			call instance void [mscorlib]System.Object::.ctor()
			nop
			ret
		} // end of method A::.ctor

	} // end of class TryCallUsingIL.A

	.class public auto ansi beforefieldinit TryCallUsingIL.B
		extends TryCallUsingIL.A
	{
		// Fields
		.field public int32 b

		// Methods
		.method public hidebysig 
			instance void MethodB () cil managed 
		{
			// Method begins at RVA 0x20a0
			// Code size 13 (0xd)
			.maxstack 8

			nop
			ldstr "B Method"
			call void [mscorlib]System.Console::WriteLine(string)
			nop
			ret
		} // end of method B::MethodB

		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Method begins at RVA 0x20ae
			// Code size 8 (0x8)
			.maxstack 8

			ldarg.0
			call instance void TryCallUsingIL.A::.ctor()
			nop
			ret
		} // end of method B::.ctor

	} // end of class TryCallUsingIL.B

	.class private auto ansi beforefieldinit TryCallUsingIL.Program
		extends [mscorlib]System.Object
	{
		// Methods
		.method private hidebysig static 
			void Main (
				string[] args
			) cil managed 
		{
			// Method begins at RVA 0x2050
			// Code size 45 (0x2d)
			.maxstack 1
			.entrypoint
			.locals init (
				[0] class TryCallUsingIL.A a,
				[1] class TryCallUsingIL.A a2
			)

			nop
			newobj instance void TryCallUsingIL.B::.ctor()
			stloc.0
			ldloc.0
			callvirt instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
			call void [mscorlib]System.Console::WriteLine(object)
			nop
			newobj instance void TryCallUsingIL.B::.ctor()
			stloc.1
			ldloc.1
			callvirt instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
			call void [mscorlib]System.Console::WriteLine(object)
			nop
			ldloc.0
			callvirt instance void TryCallUsingIL.A::Method()
			nop
			ret
		} // end of method Program::Main

		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Method begins at RVA 0x2089
			// Code size 8 (0x8)
			.maxstack 8

			ldarg.0
			call instance void [mscorlib]System.Object::.ctor()
			nop
			ret
		} // end of method Program::.ctor

	} // end of class TryCallUsingIL.Program



