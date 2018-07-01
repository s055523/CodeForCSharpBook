.assembly Callvirt
{
}

.class private auto ansi beforefieldinit Callvirt.A
	extends [mscorlib]System.Object
{
	// Methods
	.method public hidebysig 
		instance void Print () cil managed 
	{
		// Method begins at RVA 0x2076
		// Code size 13 (0xd)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldstr "A"
		IL_0006: call void [mscorlib]System.Console::WriteLine(string)
		IL_000b: nop
		IL_000c: ret
	} // end of method A::Print

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x206d
		// Code size 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method A::.ctor

} // end of class Callvirt.A
.class private auto ansi beforefieldinit Callvirt.B
	extends Callvirt.A
{
	// Methods
	.method public hidebysig 
		instance void Print () cil managed 
	{
		// Method begins at RVA 0x2084
		// Code size 13 (0xd)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldstr "B"
		IL_0006: call void [mscorlib]System.Console::WriteLine(string)
		IL_000b: nop
		IL_000c: ret
	} // end of method B::Print

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x2092
		// Code size 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void Callvirt.A::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method B::.ctor

} // end of class Callvirt.B
.class private auto ansi beforefieldinit Callvirt.Program
	extends [mscorlib]System.Object
{
	// Methods
	.method private hidebysig static 
		void Main (
			string[] args
		) cil managed 
	{
		// Method begins at RVA 0x2050
		// Code size 17 (0x11)
		.maxstack 1
		.entrypoint
		.locals init (
			[0] class Callvirt.A
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: ldloc.0
		IL_0004: call instance void Callvirt.A::Print()
		IL_0009: nop
		IL_000a: call valuetype [mscorlib]System.ConsoleKeyInfo [mscorlib]System.Console::ReadKey()
		IL_000f: pop
		IL_0010: ret
	} // end of method Program::Main

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Method begins at RVA 0x206d
		// Code size 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method Program::.ctor

} // end of class Callvirt.Program


